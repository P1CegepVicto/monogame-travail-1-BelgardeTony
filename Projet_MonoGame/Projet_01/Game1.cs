using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Projet_01
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle fenetre;
        Texture2D backgroundTexture;

        GameObject Hillary;
        GameObject Trump;
        GameObject brique;

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
            fenetre = new Rectangle(0, 0,graphics.GraphicsDevice.DisplayMode.Width,graphics.GraphicsDevice.DisplayMode.Height);
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
            Trump = new GameObject();
            Trump.estVivant = true;
            
            Hillary = new GameObject();
            Hillary.estVivant = true;

            brique = new GameObject();
            brique.estVivant = false;

            //***********************************************************

            backgroundTexture = Content.Load<Texture2D>("backgroundTexture.png");

            //Limite à gauche de X = -10
            //Limite à droite de X = 1740
            //Limite en hauteur de Y = -15
            //Limite en bas de Y = 885 
            Trump.position.X = 820;
            Trump.position.Y = 400;
            Trump.sprite = Content.Load<Texture2D>("Trump.png");

            brique.sprite = Content.Load<Texture2D>("brique.png");

            Hillary.position.X = 820;
            Hillary.position.Y = 15;
            Hillary.sprite = Content.Load<Texture2D>("Hillary.png");
            Hillary.vitesse.X = 3;

            
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

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                Trump.position.X -= 5;
                if (Trump.position.X < -10)
                {
                    Trump.position.X = 1740;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                Trump.position.X += 5;
                if (Trump.position.X > 1740)
                {
                    Trump.position.X = -10;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                Trump.position.Y += 5;
                if (Trump.position.Y > 885)
                {
                    Trump.position.Y = -15;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                Trump.position.Y -= 5;
                if (Trump.position.Y < -15)
                {
                    Trump.position.Y = 885;
                }

            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                brique.estVivant = true;
                brique.position.X = Trump.position.X + 45;
                brique.position.Y = Trump.position.Y -100;    
            }
                //*****************Hillary controle*************
            //if (Keyboard.GetState().IsKeyDown(Keys.Left))
            //{
            //    Hillary.position.X -= 5;
            //}

            //if (Keyboard.GetState().IsKeyDown(Keys.Right))
            //{
            //    Hillary.position.X += 5;
            //}

            //if (Keyboard.GetState().IsKeyDown(Keys.Down))
            //{
            //    Hillary.position.Y += 5;
            //}

            //if (Keyboard.GetState().IsKeyDown(Keys.Up))
            //{
            //    Hillary.position.Y -= 5;
            //}

            // TODO: Add your update logic here
            UpdateHillary();
            Updatebrique();
            base.Update(gameTime);
        }
        protected void Updatebrique()
        {
            brique.position.Y -= 6; 
        }

        protected void UpdateHillary()
        {
            

            if (Hillary.position.X < fenetre.Left)
            {
                Hillary.vitesse.X = 3;
            }
            
            if(Hillary.position.X + Hillary.sprite.Bounds.Width > fenetre.Right)
            {
                Hillary.vitesse.X = -3;
            }
            
            Hillary.position += Hillary.vitesse;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, graphics.GraphicsDevice.DisplayMode.Width, graphics.GraphicsDevice.DisplayMode.Height), Color.White);

            spriteBatch.Draw(Trump.sprite, Trump.position, Color.White);

            spriteBatch.Draw(Hillary.sprite, Hillary.position, Color.IndianRed);
            if (brique.estVivant == true)
            {
                spriteBatch.Draw(brique.sprite, brique.position, Color.White);
            }




            spriteBatch.End();


            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
