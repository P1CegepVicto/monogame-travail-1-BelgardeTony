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

        GameObject Naruto;
        //GameObject[] Madara;
        GameObject Madara;
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

            //***********Besoin d'aide*************************************
            //Madara = new GameObject[12];
            //for (int i = 0; i < Madara.Length; i++)
            //{
            //    Madara[i].sprite = Content.Load<Texture2D>("Madara.png");
            //}
            //*************************************************************

            Kunai = new GameObject();
            Kunai.estVivant = false;
            Kunai.sprite = Content.Load<Texture2D>("Kunai.png");

            Shuriken = new GameObject();
            Shuriken.estVivant = false;
            Shuriken.sprite = Content.Load<Texture2D>("Shuriken.png");

            background = Content.Load<Texture2D>("Background.png");

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

            UpdateNaruto();
            UpdateMadara();
            UpdateProjectile();
            base.Update(gameTime);
        }

        protected void UpdateNaruto()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                Naruto.position.X -= 10;
                if (Naruto.position.X < 0)
                {
                    Naruto.position.X = 1720;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                Naruto.position.X += 10;
                if (Naruto.position.X > 1720)
                {
                    Naruto.position.X = 0;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                Naruto.position.Y += 10;
                if (Naruto.position.Y > 945)
                {
                    Naruto.position.Y = 0;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                Naruto.position.Y -= 10;
                if (Naruto.position.Y < 0)
                {
                    Naruto.position.Y = 945;
                }

            }
        }

        protected void UpdateMadara()
        {

        }

        protected void UpdateProjectile()
        {

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
            //spriteBatch.Draw(Madara.sprite, Madara.position, Color.White);




            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
