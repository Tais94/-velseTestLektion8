using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Game1
{
    class PlayerBuilder : IBuilder
    {
        private GameObject gameObject;
        

        public void Buildpart(Vector2 position)
        {
            this.gameObject = new GameObject(position, 3f);
            //     this.gameObject = new GameObject(Vector2.Zero);
            //gameObject.AddComponent(new SpriteRenderer(gameObject, "ghostIce", 1));
            gameObject.AddComponent(new SpriteRenderer(gameObject, "Player", 1));
            gameObject.AddComponent(new Animator(gameObject));
            gameObject.AddComponent(new Player(gameObject, 300));
            gameObject.AddComponent(new Collider(gameObject, 0.5f));
            (gameObject.GetComponent("Collider") as Collider).doCollisionCheck = true;
            gameObject.Tag = "Player";
          

           
        }

        public GameObject GetResult()
        {
            return gameObject;
        }
    }
}
