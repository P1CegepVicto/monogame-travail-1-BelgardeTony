using Microsoft.Xna.Framework;
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
        bool GameState;
        bool start;

        
        Texture2D black;
        Texture2D gameover;
        
        
        SpriteFont texte;
        SpriteFont score;
        SpriteFont vie;
        SpriteFont balle;

        int munitions = 6;
        int point = 0;
        int life = 3;
        int projectile;

        GameObject starts;
        GameObject background2;
        GameObject background;
        GameObject soldat;
        //GameObject enemie;
        GameObject balles;
        GameObject[] enemie = new GameObject[5];
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



                //texte = Content.Load<SpriteFont>("Font");
                //score = Content.Load<SpriteFont>("Font");
                //vie = Content.Load<SpriteFont>("Font");
                //balle = Content.Load<SpriteFont>("Font");
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
            UpdateEnemi();
            UpdateProjectile(gameTime);
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

        protected void UpdateEnemi()
        {
            
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
                    enemie[i].position.X = random.Next(500, fenetre.Right);
                    enemie[i].position.Y = random.Next();
                    enemie[i].vitesse.X = -10;
                    enemie[i].vitesse.Y = random.Next(10, 20);
                }
                if (balles.estVivant == false && soldat.estVivant == true && enemie[i].estVivant == true)
                {
                    if (enemie[i].estVivant == true && soldat.estVivant == true)
                    {

                        balles.position.X = enemie[i].position.X + 50;
                        balles.position.Y = enemie[i].position.Y + 100;
                        balles.estVivant = true;
                    }
                }
            }
        }

        protected void UpdateProjectile(GameTime gameTime)
        {
            
            if (soldat.estVivant == true)
            {
                for (int i = 0; i < balles2.Length; i++)
                {
                    balles2[i].position.X += 30;
                    if (balles2[i].estVivant == false)
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.Space))
                        {
                            balles2[i].estVivant = true;
                            balles2[i].position.X = soldat.position.X + 150;
                            balles2[i].position.Y = soldat.position.Y + 50;
                            munitions = munitions - 1;
                        }
                    }
                    if (balles2[i].position.X == fenetre.Right)
                    {
                        balles2[i].estVivant = false;
                    }
                }

                balles.position.X -= 20;
                if (balles.position.X < fenetre.Left)
                {
                    balles.estVivant = false;
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


            //    spriteBatch.Draw(black, new Rectangle(0, 0 , graphics.GraphicsDevice.DisplayMode.Width,graphics.GraphicsDevice.DisplayMode.Height),Color.White);
            //spriteBatch.Draw(starts.sprite, starts.position, Color.White);
            //if (GameState == true && start == false)
            //{
            spriteBatch.Draw(background.sprite, background.position, Color.White);
            spriteBatch.Draw(background2.sprite, background2.position, effects: SpriteEffects.FlipHorizontally);

            spriteBatch.Draw(soldat.sprite, soldat.position);
            
            spriteBatch.Draw(balles.sprite, balles.position);

            for (int i = 0; i < enemie.Length; i++)
            {
                spriteBatch.Draw(enemie[i].sprite, enemie[i].position);
            }

            for (int i = 0; i < balles2.Length; i++)
            {
                if (balles2[i].estVivant == true)
                {
                    spriteBatch.Draw(balles2[i].sprite, balles2[i].position, effects: SpriteEffects.FlipHorizontally);
                }
           }
            

            //}




            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
