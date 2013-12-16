using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Accent.System.IO;

namespace Accent.Rhythm {
	public class Note {
		public Note() 
			: this(0) {}

		public Note(int time) 
			: this(time, 0) {}
		public Note(int time, int length) {
			Time = time;
			Length = length;
		}

		// ==============================
		// Public Properties
		// ==============================
		public int Time;
		public int Length;
		public KeyState State {
			get {
				if (Length == 0)
					return KeyState.Tap;
				return KeyState.Hold;
			}
		}
	}
}
