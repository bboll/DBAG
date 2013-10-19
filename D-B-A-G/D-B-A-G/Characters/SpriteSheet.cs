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
        public int m_currentFrame = 7;
        public int m_prevFrame;
        public int m_spriteWidth = 25;
        public int m_spriteHeight = 35;
        public Rectangle m_sourceRect;
        public Vector2 m_currentVelocity;
        public Vector2 m_origin;

        public SpriteSheet(Texture2D texture, int spriteWidth, int spriteHeight)
        {
            m_texture = texture;
            m_spriteWidth = spriteWidth;
            m_spriteHeight = spriteHeight;
            //Might need to pass this
            m_currentFrame = 0;
        }
        
        public void animate(SpriteBatch spriteBatch, Vector2 velocity)
        {
          //Update current velocity
          m_prevFrame = m_currentFrame;
          m_currentVelocity = velocity;
 
          /*Check only one key is pressed, if more than one is pressed, return from the function
          if (m_currentVelocity.X == 0 && m_currentVelocity.Y == 0)
          {
             return;
          }*/
 
          //Check if right key is pressed, if it is make the animation call
          //and move the sprite 5 pixels in the positive X direction
          if (m_currentVelocity.X > 0)
          {
            m_currentFrame = 4;
            spriteBatch.Draw(sprite, 
   
            //m_position.X += 5;
          }
 
          //Ditto previous but left and 5 pixels in the negative X
          if (m_currentState.IsKeyDown(Keys.Left))
          {
              animateLeft(gameTime);
              //m_position.X -= 5;
          }
 
          //Ditto previous but Up and 5 pixels in the negative Y
          if (m_currentState.IsKeyDown(Keys.Up))
          {
              animateUp(gameTime);
              //m_position.Y -= 5;
          }
 
          //Ditto previous but down and 5 pixels in the positive Y
          if (m_currentState.IsKeyDown(Keys.Down))
          {
              animateDown(gameTime);
              //m_position.Y += 5;
          }
 
          //Check if no keys are pressed, if they aren't reset the frame to
          //the standing frame of each direction
          if (m_currentState.GetPressedKeys().Length == 0)
          {
             if (m_currentFrame >= 0 && m_currentFrame <= 2)
                {
                    
                    m_currentFrame = 1;
                }
             if (m_currentFrame >= 3 && m_currentFrame <= 5)
                {   
                    //Right
                    m_currentFrame = 4;
                }
             if (m_currentFrame >= 6 && m_currentFrame <= 8)
                {
                    m_currentFrame = 7;
                }
             if (m_currentFrame >= 9 && m_currentFrame <= 11)
                {
                    m_currentFrame = 10;
                }
          }
 
          //Set the rectangle for drawing
          m_sourceRect = new Rectangle(m_currentFrame * m_spriteWidth, 0, m_spriteWidth, m_spriteHeight);
 
          //Set the origin up
          m_origin = new Vector2(m_sourceRect.Width / 2, m_sourceRect.Height / 2);
 
      }

        private void animateRight(SpriteBatch sprite)
        {
            //Check if the velocity state is a 0, if it is snap straight to the standing
            //frame for the direction. Allows quick turning
            if (m_currentVelocity == 0)
            {
                m_currentFrame = 4;
            }
        }
    }
}
