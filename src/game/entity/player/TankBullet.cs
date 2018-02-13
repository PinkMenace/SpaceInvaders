using Microsoft.Xna.Framework;

namespace SpaceInvaders.src.game.entity
{
    class TankBullet : Bullet
    {
        public TankBullet(Vector2 Position):base(Position)
        {
            addPosition(-(float)(getWidth() / 2), 0); // Position needs to be corrected here so we can 
                                                      // align the bullet to the middle of the tank
            isCollidable = true;
            bulletSpeed *= -1;
        }

        internal override void LoadDrawOptions()
        {
            DrawOptions.Color = Color.Green;
        }
    }
}
