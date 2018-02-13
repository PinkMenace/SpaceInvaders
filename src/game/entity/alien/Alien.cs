using Microsoft.Xna.Framework;
using SpaceInvaders.src.misc;
using SpaceInvaders.src.animation;

namespace SpaceInvaders.src.game.entity.alien
{
    class Alien : Entity
    {
        private Animation defaultAlienAnimation;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Position"></param>
        /// <param name="DefaultShipOrganization">Determines which alien ship texture to load. Default will at random choose a random alien.</param>
        public Alien(Vector2 Position, int DefaultShipOrganization = -1) : base(Position)
        { 
            LoadAnimation(DefaultShipOrganization);
            LoadDrawOptions();        
        }

        internal void LoadAnimation(int ShipNumber)
        {
            defaultAlienAnimation = new Animation();

            // Ship Animation
            if (ShipNumber < 0 || 5 < ShipNumber)
                ShipNumber = new System.Random().Next(5);

            switch (ShipNumber)
            {
                case 0:
                    defaultAlienAnimation.AddFrame(ResourceCache.AlienTextures[0]);
                    break;
                case 1:
                    defaultAlienAnimation.AddFrame(ResourceCache.AlienTextures[1]);
                    break;
                case 2:
                    defaultAlienAnimation.AddFrame(ResourceCache.AlienTextures[2]);
                    break;
                case 3:
                    defaultAlienAnimation.AddFrame(ResourceCache.AlienTextures[3]);
                    break;
                case 4:
                    defaultAlienAnimation.AddFrame(ResourceCache.AlienTextures[4]);
                    addPosition(4, 0);
                    break;
                default:
                    defaultAlienAnimation.AddFrame(ResourceCache.AlienTextures[5]);
                    addPosition(4, 0);
                    break;
            }

            Animation = defaultAlienAnimation;
        }

        internal override void Update(int time)
        {
            base.Update(time);
        }

        public void Move(float X, float Y)
        {
            addDelta(X, Y);
        }

        internal override Bullet Shoot()
        {
            int PosX = (int)(Position.X + getWidth() / 2);
            int PosY = (int)(Position.Y + getHeight());

            AlienBullet bullet = new AlienBullet(new Vector2(PosX, PosY));
            return bullet;
        }
    }
}
