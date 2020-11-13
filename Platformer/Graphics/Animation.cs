using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer.Graphics
{
    public class Animation
    {
        public float animationSpeed = 1f;

        public float wait = 0;

        public int currentFrame;

        public Texture2D sprites;

        public bool shouldLoop;

        public bool shouldPlay = true;

        public int howManyFrames;

        public Animation(Texture2D sprites, int howManyFrames, float animationSpeed, bool shouldLoop)
        {
            this.animationSpeed = animationSpeed;
            this.sprites = sprites;
            this.shouldLoop = shouldLoop;
            this.howManyFrames = howManyFrames;
        }
        public Animation(Texture2D sprites, int howManyFrames, float animationSpeed, bool shouldLoop, bool shouldPlay)
        {
            this.animationSpeed = animationSpeed;
            this.sprites = sprites;
            this.shouldLoop = shouldLoop;
            this.howManyFrames = howManyFrames;
            this.shouldPlay = shouldPlay;
        }

        public void SetFrame(int whichFrame)
        {
            currentFrame = whichFrame;
        }
    }
}
