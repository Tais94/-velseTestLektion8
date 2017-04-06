using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class SpriteRenderer : Component, ILoadable, IDrawable
    {

        private float layer;
        private string spriteName;
        public Texture2D sprite { get; set; }
        private Rectangle rectangle;
        private Vector2 offset;
        private bool rect;
        private Color color;
        public SpriteEffects spriteEffect { get; set; }
   

        public Rectangle Rectangle
        {
            get { return rectangle; }
            set { rectangle = value; }
        }

        public Vector2 Offset
        {
            get { return offset; }
            set { offset = value; }
        }

        public SpriteRenderer(GameObject gameObject, string spriteName, float layer) : base(gameObject)
        {
            this.layer = layer;
            this.spriteName = spriteName;
            this.rect = false;
            this.color = Color.White;
            this.spriteEffect = SpriteEffects.None;
            
        }

        /// <summary>
        /// Alternate sprite renderer
        /// </summary>
        /// <param name="gameObject">Gameobject which use it</param>
        /// <param name="spriteName">Name of the animation sprite</param>
        /// <param name="layer">The layer</param>
        /// <param name="rect">Rectangle</param>
        /// <param name="color">The color.\n\nIf Null, it will standard white</param>
        public SpriteRenderer(GameObject gameObject, string spriteName, float layer, bool rect, Color color) : base(gameObject)
        {
            this.layer = layer;
            this.spriteName = spriteName;
            this.rect = rect;
            if (color == null)
            {
                this.color = Color.White;
            } else { this.color = color; }
            this.spriteEffect = SpriteEffects.None;
        }

        public void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>(spriteName);
            if (rect)
            {
                rectangle = sprite.Bounds;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (GameObject.GetComponent("Player") is Player)
            {
                if ((GameObject.GetComponent("Player") as Player).visible == true)
                {
                    spriteBatch.Draw(sprite, GameObject.Transform.Position, rectangle, color, 0, Vector2.Zero, GameObject.scaleFactor, spriteEffect, layer);
                }
            } else { spriteBatch.Draw(sprite, GameObject.Transform.Position, rectangle, color, 0, Vector2.Zero, GameObject.scaleFactor, spriteEffect, layer); }            
        }
    }
}
