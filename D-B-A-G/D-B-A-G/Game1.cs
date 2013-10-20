#region Using Statements
//using System;
//using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

using D_B_A_G.Virtual;
using D_B_A_G.Characters;
using D_B_A_G.MapObjects;
using D_B_A_G.Areas;

namespace D_B_A_G
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        
        //Put "global" objects here
        public Player Hero;
        public Sandbox1 SandboxLevel1;

        public Game1()
            : base()
        {
            //Set up the content system (allows .png imports)
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "../../../Content";

            //Create the game starting points
            SandboxLevel1 = new Sandbox1(this.Content.Load<Texture2D>("TestMap"), this);

            //Set the screen size
            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 1000;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {  
            base.Initialize();
            SandboxLevel1.initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Hero = new Player(this.Content.Load<Texture2D>("male_walkcycle"), 700, 300, true, true);
            Hero.addTexture(this.Content.Load<Texture2D>("HEAD_robe_hood"));
            Hero.addTexture(this.Content.Load<Texture2D>("TORSO_leather_armor_torso"));
            Hero.addTexture(this.Content.Load<Texture2D>("BELT_Leather"));
            Hero.addTexture(this.Content.Load<Texture2D>("male_pants"));
            Hero.addTexture(this.Content.Load<Texture2D>("TORSO_leather_armor_shoulders"));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            //Exit on escape
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //Level 1
            SandboxLevel1.update();

            //Increment game timer
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            SandboxLevel1.draw(spriteBatch);
            Hero.draw(spriteBatch);
            
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
