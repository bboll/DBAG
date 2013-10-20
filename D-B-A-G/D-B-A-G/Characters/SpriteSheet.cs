//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace D_B_A_G.Characters
{
    public class SpriteSheet
    {
        Texture2D[] m_texture;
        int numTextures;
        //Default frame, looking down stationary?
        public int facing = 2;
        public int m_currentFrame = 0;
        public int m_spriteWidth = 64;
        public int m_spriteHeight = 64;
        public int spriteTimer = 10;
        public Rectangle m_sourceRect;
        public Vector2 m_origin;

        public SpriteSheet(Texture2D texture, int spriteWidth, int spriteHeight)
        {
            m_texture = new Texture2D[1];
            m_texture[0] = texture;
            numTextures = 1;
            m_currentFrame = 0;
        }

        public SpriteSheet(Texture2D[] texture, int num_Textures, int spriteWidth, int spriteHeight)
        {
            m_texture = texture;
            numTextures = num_Textures;
            m_currentFrame = 0;
        }

        public void addTexture(Texture2D newTexture)
        {
            numTextures += 1;
            Texture2D[] temp = new Texture2D[numTextures];
            for (int i = 0; i < numTextures - 1; ++i)
                temp[i] = m_texture[i];
            temp[numTextures - 1] = newTexture;
            m_texture = temp;
        }

        public void animate(SpriteBatch spriteBatch, Vector2 velocity, Vector2 location)
        {            
            //Get direction
            if (velocity.X > 0) facing = 3;
            else if (velocity.X < 0) facing = 1;
            else if (velocity.Y > 0) facing = 2;
            else if (velocity.Y < 0) facing = 0;
            else m_currentFrame = 0; 

            //Set the rectangle for drawing
            m_sourceRect = new Rectangle(m_currentFrame * m_spriteWidth, facing * m_spriteHeight, m_spriteWidth, m_spriteHeight);

            if (spriteTimer == 0)
            {
                m_currentFrame += 1;
                if (m_currentFrame * m_spriteWidth >= m_texture[0].Bounds.Width) m_currentFrame = 0;
                spriteTimer = 10;
            }
            else spriteTimer -= 1;

            //Set the origin up
            m_origin = new Vector2(2, 9);

            //Draw 
            for(int i=0; i<numTextures; ++i)
                spriteBatch.Draw(m_texture[i], location, m_sourceRect, Color.White, 0.0f, m_origin, 1.3f, SpriteEffects.None, 0);
        }
    }
}
