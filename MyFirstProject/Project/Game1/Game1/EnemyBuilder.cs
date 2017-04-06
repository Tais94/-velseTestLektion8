using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class EnemyBuilder: IBuilder
    {
        private GameObject gameObject;


        public void Buildpart(Vector2 position)
        {
            this.gameObject = new GameObject(position, 5f);
            gameObject.AddComponent(new SpriteRenderer(gameObject, "ghostIce", 1));
            gameObject.AddComponent(new Animator(gameObject));
            gameObject.AddComponent(new Enemy(gameObject));
            gameObject.AddComponent(new Collider(gameObject, 0.5f));
            (gameObject.GetComponent("Collider") as Collider).doCollisionCheck = true;
        }

        public GameObject GetResult()
        {
            return gameObject;
        }
    }
}
