using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Game1.DIRECTION;

namespace Game1
{
    class Enemy : Component, IUpdatable, ILoadable, ICollisionStay
    {
        public Animator animator;
        public IStrategy strategy { get; set; }
        public DIRECTION direction;
        private GameObject player;
        public bool stopped;
       
             

        public Enemy(GameObject gameObject) : base(gameObject)
        {
            this.stopped = false;
 
        }

        public void LoadContent(ContentManager content)
        {
            player = GameWorld.Instance.FindGameObjectWithTag("Player");
            animator = (Animator)GameObject.GetComponent("Animator");

            direction = Right;


            animator.CreateAnimation("IdleLeft", new Animation(4, 128, 0, 32, 32, 6, Vector2.Zero));
            animator.CreateAnimation("IdleRight", new Animation(4, 32, 0, 32, 32, 6, Vector2.Zero));

            animator.CreateAnimation("WalkLeft", new Animation(4, 160, 0, 32, 32, 6, Vector2.Zero));
            animator.CreateAnimation("WalkRight", new Animation(4, 64, 0, 32, 32, 6, Vector2.Zero));

            animator.CreateAnimation("DieLeft", new Animation(9, 96, 0, 32, 32, 6, Vector2.Zero));
            animator.CreateAnimation("DieRight", new Animation(9, 0, 0, 32, 32, 6, Vector2.Zero));

            animator.PlayAnimation("WalkRight");
            strategy = new SeekTarget(GameObject.Transform, animator);
        }

        public void Update()
        {

            
            if ( 
                (
                 GameObject.Transform.Position.X + 300 < player.Transform.Position.X || 
                 GameObject.Transform.Position.X - 300 < player.Transform.Position.X
                ) && 
                 !(strategy is FollowTarget) && 
                 (player.GetComponent("Player") as Player).visible &&
                 !(strategy is Idle) &&
                 strategy != null
                )
            {
                strategy = new FollowTarget(player.Transform, GameObject.Transform, animator, this);
            }
                

          
            if (strategy != null)
            {
                strategy.Execute(ref direction);
            }
            

        }

        public void OnCollisionStay(Collider other)
        {
            if (stopped)
            {
                if (other.GameObject.GetComponent("Player") is Player)
                {
                    (other.GameObject.GetComponent("Player") as Player).visible = true;
                    (other.GameObject.GetComponent("Player") as Player).GettingKilled();
                }
                stopped = false;
            }
        }
    } 
}
