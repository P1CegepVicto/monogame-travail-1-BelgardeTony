﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace Guerre
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Rectangle fenetre;
        Random random = new Random();
        bool play = false;
        bool startMenu = true;

        
        Texture2D black;
        Texture2D gameover;
        
        
        SpriteFont texte;
        SpriteFont score;
        SpriteFont vie;
        SpriteFont balle;

        int munitions = 6;
        int point = 0;
        int life = 5;

        GameObject starts;
        GameObject background2;
        GameObject background;
        GameObject soldat;
        GameObject balles;
        GameObject[] enemie = new GameObject[3];
        GameObject[] balles2 = new GameObject[6];



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.graphics.PreferredBackBufferWidth = graphics.GraphicsDevice.DisplayMode.Width;
            this.graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.DisplayMode.Height;
            this.graphics.ToggleFullScreen();
            fenetre = new Rectangle(0, 0, graphics.GraphicsDevice.DisplayMode.Width, graphics.GraphicsDevice.DisplayMode.Height);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            starts = new GameObject();
            starts.estVivant = true;
            starts.sprite = Content.Load<Texture2D>("starts.png");
            starts.position.X = 750;
            starts.position.Y = 450;

            black = Content.Load<Texture2D>("Dead.jpg");
            gameover = Content.Load<Texture2D>("Gameover.png");


            //if (GameState == true && start == false)
            //{
                Song song = Content.Load<Song>("sounds//GameSong");
                MediaPlayer.Play(song);

                soldat = new GameObject();
                soldat.estVivant = true;
                soldat.sprite = Content.Load<Texture2D>("soldat.png");
                soldat.position.X = 0;
                soldat.position.Y = 200;

                

                balles = new GameObject();
                balles.estVivant = false;
                balles.sprite = Content.Load<Texture2D>("balle.png");
                balles.position.X = 600;
                balles.position.Y = 200;

            for (int i = 0; i < balles2.Length; i++)
            {
                balles2[i] = new GameObject();
                balles2[i].estVivant = false;
                balles2[i].sprite = Content.Load<Texture2D>("balle.png");
                balles2[i].position.X = soldat.position.X+150;
                balles2[i].position.Y = soldat.position.Y+50;
            }
            for (int i = 0; i < enemie.Length; i++)
            {
                enemie[i] = new GameObject();
                enemie[i].estVivant = true;
                enemie[i].sprite = Content.Load<Texture2D>("enemie.png");
                enemie[i].position.X = random.Next(500, fenetre.Right);
                enemie[i].position.Y = random.Next();
                enemie[i].vitesse.Y = random.Next(10, 20);
                enemie[i].vitesse.X = -10;
            }

                background = new GameObject();
                background.estVivant = true;
                background.sprite = Content.Load<Texture2D>("background.jpg");
                background.position.X = 0;
                background.position.Y = 0;
                background.vitesse.X = -10;

                background2 = new GameObject();
                background2.sprite = Content.Load<Texture2D>("background.jpg");
                background2.position.X = 1920;
                background2.position.Y = 0;
                background2.vitesse.X = -10;



            texte = Content.Load<SpriteFont>("Font");
            score = Content.Load<SpriteFont>("Font");
            vie = Content.Load<SpriteFont>("Font");
            balle = Content.Load<SpriteFont>("Font");
            //}


            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (background2.position.X == fenetre.Left)
            {
                background.position.X = background2.position.X + background2.sprite.Width;
            }
            if (background.position.X == fenetre.Left)
            {
                background2.position.X = background.position.X + background.sprite.Width;
            }


            // TODO: Add your update logic here
           
                    UpdateSoldat();
                    UpdateEnemi(gameTime);
                    UpdateBackground();
                    UpdateBackground2();
            base.Update(gameTime);
        }

         protected void UpdateBackground2()
        {
            background2.position += background2.vitesse;
        }

        protected void UpdateBackground()
        {
            background.position += background.vitesse;
        }

        protected void UpdateSoldat()
        {
            if (soldat.estVivant == true)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    soldat.position.Y -= 15;
                    if (soldat.position.Y < 0)
                    {
                        soldat.position.Y = 0;
                    }
                }

                if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    soldat.position.Y += 15;
                    if (soldat.position.Y > 800)
                    {
                        soldat.position.Y = 800;
                    }
                }
            }
        }

        protected void UpdateEnemi(GameTime gameTime)
        {
            //Projectile********************************
            if (soldat.estVivant == true)
            {
                
                    if (Keyboard.GetState().IsKeyDown(Keys.Space) && gameTime.TotalGameTime.Seconds % 1 == 0)
                    {
                    
                        if (random.Next(0, 6) == 0 && balles2[0].estVivant == false && munitions > 0 && munitions <= 6)
                    { 
                            balles2[0].estVivant = true;
                            balles2[0].position.X = soldat.position.X + 150;
                            balles2[0].position.Y = soldat.position.Y + 50;
                            munitions -= 1;
                            Song son = Content.Load<Song>("sounds//Fusil");
                            MediaPlayer.Play(son);
                    }
                    
                        if (random.Next(0, 6) == 1 && balles2[1].estVivant == false && munitions > 0 && munitions <= 6)
                        {
                            balles2[1].estVivant = true;
                            balles2[1].position.X = soldat.position.X + 150;
                            balles2[1].position.Y = soldat.position.Y + 50;
                            munitions -= 1;
                            Song son = Content.Load<Song>("sounds//Fusil");
                            MediaPlayer.Play(son);
                        
                    }
                    
                        if (random.Next(0, 6) == 2 && balles2[2].estVivant == false && munitions > 0 && munitions <= 6)
                        {
                            balles2[2].estVivant = true;
                            balles2[2].position.X = soldat.position.X + 150;
                            balles2[2].position.Y = soldat.position.Y + 50;
                            munitions -= 1;
                            Song son = Content.Load<Song>("sounds//Fusil");
                            MediaPlayer.Play(son);
                        
                    }
                    
                        if (random.Next(0, 6) == 3 && balles2[3].estVivant == false && munitions > 0 && munitions <= 6)
                        {
                            balles2[3].estVivant = true;
                            balles2[3].position.X = soldat.position.X + 150;
                            balles2[3].position.Y = soldat.position.Y + 50;
                            munitions -= 1;
                            Song son = Content.Load<Song>("sounds//Fusil");
                            MediaPlayer.Play(son);
                        
                    }
                    
                        if (random.Next(0, 6) == 4 && balles2[4].estVivant == false && munitions > 0 && munitions <= 6)
                        {
                            balles2[4].estVivant = true;
                            balles2[4].position.X = soldat.position.X + 150;
                            balles2[4].position.Y = soldat.position.Y + 50;
                            munitions -= 1;
                            Song son = Content.Load<Song>("sounds//Fusil");
                            MediaPlayer.Play(son);
                        
                    }
                    if (random.Next(0, 6) == 5 && balles2[5].estVivant == false && munitions > 0 && munitions <= 6)
                        {
                            balles2[5].estVivant = true;
                            balles2[5].position.X = soldat.position.X + 150;
                            balles2[5].position.Y = soldat.position.Y + 50;
                            munitions -= 1;
                            Song son = Content.Load<Song>("sounds//Fusil");
                            MediaPlayer.Play(son);
                        
                    }
                }
                if (munitions <= 5 && munitions >= 0)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.R))
                    {
                        munitions = 6;
                    }
                }
                if (balles2[0].estVivant == true || balles2[1].estVivant == true || balles2[2].estVivant == true || balles2[3].estVivant == true || balles2[4].estVivant == true || balles2[5].estVivant == true)
                {
                    balles2[0].position.X += 20;
                    balles2[1].position.X += 20;
                    balles2[2].position.X += 20;
                    balles2[3].position.X += 20;
                    balles2[4].position.X += 20;
                    balles2[5].position.X += 20;
                    if (balles2[0].position.X >= fenetre.Right)
                    {
                        balles2[0].estVivant = false;
                        balles2[0].position.X = -5;
                        balles2[0].position.Y = -5;
                    }
                    if (balles2[1].position.X >= fenetre.Right)
                    {
                        balles2[1].estVivant = false;
                        balles2[1].position.X = -5;
                        balles2[1].position.Y = -5;
                    }
                    if (balles2[2].position.X >= fenetre.Right)
                    {
                        balles2[2].estVivant = false;
                        balles2[2].position.X = -5;
                        balles2[2].position.Y = -5;
                    }
                    if (balles2[3].position.X >= fenetre.Right)
                    {
                        balles2[3].estVivant = false;
                        balles2[3].position.X = -5;
                        balles2[3].position.Y = -5;
                    }
                    if (balles2[4].position.X >= fenetre.Right)
                    {
                        balles2[4].estVivant = false;
                        balles2[4].position.X = -5;
                        balles2[4].position.Y = -5;
                    }
                    if (balles2[5].position.X >= fenetre.Right)
                    {
                        balles2[5].estVivant = false;
                        balles2[5].position.X = -5;
                        balles2[5].position.Y = -5;
                    }
                }
                //for (int i = 0; i < balles2.Length; i++)
                //{
                //    balles2[i].position.X += 20;
                //    if (balles2[i].estVivant == false)
                //    {
                //        if (Keyboard.GetState().IsKeyDown(Keys.Space))
                //        {
                //            balles2[i].estVivant = true;
                //            balles2[i].position.X = soldat.position.X + 150;
                //            balles2[i].position.Y = soldat.position.Y + 50;
                //            munitions -= 1;
                //            Song son = Content.Load<Song>("sounds//Fusil");
                //            MediaPlayer.Play(son);
                //        }
                //    }
                //    if (munitions <= 5 || munitions == 0)
                //    {
                //        if (Keyboard.GetState().IsKeyDown(Keys.R))
                //        {
                //            munitions = 6;
                //        }
                //    }
                //    if (balles2[i].position.X >= fenetre.Right || munitions == 0)
                //    {
                //        balles2[i].estVivant = false;
                //    }
                //}

                balles.position.X -= 20;
                if (balles.position.X < fenetre.Left)
                {
                    balles.estVivant = false;
                    balles.position.X = -10;
                    balles.position.Y = -10;
                }
            }

            //enemie****************************************************************
            for (int i = 0; i < enemie.Length; i++)
            {
                enemie[i].position += enemie[i].vitesse;

                if (enemie[i].position.Y < fenetre.Top)
                {
                    enemie[i].position.Y = fenetre.Top;
                }

                if (enemie[i].position.Y + enemie[i].sprite.Bounds.Height > fenetre.Bottom)
                {
                    enemie[i].position.Y = fenetre.Bottom - enemie[i].sprite.Bounds.Height;
                }

                if (enemie[i].position.Y == fenetre.Top)
                {
                    enemie[i].vitesse.Y = random.Next(10, 20);
                }

                if (enemie[i].position.Y + enemie[i].sprite.Bounds.Height == fenetre.Bottom)
                {
                    enemie[i].vitesse.Y = random.Next(-12, -3);
                }
                if (enemie[i].position.X < fenetre.Left)
                {
                    enemie[i].estVivant = false;
                }

                if (enemie[i].estVivant == false)
                {
                    enemie[i].estVivant = true;
                    enemie[i].position.X = fenetre.Right;
                    enemie[i].position.Y = random.Next();
                    enemie[i].vitesse.X = -10;
                    enemie[i].vitesse.Y = random.Next(5, 20);
                }
                if (balles.estVivant == false && soldat.estVivant == true && enemie[i].estVivant == true)
                {
                    if (enemie[i].estVivant == true && soldat.estVivant == true)
                    {

                        balles.position.X = enemie[i].position.X + 50;
                        balles.position.Y = enemie[i].position.Y + 100;
                        balles.estVivant = true;
                        Song son = Content.Load<Song>("sounds//Fusil");
                        MediaPlayer.Play(son);
                    }
                }

                if (soldat.estVivant == true && enemie[i].estVivant == true && balles.estVivant == true || balles2[i].estVivant == true)
                {
                    if (enemie[i].estVivant == true && soldat.estVivant == true)
                    {
                        if (soldat.GetRect().Intersects(enemie[i].GetRect()))
                        {
                            soldat.estVivant = false;
                            balles.estVivant = false;
                            balles2[i].position.X = -5;
                            balles2[i].position.Y = -5;
                            balles2[i].estVivant = false;
                            life -= 1;
                        }
                    }
                    if (soldat.estVivant == true && balles.estVivant == true)
                    {
                        if (soldat.GetRect().Intersects(balles.GetRect()))
                        {
                            soldat.estVivant = false;
                            balles.estVivant = false;
                            balles2[i].position.X = -5;
                            balles2[i].position.Y = -5;
                            balles2[i].estVivant = false;
                            life -= 1;
                        }
                    }

                    if (balles2[0].estVivant == true || balles2[1].estVivant == true || balles2[2].estVivant == true || balles2[3].estVivant == true || balles2[4].estVivant == true || balles2[5].estVivant == true && enemie[i].estVivant == true && soldat.estVivant == true)
                    {

                        if (enemie[i].GetRect().Intersects(balles2[0].GetRect()))
                        {
                            enemie[i].estVivant = false;
                            balles2[0].estVivant = false;
                            balles.estVivant = false;
                            point += 1;
                        }
                        if (enemie[i].GetRect().Intersects(balles2[1].GetRect()))
                        {
                            enemie[i].estVivant = false;
                            balles2[1].estVivant = false;
                            balles.estVivant = false;
                            point += 1;
                        }
                        if (enemie[i].GetRect().Intersects(balles2[2].GetRect()))
                        {
                            enemie[i].estVivant = false;
                            balles2[2].estVivant = false;
                            balles.estVivant = false;
                            point += 1;
                        }
                        if (enemie[i].GetRect().Intersects(balles2[3].GetRect()))
                        {
                            enemie[i].estVivant = false;
                            balles2[3].estVivant = false;
                            balles.estVivant = false;
                            point += 1;
                        }
                        if (enemie[i].GetRect().Intersects(balles2[4].GetRect()))
                        {
                            enemie[i].estVivant = false;
                            balles2[4].estVivant = false;
                            balles.estVivant = false;
                            point += 1;
                        }
                        if (enemie[i].GetRect().Intersects(balles2[5].GetRect()))
                        {
                            enemie[i].estVivant = false;
                            balles2[5].estVivant = false;
                            balles.estVivant = false;
                            point += 1;
                        }
                    }

                    if (balles2[i].estVivant == true && balles.estVivant == true)
                    {
                        if (balles.GetRect().Intersects(balles2[0].GetRect()))
                        {
                            balles.estVivant = false;
                            balles2[0].estVivant = false;
                        }
                        if (balles.GetRect().Intersects(balles2[1].GetRect()))
                        {
                            balles.estVivant = false;
                            balles2[1].estVivant = false;
                        }
                        if (balles.GetRect().Intersects(balles2[2].GetRect()))
                        {
                            balles.estVivant = false;
                            balles2[2].estVivant = false;
                        }
                        if (balles.GetRect().Intersects(balles2[3].GetRect()))
                        {
                            balles.estVivant = false;
                            balles2[3].estVivant = false;
                        }
                        if (balles.GetRect().Intersects(balles2[4].GetRect()))
                        {
                            balles.estVivant = false;
                            balles2[4].estVivant = false;
                        }
                        if (balles.GetRect().Intersects(balles2[5].GetRect()))
                        {
                            balles.estVivant = false;
                            balles2[5].estVivant = false;
                        }
                    }
                }

                if (life > 0)
                {
                    if (soldat.estVivant == false)
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.T))
                        {
                            soldat.estVivant = true;
                            soldat.position.X = fenetre.Left;
                            soldat.position.Y = 100;
                            munitions = 6;

                        }
                    }
                }
                if (life <= 0 && soldat.estVivant == false)
                {
                    enemie[i].estVivant = false;
                    balles.estVivant = false;
                    if (Keyboard.GetState().IsKeyDown(Keys.Back))
                    {
                        point = 0;
                        life = 5;
                        soldat.estVivant = true;
                        enemie[i].estVivant = true;
                    }
                }
                if (startMenu == true)
                {
                    enemie[i].estVivant = false;
                    point = 0;
                    soldat.estVivant = false;
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        startMenu = false;
                        soldat.estVivant = true;
                    }
                }
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            // TODO: Add your drawing code here

            if (startMenu == true)
            {
                spriteBatch.Draw(black, new Rectangle(0, 0, graphics.GraphicsDevice.DisplayMode.Width, graphics.GraphicsDevice.DisplayMode.Height), Color.White);
                spriteBatch.Draw(starts.sprite, starts.position, Color.White);
                
            }
            if (startMenu == false)
            {

                spriteBatch.Draw(background.sprite, background.position, Color.White);
                spriteBatch.Draw(background2.sprite, background2.position, effects: SpriteEffects.FlipHorizontally);
                spriteBatch.DrawString(vie, " Vie: " + life.ToString(), new Vector2(850, 0), Color.Black);
                spriteBatch.DrawString(balle, " Balles: " + munitions.ToString(), new Vector2(1700, 1000), Color.Black);
                spriteBatch.DrawString(score, " Score: " + point.ToString(), new Vector2(1700, 0), Color.Black);

                if (soldat.estVivant == true)
                {
                    spriteBatch.Draw(soldat.sprite, soldat.position);
                }

                if (balles.estVivant == true)
                {
                    spriteBatch.Draw(balles.sprite, balles.position);
                }


                for (int i = 0; i < enemie.Length; i++)
                {
                    if (enemie[i].estVivant == true)
                    {
                        spriteBatch.Draw(enemie[i].sprite, enemie[i].position);
                    }
                }

                for (int i = 0; i < balles2.Length; i++)
                {
                    if (balles2[i].estVivant == true && soldat.estVivant == true)
                    {
                        spriteBatch.Draw(balles2[i].sprite, balles2[i].position, effects: SpriteEffects.FlipHorizontally);
                    }
                }
            }
            if (life <= 0 && soldat.estVivant == false)
            {
                spriteBatch.Draw(black, new Rectangle(0, 0, graphics.GraphicsDevice.DisplayMode.Width, graphics.GraphicsDevice.DisplayMode.Height), Color.White);

                spriteBatch.Draw(gameover, new Rectangle(0, 0, graphics.GraphicsDevice.DisplayMode.Width, graphics.GraphicsDevice.DisplayMode.Height), Color.White);

                spriteBatch.DrawString(score, " Score: " + point.ToString(), new Vector2(850, 600), Color.White);

                
            }


            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
