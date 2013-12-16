using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Input;

namespace Accent.System.IO {
	public class KeyboardInput {
		public KeyboardInput() { }

		public void Update(KeyboardState newState) {
			Previous = Current;
			Current = newState;
		}

		public bool IsTap(Keys key) {
			return Current.IsKeyDown(key) && Previous.IsKeyUp(key);
		}

		public KeyboardState Current {
			get;
			protected set;
		}

		public KeyboardState Previous {
			get;
			protected set;
		}
	}
}
