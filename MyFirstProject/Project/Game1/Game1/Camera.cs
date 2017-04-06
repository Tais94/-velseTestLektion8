using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class Camera
    {
        private static Camera instance;
        public static Camera Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Camera();
                }
                return instance;
            }
        }

        //

        protected float zoom;
        public Matrix transform;
        public Vector2 pos;
        protected float rotation;

        private Camera()
        {
            zoom = 1.0f;
            rotation = 0.0f;
            pos = Vector2.Zero;
        }

        public float Zoom
        {
            get { return zoom; }
            set { zoom = value; if (zoom < 0.1f) zoom = 0.1f; }
        }

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public void Move(Vector2 amount)
        {
            pos += amount;
        }

        public Vector2 Pos
        {
            get { return pos; }
            set { pos = value; }
        }

        public Matrix GetTransformation(GraphicsDevice graphicsDevice)
        {
            transform = Matrix.CreateTranslation(new Vector3(pos.X, pos.Y, 0)) * 
                Matrix.CreateRotationZ(rotation) * 
                Matrix.CreateScale(new Vector3(zoom, zoom, 1)) * 
                Matrix.CreateTranslation(new Vector3(graphicsDevice.Viewport.Width * 0.5f, graphicsDevice.Viewport.Height * 0.5f, 0));
            return transform;
        }
    }
}
