using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class TabletBuilder2 : IBuilder
    {
        private GameObject gameObject;
        public void Buildpart(Vector2 position)
        {
            this.gameObject = new GameObject(position, 3f);
            gameObject.AddComponent(new SpriteRenderer(gameObject, "TabletsSheet", 1, true, Color.White));
            gameObject.AddComponent(new Animator(gameObject));
            gameObject.AddComponent(new Tablet2(gameObject));
            gameObject.AddComponent(new Collider(gameObject, 0f));

        }

        public GameObject GetResult()
        {
            return gameObject;
        }
    
    }
}
