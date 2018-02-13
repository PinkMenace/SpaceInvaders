using Microsoft.Xna.Framework;
using SpaceInvaders.src.animation;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.src.misc;
using System.Collections.Generic;

namespace SpaceInvaders.src.game.entity
{
    class Bullet : Entity
    {
        private readonly double BULLET_SPEED = .5;

        protected double bulletSpeed;
        Animation defaultAnimation;

        public Bullet(Vector2 SpawnPosition) : base(SpawnPosition)
        {
            bulletSpeed = BULLET_SPEED * DrawOptions.Scale.Y;
            LoadAnimation();
        }

        internal override void LoadAnimation()
        {
            List<Texture2D> textures = ResourceCache.BulletTexture;

            defaultAnimation = new Animation(10);

            foreach (Texture2D texture in textures)
            {
                defaultAnimation.AddFrame(texture);
            }

            Animation = defaultAnimation;
        }

        internal override void Update(int time)
        {
            base.Update(time);

            position.Y += (float)bulletSpeed;
        }
    }
}
