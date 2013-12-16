using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Accent.Graphics.Particles.Effectors {
	class PointAttractor: IParticleEffector {
		public PointAttractor(Vector2 position, float strength) {
			Position = position;
			Strength = strength;
		}

		public void Apply(ParticleInstance inst) {
			
		}

		public Vector2 Position {
			get;
			protected set;
		}

		public float Strength {
			get;
			protected set;
		}
	}
}
