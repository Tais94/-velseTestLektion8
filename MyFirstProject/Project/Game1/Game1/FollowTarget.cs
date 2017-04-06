using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Game1.DIRECTION;

namespace Game1
{
    class FollowTarget: IStrategy
    {
        private Transform target;
        private Transform transform;
        private float movementSpeed = 50;
        private Animator animator;
        private Vector2 lastSeen;
        private Enemy thisone;

        public FollowTarget(Transform target, Transform transform, Animator animator, Enemy thisone)
        {
            this.target = target;
            this.transform = transform;
            this.animator = animator;
            this.thisone = thisone;
        }

        public void Execute(ref DIRECTION direction)
        {
            Vector2 translation = Vector2.Zero;
            
            if ((target.GameObject.GetComponent("Player") as Player).visible == true)
            {
                if (target.Position.X > transform.Position.X)
                {
                    if (target.Position.X - 300 < transform.Position.X) { lastSeen.X = target.Position.X; }
                } else if (target.Position.X < transform.Position.X)
                {
                    if (transform.Position.X - 300 < target.Position.X) { lastSeen.X = target.Position.X; }
                }
            }

            if (lastSeen.X < transform.Position.X)
            {
                translation += new Vector2(-3, 0);
                if (lastSeen.X > transform.Position.X + new Vector2(translation.X * movementSpeed * GameWorld.Instance.deltaTime).X)
                {
                    translation.X = lastSeen.X - transform.Position.X;
                }
                direction = Left;
            }

            if (lastSeen.X > transform.Position.X)
            {
                translation += new Vector2(3, 0);
                if (lastSeen.X < transform.Position.X + new Vector2(translation.X * movementSpeed * GameWorld.Instance.deltaTime).X)
                {
                    translation.X = lastSeen.X - transform.Position.X;
                }
                direction = Right;
            }

            transform.Translate(translation * movementSpeed * GameWorld.Instance.deltaTime);

            if (translation != Vector2.Zero)
            {
                animator.PlayAnimation("Walk" + direction);
            } else
            {
                thisone.strategy = new SeekTarget(transform, animator);
                thisone.stopped = true;
            }
        }
    }
}
