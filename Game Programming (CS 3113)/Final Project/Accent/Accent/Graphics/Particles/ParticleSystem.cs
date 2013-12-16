using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Accent.Graphics {
	/// <summary>
	/// Represents a system of particles and particle emitters.
	/// </summary>
	class ParticleSystem {
		public ParticleSystem() {
		}
		public void RegisterGraphicsObject(GraphicsObject obj) {
			objects.AddLast(obj);
		}
		public void Update(GameTime gameTime) {
			foreach (GraphicsObject obj in objects)
				obj.Update(gameTime);
		}
		public void Draw(SpriteBatch sb) {
			foreach (GraphicsObject obj in objects)
				obj.Draw(sb);
		}

		LinkedList<GraphicsObject> objects = new LinkedList<GraphicsObject>();
	}
}
