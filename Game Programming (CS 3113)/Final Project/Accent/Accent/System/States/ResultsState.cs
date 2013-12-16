using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Accent.System.States {
	class ResultsState: GameState {
		int noPlayers = 2;

		ScoreReport Left;
		ScoreReport Right;
		SpriteFont sysFont;

		Vector2 resultsTextPosition = new Vector2(150, 50);
		Vector2 LeftResultPosition = new Vector2(100, 150);
		Vector2 RightResultPosition = new Vector2(300, 150);

		public ResultsState(ActGame Host, ScoreReport Left, ScoreReport Right, SpriteFont Font)
			: base(Host) {
				this.Left = Left;
				this.Right = Right;
				this.sysFont = Font;
		}
		public override void Update(Microsoft.Xna.Framework.GameTime delta) {
			bool requestTrackSelect = false;
			for (int i = 0; i < noPlayers; ++i)
				requestTrackSelect |= Host.KeyboardInput.IsTap(Host.Config.IO.Keyboard.Players[i].Rhythm.InputKeys[2]);
			if (requestTrackSelect) {
				RequestStateChange(GameStateID.TrackSelect);
			}
		}
		public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch Sb, Graphics.PrimitiveRenderer Pr) {
			StringBuilder str = new StringBuilder();

			if (Left.Damage == Right.Damage)
				str.AppendLine("Draw!");
			else {
				str.Append("Winrar: ");
				if (Left.Damage < Right.Damage)
					str.AppendLine("Player 1!");
				else
					str.AppendLine("Player 2!");
			}
			str.AppendLine("Press Enter to return to track select...");
			Sb.Begin();
			Sb.DrawString(sysFont, str, resultsTextPosition, Color.White);

			str.Clear();
			str.AppendLine("Player 1\n");
			str.AppendFormat("Result score: {0}\n", Left.Score);
			str.AppendFormat("Final health: {0}\n", Left.Health);
			str.AppendFormat("Damage dealt: {0}\n", Left.Damage);
			Sb.DrawString(sysFont, str, LeftResultPosition, Color.White);

			str.Clear();
			str.AppendLine("Player 2\n");
			str.AppendFormat("Result score: {0}\n", Right.Score);
			str.AppendFormat("Final health: {0}\n", Right.Health);
			str.AppendFormat("Damage dealt: {0}\n", Right.Damage);
			Sb.DrawString(sysFont, str, RightResultPosition, Color.White);

			Sb.End();
		}
		public override GameState PerformStateChange() {
			GameState State = this;
			switch (RequestedState) {
				case GameStateID.TrackSelect:
					State = new TrackSelectState(Host, Host.Tracks, Host.sysFont);
					break;
			}
			return State;
		}
	}
}
