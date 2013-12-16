using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Accent.Entity;

namespace Accent.Graphics {
	/// <summary>
	/// An object that can be displayed.
	/// </summary>
	class GraphicsObject {
		public GraphicsObject(GameObject host) {
			Host = host;
		}

		public void Update(GameTime gameTime) {
			foreach (Emitter emitter in emitters)
				emitter.Update(gameTime);
		}

		public void Draw(SpriteBatch sb) {
			//sb.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Host.Transform);
			sb.Begin();
			foreach (Emitter emitter in emitters)
				foreach (ParticleInstance inst in emitter.Instances)
					emitter.TemplateParticle.Draw(sb, inst);
			sb.End();
		}
		/// <summary>
		/// Creates a new Emitter at the specified position. Created emitter does not produce fading particles.
		/// </summary>
		/// <param name="position"></param>
		/// <param name="speed"></param>
		/// <param name="angle"></param>
		/// <param name="spread"></param>
		/// <param name="generationSpeed"></param>
		/// <param name="life"></param>
		/// <param name="template"></param>
		/// <param name="generate"></param>
		/// <returns></returns>
		public Emitter CreateEmitter(Vector2 position, float speed, float angle, float spread, int generationSpeed, long life, Particle template, bool generate) {
			emitters.AddLast(new Emitter(this, position, speed, angle, spread, generationSpeed, life, life, template, generate));
			return emitters.Last.Value;
		}

		/// <summary>
		/// Creates a new Emitter at the specified position. Fade enabled.
		/// </summary>
		/// <param name="position"></param>
		/// <param name="speed"></param>
		/// <param name="angle"></param>
		/// <param name="spread"></param>
		/// <param name="generationSpeed"></param>
		/// <param name="life"></param>
		/// <param name="fadeTime"></param>
		/// <param name="template"></param>
		/// <param name="generate"></param>
		/// <returns></returns>
		public Emitter CreateEmitter(Vector2 position, float speed, float angle, float spread, int generationSpeed, long life, long fadeTime, Particle template, bool generate) {
			emitters.AddLast(new Emitter(this, position, speed, angle, spread, generationSpeed, life, fadeTime, template, generate));
			return emitters.Last.Value;
		}

		/// <summary>
		/// Removes an emitter from the graphics object.
		/// </summary>
		/// <param name="obj">The emitter to remove. It should be created from the current instance's CreateEmitter method.</param>
		public void RemoveEmitter(Emitter obj) {
			emitters.Remove(obj);
		}

		// ==============================
		// Public Properties
		// ==============================
		/// <summary>
		/// The object that this graphics object is linked to.
		/// </summary>
		public GameObject Host;

		public Matrix Transform {
			get {
				if (Host != null)
					return Host.Transform;
				return Matrix.Identity;
				}
		}

		/// <summary>
		/// The list of emitters that are linked to this graphics object.
		/// </summary>
		public LinkedList<Emitter> emitters = new LinkedList<Emitter>();
	}
}
