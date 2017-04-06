using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    class Player:Component, IAnimateable, ILoadable, IUpdatable, ICollisionEnter, ICollisionStay
    {
        private float speed;
        public Animator animator;
        private IStrategy strategy;
        private DIRECTION direction;
        private bool canMove;
        private bool activate;
        public bool visible;
        public bool sPressed;

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public Player(GameObject gameObject, float speed) : base(gameObject)
        {
            this.Speed = speed;
            this.canMove = true;
            this.activate = false;
            this.visible = true;
        }


        public void LoadContent(ContentManager Content)
        {
            KeyboardState keystate = Keyboard.GetState();
            animator = (Animator)GameObject.GetComponent("Animator");
            strategy = new Idle(animator);
            CreateAnimation();
        }

        public void Update()
        {
            KeyboardState keystate = Keyboard.GetState();
            if (canMove)
            {
                if (keystate.IsKeyDown(Keys.A) || keystate.IsKeyDown(Keys.D))
                {
                    if (!(strategy is Walk))
                    {
                        strategy = new Walk(GameObject.Transform, animator, GameObject);
                    }

                } else if (!(strategy is Use) && keystate.IsKeyDown(Keys.S))
                {
                    activate = true;
                    strategy = new Use(GameObject.Transform, animator, GameObject);
                    canMove = false;

                } else if (!(strategy is Use))
                {
                    strategy = new Idle(animator);

                }


            }

            if (strategy != null)
            {
                strategy.Execute(ref direction);
            }

            if (direction == DIRECTION.Left)
            {
                (GameObject.GetComponent("SpriteRenderer") as SpriteRenderer).spriteEffect = SpriteEffects.FlipHorizontally;
            } else if (direction == DIRECTION.Right)
            {
                (GameObject.GetComponent("SpriteRenderer") as SpriteRenderer).spriteEffect = SpriteEffects.None;
            }
        }

        public void CreateAnimation()
        {

            animator.CreateAnimation("WalkLeft", new Animation(10, 64, 0, 64, 64, 10, Vector2.Zero));
            animator.CreateAnimation("WalkRight", new Animation(10, 64, 0, 64, 64, 10, Vector2.Zero));

            animator.CreateAnimation("IdleLeft", new Animation(9, 0, 0, 64, 64, 9, Vector2.Zero));
            animator.CreateAnimation("IdleRight", new Animation(9, 0, 0, 64, 64, 9, Vector2.Zero));

            animator.CreateAnimation("UseRight", new Animation(5, 256, 0, 64, 64, 7, Vector2.Zero));
            animator.CreateAnimation("UseLeft", new Animation(5, 256, 0, 64, 64, 7, Vector2.Zero));

            animator.CreateAnimation("DieRight", new Animation(2, 256, 16, 64, 64, 4, Vector2.Zero));
            animator.CreateAnimation("DieLeft", new Animation(2, 256, 16, 64, 64, 4, Vector2.Zero));
            animator.CreateAnimation("DeadRight", new Animation(1, 256, 18, 64, 64, 4, Vector2.Zero));
            animator.CreateAnimation("DeadLeft", new Animation(1, 256, 18, 64, 64, 4, Vector2.Zero));

            animator.PlayAnimation("IdleRight");
            direction = DIRECTION.Right;
        }

        public void OnAnimationDone(string animationName)
        {
            if (animationName.Contains("Die"))
            {
                canMove = false;
                strategy = new Dead(animator);
            }
            if (animationName.Contains("Idle"))
            {
                canMove = true;

            }
            if (animationName.Contains("Walk"))
            {
                canMove = true;
            }

            if (animationName.Contains("Use"))
            {
                if (visible)
                {
                    canMove = true;
                    strategy = new Idle(animator);
                }
                activate = false;
            }
        }

        public void OnCollisionStay(Collider other)
        {
            KeyboardState x = Keyboard.GetState();
            if (activate)
            {
                if (other.GameObject.GetComponent("Tablet") is Tablet)
                {
                    (other.GameObject.GetComponent("Tablet") as Tablet).Collect();
                    activate = false;
                }
                if (other.GameObject.GetComponent("Tablet2") is Tablet2)
                {
                    (other.GameObject.GetComponent("Tablet2") as Tablet2).Collect();
                    activate = false;
                }
                if (other.GameObject.GetComponent("Altar") is Altar)
                {
                    if ((other.GameObject.GetComponent("Altar") as Altar).IsComplete())
                    {
                        foreach (GameObject obj in GameWorld.Instance.GameObjects)
                        {
                            if (obj.GetComponent("Player") is Player)
                            {
                                (obj.GetComponent("Player") as Player).canMove = false;
                                (obj.GetComponent("Player") as Player).direction = DIRECTION.Left;
                                (obj.GetComponent("Player") as Player).strategy = new Idle((obj.GetComponent("Player") as Player).animator);
                            }
                            if (obj.GetComponent("Enemy") is Enemy)
                            {
                                obj.Transform.Position = new Vector2(2200, -50);
                                (obj.GetComponent("Enemy") as Enemy).direction = DIRECTION.Right;
                                (obj.GetComponent("Enemy") as Enemy).strategy = null;
                                (obj.GetComponent("Enemy") as Enemy).animator.PlayAnimation("Die" + (obj.GetComponent("Enemy") as Enemy).direction);
                            }
                        }
                    }

                }
            }

            if (other.GameObject.GetComponent("Potty") is Potty)
            {
                if (x.IsKeyDown(Keys.S) && sPressed == false)
                {
                    sPressed = true;
                    (other.GameObject.GetComponent("Potty") as Potty).ChangeHiding();
                    visible = !visible;
                }
                if (x.IsKeyUp(Keys.S))
                {
                    sPressed = false;
                }
            }
        }

        public void OnCollisionEnter(Collider other)
        {
            KeyboardState x = Keyboard.GetState();
            if (other.GameObject.GetComponent("Enemy") is Enemy && x.IsKeyUp(Keys.U) && visible)
            {
                GettingKilled();
            }
        }

        public void GettingKilled()
        {
            foreach (GameObject obj in GameWorld.Instance.GameObjects)
            {
                if (obj.GetComponent("Enemy") is Enemy)
                {
                    (obj.GetComponent("Enemy") as Enemy).strategy = new Idle((Animator)obj.GetComponent("Animator"));
                }
            }
            strategy = null;
            visible = true;
            animator.PlayAnimation("Die" + direction);
            canMove = false;
        }
    }
}
