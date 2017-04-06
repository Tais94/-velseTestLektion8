using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Game1
{
    class TabletBuilder : IBuilder
    {
        private GameObject gameObject;
        public void Buildpart(Vector2 position)
        {
            this.gameObject = new GameObject(position, 3f);
            gameObject.AddComponent(new SpriteRenderer(gameObject, "TabletsSheet", 1, true, Color.White));
            gameObject.AddComponent(new Animator(gameObject));
            gameObject.AddComponent(new Tablet(gameObject));
            gameObject.AddComponent(new Collider(gameObject, 0f));
        }

        public GameObject GetResult()
        {
            return gameObject;
        }
    }
}
