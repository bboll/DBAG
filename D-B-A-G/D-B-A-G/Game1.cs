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
            SandboxLevel1 = new Sandbox1(this.Content.Load<Texture2D>("Terrain/TestMap"), this);

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
            Texture2D[] tempArray = new Texture2D[2];
            tempArray[0] = this.Content.Load<Texture2D>("Attacks/male_slash");
            tempArray[1] = this.Content.Load<Texture2D>("Attacks/male_bow");
            Hero = new Player(this.Content.Load<Texture2D>("Characters/male_walkcycle"), tempArray, 2, 700, 300, true, true);

            //Add clothing
            Hero.addTexture(this.Content.Load<Texture2D>("Clothing/HEAD_robe_hood"));
            Hero.addTexture(this.Content.Load<Texture2D>("Clothing/TORSO_leather_armor_torso"));
            Hero.addTexture(this.Content.Load<Texture2D>("Clothing/TORSO_leather_armor_bracers"));
            Hero.addTexture(this.Content.Load<Texture2D>("Clothing/BELT_Leather"));
            //Hero.addTexture(this.Content.Load<Texture2D>("Clothing/male_pants"));
            Hero.addTexture(this.Content.Load<Texture2D>("Clothing/TORSO_leather_armor_shoulders"));
            Hero.addTexture(this.Content.Load<Texture2D>("Clothing/FEET_shoes_brown"));

            //Add dagger attack clothing
            Hero.addAttackTexture(this.Content.Load<Texture2D>("Attacks/Clothing/Dagger/HEAD_robe_hood"), 0);
            Hero.addAttackTexture(this.Content.Load<Texture2D>("Attacks/Clothing/Dagger/TORSO_leather_armor_torso"), 0);
            Hero.addAttackTexture(this.Content.Load<Texture2D>("Attacks/Clothing/Dagger/TORSO_leather_armor_bracers"), 0);
            Hero.addAttackTexture(this.Content.Load<Texture2D>("Attacks/Clothing/Dagger/BELT_Leather"), 0);
            //Hero.addAttackTexture(this.Content.Load<Texture2D>("Attacks/Clothing/Dagger/male_slash_pants"), 0);
            Hero.addAttackTexture(this.Content.Load<Texture2D>("Attacks/Clothing/Dagger/TORSO_leather_armor_shoulders"), 0);
            Hero.addAttackTexture(this.Content.Load<Texture2D>("Attacks/Clothing/Dagger/FEET_shoes_brown"), 0);
            Hero.addAttackTexture(this.Content.Load<Texture2D>("Attacks/Weapons/WEAPON_dagger"), 0);

            //Add bow attack clothing
            Hero.addAttackTexture(this.Content.Load<Texture2D>("Attacks/Clothing/Bow/HEAD_robe_hood"), 1);
            Hero.addAttackTexture(this.Content.Load<Texture2D>("Attacks/Clothing/Bow/TORSO_leather_armor_torso"), 1);
            Hero.addAttackTexture(this.Content.Load<Texture2D>("Attacks/Clothing/Bow/TORSO_leather_armor_bracers"), 1);
            Hero.addAttackTexture(this.Content.Load<Texture2D>("Attacks/Clothing/Bow/BELT_Leather"), 1);
            //Hero.addAttackTexture(this.Content.Load<Texture2D>("Attacks/Clothing/Bow/male_slash_pants"), 1);
            Hero.addAttackTexture(this.Content.Load<Texture2D>("Attacks/Clothing/Bow/TORSO_leather_armor_shoulders"), 1);
            Hero.addAttackTexture(this.Content.Load<Texture2D>("Attacks/Clothing/Bow/FEET_shoes_brown"), 1);
            Hero.addAttackTexture(this.Content.Load<Texture2D>("Attacks/Weapons/WEAPON_bow"), 1);


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
