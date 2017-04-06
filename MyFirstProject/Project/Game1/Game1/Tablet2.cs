using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Tablet2 : Component, ILoadable, IUpdatable
    {
        public Animator animator;
        private GameObject player;

        public Tablet2(GameObject gameObject) : base(gameObject)
        {

        }

        public void LoadContent(ContentManager content)
        {
            player = GameWorld.Instance.FindGameObjectWithTag("Player");
            animator = (Animator)GameObject.GetComponent("Animator");


            
            animator.CreateAnimation("IdleO", new Animation(4, 16, 0, 16, 16, 4, Vector2.Zero));
            animator.PlayAnimation("IdleO");
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
                    (obj.GetComponent("Altar") as Altar).AddOneO();
                }
            }
            GameWorld.Instance.RemoveGameObjects.Add(GameObject);
        }
    
    }
}
