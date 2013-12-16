using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Accent.System.States {
	class TitleState: GameState {
		SpriteFont sysFont;
		Vector2 titlePos = new Vector2(200, 50);

		public TitleState(ActGame Host, SpriteFont Font)
		:	base(Host) {
			sysFont = Font;
		}

		public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch Sb, Graphics.PrimitiveRenderer Pr) {
			Sb.Begin();
			StringBuilder str = new StringBuilder();
			str.AppendLine("Accent");
			str.AppendLine("Press 'space' to start!");
			Sb.DrawString(sysFont, str, titlePos, Color.White);
			Sb.End();
		}

		public override void Update(Microsoft.Xna.Framework.GameTime delta) {
			if (Host.KeyboardInput.IsTap(Keys.Space))
				RequestStateChange(GameStateID.TrackSelect);
		}

		public override GameState PerformStateChange() {
			GameState State = this;
			switch (RequestedState) {
				case GameStateID.TrackSelect:
					State = new TrackSelectState(Host, Host.Tracks, Host.sysFont);
					break;
			}
			RequestedState = GameStateID.Invalid;
			return State;
		}
	}
}
