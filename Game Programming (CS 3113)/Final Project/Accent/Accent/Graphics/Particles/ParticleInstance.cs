using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Accent.Graphics {
	/// <summary>
	/// Represents a particle in a particle generator system.
	/// </summary>
	class ParticleInstance {
		public ParticleInstance() 
			:	this(Vector2.Zero, Vector2.Zero, null)
		{}

		public ParticleInstance(Vector2 position, Vector2 velocity, Emitter source) {
			this.position = position;
			this.velocity = velocity;
			angle = (float)Math.Atan2(-velocity.Y, velocity.X);
			this.source = source;
			age = 0;
		}

		public void AddVelocity(Vector2 dv) {
			this.dv += dv;
		}
		public void Update(GameTime gametime) {
			velocity += dv * (float)gametime.ElapsedGameTime.TotalMilliseconds;
			position += velocity * (float)gametime.ElapsedGameTime.TotalMilliseconds;
			age += (long)gametime.ElapsedGameTime.TotalMilliseconds;
			dv = Vector2.Zero;
		}

		// ==============================
		// Public Properties
		// ==============================
		public Vector2 Position {
			get { return position; }
		}
		public Vector2 Velocity {
			get { return velocity; }
		}
		public float Angle {
			get { return angle; }
		}
		//public float AngularVelocity {
		//    get { return angularVelocity; }
		//}
		public long Age {
			get { return age; }
		}
		public Emitter Source {
			get { return source; }
		}

		// ==============================
		// Private Fields
		// ==============================
		long age;
		float angle;
		//float angularVelocity;
		Vector2 position;
		Vector2 velocity;
		Vector2 dv;
		Emitter source;
	}
}
