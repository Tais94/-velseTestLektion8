using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Walk: IStrategy
    {
        private Animator animator;
        private Transform transform;
        private GameObject gameObject;
        private Player player;

        public Walk(Transform transform, Animator animator, GameObject gameObject)
        {
            this.transform = transform;
            this.animator = animator;
            this.gameObject = gameObject;
            player = (Player)gameObject.GetComponent("Player");
        }

        public void Execute(ref DIRECTION direction)
        {
            Vector2 translation = Vector2.Zero;


            KeyboardState keystate = Keyboard.GetState();
         
            if (keystate.IsKeyDown(Keys.A))
            {
                translation.X -= 1;
            }
            if (keystate.IsKeyDown(Keys.D))
            {
                translation.X += 1;
            }

            if (translation.X > 0)
            {
                direction = DIRECTION.Right;
                animator.PlayAnimation("Walk" + direction);
            }
            if (translation.X < 0)
            {
                direction = DIRECTION.Left;
                animator.PlayAnimation("Walk" + direction);
            }
          

            gameObject.Transform.Translate(translation * GameWorld.Instance.deltaTime * player.Speed);
        }

    }
}
