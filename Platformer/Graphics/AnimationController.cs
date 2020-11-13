using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Graphics
{
    public class AnimationController
    {
        public bool IsPlaying { get; private set; }

        public Dictionary<string, Animation> animations;

        public Animation CurrentAnimation { get; private set; }

        public AnimationController(Dictionary<string, Animation> animations) 
        {
            this.animations = animations;

            CurrentAnimation = animations.Values.First();
        }

        public void Play(string animationName)
        {
            if (animations.ContainsKey(animationName))
            {
                IsPlaying = true;
                CurrentAnimation = animations[animationName];
            } else
            {
                throw new Exception($"The animation {animationName} does not exist.");
            }
        }

        public void Stop()
        {
            IsPlaying = false;
            CurrentAnimation = null;
        }
        
        public void Update()
        {
            if (IsPlaying && CurrentAnimation.shouldPlay)
            {
                if (CurrentAnimation.wait < CurrentAnimation.animationSpeed)
                {
                    CurrentAnimation.wait++;
                }
                else
                {
                    if (CurrentAnimation.currentFrame < CurrentAnimation.howManyFrames - 1)
                    {
                        CurrentAnimation.currentFrame++;
                    }
                    else
                    {
                        if (CurrentAnimation.shouldLoop)
                        {
                            CurrentAnimation.currentFrame = 0;
                        }
                    }

                    CurrentAnimation.wait = 0;
                }
            }
        }

        public void SetFrame(int frame)
        {
            CurrentAnimation.SetFrame(frame);
        }
    }
}
