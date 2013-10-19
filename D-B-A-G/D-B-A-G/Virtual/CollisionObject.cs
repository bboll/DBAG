using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using D_B_A_G.Characters;

namespace D_B_A_G.Virtual
{
    public class CollisionObject
    {
        public int height;
        public int width;
        public Vector2 pos;
        public Vector2 velocity;
        public bool isSolid;
        public bool isAnimated;
        public Texture2D sprite;
        public Vector4 domain;
        public bool restrictDomain;
        public SpriteSheet SpriteObj;

        //Helpers
        protected Vector2 centerOffset;
       
        //==============================================================================================================Constructors
        public CollisionObject()
        {
            height = 0;
            width = 0;
            pos.X = 0;
            pos.Y = 0;
            isSolid = true;
            isAnimated = false;
        }
        public CollisionObject(int Height, int Width, int X = 0, int Y = 0, bool Solid = true, bool animated = false)
        {
            //Set basic elements
            height = Height;
            width = Width;
            pos.X = X;
            pos.Y = Y;
            isSolid = Solid;
            isAnimated = animated;

            //Construct Sprite object
            SpriteObj = new SpriteSheet(sprite, width, height);

            if (isAnimated)
            {
                height = SpriteObj.m_spriteHeight;
                width = SpriteObj.m_spriteWidth;
            }

            //Set domain restrictions
            restrictDomain = false;

            //Set the center
            centerOffset.X = (-1) * (width / 2);
            centerOffset.Y = (-1) * (height / 2);

        }
        public CollisionObject(Texture2D Sprite, int X = 0, int Y = 0, bool Solid = true, bool animated = false)
        {
            //Set basic elements
            height = Sprite.Bounds.Height;
            width = Sprite.Bounds.Width;
            pos.X = X;
            pos.Y = Y;
            isSolid = Solid;
            isAnimated = animated;

            //Set the sprite
            sprite = Sprite;

            //Construct Sprite object
            SpriteObj = new SpriteSheet(sprite, width, height);

            if (isAnimated)
            {
                height = SpriteObj.m_spriteHeight;
                width = SpriteObj.m_spriteWidth;
            }

            //Set the center
            centerOffset.X = (-1) * (width / 2);
            centerOffset.Y = (-1) * (height / 2);

        }

        //===============================================================================================================Collision functions
        public bool collidesWith(CollisionObject otherOBJ)
        {
            //Check the left side
            if ((((pos.X + (width / 2) > otherOBJ.pos.X - (otherOBJ.width / 2)) && (pos.X - (width / 2) < otherOBJ.pos.X + (otherOBJ.width / 2)))
            
                //Check the right side
                || ((pos.X - (width / 2) < otherOBJ.pos.X + (otherOBJ.width / 2)) && (pos.X + (width / 2) > otherOBJ.pos.X - (otherOBJ.width / 2))))

                //Check the top
                && (((pos.Y + (height / 2) > otherOBJ.pos.Y - (otherOBJ.height / 2)) && (pos.Y - (height / 2) < otherOBJ.pos.Y + (otherOBJ.height / 2)))

                //Check the bottom
                || ((pos.Y - (height / 2) < otherOBJ.pos.Y + (otherOBJ.height / 2)) && (pos.Y + (height / 2) > otherOBJ.pos.Y - (otherOBJ.height / 2)))))
                return true;

            //If no sides touch, there was no collision
            return false;
        }
        public void resolveCollision(CollisionObject otherOBJ)
        {
            //Check what the smallest resolution would be (X or Y)
            int diff_Left = (int)((otherOBJ.pos.X + (otherOBJ.width / 2)) - (pos.X - (width / 2)));
            if (diff_Left < 0) diff_Left *= -1;
            int diff_Right = (int)((pos.X + (width / 2)) - (otherOBJ.pos.X - (otherOBJ.width / 2)));
            if (diff_Right < 0) diff_Right *= -1;
            int diff_Top = (int)((pos.Y + (height / 2)) - (otherOBJ.pos.Y - (otherOBJ.height / 2)));
            if (diff_Top < 0) diff_Top *= -1;
            int diff_Bottom = (int)((otherOBJ.pos.Y + (otherOBJ.height / 2)) - (pos.Y - (height / 2)));
            if (diff_Bottom < 0) diff_Bottom *= -1;

            //Resolve the right side
            if (diff_Right < diff_Left && diff_Right < diff_Top && diff_Right < diff_Bottom)
            {
                if ((pos.X + (width / 2) > otherOBJ.pos.X - (otherOBJ.width / 2)) && (pos.X - (width / 2) < otherOBJ.pos.X + (otherOBJ.width / 2)))
                    pos.X = pos.X - diff_Right;
            }

            //Resolve the left side
            else if (diff_Left < diff_Right && diff_Left < diff_Top && diff_Left < diff_Bottom)
            {
                if ((pos.X - (width / 2) < otherOBJ.pos.X + (otherOBJ.width / 2)) && (pos.X + (width / 2) > otherOBJ.pos.X - (otherOBJ.width / 2)))
                    pos.X = pos.X + diff_Left;
            }

            //Resolve the top
            else if (diff_Top < diff_Bottom && diff_Top < diff_Left && diff_Top < diff_Right)
            {
                if ((pos.Y + (height / 2) > otherOBJ.pos.Y - (otherOBJ.height / 2)) && (pos.Y - (height / 2) < otherOBJ.pos.Y + (otherOBJ.height / 2)))
                    pos.Y = pos.Y - diff_Top;
            }

            //Resolve the bottom
            else if (diff_Bottom < diff_Top && diff_Bottom < diff_Left && diff_Bottom < diff_Right)
            {
                if ((pos.Y - (height / 2) < otherOBJ.pos.Y + (otherOBJ.height / 2)) && (pos.Y + (height / 2) > otherOBJ.pos.Y - (otherOBJ.height / 2)))
                    pos.Y = pos.Y + diff_Bottom;
            }
        }

        //Update
        public void update()
        {
            pos.X += velocity.X;
            pos.Y += velocity.Y;

            if (restrictDomain)
            {
                if (pos.X - width / 2 < domain.X) pos.X = domain.X + width / 2;
                if (pos.X + width / 2 > domain.Z) pos.X = domain.Z - width / 2;
                if (pos.Y - height / 2 < domain.Y) pos.Y = domain.Y + height / 2;
                if (pos.Y + height / 2 > domain.W) pos.Y = domain.W - height / 2;
            }
        }

        //==============================================================================================================Drawing/Animating functions
        public void draw(SpriteBatch drawHere)
        {
            //possible just call animate
            drawHere.End();
            drawHere.Begin();
            if(isAnimated) SpriteObj.animate(drawHere, sprite, velocity, pos + centerOffset);
            else drawHere.Draw(sprite, pos + centerOffset, Color.White);
            drawHere.End();
        }
        public void resize(int Width, int Height)
        {

        }
    }
}

