using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Accent.Graphics.Particles.Effectors {
	/// <summary>
	/// Effector interface. Modifies a particle.
	/// </summary>
	interface IParticleEffector {
		/// <summary>
		/// Applies the effector on the passed in particle instance.
		/// </summary>
		/// <param name="inst">The instance to modify.</param>
		void Apply(ParticleInstance inst);
	}
}
