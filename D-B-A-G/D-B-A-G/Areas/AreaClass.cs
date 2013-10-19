using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using D_B_A_G.Abstract;
using D_B_A_G.Characters;
using D_B_A_G.MapObjects;

namespace D_B_A_G.Areas
{
    public class AreaClass
    {
        //Required objects
        public Map mapObject;
        public Game1 ROOT;
        
        //Constructor
        public AreaClass() { } //Preferably do not use this one... it will break
        public AreaClass(Texture2D texture, Game1 root)
        {           
            mapObject = new Map(texture);
            ROOT = root;
        }

        //Initialize the area
        public void initialize()
        {
            //Make the character stay on the screen
            ROOT.Hero.domain = new Vector4(0, 0, mapObject.width, mapObject.width);
            //ROOT.Content.RootDirectory = "../../../Content";
        }

        //Update
        public void update()
        {
        }

        //Draw all (call base.draw in children)
        public void draw(SpriteBatch drawHere)
        {
            //Draw the map
            mapObject.draw(drawHere);
        }
    }
}
