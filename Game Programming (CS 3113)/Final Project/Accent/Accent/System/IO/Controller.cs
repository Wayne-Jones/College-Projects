using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Accent.Rhythm;

namespace Accent.System.IO {
	public abstract class Controller {
		protected const int noMovementKeys = 6;
		protected readonly int noRhythmKeys;

		public Controller(int keys) {
			noRhythmKeys = keys;
			KeyCount = keys + noMovementKeys;
			states = new KeyState[KeyCount];
		}

		public abstract void Update(GameTime gameTime);
		//public virtual void Update(GameTime gameTime) {
		//    for (int i = 0; i < KeyCount; ++i) {
		//        switch (states[i])
		//        {
		//            case KeyState.Release:
		//                states[i] = KeyState.None;
		//                break;
		//            case KeyState.Tap:
		//                states[i] = KeyState.Release 
		//        }
		//        if (states[i] == KeyState.Tap)
		//        {
		//            states[i] = KeyState.Release;
		//        } else if (states[i] == KeyState.Release) {
		//            states[i] = KeyState.None;
		//        }
		//    }
		//}

		// ==============================
		// Public Properties
		// ==============================
		public int KeyCount {
			get;
			private set;
		}
		/// <summary>
		/// 0: Up, 1: Down, 2: Left, 3: Right;
		/// 4: CCW, 5: CW;
		/// ...: Rhythm Keys
		/// </summary>
		public KeyState[] States {
			get { return states; }
		}

		public KeyState this[Keys key] {
			get { return states[(int)key]; }
		}

		public KeyState this[int rhythmKeyNo] {
			get {
				if (rhythmKeyNo > noRhythmKeys)
					throw new ArgumentOutOfRangeException();
				return states[rhythmKeyNo + noMovementKeys]; 
			}
		}

		// ==============================
		// Protected Methods
		// ==============================
		/// <summary>
		/// Sets the state of the specified key to the specified state.
		/// </summary>
		/// <param name="KeyNo">Key number, must be within bounds.</param>
		/// <param name="state">State</param>
		protected void SetRhythmState(int KeyNo, KeyState state) {
			states[KeyNo + noMovementKeys] = state;
		}

		protected void SetMovementState(int KeyNo, KeyState state) {
			states[KeyNo] = state;
		}
		// ==============================
		// Private Implementation
		// ==============================
		KeyState[] states;

		public enum Keys {
			Up = 0,
			Down,
			Left,
			Right,
			CCW,
			CW
		}
	}
}
