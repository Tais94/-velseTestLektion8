using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class SeekTarget:IStrategy
    {
        private Transform transform;
        private float movementSpeed = 50;
        private Animator animator;
        private float timer;

        public SeekTarget(Transform transform, Animator animator)
        {
            this.transform = transform;
            this.animator = animator;
            this.timer = 0;
        }

        public void Execute(ref DIRECTION direction)
        {
            Vector2 translation = Vector2.Zero;
            timer += GameWorld.Instance.deltaTime;
            if (timer > 10)
            {
                timer = 0;
                Random rnd = new Random();
                int x = rnd.Next(0,2);
                if (x == 0)
                {
                    direction = DIRECTION.Left;
                } else
                {
                    direction = DIRECTION.Right;
                }
            }

            if (direction == DIRECTION.Right)
            {
                translation += new Vector2(1, 0);
            } else
            {
                translation += new Vector2(-1, 0);
            }

            transform.Translate(translation * movementSpeed * GameWorld.Instance.deltaTime);

            animator.PlayAnimation("Walk" + direction);
        }
    }
}
