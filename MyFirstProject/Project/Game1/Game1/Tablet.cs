using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using static Game1.DIRECTION;

namespace Game1
{
    class Tablet:Component, ILoadable, IUpdatable
    {
        public Animator animator;
        private GameObject player;

        public Tablet(GameObject gameObject) : base(gameObject)
        {

        }

        public void LoadContent(ContentManager content)
        {
            player = GameWorld.Instance.FindGameObjectWithTag("Player");
            animator = (Animator)GameObject.GetComponent("Animator");


            animator.CreateAnimation("IdleB", new Animation(4, 0, 0, 16, 16, 4, Vector2.Zero));
            animator.PlayAnimation("IdleB");
        }


        public void Update()
        {
            
        }

        public void Collect()
        {
            foreach (GameObject obj in GameWorld.Instance.GameObjects)
            {
                if (obj.GetComponent("Altar") is Altar)
                {
                    (obj.GetComponent("Altar") as Altar).AddOneB();
                }
            }
            GameWorld.Instance.RemoveGameObjects.Add(GameObject);
        }
    }
}
