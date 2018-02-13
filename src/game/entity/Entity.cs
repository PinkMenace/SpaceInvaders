using Microsoft.Xna.Framework;
using SpaceInvaders.src.animation;

using SpaceInvaders.src.misc;

namespace SpaceInvaders.src.game.entity
{
    class Entity : Sprite
    {
        private bool isAlive;

        public Entity(Vector2 Position) : base(Position)
        {
            DrawOptions.Scale = new Vector2(Constaints.ScreenResolution.getScale().X, Constaints.ScreenResolution.getScale().Y);
            isAlive = true;
        }

        internal virtual Bullet Shoot()
        {
            return null;
        }
        
        internal virtual void Kill()
        {
            isCollidable = false;
            isAlive = false;
        }

        public bool IsAlive
        {
            get { return isAlive; }
            set { isAlive = value; }
        }
    }
}