using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using D_B_A_G.Abstract;

namespace D_B_A_G.Characters
{
    class Player : CollisionObject
    {
        //Constructors
        public Player(Texture2D Sprite, int X = 0, int Y = 0, bool Solid = true)
        {
            //Set basic elements
            height = Sprite.Bounds.Height;
            width = Sprite.Bounds.Width;
            pos.X = X;
            pos.Y = Y;
            velocity.X = 0;
            velocity.Y = 0;
            isSolid = Solid;

            //Set the sprite
            sprite = Sprite;

            //Set the center
            centerOffset.X = (-1) * (width / 2);
            centerOffset.Y = (-1) * (height / 2);
        }

        public void update()
        {
            //Call the generic update
            base.update();

            //Change the hero's direction off keyboard input
            if (Keyboard.GetState().IsKeyDown(Keys.Left)) velocity.X = -1;
            if (Keyboard.GetState().IsKeyDown(Keys.Right)) velocity.X = 1;
            if (Keyboard.GetState().IsKeyDown(Keys.Up)) velocity.Y = -1;
            if (Keyboard.GetState().IsKeyDown(Keys.Down)) velocity.Y = 1;

            //Stop moving after keys are released
            if (Keyboard.GetState().IsKeyUp(Keys.Left) && Keyboard.GetState().IsKeyUp(Keys.Right)) velocity.X = 0;
            if (Keyboard.GetState().IsKeyUp(Keys.Up) && Keyboard.GetState().IsKeyUp(Keys.Down)) velocity.Y = 0;

            //Make the movement a constant speed (even on diagonals)
            if (velocity.X != 0 || velocity.Y != 0)
            {
                velocity.Normalize();
                velocity *= 4;
            }
        }
    }
}
