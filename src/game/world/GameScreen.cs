using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.src.game.entity;
using SpaceInvaders.src.game.entity.alien;
using SpaceInvaders.src.misc;
using System.Collections.Generic;

namespace SpaceInvaders.src.game.world
{
    class GameScreen
    {
        private readonly int MAX_ALIEN_SHOOTS = 5;
        private readonly int MAX_TANK_SHOTS = 2;

        AlienManager alienManager;
        private Tank player;
        List<Bullet> bullets;
        GameState state;

        private int playerBulletsOnScreen;
        private int alienBulletsOnScreen;
        
        // Used for player lives.
        Texture2D tankTexture;

        int currentScore;
        int previousScore;
        Texture2D scoreImage;

        public GameScreen()
        {
            alienManager = new AlienManager();
            bullets = new List<Bullet>();
            alienBulletsOnScreen = 0;

            player = new Tank(new Vector2(0));
            CenterPlayer();
            tankTexture = player.Animation.getTexture();

            currentScore = 0;
            previousScore = 0;

            scoreImage = new TextToImage(ScoreToString(currentScore), null).Image;
        }

        private string ScoreToString(int score)
        { 
            return score.ToString("D8");
        }

        private void CenterPlayer()
        {
            int XSpawnPosition = (Constaints.ScreenResolution.ScreenWidth / 2);
            int YSpawnPosition = (int)(Constaints.GameArea.Height - (player.getHeight()));

            Vector2 PlayerSpawnPosition = new Vector2(XSpawnPosition, YSpawnPosition);

            player.setPosition(XSpawnPosition, YSpawnPosition);
        }

        public void Update(int time)
        {
            if (player != null)
            {
                if (player.IsAlive)
                {
                    HandlePlayer(time);
                }
                else if (0 < player.getLives())
                    Respawn();
                else
                {
                    player = null;
                    state = GameState.GameOver;
                }
            }
            alienManager.Update(time);

            HandleBullets(time);

            if (previousScore != currentScore)
            {
                scoreImage = new TextToImage(ScoreToString(currentScore), null).Image;
                previousScore = currentScore;
            }
        }

        private void HandlePlayer(int time)
        {
            if (player == null)
                return;

            player.Update(time);
            alienManager.HandleCollision(player, ref currentScore);

            Bullet bullet = player.Shoot();

            if (bullet != null)
                bullets.Add(bullet);         
        }

        private void HandleBullets(int time)
        {
            foreach (Bullet bulletsShoot in alienManager.FiredBullets)
                if (alienBulletsOnScreen < MAX_ALIEN_SHOOTS)
                {
                    bullets.Add(bulletsShoot);
                    alienBulletsOnScreen++;
                }

            if(player!=null && player.Shoot() != null)
            {
                if(playerBulletsOnScreen < MAX_TANK_SHOTS)
                {
                    bullets.Add(player.Shoot());
                    playerBulletsOnScreen++;
                }
            }
            
            for (int x = 0; x < bullets.Count; x++)
            {
                Bullet bullet = bullets[x];

                bullet.Update(time);

                if (bullet.GetType() == typeof(AlienBullet))
                {

                    if (bullet.Collision(player))
                    {
                        player.Kill();
                        bullet.Kill();
                    }

                    if (Constaints.GameArea.Height < bullet.Position.Y)
                    {
                        RemoveBullet(bullet);
                        alienBulletsOnScreen--;
                    }
                }

                else
                {
                    alienManager.HandleCollision(bullet, ref currentScore);

                    

                    if (bullet.Position.Y < 0)
                    {
                        RemoveBullet(bullet);
                        playerBulletsOnScreen--;
                    }
                }

                if (!bullet.IsAlive)
                    bullets.Remove(bullet);
            }
        }

        private void Respawn()
        {
            player.Respawn();
            CenterPlayer();

            alienManager.ResetAlienPosition();
            bullets.Clear();

            alienBulletsOnScreen = 0;
            playerBulletsOnScreen = 0;
        }

        private void RemoveBullet(Bullet bullet)
        {
            bullets.Remove(bullet);

            if (bullet.GetType() == typeof(AlienBullet))
                alienBulletsOnScreen--;
        }

        public GameState State
        {
            get
            {
                if (state == GameState.Playing && alienManager.State == GameState.Playing)
                    return GameState.Playing;
                else
                    return GameState.GameOver;
            }
        }

        public void Render(SpriteBatch spriteBatch)
        {
            foreach (Alien alien in alienManager.Aliens)
                if (alien != null)
                    alien.Draw(spriteBatch);

            foreach (Bullet bullet in bullets)
                if (bullet != null)
                    bullet.Draw(spriteBatch);

            if (player != null)
            {
                player.Draw(spriteBatch);

                for (int x = 0; x < player.getLives(); x++)
                {
                    int PosX = (tankTexture.Width * x + 10 * x);
                    int PosY = Constaints.ScreenResolution.ScreenHeight - (tankTexture.Height);
                    Vector2 drawPosition = new Vector2(PosX, PosY);

                    float scale = (int)Constaints.ScreenResolution.getScale().X >> 2;
                    scale = (scale == 0) ? .50f : scale;

                    spriteBatch.Draw(tankTexture, drawPosition, null, Color.White, 0, new Vector2(0), scale, SpriteEffects.None, 0 );
                }
            }
            
            spriteBatch.Draw(scoreImage, new Vector2(0), null, Color.White, 0, new Vector2(0),Constaints.ScreenResolution.getScale(), SpriteEffects.None, 0);            
        }
    }
}
