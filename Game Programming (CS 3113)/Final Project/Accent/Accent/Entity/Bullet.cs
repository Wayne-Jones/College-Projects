using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Accent.Entity
{
    public class Bullet:GameObject
    {
        public float speed = 5.0f;
        public Boolean isAlive;
		public float Damage;
        
        public Bullet()
        {
            this.isAlive = true;
        } 
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            this.position.X += speed * ((float)Math.Sin(angle));
            this.position.Y -= speed * ((float)Math.Cos(angle));
        }
        public Bullet clone()
        {
            Bullet copy = this.MemberwiseClone() as Bullet;
            copy.speed = this.speed;
            copy.isAlive = this.isAlive;
            copy.position = this.position;
            copy.angle = this.angle;
            return copy;
        }
    }
}