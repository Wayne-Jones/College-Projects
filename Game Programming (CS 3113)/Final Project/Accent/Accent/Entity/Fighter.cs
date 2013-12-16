using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Accent.System.IO;

namespace Accent.Entity {
	/// <summary>
	/// Represents a fighter plane object.
	/// </summary>
	class Fighter: GameObject {
		public delegate void RegisterBulletFunc(Bullet bullet);
		RegisterBulletFunc RegisterBullet;

		public Rectangle ValidRect {
			get;
			set;
		}
		//Vector2 windowSize;
        ActGame game;
        Bullet template;
		//Boolean timeToFire;
		//public Boolean isHit=false;
		public float MovementSpeed = 1.0f;
		public float RotationSpeed = MathHelper.Pi / 50;
		public float Damage;
		public float BulletDamage = 1;
		//public float health=100;
		public float health = 0;
		public float Power = 0;
		//int maxBullets=20;
        float baseFreq=5;
        public float freq;
        float period;
		//Keys key;
        float timeSinceFired;

		
		public float ShieldStrength {
			get;
			protected set;
		}
		public State CurrentState;

		public Fighter(ActGame game, Rectangle ValidRect, Bullet template, RegisterBulletFunc RegisterBullet) {
			this.ValidRect = ValidRect;
			this.game = game;
			this.template = template;
			this.RegisterBullet = RegisterBullet;
		}

		public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
			handleInput();
			switch (CurrentState) {
				case Fighter.State.Attack:
					freq = baseFreq * (1 + Power);
					if (freq > 0) {
						period = 1000 / freq;

						float timeLeft = (float)gameTime.ElapsedGameTime.TotalMilliseconds + timeSinceFired;
						for (; timeLeft > period; timeLeft -= period) {
							Bullet temp = new Bullet();
							temp = template.clone();
							temp.angle = this.angle;
							temp.Damage = BulletDamage;
							temp.position = Vector2.Transform(new Vector2(0, -40), this.AbsoluteTransform);
							RegisterBullet(temp);
						}
						timeSinceFired = timeLeft;
					}
					break;
				case Fighter.State.Defend:
					ShieldStrength = (1 + Power) / 2;
					health -= ShieldStrength * Damage;
					Damage = 0;
					break;
				case Fighter.State.None:
				case Fighter.State.PhaseShift:
					break;
			}
            checkScreenBounds();
        }

		void handleInput() {
			if (controller != null) {
				if (controller[Controller.Keys.Up] == System.IO.KeyState.Hold ||
					controller[Controller.Keys.Up] == System.IO.KeyState.Tap)
					Move(0, -MovementSpeed, 0);
				if (controller[Controller.Keys.Down] == System.IO.KeyState.Hold ||
					controller[Controller.Keys.Down] == System.IO.KeyState.Tap)
					Move(0, MovementSpeed, 0);
				if (controller[Controller.Keys.Left] == System.IO.KeyState.Hold ||
					controller[Controller.Keys.Left] == System.IO.KeyState.Tap)
					Move(-MovementSpeed, 0, 0);
				if (controller[Controller.Keys.Right] == System.IO.KeyState.Hold ||
					controller[Controller.Keys.Right] == System.IO.KeyState.Tap)
					Move(MovementSpeed, 0, 0);
				if (controller[Controller.Keys.CCW] == System.IO.KeyState.Hold ||
					controller[Controller.Keys.CCW] == System.IO.KeyState.Tap)
					Move(0, 0, -RotationSpeed);
				if (controller[Controller.Keys.CW] == System.IO.KeyState.Hold ||
					controller[Controller.Keys.CW] == System.IO.KeyState.Tap)
					Move(0, 0, RotationSpeed);
			}
		}

        void checkScreenBounds()
        {
			//this.Position = new Vector2((MathHelper.Clamp(this.position.X, 0 + this.Sprite.Dimensions.Width / 2, windowSize.X - this.Sprite.Dimensions.Width / 2)), MathHelper.Clamp(this.position.Y, 0 + this.Sprite.Dimensions.Width / 2, windowSize.Y - this.Sprite.Dimensions.Width / 2));
			position.X = MathHelper.Clamp(position.X, ValidRect.X, ValidRect.X + ValidRect.Width);
			position.Y = MathHelper.Clamp(position.Y, ValidRect.Y, ValidRect.Y + ValidRect.Height);
        }
		public enum State {
			None,
			PhaseShift,
			Attack,
			Defend
		}
	}
}
