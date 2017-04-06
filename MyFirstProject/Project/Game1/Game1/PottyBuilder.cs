using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class PottyBuilder:IBuilder
    {
        private GameObject gameObject;


        public void Buildpart(Vector2 position)
        {
            this.gameObject = new GameObject(position, 2.5f);
            gameObject.AddComponent(new SpriteRenderer(gameObject, "PottySheet", Global.LayerDepth.Details));
            gameObject.AddComponent(new Animator(gameObject));
            gameObject.AddComponent(new Potty(gameObject));
            gameObject.AddComponent(new Collider(gameObject, 0f));
        }

        public GameObject GetResult()
        {
            return gameObject;
        }
    }
}
