using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Accent.Graphics;
using Accent.System.IO;

namespace Accent.System.States {
	/// <summary>
	/// Represents a GameState. 
	/// Inherit from this class to create a new state.
	/// </summary>
	public abstract class GameState {

		protected ActGame Host;

		/// <summary>
		/// Performs the specified state change. 
		/// </summary>
		/// <returns></returns>
		public abstract GameState PerformStateChange();

		/// <summary>
		/// Requests a state change. 
		/// </summary>
		/// <param name="State"></param>
		protected void RequestStateChange(GameStateID State) {
			RequestedState = State;
		}

		/// <summary>
		/// Creates a new GameState object.
		/// </summary>
		/// <param name="Host">Hosting game. Cannot be null.</param>
		protected GameState(ActGame Host) {
			if (Host == null)
				throw new ArgumentNullException("Host");
			this.Host = Host;
		}
		/// <summary>
		/// Performs an update.
		/// </summary>
		/// <param name="delta">Change in time since the last update.</param>
		public abstract void Update(GameTime delta);
		public abstract void Draw(SpriteBatch Sb, PrimitiveRenderer Pr);
		public GameStateID RequestedState {
			get;
			protected set;
		}
	}

	public enum GameStateID: int {
		Invalid = 0,
		Exit = 1,
		TrackSelect = 2,
		Playing = 4,
		Results = 8,
	}
}
