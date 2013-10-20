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
        Texture2D[] m_attacks;
        Texture2D[] m_attackTextures;
        int numTextures;
        int numAttacks;
        int numAttackTextures;
        public int currentAttack;
        public int facing = 2;
        public int m_currentFrame = 0;
        public int m_spriteWidth = 64;
        public int m_spriteHeight = 64;
        public int spriteTimer = 10;
        public bool isattacking = false;
        public Rectangle m_sourceRect;
        public Vector2 m_origin;

        //Attacks
        public int m_attackFrame = 0;
        public float attacktimer = 0;      // instantiates your timer for each
        
        public SpriteSheet(Texture2D texture, int spriteWidth, int spriteHeight)
        {
            m_texture = new Texture2D[1];
            m_texture[0] = texture;
            numTextures = 1;
            m_currentFrame = 0;
        }

        public SpriteSheet(Texture2D texture, Texture2D[] attacks, int spriteWidth, int spriteHeight)
        {
            m_texture = new Texture2D[1];
            m_texture[0] = texture;
            numTextures = 1;
            m_attacks = new Texture2D[1];
            m_attackTextures = new Texture2D[0];
            numAttacks = 1;
            numAttackTextures = 0;
            m_attacks = attacks;
            m_currentFrame = 0;
        }

        public SpriteSheet(Texture2D[] texture, Texture2D[] attacks, int num_attacks, int num_Textures, int spriteWidth, int spriteHeight)
        {
            m_texture = texture;
            numTextures = num_Textures;
            m_attacks = new Texture2D[1];
            numAttacks = num_attacks;
            m_attackTextures = new Texture2D[0];
            m_attacks[0] = attacks[0];
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
        public void addAttackTexture(Texture2D newTexture)
        {
            numAttackTextures += 1;
            Texture2D[] temp = new Texture2D[numAttackTextures];
            for (int i = 0; i < numAttackTextures - 1; ++i)
                temp[i] = m_attackTextures[i];
            temp[numAttackTextures - 1] = newTexture;
            m_attackTextures = temp;
        }

        public void animate(SpriteBatch spriteBatch, Vector2 velocity, Vector2 location, bool isAttacking = false)
        {            
            //Get direction
            if (velocity.X > 0) facing = 3;
            else if (velocity.X < 0) facing = 1;
            else if (velocity.Y > 0) facing = 2;
            else if (velocity.Y < 0) facing = 0;
            else m_currentFrame = 0; 

            //Set the rectangle for drawing
            if (!isAttacking) m_sourceRect = new Rectangle(m_currentFrame * m_spriteWidth, facing * m_spriteHeight, m_spriteWidth, m_spriteHeight);

            if (spriteTimer == 0)
            {
                m_currentFrame += 1;
                if (m_currentFrame * m_spriteWidth >= m_texture[0].Bounds.Width) m_currentFrame = 0;
                spriteTimer = 10;
            }
            else spriteTimer -= 1;

            //Set the origin up
            m_origin = new Vector2(2, 9);

            //if NOT attacking
            if (!isAttacking)
            {
                //Draw 
                for (int i = 0; i < numTextures; ++i)
                    spriteBatch.Draw(m_texture[i], location, m_sourceRect, Color.White, 0.0f, m_origin, 1.3f, SpriteEffects.None, 0);
            }
            else
            {
               // if (velocity.X == 0 && velocity.Y == 0)
                //{
                    //attacktimer = 0;      // instantiates your timer for each (float)
                    if(attacktimer == 0)
                    {
                      m_attackFrame += 1;
                      if (m_attackFrame * m_spriteWidth >= m_attacks[currentAttack].Bounds.Width) m_attackFrame = 0;
                      attacktimer = 9;
                    }
                    else { attacktimer -= 1; }

                    m_sourceRect = new Rectangle(m_attackFrame * m_spriteWidth, facing * m_spriteHeight, m_spriteWidth, m_spriteHeight);
                    spriteBatch.Draw(m_attacks[currentAttack], location, m_sourceRect, Color.White, 0.0f, m_origin, 1.3f, SpriteEffects.None, 0);
                    //m_sourceRect.X = 0;
                    for (int i = 0; i < numAttackTextures; ++i)
                        spriteBatch.Draw(m_attackTextures[i], location, m_sourceRect, Color.White, 0.0f, m_origin, 1.3f, SpriteEffects.None, 0);
               // }
                //Need to handle isAttacking and trying to mode at the same time, otherwise NO DRAW
            }
        }
    }
}
