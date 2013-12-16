using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Accent.System.IO {
	class KeyboardController: Controller {
		public KeyboardController(int noKeys, KeyboardInput keyboardInput, Configuration.IOConfig.KeyboardConfig.PlayerKeyConfig config)
		:	base(noKeys) {
			KeyConfig = config;
			keyboard = keyboardInput;
		}

		public override void Update(GameTime delta) {
			for (int i = 0; i < KeyConfig.Movement.KeyList.Length; ++i) {
				if (keyboard.Current.IsKeyDown(KeyConfig.Movement.KeyList[i]))
				{
					// Key is currently down
					if (keyboard.Previous.IsKeyDown(KeyConfig.Movement.KeyList[i]))
						// Key was down last frame
						SetMovementState(i, KeyState.Hold);
					else
						// Key was not down last frame
						SetMovementState(i, KeyState.Tap);
				}
				else
				{
					// Key is not currently down
					if (keyboard.Previous.IsKeyDown(KeyConfig.Movement.KeyList[i]))
						// Key was down last frame
						SetMovementState(i, KeyState.Release);
					else
						// Key was up last frame
						SetMovementState(i, KeyState.None);
				}
			}

			for (int i = 0; i < KeyConfig.Rhythm.KeyCount; ++i)
			{
				if (keyboard.Current.IsKeyDown(KeyConfig.Rhythm.InputKeys[i]))
				{
					// Key is currently down
					if (keyboard.Previous.IsKeyDown(KeyConfig.Rhythm.InputKeys[i]))
						// Key was down last frame
						SetRhythmState(i, KeyState.Hold);
					else
						// Key was not down last frame
						SetRhythmState(i, KeyState.Tap);
				}
				else
				{
					// Key is not currently down
					if (keyboard.Previous.IsKeyDown(KeyConfig.Rhythm.InputKeys[i]))
						// Key was down last frame
						SetRhythmState(i, KeyState.Release);
					else
						// Key was up last frame
						SetRhythmState(i, KeyState.None);
				}
			}

		}

		KeyboardInput keyboard;
		// MouseInput mouse;
		Configuration.IOConfig.KeyboardConfig.PlayerKeyConfig KeyConfig;
	}
}
