using Microsoft.Xna.Framework;
using SpaceInvaders.src.game.entity;
using SpaceInvaders.src.game.entity.alien;
using SpaceInvaders.src.misc;
using System;
using System.Collections.Generic;

namespace SpaceInvaders.src.game.world
{
    class AlienManager
    {
        private readonly double DEFAULT_ALIEN_HORIZONTAL_MOVEMENT_SPEED = .01;
        private readonly double DEFAULT_ALIEN_VERTICAL_MOVEMENT_SPEED = 10;

        private readonly int ALIEN_COLUMNS = 11;
        private readonly int ALIEN_ROWS = 5;

        private enum AlienMoveDiration { Left, Right };
        AlienMoveDiration direction;
        GameState state;

        private Alien[,] aliens;
        private double currentMovementSpeed;
        private int aliensAlive;

        private Random rand;

        List<Bullet> bulletsShot;
        
        public AlienManager()
        {
            direction = AlienMoveDiration.Right;
            state = GameState.Playing;
            aliens = new Alien[ALIEN_COLUMNS, ALIEN_ROWS];
            bulletsShot = new List<Bullet>();

            currentMovementSpeed = DEFAULT_ALIEN_HORIZONTAL_MOVEMENT_SPEED;
            rand = new Random();

            // Here, all we are doing is initializing the aliens. We can not set the position until the 'RestAlienPosition()
            // function is called. If the playe dies mid gamethe aliens need to be rested to the middle of the screen.
            for(int x=0;x < ALIEN_COLUMNS; x++)
            {
                for(int y=0;y < ALIEN_ROWS; y++)
                {
                    aliens[x, y] = new Alien(new Vector2(0), y);
                }
            }

            ResetAlienPosition();
        }

        public void ResetAlienPosition()
        {
            if (new Random().Next(100) < 50)
                direction = AlienMoveDiration.Right;
            else
                direction = AlienMoveDiration.Left;

            for (int x = 0; x < ALIEN_COLUMNS; x++)
            {
                for (int y = 0; y < ALIEN_ROWS; y++)
                {
                    if (aliens[x, y] != null)
                    {
                        Alien alien = aliens[x, y];
                        alien.setPosition((int)((x * 16) * aliens[x, y].DrawOptions.Scale.X), 0);
                    }
                }
            }

            float WidestAlienPos = getColumnWidth();

            for (int x = ALIEN_COLUMNS - 1; 0 <= x && WidestAlienPos == 0; x--)
            {
                for (int y = ALIEN_ROWS - 1; 0 <= y && WidestAlienPos == 0; y--)
                {
                    if (aliens[x, y] != null)
                    {
                        WidestAlienPos = (float)(aliens[ALIEN_COLUMNS - 1, ALIEN_ROWS - 1].Position.X + aliens[ALIEN_COLUMNS - 1, ALIEN_ROWS - 1].getWidth());
                        WidestAlienPos = (int)(WidestAlienPos) >> 1;
                    }
                }
            }

            int ScreenWidth = Constaints.ScreenResolution.ScreenWidth;

            for (int x = 0; x < ALIEN_COLUMNS; x++)
            {
                for (int y = 0; y < ALIEN_ROWS; y++)
                {
                    if (aliens[x, y] != null)
                    {
                        if (aliens[x, y] != null)
                        {
                            Alien alien = aliens[x, y];
                            float PosX = (ScreenWidth >> 1) - WidestAlienPos;
                            float PosY = (float)(32 + (y * 16) * alien.DrawOptions.Scale.X);

                            // We offset the location of the alien with the smallest width to align it with the 
                            // its other firends.
                            int offset = 0;
                            if (alien.getWidth() == alien.DrawOptions.Scale.X * 8)
                                offset = 5;

                            alien.addPosition(PosX + offset, PosY);

                            aliensAlive++;
                        }
                    }
                }
            }
        }

        public void Update(int time)
        {
            if (aliensAlive == 0)
                return;

            int currentAliensAlive = AliensCount;

            for (int x = 0; x < ALIEN_COLUMNS; x++)
            {
                for (int y = 0; y < ALIEN_ROWS; y++)
                {
                    if (aliens[x, y] != null)
                    {
                        aliens[x, y].Update(time);

                        if (aliens[x, y].IsAlive)
                        {
                            UpdateMovement();
                        }
                        else
                        {
                            aliens[x, y] = null;
                            aliensAlive--;
                        }
                    }
                }
            }

            if (currentAliensAlive != AliensCount)
            {
                if (AliensCount % 11 == 0)
                          currentMovementSpeed *= 2;
            }
            
            int alienShoot = rand.Next(1, 100);

            if( alienShoot <= 1)
            {
                // We look at every aliens that is on the bottom edge of the column,
                // if we find an alien that is not null we we select it as a possible
                // attacker, and we do this through out the entire array of aliens so we can
                // have only the aliens that are on the outside fire
                List<Alien> possibleAttackers = new List<Alien>();

                for (int x = ALIEN_COLUMNS - 1; 0 <= x; x--)
                {
                    for (int y = ALIEN_ROWS - 1; 0 <= y; y--)
                    {
                        if (aliens[x, y] != null && aliens[x, y].IsAlive)
                        {
                            possibleAttackers.Add(aliens[x, y]);
                            break;
                        }
                    }
                }


                if (0 < possibleAttackers.Count)
                {
                    Alien attacker = possibleAttackers[rand.Next() % possibleAttackers.Count];
                    bulletsShot.Add(attacker.Shoot());
                }
            }
            
        }

        public void HandleCollision(Entity entity, ref int Points)
        {
            // We handle the situation differently if the entity is passed is the player. We quickly check the bottom row of the alien fleet to see if the player 
            // has faild to killed them all before, if so the player dies.
            if (entity.GetType() == (Type)typeof(entity.Tank))
            {
                bool alienOnSamePlaneAsPlayer = false;

                for (int x = ALIEN_COLUMNS - 1; 0 <= x && !alienOnSamePlaneAsPlayer; x--)
                {
                    for (int y = ALIEN_ROWS - 1; 0 <= y && !alienOnSamePlaneAsPlayer; y--)
                    {
                        if (aliens[x, y] != null)
                        {
                            if (entity.Collision(aliens[x,y]))
                            {
                                aliens[x, y].Kill();
                                entity.Kill();
                                state = GameState.GameOver;
                            }
                            else
                                break;
                        }
                    }
                }
            }
            else if (entity.GetType() == (Type)typeof(entity.TankBullet))
            {
                for (int x = 0; x < ALIEN_COLUMNS; x++)
                {
                    for (int y = 0; y < ALIEN_ROWS; y++)
                    {
                        if (aliens[x, y] != null && entity.Collision(aliens[x, y]))//.Collision(entity))
                        {
                            aliens[x, y].Kill();
                            entity.Kill();
                            Points += 100;
                        }
                    }
                }
            }
        }

        #region Movement

        private void UpdateMovement()
        {
            for (int y = ALIEN_ROWS - 1; 0 <= y; y--)
            {
                for (int x = ALIEN_COLUMNS - 1; 0 <= x; x--)
                {
                    if (aliens[x, y] != null)
                    {
                        if (aliens[x, y].Position.Y + (aliens[x, y].getHeight() + (DEFAULT_ALIEN_VERTICAL_MOVEMENT_SPEED)) >= Constaints.GameArea.Height)
                        {
                            state = GameState.GameOver;
                            return;
                        }
                    }
                }
            }

            if (direction == AlienMoveDiration.Right)
            {
                MoveRight();
            }
            else
            {
                MoveLeft();
            }
        }

        private void MoveLeft()
        {
            int PosX = 0;
            int PosY = 0;

            for (int x = 0; x < ALIEN_COLUMNS && PosX == 0; x++)
            {
                for (int y = ALIEN_ROWS - 1; 0 <= y && PosY == 0; y--)
                {
                    if (aliens[x, y] != null)
                    {
                        PosX = x;
                        PosY = y;
                    }
                }

            }

            if (aliens[PosX, PosY].Position.X <= Constaints.GameArea.X)
            {
                direction = AlienMoveDiration.Right;
                MoveFleet(0, DEFAULT_ALIEN_VERTICAL_MOVEMENT_SPEED);
            }
            else
                MoveFleet(-currentMovementSpeed, 0);

        }

        private void MoveRight()
        {
            int PosX = 0;
            int PosY = 0;

            for (int x = ALIEN_COLUMNS - 1; 0 <= x && PosX == 0; x--)
            {
                for (int y = ALIEN_ROWS - 1; 0 <= y && PosY == 0; y--)
                {
                    if (aliens[x, y] != null)
                    {
                        PosX = x;
                        PosY = y;
                    }
                }
            }

            if (Constaints.GameArea.Width <= (aliens[PosX,PosY].Position.X + aliens[PosX,PosY].getWidth() + 1 ))
            {
                direction = AlienMoveDiration.Left;
                MoveFleet(0, DEFAULT_ALIEN_VERTICAL_MOVEMENT_SPEED);
            }
            else
                MoveFleet(currentMovementSpeed, 0);
        }

        private void MoveFleet(double X, double Y)
        {
            for (int x = 0; x < ALIEN_COLUMNS; x++)
            {
                for (int y = 0; y < ALIEN_ROWS; y++)
                {
                    if (aliens[x, y] != null)
                        aliens[x, y].Move((float)X, (float)Y);
                }
            }
        }
        
        #endregion

        public int getColumnWidth()
        {
            int WidestAlienPos = 0;

            for (int x = ALIEN_COLUMNS - 1; 0 <= x && WidestAlienPos == 0; x--)
            {
                for (int y = ALIEN_ROWS - 1; 0 <= y && WidestAlienPos == 0; y--)
                {
                    if (aliens[x, y] != null)
                    {
                        WidestAlienPos = (int)(aliens[ALIEN_COLUMNS - 1, ALIEN_ROWS - 1].Position.X + aliens[ALIEN_COLUMNS - 1, ALIEN_ROWS - 1].getWidth());
                        WidestAlienPos = (WidestAlienPos) >> 1;
                    }
                }
            }

            return WidestAlienPos;
        }

        public Alien[,] Aliens { get { return aliens; } }
        public Bullet[] FiredBullets
        {
            get
            {
                Bullet[] bullets = new Bullet[bulletsShot.Count];
                bulletsShot.CopyTo(bullets);
                bulletsShot.Clear();
                return bullets;
            }
        }
        public GameState State { get { return state; } }

        public int AlienWidth { get { return ALIEN_COLUMNS; } }
        public int AlienHeight { get { return ALIEN_ROWS; } }
        public int AliensCount { get { return aliensAlive; } }
    }
}