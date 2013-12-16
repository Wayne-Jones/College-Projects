using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Accent.Graphics.Particles.Effectors;

namespace Accent.Graphics {
	class Emitter {
		public Emitter(GraphicsObject host, Vector2 position, float speed, float angle, float spread, int generationSpeed, long life, long fadeAge, Particle template, bool generate) {
			this.host = host;
			Position = position;
			Speed = speed;
			Angle = angle;
			GenerationSpeed = generationSpeed;
			Spread = spread;
			TemplateParticle = template;
			MaxLife = life;
			Generate = generate;
			generationPeriod = 1000.0 / generationSpeed;
			FadeAge = fadeAge;
		}
		// ==============================
		// Public properties
		// ==============================
		/// <summary>
		/// Position of the emitter. Particles will be emitted from this point.
		/// </summary>
		public Vector2 Position;
		/// <summary>
		/// Speed of the emitted particles.
		/// </summary>
		public float Speed;
		/// <summary>
		/// Direction of the emitter.
		/// </summary>
		public float Angle;
		/// <summary>
		/// Amount of particles to generate, per second.
		/// </summary>
		public int GenerationSpeed;
		/// <summary>
		/// Amount of time to wait before starting to fade the particle.
		/// </summary>
		public long FadeAge;

		public bool Generate;
		/// <summary>
		/// The spread of the emitted particles. [0, 1]
		/// 0:   No spread; Particles shoot out in a straight line
		/// 0.5: Half-circle spread; Particles spread across a 
		///	     semicircle in the emitter's direction
		/// 1:   Full circle spread; Angle is ignored.
		/// </summary>
		public float Spread;
		/// <summary>
		/// Template to use when emitting particles.
		/// </summary>
		public Particle TemplateParticle;

		public long MaxLife;

		public LinkedList<ParticleInstance> Instances {
			get { return instances; }
		}

		public Matrix Transform {
			get {
				return transform;
			}
		}

		public GraphicsObject Host {
			get {
				return host;
			}
		}
		// ==============================
		// Public Methods
		// ==============================
		/// <summary>
		/// Generates a new particle based on the provided template particle.
		/// </summary>
		/// <returns>The generated particle.</returns>
		public ParticleInstance NextParticleInstance(Vector2 position, float angle) {
			Vector3 particlePos = Vector3.Transform(new Vector3(position, 1), Host.Host.AbsoluteTransform);
			// TODO: Account for angular velocity changes
			// TODO: Move particles slightly to account for "different" generation times on the same frame
			float particleAngle = -host.Host.Angle + Angle + (float)(rand.NextDouble() - 0.5) * Spread;
			Vector2 particleVel = new Vector2((float)Math.Cos(particleAngle) * Speed, -(float)Math.Sin(particleAngle) * Speed);
			// Ignore host velocity
			// particleVel -= host.Host.Velocity;
			return TemplateParticle.MakeInstance(new Vector2(particlePos.X, particlePos.Y), particleVel, this);
		}

		public void Update(GameTime gameTime) {
			if (Generate) {
				double generationTime = gameTime.ElapsedGameTime.TotalMilliseconds + overflowTime;
				int particlesToGenerate = (int)(generationTime / generationPeriod);
				overflowTime = generationTime % generationPeriod;
				if (particlesToGenerate > 0) {
					float oneOverParticlesToGenerate = (1 / particlesToGenerate);
					Vector2 dPosition = host.Host.Velocity * oneOverParticlesToGenerate;
					Vector2 position = new Vector2(0, 0);
					
					// Not accounting for angle
					for (int i = 0; i < particlesToGenerate; ++i, position += dPosition) {
						instances.AddLast(NextParticleInstance(position, 0));
					}
				}
			}

			for (LinkedListNode<ParticleInstance> inst = instances.First; inst != null; inst = inst.Next) {
				inst.Value.Update(gameTime);
				if (inst.Value.Age > MaxLife)
					expired.Enqueue(inst);
			}
			while (expired.Count > 0)
				instances.Remove(expired.Dequeue());
		}

		// ==============================
		// Private fields
		// ==============================
		Matrix transform = Matrix.Identity;
		GraphicsObject host;
		LinkedList<ParticleInstance> instances = new LinkedList<ParticleInstance>();
		Queue<LinkedListNode<ParticleInstance>> expired = new Queue<LinkedListNode<ParticleInstance>>();
		double generationPeriod;
		/// <summary>
		/// Time from the last update that did not result in a particle.
		/// </summary>
		double overflowTime;
		static Random rand = new Random();
	}
}