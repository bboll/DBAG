using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using D_B_A_G.Virtual;
using D_B_A_G.Characters;

namespace D_B_A_G.Weapons
{
    public class Projectile : CollisionObject
    {
        protected int damage = 5;

        public Projectile(Texture2D Sprite, int Width, int Height, Vector2 Velocity, bool animate = false)
        {
            isAnimated = animate;
            isAlive = true;
            width = Width;
            height = Height;
            velocity = Velocity;
            sprite = Sprite;
            SpriteObj = new SpriteSheet(Sprite, width, height);

            //Set the center
            centerOffset.X = (-1) * (sprite.Bounds.Width / 2);
            centerOffset.Y = (-1) * (sprite.Bounds.Height / 2);
        }

        public void resolveCollision(ref CollisionObject otherObj)
        {
            base.resolveCollision(otherObj);
            otherObj.dealtdamage(damage);
        }
    }
}
