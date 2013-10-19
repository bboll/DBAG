#region Using Statements
using System;
using System.Collections.Generic;
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
        //CollisionObject OtherNinja;
        //CollisionObject PushMe;
        //Map testMap;

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
            //Make the character stay on the screen
            //Hero.restrictDomain = true;
            //Hero.domain = new Vector4(0, 0, 1000, 800);
            //PushMe.restrictDomain = true;
            //PushMe.domain = new Vector4(0, 0, 1000, 800);
            //OtherNinja.velocity.X = -1;

            //Make the map collision
            //testMap.Walls = new CollisionObject[4];
            //testMap.numWalls = 4;
            //testMap.Walls[0] = new CollisionObject(550, 25, 140, 395);
            //testMap.Walls[1] = new CollisionObject(38, 380, 325, 122);
            //testMap.Walls[2] = new CollisionObject(40, 475, 670, 355);
            //testMap.Walls[3] = new CollisionObject(200, 320, 850, 720);
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
            Hero = new Player(this.Content.Load<Texture2D>("female_walkcycle"), 700, 300, true, true);
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
