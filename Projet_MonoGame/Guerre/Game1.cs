using Guerre;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace Projet_3_Guerre
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
        Texture2D mort;
        Texture2D GameOver;
        Random random = new Random();
        SpriteFont texte;
        SpriteFont point;
        SpriteFont vie;
        SpriteFont projectile;

        int scores = 0;
        int lifes = 10;
        int munition = 6;

        GameObject soldat;
        GameObject[] enemie = new GameObject[1];
        GameObject[] balle = new GameObject[6];
        GameObject balle2;

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

            Song song = Content.Load<Song>("sounds//01 - Shepherd Of Fire");
            MediaPlayer.Play(song);

            soldat = new GameObject();
            soldat.estVivant = false;
            soldat.sprite = Content.Load<Texture2D>("soldat.png");


            balle2 = new GameObject();
            balle2.estVivant = false;
            balle2.sprite = Content.Load<Texture2D>("balle.png");

            background = Content.Load<Texture2D>("background.jpg");
            mort = Content.Load<Texture2D>("Dead.jpg");
            GameOver = Content.Load<Texture2D>("Gameover.png");

            texte = Content.Load<SpriteFont>("Font");
            point = Content.Load<SpriteFont>("Font");
            vie = Content.Load<SpriteFont>("Font");
            projectile = Content.Load<SpriteFont>("Font");


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

            MoveEnemie();
            MoveSoldat();
            UpdateProjectile();
            base.Update(gameTime);
        }

        protected void MoveEnemie()
        {

        }


        protected void MoveSoldat()
        {

        }


        protected void UpdateProjectile()
        {
            //balle[i].position.X += 40; 
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            spriteBatch.DrawString(texte, "time:" + gameTime.TotalGameTime.TotalSeconds.ToString(), new Vector2(50, 0), Color.Black);


            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
