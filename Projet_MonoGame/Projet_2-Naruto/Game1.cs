using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        Random random = new Random();
        int rotate = 0;

        GameObject Naruto;
        GameObject[] Madara;
        GameObject Kunai;
        GameObject Shuriken;

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

            Naruto = new GameObject();
            Naruto.estVivant = true;
            Naruto.sprite = Content.Load<Texture2D>("Naruto.png");
            Naruto.position.X = 0;
            Naruto.position.Y = 0;


            Kunai = new GameObject();
            Kunai.estVivant = false;
            Kunai.sprite = Content.Load<Texture2D>("Kunai.png");

            Shuriken = new GameObject();
            Shuriken.estVivant = false;
            Shuriken.sprite = Content.Load<Texture2D>("Shuriken.png");

            background = Content.Load<Texture2D>("Background.png");

            Madara = new GameObject[5];

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

            // TODO: Add your update logic here

            rotate -= 1;
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
            if (Shuriken.estVivant == false && Kunai.estVivant == false)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    Shuriken.estVivant = true;
                    Shuriken.position.X = Naruto.position.X + 200 ;
                    Shuriken.position.Y = Naruto.position.Y + 50;
                }
            }

            if (Kunai.estVivant == false && Shuriken.estVivant == false)
            {
                if(Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    Kunai.estVivant = true;
                    Kunai.position.X = Naruto.position.X + 200;
                    Kunai.position.Y = Naruto.position.Y + 50;
                }
            }
        }

        protected void MoveMadara()
        {
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
            }
        }

        protected void UpdateProjectile()
        {
           
                Shuriken.position.X += 20;
           
                Kunai.position.X += 20;
            
                if (Shuriken.position.X >= 1920)
            {
                Shuriken.estVivant = false;
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
            spriteBatch.Draw(Naruto.sprite, Naruto.position, Color.White);
            for (int i = 0; i < Madara.Length; i++)
            {
                spriteBatch.Draw(Madara[i].sprite, Madara[i].position);
            }
            if (Shuriken.estVivant == true)
            {
                spriteBatch.Draw(Shuriken.sprite, Shuriken.position, origin: new Vector2(37/2, 38/2), rotation:rotate /3);
            }
            if (Kunai.estVivant == true)
            {
                spriteBatch.Draw(Kunai.sprite, Kunai.position);
            }



            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
