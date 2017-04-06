using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Die: IStrategy
    {
        private Animator animator;

        public Die(Animator animator)
        {
            this.animator = animator;
        }

        public void Execute(ref DIRECTION direction)
        {
            animator.PlayAnimation("Die" + direction);
        }
    }
}
