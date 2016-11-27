using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace Projet_2_Naruto
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle fenetre;
        Texture2D background;
        Texture2D Mort;
        Texture2D Gameover;
        Random random = new Random();
        SpriteFont Text;
        SpriteFont score;
        SpriteFont Vie;

        int rotate = 0;
        int rotate1 = 0;
        int projectiletirer;
        int kills = 0;
        int dead = 10;
        int kill;

        GameObject Naruto;
        GameObject[] Madara;
        GameObject Kunai;
        GameObject Shuriken;
        GameObject Shuriken2;
        GameObject Shuriken3;

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

            Song song = Content.Load<Song>("sound//ThemeSong");
            MediaPlayer.Play(song);

            if (dead <= 0)
            {
                Song son = Content.Load<Song>("sound//DeadSong");
                MediaPlayer.Play(son);
            }

            Naruto = new GameObject();
            Naruto.estVivant = true;
            Naruto.sprite = Content.Load<Texture2D>("Naruto.png");
            Naruto.position.X = 0;
            Naruto.position.Y = 200;

            Kunai = new GameObject();
            Kunai.estVivant = false;
            Kunai.sprite = Content.Load<Texture2D>("Kunai.png");

            Shuriken = new GameObject();
            Shuriken.estVivant = false;
            Shuriken.sprite = Content.Load<Texture2D>("Shuriken.png");

            Shuriken2 = new GameObject();
            Shuriken2.estVivant = false;
            Shuriken2.sprite = Content.Load<Texture2D>("Shuriken.png");

            Shuriken3 = new GameObject();
            Shuriken3.estVivant = false;
            Shuriken3.sprite = Content.Load<Texture2D>("Shuriken.png");

            background = Content.Load<Texture2D>("Background.png");
            Mort = Content.Load<Texture2D>("Dead.jpg");
            Gameover = Content.Load<Texture2D>("Gameover.png");

            Text = Content.Load<SpriteFont>("Font");
            score = Content.Load<SpriteFont>("Font");
            Vie = Content.Load<SpriteFont>("Font");

            Madara = new GameObject[50];

            for (int i = 0; i < Madara.Length; i++)
            {
                Madara[i] = new GameObject();
                Madara[i].estVivant = true;
                Madara[i].position.X = 1600;
                Madara[i].position.Y = 300;
                Madara[i].vitesse.X = random.Next(3, 9);
                Madara[i].vitesse.Y = random.Next(3, 9);
                Madara[i].sprite = Content.Load<Texture2D>("Madara.png");

            }


            //******************************
            //Limite à gauche de X = 0
            //Limite à droite de X = 1720
            //Limite en hauteur de Y = 0
            //Limite en bas de Y = 945 
            //*******************************

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

            kill = kills;

            // TODO: Add your update logic here

            rotate -= 1;
            rotate1 += 1;
            MoveNaruto();
            MoveMadara();
            UpdateProjectile();
            base.Update(gameTime);
        }

        protected void MoveNaruto()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                Naruto.position.X -= 10;
                if (Naruto.position.X < 0)
                {
                    Naruto.position.X = 0;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                Naruto.position.X += 10;
                if (Naruto.position.X > 1720)
                {
                    Naruto.position.X = 1720;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                Naruto.position.Y += 10;
                if (Naruto.position.Y > 945)
                {
                    Naruto.position.Y = 945;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                Naruto.position.Y -= 10;
                if (Naruto.position.Y < 0)
                {
                    Naruto.position.Y = 0;
                }

            }

            if (Naruto.estVivant == true)
            {
                if (Shuriken.estVivant == false && Kunai.estVivant == false)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        Shuriken.estVivant = true;
                        Shuriken.position.X = Naruto.position.X + 200;
                        Shuriken.position.Y = Naruto.position.Y + 50;
                    }
                }

                if (Kunai.estVivant == false && Shuriken.estVivant == false)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        Kunai.estVivant = true;
                        Kunai.position.X = Naruto.position.X + 200;
                        Kunai.position.Y = Naruto.position.Y + 50;
                    }
                }
            }
        }

        protected void MoveMadara()
        {
            if (Shuriken2.estVivant == false)
            {
                projectiletirer = random.Next(0, 6);

            }

            for (int i = Madara.Length - 1; i >= 0; i--)
            {


                Madara[i].position += Madara[i].vitesse;

                if (Madara[i].position.X < fenetre.Left)
                {
                    Madara[i].position.X = fenetre.Left;
                }
                if (Madara[i].position.Y < fenetre.Top)
                {
                    Madara[i].position.Y = fenetre.Top;
                }
                if (Madara[i].position.X + Madara[i].sprite.Bounds.Width > fenetre.Right)
                {
                    Madara[i].position.X = fenetre.Right - Madara[i].sprite.Bounds.Width;
                }
                if (Madara[i].position.Y + Madara[i].sprite.Bounds.Height > fenetre.Bottom)
                {
                    Madara[i].position.Y = fenetre.Bottom - Madara[i].sprite.Bounds.Height;
                }
                if (Madara[i].position.X > fenetre.Right)
                {
                    Madara[i].position.X = fenetre.Right;
                }
                if (Madara[i].position.Y == fenetre.Top)
                {
                    Madara[i].vitesse.Y = random.Next(3, 12);
                }

                if (Madara[i].position.Y + Madara[i].sprite.Bounds.Height == fenetre.Bottom)
                {
                    Madara[i].vitesse.Y = random.Next(-12, -3);
                }

                if (Madara[i].position.X + Madara[i].sprite.Bounds.Width == fenetre.Right)
                {
                    Madara[i].vitesse.X = random.Next(-12, -3);
                }

                if (Madara[i].position.X <= fenetre.Left)
                {
                    Madara[i].vitesse.X = random.Next(3, 12);
                }

                if (Naruto.estVivant == true && Madara[i].estVivant == true && Shuriken.estVivant == true || Shuriken2.estVivant == true || Shuriken3.estVivant == true || Kunai.estVivant == true)
                {
                    if (Madara[i].estVivant == true && Naruto.estVivant == true)
                    {
                        if (Naruto.GetRect().Intersects(Madara[i].GetRect()))
                        {
                            Naruto.estVivant = false;
                            Shuriken2.estVivant = false;
                            Shuriken3.estVivant = false;
                            dead -= 1;
                        }
                    }
                    if (Naruto.estVivant == true && Shuriken2.estVivant == true || Shuriken3.estVivant == true)
                    {
                        if (Naruto.GetRect().Intersects(Shuriken2.GetRect()) || Naruto.GetRect().Intersects(Shuriken3.GetRect()))
                        {
                            Naruto.estVivant = false;
                            Shuriken2.estVivant = false;
                            Shuriken3.estVivant = false;
                            dead -= 1;
                        }
                    }

                    if (Shuriken.estVivant == true && Madara[i].estVivant == true && Naruto.estVivant == true)
                    {

                        if (Madara[i].GetRect().Intersects(Shuriken.GetRect()))
                        {
                            Madara[i].estVivant = false;
                            Shuriken.estVivant = false;
                            Shuriken3.estVivant = false;
                            Shuriken2.estVivant = false;
                            kills += 1;

                            if (Madara[i].estVivant == false)
                            {
                                Madara[i].estVivant = true;
                                Madara[i].position.X = 1600;
                                Madara[i].position.Y = random.Next();
                                Madara[i].vitesse.X = random.Next(3, 9);
                                Madara[i].vitesse.Y = random.Next(3, 9);
                            }
                        }
                    }

                    if (Kunai.estVivant == true && Naruto.estVivant == true && Madara[i].estVivant == true)
                    {
                        if (Madara[i].GetRect().Intersects(Kunai.GetRect()))
                        {
                            Madara[i].estVivant = false;
                            Kunai.estVivant = false;
                            Shuriken3.estVivant = false;
                            Shuriken2.estVivant = false;
                            kills += 2;

                            if (Madara[i].estVivant == false)
                            {
                                Madara[i].estVivant = true;
                                Madara[i].position.X = 1600;
                                Madara[i].position.Y = random.Next();
                                Madara[i].vitesse.X = random.Next(3, 9);
                                Madara[i].vitesse.Y = random.Next(3, 9);
                            }
                        }
                    }
                    if (Kunai.estVivant == true || Shuriken.estVivant == true && Shuriken2.estVivant == true || Shuriken3.estVivant == true)
                    {
                        if(Kunai.GetRect().Intersects(Shuriken3.GetRect()))
                        {
                            Kunai.estVivant = false;
                            Shuriken3.estVivant = false;
                        }

                        if (Kunai.GetRect().Intersects(Shuriken2.GetRect()))
                        {
                            Kunai.estVivant = false;
                            Shuriken2.estVivant = false;
                        }

                        if (Shuriken.GetRect().Intersects(Shuriken2.GetRect()))
                        {
                            Shuriken.estVivant = false;
                            Shuriken2.estVivant = false;
                        }

                        if (Shuriken.GetRect().Intersects(Shuriken3.GetRect()))
                        {
                            Shuriken.estVivant = false;
                            Shuriken3.estVivant = false;
                        }
                    }
                }

                if (dead > 0)
                {
                    if (Naruto.estVivant == false)
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.Back))
                        {
                            Naruto.estVivant = true;
                            Naruto.position.X = 0;
                            Naruto.position.Y = 0;

                        }
                    }
                }
                if (projectiletirer == i && Shuriken2.estVivant == false && Shuriken3.estVivant == false)
                {
                    if (Madara[i].estVivant == true)
                    {
                        if (Madara[i].position.X > Naruto.position.X)
                        {
                            Shuriken2.position.X = Madara[i].position.X + 50;
                            Shuriken2.position.Y = Madara[i].position.Y + 100;
                            Shuriken2.estVivant = true;
                        }

                        else if (Madara[i].position.X < Naruto.position.X)
                        {
                            Shuriken3.position.X = Madara[i].position.X + 50;
                            Shuriken3.position.Y = Madara[i].position.Y + 50;
                            Shuriken3.estVivant = true;
                        }
                    }
                }
            }
        }

        protected void UpdateProjectile()
        {

            Shuriken.position.X += 40;

            Kunai.position.X += 40;

            Shuriken2.position.X -= 20;

            Shuriken3.position.X += 20;



            if (Shuriken.position.X >= 1920)
            {
                Shuriken.estVivant = false;
            }

            if (Shuriken2.position.X <= 0)
            {
                Shuriken2.estVivant = false;
            }

            if (Shuriken3.position.X >= 1920)
            {
                Shuriken3.estVivant = false;
            }

            if (Kunai.position.X >= 1920)
            {
                Kunai.estVivant = false;
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


            

            spriteBatch.Draw(background, new Rectangle(0, 0, graphics.GraphicsDevice.DisplayMode.Width, graphics.GraphicsDevice.DisplayMode.Height), Color.White);

            spriteBatch.DrawString(Text, "time:" + gameTime.TotalGameTime.TotalSeconds.ToString(), new Vector2(50, 0), Color.Black);

            spriteBatch.DrawString(score, " Score: " + kills.ToString(), new Vector2(1700, 0), Color.Black);

            spriteBatch.DrawString(Vie, " Vie: " + dead.ToString(), new Vector2(850, 0), Color.Black);

            if (Naruto.estVivant == true)
            {
                spriteBatch.Draw(Naruto.sprite, Naruto.position, Color.White);
            }
            
                for (int i = 0; i < Madara.Length; i++)
                {
                    if (Madara[i].position.X > Naruto.position.X)
                    {
                        if (Madara[i].estVivant == true)
                        {
                            spriteBatch.Draw(Madara[i].sprite, Madara[i].position);
                        }
                    }

                    if (Madara[i].position.X < Naruto.position.X)
                    {
                        if (Madara[i].estVivant == true)
                        {
                            spriteBatch.Draw(Madara[i].sprite, Madara[i].position, effects: SpriteEffects.FlipHorizontally);
                        }
                    }
                }

            if (Shuriken.estVivant == true)
            {
                spriteBatch.Draw(Shuriken.sprite, Shuriken.position, origin: new Vector2(37/2, 38/2), rotation:rotate /3);
            }

            if (Shuriken2.estVivant == true)
            {
                spriteBatch.Draw(Shuriken2.sprite, Shuriken2.position, origin: new Vector2(37/2, 38/2), rotation: rotate1 /3);
            }

            if (Shuriken3.estVivant == true)
            {
                spriteBatch.Draw(Shuriken3.sprite, Shuriken3.position, origin: new Vector2(37/2, 38/2), rotation: rotate /3);
            }

            if (Kunai.estVivant == true)
            {
                spriteBatch.Draw(Kunai.sprite, Kunai.position);
            }
            
            if (dead <= 0 && Naruto.estVivant == false)
            {
                spriteBatch.Draw(Mort, new Rectangle(0, 0, graphics.GraphicsDevice.DisplayMode.Width, graphics.GraphicsDevice.DisplayMode.Height), Color.White);

                spriteBatch.Draw(Gameover, new Rectangle(0, 0, graphics.GraphicsDevice.DisplayMode.Width, graphics.GraphicsDevice.DisplayMode.Height), Color.White);

                spriteBatch.DrawString(score, " Score: " + kill.ToString(), new Vector2(850, 600), Color.White);
            }

            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
