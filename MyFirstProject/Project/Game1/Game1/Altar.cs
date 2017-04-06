using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Game1
{
    class Altar:Component, ILoadable,IUpdatable
    {
        public Animator animator;
        private int collectedB;
        private int collectedO;

        public Altar(GameObject gameObject) : base(gameObject)
        {
            this.collectedO = 0;
            this.collectedB = 0;
        }

        public void LoadContent(ContentManager content)
        {
            animator = (Animator)GameObject.GetComponent("Animator");

            animator.CreateAnimation("Empty", new Animation(1, 0, 0, 64, 32, 4, Vector2.Zero));
            animator.CreateAnimation("OneB", new Animation(4, 96, 0, 64, 32, 4, Vector2.Zero));
            animator.CreateAnimation("TwoB", new Animation(4, 64, 0, 64, 32, 4, Vector2.Zero));
            animator.CreateAnimation("OneO", new Animation(4, 160, 0, 64, 32, 4, Vector2.Zero));
            animator.CreateAnimation("OneBOneO", new Animation(4, 128, 0, 64, 32, 4, Vector2.Zero));
            animator.CreateAnimation("Full", new Animation(4, 32, 0, 64, 32, 4, Vector2.Zero));
            animator.PlayAnimation("Empty");
        }

        public void Update()
        {
            if (collectedO == 0 && collectedB == 0)
            {
                animator.PlayAnimation("Empty");
            }
            if (collectedO == 0 && collectedB == 1)
            {
                animator.PlayAnimation("OneB");
            }
            if (collectedO == 0 && collectedB == 2)
            {
                animator.PlayAnimation("TwoB");
            }
            if (collectedO == 1 && collectedB == 0)
            {
                animator.PlayAnimation("OneO");
            }
            if (collectedO == 1 && collectedB == 1)
            {
                animator.PlayAnimation("OneBOneO");
            }
            if (collectedO == 1 && collectedB == 2)
            {
                animator.PlayAnimation("Full");
            }
        }

        public void AddOneO()
        {
            if (collectedO < 1)
            {
                collectedO++;
            }
        }
        public void AddOneB()
        {
            if (collectedB < 2)
            {
                collectedB++;
            }
        }
        public bool IsComplete()
        {
            if (collectedB >= 2 && collectedO >= 1)
            {
                return true;
            } else { return false; }
        }
    }
}
