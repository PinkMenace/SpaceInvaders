using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.src.misc;
using System;
using System.Collections.Generic;

namespace SpaceInvaders.src.animation
{
    class Animation
    {
        private bool animationDone;
        private bool loopAnimation;

        private int animationTime;
        private int defaultAnimationLength;

        private int frameIndex;
        private List<Frame> frames;

        public Animation()
        {
            defaultAnimationLength = 0;
            frames = new List<Frame>();
        }
        public Animation(int DefaultAnimationLength)
        {
            defaultAnimationLength = DefaultAnimationLength;
            frames = new List<Frame>();
        }

        public void Update(int time)
        {
            if (FrameCount == 0)
                return;

            animationTime += time;

            if (CurrentFrame.Duration < animationTime)
            {
                frameIndex++;
                animationTime = 0;

                if (FrameCount <= frameIndex)
                {
                    frameIndex = 0;
                }
            }
        }

        public Animation AddFrame(string TexturePath, int? AnimationLength = null)
        {
            if (AnimationLength.HasValue)
            {
                defaultAnimationLength = AnimationLength.Value;
            }

            Texture2D texture = SpriteLoader.LoadTexture(TexturePath);
            Frame newFrame = new Frame(texture, defaultAnimationLength);
            frames.Add(newFrame);

            return this;
        }
        public Animation AddFrame(Texture2D Texture, int? AnimationLength = null)
        {
            if (AnimationLength.HasValue)
            {
                defaultAnimationLength = AnimationLength.Value;
            }

            Frame newFrame = new Frame(Texture, defaultAnimationLength);
            frames.Add(newFrame);

            return this;
        }

        public Texture2D getTexture() { return frames[frameIndex].Texture; }
        public Texture2D getTexture(int i)
        {
            if (i < 0 || FrameCount < i)
                throw new IndexOutOfRangeException();

            return frames[i].Texture;
        }

        public bool AnimationDone { get { return animationDone; } }

        public Frame CurrentFrame { get { return frames[frameIndex]; } }
        public int FrameCount { get { return frames.Count; } }        
        public int TextureWidth { get { return CurrentFrame.Texture.Width; } }
        public int TextureHeight { get { return CurrentFrame.Texture.Height; } }
    }
}
