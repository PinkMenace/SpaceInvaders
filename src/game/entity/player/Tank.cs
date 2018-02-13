using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.src.animation;
using SpaceInvaders.src.input;
using SpaceInvaders.src.misc;

using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaders.src.game.entity
{
    class Tank : Entity
    {
        private readonly double DEFAULT_MOVEMENT_SPEED = 1;
        private readonly double WEAPON_COOLDOWN = 30;

        private Animation defaultTankAnimation;
        private double timeSinceLastShoot;
        private double currentMovementSpeed;
        private bool playerShoot;

        private int lives;

        public Tank(Vector2 Position) : base(Position)
        {
            currentMovementSpeed = DEFAULT_MOVEMENT_SPEED * DrawOptions.Scale.X;
            playerShoot = false;
            LoadAnimation();
            LoadDrawOptions();

            lives = 3;
        }

        internal override void LoadAnimation()
        {
            List<Texture2D> textures = ResourceCache.TankTexture;

            defaultTankAnimation = new Animation();

            foreach (Texture2D texture in textures)
            {
                defaultTankAnimation.AddFrame(texture);
            }

            Animation = defaultTankAnimation;
        }
        internal override void LoadDrawOptions()
        {
            DrawOptions.Color = Color.Green;
        }

        internal override void Update(int time)
        {
            base.Update(time);

            HandleInput();
            timeSinceLastShoot--;
        }

        private void HandleInput()
        {
            input.InputHandler.Update();

            HandleKeyboard();
        }

        private void HandleKeyboard()
        {
            if (InputHandler.IsKeyPressed(Keys.Right) || InputHandler.IsKeyHeld(Keys.Right))
            {
                if ((Position.X + getWidth()) < Constaints.GameArea.Width)
                    delta.X += (float)DEFAULT_MOVEMENT_SPEED;
            }
            if (InputHandler.IsKeyPressed(Keys.Left) || InputHandler.IsKeyHeld(Keys.Left))
            {
                if (Constaints.GameArea.X < Position.X)
                    delta.X -= (float)DEFAULT_MOVEMENT_SPEED;
            }
            if (timeSinceLastShoot <= 0)
            {
                if (InputHandler.IsKeyPressed(Keys.Space))
                {
                    playerShoot = true;
                }
            }
        }

        public void Respawn()
        {
            if (0 < lives)
            {
                lives--;
                IsAlive = true;
                isCollidable = true;
            }
        }

        public int getLives() { return lives; }

        internal override Bullet Shoot()
        {
            if (playerShoot)
            {
                int PosX = (int)(Position.X + (getWidth() * .5));
                int Posy = (int)(Position.Y + -2);
                Vector2 BulletSpawnPosition = new Vector2(PosX, Posy);

                    timeSinceLastShoot = 0;

                playerShoot = false;
                timeSinceLastShoot = WEAPON_COOLDOWN;
                return new TankBullet(BulletSpawnPosition);
            }

            return null;
        }
    }
}
