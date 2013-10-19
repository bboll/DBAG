using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using D_B_A_G.Characters;
using D_B_A_G.MapObjects;

namespace D_B_A_G.Areas
{
    public class Sandbox1 : AreaClass
    {
        //Objects specifid to the level
        public CollisionObject OtherNinja;
        public CollisionObject PushMe;

        //Constructor from the higher class
        public Sandbox1(Texture2D texture, Game1 root)
        {
            mapObject = new Map(texture);
            ROOT = root;
        }

        //Init the area
        public void initialize()
        {
            //Call the parent init
            base.initialize();

            //Make the character stay on the screen
            ROOT.Hero.restrictDomain = true;
            ROOT.Hero.domain = new Vector4(0, 0, 1000, 800);
            PushMe.restrictDomain = true;
            PushMe.domain = new Vector4(0, 0, 1000, 800);
            OtherNinja.velocity.X = -1;

            //Make the map collision
            mapObject.Walls = new CollisionObject[4];
            mapObject.numWalls = 4;
            mapObject.Walls[0] = new CollisionObject(550, 25, 140, 395);
            mapObject.Walls[1] = new CollisionObject(38, 380, 325, 122);
            mapObject.Walls[2] = new CollisionObject(40, 475, 670, 355);
            mapObject.Walls[3] = new CollisionObject(200, 320, 850, 720);
        }

        //Load content for the level
        public void loadContent()
        {
            OtherNinja = new CollisionObject(ROOT.Content.Load<Texture2D>("ninja"), 300, 550);
            PushMe = new CollisionObject(ROOT.Content.Load<Texture2D>("block"), 500, 420);
            mapObject = new Map(ROOT.Content.Load<Texture2D>("TestMap"), 0, 0);
        }

        //Update
        public void update()
        {
            OtherNinja.update();

            //Update position before collision checking to prevent sticking
            ROOT.Hero.update();

            //Check collisions (should be easier later)
            if (ROOT.Hero.collidesWith(OtherNinja)) OtherNinja.resolveCollision(ROOT.Hero);
            if (ROOT.Hero.collidesWith(PushMe)) PushMe.resolveCollision(ROOT.Hero);
            if (PushMe.collidesWith(OtherNinja)) PushMe.resolveCollision(OtherNinja);

            //Update the block's bounds
            PushMe.update();

            //Map collisions
            mapObject.resolveMap(ref ROOT.Hero);
            mapObject.resolveMap(ref PushMe);

            //NPC Ninja map collision
            if ((OtherNinja.pos.X - OtherNinja.width / 2 < 0) || (mapObject.collidesWith(OtherNinja) && OtherNinja.velocity.X < 0))
            {
                mapObject.resolveMap(ref OtherNinja);
                OtherNinja.pos.X += 1;
                OtherNinja.velocity.Y = 1;
                if (OtherNinja.pos.Y > 400) OtherNinja.velocity.Y = -1;
                OtherNinja.velocity.X = 0;
            }
            else if ((OtherNinja.pos.X + OtherNinja.width / 2 > 1000) || (mapObject.collidesWith(OtherNinja) && OtherNinja.velocity.X > 0))
            {
                mapObject.resolveMap(ref OtherNinja);
                OtherNinja.pos.X -= 1;
                OtherNinja.velocity.Y = 1;
                if (OtherNinja.pos.Y > 400) OtherNinja.velocity.Y = -1;
                OtherNinja.velocity.X = 0;
            }
            if ((OtherNinja.pos.Y - OtherNinja.height / 2 < 0) || (mapObject.collidesWith(OtherNinja) && OtherNinja.velocity.Y < 0))
            {
                mapObject.resolveMap(ref OtherNinja);
                OtherNinja.pos.Y += 1;
                OtherNinja.velocity.X = 1;
                if (OtherNinja.pos.X > 500) OtherNinja.velocity.X = -1;
                OtherNinja.velocity.Y = 0;
            }
            else if ((OtherNinja.pos.Y + OtherNinja.height / 2 > 800) || (mapObject.collidesWith(OtherNinja) && OtherNinja.velocity.Y > 0))
            {
                mapObject.resolveMap(ref OtherNinja);
                OtherNinja.pos.Y -= 1;
                OtherNinja.velocity.X = 1;
                if (OtherNinja.pos.X > 500) OtherNinja.velocity.X = -1;
                OtherNinja.velocity.Y = 0;
            }

            //If all else fails, move the hero off the block or the ninja off it
            if (ROOT.Hero.collidesWith(OtherNinja) && (ROOT.Hero.pos.X + ROOT.Hero.width / 2 > 1000 || ROOT.Hero.pos.X - ROOT.Hero.width / 2 < 0)) { OtherNinja.resolveCollision(ROOT.Hero); OtherNinja.velocity *= -1; }
            if (ROOT.Hero.collidesWith(PushMe) && (ROOT.Hero.pos.X + ROOT.Hero.width / 2 > 1000 || ROOT.Hero.pos.X - ROOT.Hero.width / 2 < 0)) PushMe.resolveCollision(ROOT.Hero);
            if (PushMe.collidesWith(OtherNinja)) { OtherNinja.resolveCollision(PushMe); OtherNinja.velocity *= -1; }
            else if (ROOT.Hero.collidesWith(PushMe) && ROOT.Hero.collidesWith(OtherNinja)) { OtherNinja.resolveCollision(ROOT.Hero); OtherNinja.velocity *= -1; }
            if (ROOT.Hero.collidesWith(PushMe)) ROOT.Hero.resolveCollision(PushMe);
        }

        //Draw
        public void draw(SpriteBatch sprites)
        {
            //Call the parent draw
            base.draw(sprites);

            ROOT.Hero.draw(sprites);
            OtherNinja.draw(sprites);
            PushMe.draw(sprites);
        }
    }
}
