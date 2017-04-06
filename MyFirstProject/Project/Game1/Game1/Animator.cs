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
    class Animator : Component, IUpdatable
    {
        private SpriteRenderer spriteRenderer;
        private int currentIndex;
        private float timeElapsed;
        private float fps;
        private Rectangle[] rectangles;
        private string animationName;
        private Dictionary<string, Animation> animations;


        public Animator(GameObject gameObject) : base(gameObject)
        {
            fps = 5;
            this.spriteRenderer = (SpriteRenderer)gameObject.GetComponent("SpriteRenderer");
            animations = new Dictionary<string, Animation>();

        }

        public void Update()
        {
            timeElapsed += GameWorld.Instance.deltaTime;
            currentIndex = (int)(timeElapsed * fps);
            if (currentIndex > rectangles.Length - 1)
            {
                GameObject.OnAnimationDone(animationName);
                timeElapsed = 0;
                currentIndex = 0;
            }
            spriteRenderer.Rectangle = rectangles[currentIndex];
        }

        public void CreateAnimation(string name, Animation animation)
        {
            animations.Add(name, animation);
        }

        public void PlayAnimation(string animationName)
        {
            if (this.animationName != animationName)
            {
                //set rectangle
                this.rectangles = animations[animationName].Rectangles;

                //reset rectangle
                this.spriteRenderer.Rectangle = rectangles[0];

                //set offset
                this.spriteRenderer.Offset = animations[animationName].Offset;

                //set animation name
                this.animationName = animationName;

                //set fps
                this.fps = animations[animationName].Fps;

                //reset the animation
                timeElapsed = 0;

                currentIndex = 0;
            }
        }
    }
}
