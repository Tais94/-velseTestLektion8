using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace Game1
{
    class Collider:Component,ILoadable,IDrawable, IUpdatable
    {
        private SpriteRenderer spriteRenderer;
        private Texture2D texture;
        public bool doCollisionCheck { private get; set; }
        private List<Collider> otherColliders;
        private float centerPercent;

        public Collider(GameObject gameObject, float centerPercent) : base(gameObject)
        {
            this.spriteRenderer = (gameObject.GetComponent("SpriteRenderer") as SpriteRenderer);
            GameWorld.Instance.colliders.Add(this);
            this.otherColliders = new List<Collider>();
            this.centerPercent = centerPercent;
        }

        public Rectangle GetCollisionBox
        {
            get
            {
                if (centerPercent == 0)
                {
                    return new Rectangle
                    (
                        (int)(GameObject.Transform.Position.X + spriteRenderer.Offset.X),
                        (int)(GameObject.Transform.Position.Y + spriteRenderer.Offset.Y),
                        (int)(spriteRenderer.Rectangle.Width * GameObject.scaleFactor),
                        (int)(spriteRenderer.Rectangle.Height * GameObject.scaleFactor)
                    );
                } else
                {
                    return new Rectangle
                    (
                        (int)(GameObject.Transform.Position.X + spriteRenderer.Offset.X + (spriteRenderer.Rectangle.Width * GameObject.scaleFactor * centerPercent / 2)),
                        (int)(GameObject.Transform.Position.Y + spriteRenderer.Offset.Y),
                        (int)(spriteRenderer.Rectangle.Width * GameObject.scaleFactor * centerPercent),
                        (int)(spriteRenderer.Rectangle.Height * GameObject.scaleFactor)
                    );
                }
                
            }
        }

        public void LoadContent(ContentManager content)
        {
            spriteRenderer = (SpriteRenderer)GameObject.GetComponent("SpriteRenderer");
            texture = content.Load<Texture2D>("CollisionTexture");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
#if DEBUG
            Rectangle topLine = new Rectangle(GetCollisionBox.X, GetCollisionBox.Y, GetCollisionBox.Width, 1);
            Rectangle bottomLine = new Rectangle(GetCollisionBox.X, GetCollisionBox.Y + GetCollisionBox.Height, GetCollisionBox.Width, 1);
            Rectangle rightLine = new Rectangle(GetCollisionBox.X + GetCollisionBox.Width, GetCollisionBox.Y, 1, GetCollisionBox.Height);
            Rectangle leftLine = new Rectangle(GetCollisionBox.X, GetCollisionBox.Y, 1, GetCollisionBox.Height);
            spriteBatch.Draw(texture, topLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(texture, bottomLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(texture, rightLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(texture, leftLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
#endif
        }

        public void Update()
        {
            CheckCollision();
        }

        public void CheckCollision()
        {
            if (doCollisionCheck)
            {
                foreach (Collider other in GameWorld.Instance.colliders)
                {
                    if (other != this)
                    {
                        if (GetCollisionBox.Intersects(other.GetCollisionBox))
                        {
                            if (!otherColliders.Contains(other))
                            {
                                foreach (Component obj in GameObject.components)
                                {
                                    if (obj is ICollisionEnter)
                                    {
                                        (obj as ICollisionEnter).OnCollisionEnter(other);
                                    }
                                }
                                otherColliders.Add(other);
                            }
                            foreach (Component obj in GameObject.components)
                            {
                                if (obj is ICollisionStay)
                                {
                                    (obj as ICollisionStay).OnCollisionStay(other);
                                }
                            }
                        } else if (otherColliders.Contains(other))
                        {
                            foreach (Component obj in GameObject.components)
                            {
                                if (obj is ICollisionExit)
                                {
                                    (obj as ICollisionExit).OnCollisionExit(other);
                                }
                            }
                            otherColliders.Remove(other);
                        }
                    }
                }
            }
        }
    }
}
