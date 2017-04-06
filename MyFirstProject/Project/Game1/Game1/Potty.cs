using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Game1
{
    class Potty:Component, ILoadable, IUpdatable
    {
        public Animator animator;
        private bool hiding;

        public Potty(GameObject gameObject) : base(gameObject)
        {
            this.hiding = false;
        }

        public void LoadContent(ContentManager content)
        {
            animator = (Animator)GameObject.GetComponent("Animator");

            animator.CreateAnimation("Empty", new Animation(1, 0, 0, 48, 96, 1, Vector2.Zero));
            animator.CreateAnimation("Used", new Animation(1, 96, 0, 48, 96, 1, Vector2.Zero));
            animator.PlayAnimation("Empty");
        }

        public void Update()
        {
            if (hiding)
            {
                animator.PlayAnimation("Used");
            } else { animator.PlayAnimation("Empty"); }
        }

        public void ChangeHiding()
        {
            hiding = !hiding;
        }
    }
}
