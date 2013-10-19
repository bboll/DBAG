using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace D_B_A_G.Characters
{
    public class SpriteSheet
    {
        Texture2D m_texture;
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
            m_texture = texture;
            m_currentFrame = 0;
        }

        public void animate(SpriteBatch spriteBatch, Texture2D texture, Vector2 velocity, Vector2 location)
        {
            //Update current velocity
            m_texture = texture;

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
                if (m_currentFrame * m_spriteWidth >= texture.Bounds.Width) m_currentFrame = 0;
                spriteTimer = 10;
            }
            else spriteTimer -= 1;

            //Set the origin up
            m_origin = new Vector2(m_sourceRect.Width / 2, m_sourceRect.Height / 2);

            //Draw         
            //spriteBatch.Draw(texture, location, m_sourceRect, Color.White, 0f, m_origin, 1.0f, SpriteEffects.None, 0);
            spriteBatch.Draw(texture, location, m_sourceRect, Color.White);
        }
    }
}
