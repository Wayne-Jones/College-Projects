using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Accent.Graphics {
	/// <summary>
	/// Contains the definition of a particle. 
	/// </summary>
	class Particle {
		public Particle() 
			:	this(null)
		{}

		public Particle(SpriteSheet sheet) {
			this.sheet = sheet;
		}

		public ParticleInstance MakeInstance(Vector2 Position, Vector2 Velocity, Emitter Source) {
			return new ParticleInstance(Position, Velocity, Source);
		}

		Sprite InstanceToSprite(ParticleInstance inst) {
			return new Sprite(sheet, sheet.Dimension, sheet.Origin);
		}

		public void Draw(SpriteBatch sb, ParticleInstance inst) {
			Color dispColor = Color.White * (inst.Age > inst.Source.FadeAge ? (float)(inst.Source.MaxLife - inst.Age) / (inst.Source.MaxLife - inst.Source.FadeAge) : 1.0f);
			InstanceToSprite(inst).Draw(sb, inst.Position, MathHelper.PiOver2 - inst.Angle, Vector2.One, dispColor);
		}

		public SpriteSheet Sheet {
			get { return sheet; }
		}

		SpriteSheet sheet;

		public enum ParticleBehavior: int {

		}
	}
}
