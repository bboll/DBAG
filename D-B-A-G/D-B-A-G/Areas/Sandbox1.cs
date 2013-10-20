//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using D_B_A_G.Virtual;
using D_B_A_G.Characters;
using D_B_A_G.MapObjects;
using D_B_A_G.Weapons;

namespace D_B_A_G.Areas
{
    public class Sandbox1 : AreaClass
    {
        //Dynamic array of projectiles
        public Projectile[] projectiles;
        public int numProjectiles = 0;

        //Objects specifid to the level
        public CollisionObject OtherNinja;
        public CollisionObject PushMe;
        public CollisionObject Demon;

        //Constructor from the higher class
        public Sandbox1(Texture2D texture, Game1 root)
        {
            mapObject = new Map(texture);
            ROOT = root;
        }

        //Init the area
        public new void initialize()
        {
            //Call the parent init
            base.initialize();
            loadContent();

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
            //Create a zombie
            OtherNinja = new CollisionObject(ROOT.Content.Load<Texture2D>("Characters/demon"), 300, 550, true, true, 3.0f);
            OtherNinja.health = 100;

            //Create a deamon
            Demon = new CollisionObject(ROOT.Content.Load<Texture2D>("Characters/demon"), 300, 550, true, true);
            Demon.health = 20;

            PushMe = new CollisionObject(ROOT.Content.Load<Texture2D>("Objects/block"), 500, 420);
            mapObject = new Map(ROOT.Content.Load<Texture2D>("Terrain/TestMap"), 0, 0);
        }

        //Update
        public new void update()
        {
            OtherNinja.update();
            Demon.update();

            //Update position before collision checking to prevent sticking
            ROOT.Hero.update();

            //Update arrows
            for (int i = 0; i < numProjectiles; ++i)
            {
                projectiles[i].update();
                if (mapObject.collidesWith(projectiles[i]) || projectiles[i].pos.X < -60 || projectiles[i].pos.X > 1060 || projectiles[i].pos.Y < -60 || projectiles[i].pos.Y > 860)
                    projectiles[i].isAlive = false;
                if (projectiles[i].collidesWith(OtherNinja)) 
                {
                    projectiles[i].update();
                    OtherNinja.resolveCollision(projectiles[i]); 
                    projectiles[i].isAlive = false; 
                    OtherNinja.dealtdamage(5); 
                }
                if (!projectiles[i].isAlive)
                {
                    for (int j = i+1; j < numProjectiles; ++j)
                        projectiles[j-1] = projectiles[j];
                    --i;
                    --numProjectiles;
                }
            }

            if (ROOT.Hero.SpriteObj.canFireArrow && Keyboard.GetState().IsKeyDown(Keys.RightShift))
            {
                numProjectiles += 1;
                ROOT.Hero.SpriteObj.canFireArrow = false;
                Vector2 tempOffset = new Vector2(0, 0);
                Projectile[] temp = new Projectile[numProjectiles];
                for (int i = 0; i < numProjectiles - 1; ++i)
                    temp[i] = projectiles[i];

                int tempFace = ROOT.Hero.SpriteObj.facing;
                if(tempFace == 0) //UP
                {
                    temp[numProjectiles - 1] = new Projectile(ROOT.Content.Load<Texture2D>("Attacks/Weapons/Arrow_Up"), 10, 68, new Vector2(0, -8));
                    tempOffset = new Vector2(0, -20);
                }
                else if (tempFace == 1) //Left
                {
                    temp[numProjectiles - 1] = new Projectile(ROOT.Content.Load<Texture2D>("Attacks/Weapons/Arrow_Left"), 68, 10, new Vector2(-8, 0));
                    tempOffset = new Vector2(-30, 0);
                }
                else if(tempFace == 2) //DOWN
                {
                    temp[numProjectiles - 1] = new Projectile(ROOT.Content.Load<Texture2D>("Attacks/Weapons/Arrow_Down"), 10, 68, new Vector2(0, 8));
                    tempOffset = new Vector2(0, 20);
                }
                else if(tempFace == 3) //RIGHT
                {
                    temp[numProjectiles - 1] = new Projectile(ROOT.Content.Load<Texture2D>("Attacks/Weapons/Arrow_Right"), 68, 10, new Vector2(8, 0));
                    tempOffset = new Vector2(30, 0);
                }
                temp[numProjectiles - 1].pos = ROOT.Hero.pos + tempOffset;
                projectiles = temp;
            }


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
        public new void draw(SpriteBatch sprites)
        {
            //Call the parent draw
            base.draw(sprites);

            //Draw arrows
            for (int i = 0; i < numProjectiles; ++i)
                projectiles[i].draw(sprites);

            ROOT.Hero.draw(sprites);
            if (Demon.isAlive) Demon.draw(sprites);
            if(OtherNinja.isAlive) OtherNinja.draw(sprites);
            PushMe.draw(sprites);
        }
    }
}
