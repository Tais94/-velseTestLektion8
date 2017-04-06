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
    class GameObject : Component, ILoadable, IDrawable, IUpdatable, IAnimateable
    {

        public List<Component> components { get; private set; }
        private Transform transform;
        public float scaleFactor { get; set; }

        public Transform Transform
        {
            get { return transform; }
        }
        public string Tag { get; set; } = "Untagged";

        public void AddComponent(Component component)
        {
            components.Add(component);
        }

        public Component GetComponent(string component)
        {
            return components.Find(n => n.GetType().Name == component);
        }
        /// <summary>
        /// Constructor for 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="scaleFactor"></param>
        public GameObject(Vector2 position, float scaleFactor)
        {
            this.transform = new Transform(this, position);
            this.scaleFactor = scaleFactor;
            components = new List<Component>();
        }

        public void LoadContent(ContentManager content)
        {
            foreach (Component com in components)
            {
                if (com is ILoadable)
                {
                    (com as ILoadable).LoadContent(content);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Component com in components)
            {
                if (com is IDrawable)
                {
                    (com as IDrawable).Draw(spriteBatch);
                }
            }
        }

        public void Update()
        {
            foreach (Component com in components)
            {
                if (com is IUpdatable)
                {
                    (com as IUpdatable).Update();
                }
            }
        }

        public void OnAnimationDone(string animationName)
        {
            foreach (Component component in components)
            {
                if (component is IAnimateable)
                {
                    (component as IAnimateable).OnAnimationDone(animationName);
                }
            }
        }
    }
}
