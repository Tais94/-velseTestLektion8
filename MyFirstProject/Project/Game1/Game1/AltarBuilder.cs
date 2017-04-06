using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Game1
{
    class AltarBuilder:IBuilder
    {
        private GameObject gameObject;


        public void Buildpart(Vector2 position)
        {
            this.gameObject = new GameObject(position, 5f);
            gameObject.AddComponent(new SpriteRenderer(gameObject, "AlterSheet", Global.LayerDepth.Background));
            gameObject.AddComponent(new Animator(gameObject));
            gameObject.AddComponent(new Altar(gameObject));
            gameObject.AddComponent(new Collider(gameObject, 0f));
        }

        public GameObject GetResult()
        {
            return gameObject;
        }
    }
}
