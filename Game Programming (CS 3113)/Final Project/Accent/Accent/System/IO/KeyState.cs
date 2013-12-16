using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Accent.System.IO {
	public enum KeyState : int {
		None = 0,
		Tap = 1,
		Hold = 2,
		Release = 4
	}
}
