using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Game1
{
    class TreeBuilder:IBuilder
    {
        private GameObject gameObject;
        public void Buildpart(Vector2 position)
        {
            this.gameObject = new GameObject(position, 3f);
            gameObject.AddComponent(new SpriteRenderer(gameObject, "trees", Global.LayerDepth.Background, true, Color.Multiply(Color.Black, 0.8f)));
        }

        public GameObject GetResult()
        {
            return gameObject;
        }
    }
}
