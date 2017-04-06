using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Animation
    {
        private float fps;
        private Vector2 offset;
        private Rectangle[] rectangle;

        public float Fps
        {
            get { return fps; }
            set { fps = value; }
        }

        public Vector2 Offset
        {
            get { return offset; }
            set { offset = value; }
        }

        public Rectangle[] Rectangles
        {
            get { return rectangle; }
            set { rectangle = value; }
        }

        public Animation(int frames, int Ypos, int xStratFrame, int width, int height, float fps, Vector2 offset)
        {
            Rectangles = new Rectangle[frames];

            Offset = offset;

            this.fps = fps;
            for (int i = 0; i < frames; i++)
            {
                Rectangles[i] = new Rectangle((i + xStratFrame) * width, Ypos, width, height);
            }
        }
    }
}
