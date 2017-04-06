using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Idle: IStrategy
    {
        private Animator animator;

        public Idle(Animator animator)
        {
            this.animator = animator;
        }

        public void Execute(ref DIRECTION direction)
        {
            animator.PlayAnimation("Idle" + direction);
        }
    }
}
