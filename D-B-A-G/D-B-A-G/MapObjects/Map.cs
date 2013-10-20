//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using D_B_A_G.Virtual;
using D_B_A_G.Characters;

namespace D_B_A_G.MapObjects
{
    public class Map : CollisionObject
    {
        public CollisionObject[] Walls;      //Array of collision points (walls, rocks, ponds, etc.)
        public int numWalls;                           //How many collision objects are in the array
        public CollisionObject[] Triggers;   //Array of triggers (doors, switches, items, etc.)
        public int numTriggers;                        //How many triggers objects are in the array
        
        public Map(Texture2D Sprite, int X = 0, int Y = 0, bool Solid = false)
        {
            //Set basic elements
            height = Sprite.Bounds.Height;
            width = Sprite.Bounds.Width;
            pos.X = X;
            pos.Y = Y;
            isSolid = Solid;

            //Set the sprite
            sprite = Sprite;

            //Set the center
            centerOffset.X = 0;
            centerOffset.Y = 0;
        }
        public new bool collidesWith(CollisionObject other)
        {
            for (int i = 0; i < numWalls; ++i)
                if (other.collidesWith(Walls[i])) return true;
            return false;
        }
        public void resolveMap(ref CollisionObject other)
        {
            for (int i = 0; i < numWalls; ++i)
                if (other.collidesWith(Walls[i])) other.resolveCollision(Walls[i]);
        }
        public void resolveMap(ref Player other)
        {
            for (int i = 0; i < numWalls; ++i)
                if (other.collidesWith(Walls[i])) other.resolveCollision(Walls[i]);
        }
    }
}
