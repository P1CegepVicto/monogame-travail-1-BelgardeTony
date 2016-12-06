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
        GameObject enemi;
        GameObject balles;
        //GameObject[] enemi = new GameObject[1];
        //GameObject[] balles = new GameObject[6];



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


            if (GameState == true && start == false)
            {
                Song song = Content.Load<Song>("sounds//GameSong");
                MediaPlayer.Play(song);

                soldat = new GameObject();
                soldat.estVivant = true;
                soldat.sprite = Content.Load<Texture2D>("soldat.png");
                soldat.position.X = 0;
                soldat.position.Y = 200;

                enemi = new GameObject();
                enemi.estVivant = true;
                enemi.sprite = Content.Load<Texture2D>("enemie.png");
                enemi.position.X = 1000;
                enemi.position.Y = 200;

                balles = new GameObject();
                balles.estVivant = true;
                balles.sprite = Content.Load<Texture2D>("balle.png");
                balles.position.X = 600;
                balles.position.Y = 200;

                background = new GameObject();
                background.estVivant = true;
                background.sprite = Content.Load<Texture2D>("background.jpg");
                background.position.X = 0;
                background.position.Y = 0;
                background.vitesse.X = -5;

                background2 = new GameObject();
                background2.sprite = Content.Load<Texture2D>("background.jpg");
                background2.position.X = 1920;
                background2.position.Y = 0;
                background2.vitesse.X = -5;

                

                //texte = Content.Load<SpriteFont>("Font");
                //score = Content.Load<SpriteFont>("Font");
                //vie = Content.Load<SpriteFont>("Font");
                //balle = Content.Load<SpriteFont>("Font");
            }
            

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

            //if (background2.position.X == fenetre.Left)
            //{
            //    background.position.X = background2.position.X + background2.sprite.Width;
            //}
            //if (background.position.X == fenetre.Left)
            //{
            //    background2.position.X = background.position.X + background.sprite.Width;
            //}


            // TODO: Add your update logic here
            UpdateSoldat();
            UpdateEnemi();
            UpdateProjectile();
            //UpdateBackground();
            //UpdateBackground2();

            base.Update(gameTime);
        }

        //protected void UpdateBackground2()
        //{
        //    background2.position += background2.vitesse;
        //}

        //protected void UpdateBackground()
        //{
        //    background.position += background.vitesse;
        //}

        protected void UpdateSoldat()
        {

        }

        protected void UpdateEnemi()
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
            // TODO: Add your drawing code here

            
                spriteBatch.Draw(black, new Rectangle(0, 0 , graphics.GraphicsDevice.DisplayMode.Width,graphics.GraphicsDevice.DisplayMode.Height),Color.White);
            spriteBatch.Draw(starts.sprite, starts.position, Color.White);
            //if (GameState == true && start == false)
            //{
            //    spriteBatch.Draw(background.sprite, background.position, Color.White);
            //    spriteBatch.Draw(background2.sprite, background2.position, effects: SpriteEffects.FlipHorizontally);

            //    spriteBatch.Draw(soldat.sprite, soldat.position, Color.White);
            //    spriteBatch.Draw(enemi.sprite, enemi.position, Color.White);
            //    spriteBatch.Draw(balles.sprite, balles.position, Color.White);

            //}




            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
