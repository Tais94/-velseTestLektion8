using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class WalkEnemy: IStrategy
    {
        private Animator animator;
        private Transform transform;
        private GameObject gameObject;
      

        public WalkEnemy(Transform transform, Animator animator, GameObject gameObject)
        {
            this.transform = transform;
            this.animator = animator;
            this.gameObject = gameObject;
           
        }

        public void Execute(ref DIRECTION direction)
        {
            Vector2 translation = Vector2.Zero;

            direction = DIRECTION.Right;
            animator.PlayAnimation("Idle" + direction);
            
            //if (translation.X > 0)
            //{
            //    direction = DIRECTION.Right;
            //    animator.PlayAnimation("Walk" + direction);
            //}
            //if (translation.X < 0)
            //{
            //    direction = DIRECTION.Left;
            //    animator.PlayAnimation("Walk" + direction);
            //}


            gameObject.Transform.Translate(translation * GameWorld.Instance.deltaTime );
        }

    }
    
    
}
