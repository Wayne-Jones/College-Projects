using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using Accent.Rhythm;
using Accent.Graphics;

namespace Accent.System.States {
	class TrackSelectState: GameState {
		// Private variables
		int[] TrackSelected;
		int[] DiffSelected;

		List<Track> Tracks;
		LinkedList<TrackSelection>[] SelectionList = new LinkedList<TrackSelection>[2];
		public SpriteFont Font;

		// Display variables
		Vector2 trackSelectListPosition = new Vector2(300, 30);
		Vector2 resultsTextPosition = new Vector2(150, 50);
		Vector2[] diffSelectListPosition;
		Vector2[] selectionListPosition;
		Vector2[] resultPosition;

		int noPlayers = 2;

		// Methods
		/// <summary>
		/// Creates a new TrackSelect state.
		/// </summary>
		/// <param name="Host">Hosting game. Cannot be null.</param>
		/// <param name="Tracks"></param>
		/// <param name="Font"></param>
		public TrackSelectState(ActGame Host, List<Track> Tracks, SpriteFont Font)
			: base(Host) {
				if (Tracks == null || Tracks.Count <= 0)
					throw new ArgumentNullException("Tracks", "Cannot be null nor empty.");
				if (Font == null)
					throw new ArgumentNullException("Font");
				this.Font = Font;
				this.Tracks = Tracks;

				TrackSelected = new int[noPlayers];
				DiffSelected = new int[noPlayers];
				diffSelectListPosition = new Vector2[noPlayers];
				resultPosition = new Vector2[noPlayers];
				selectionListPosition = new Vector2[noPlayers];

				for (int i = 0; i < SelectionList.Length; ++i)
					SelectionList[i] = new LinkedList<TrackSelection>();

				diffSelectListPosition[0] = new Vector2(80, 30);
				diffSelectListPosition[1] = new Vector2(520, 30);

				selectionListPosition[0] = new Vector2(80, 300);
				selectionListPosition[1] = new Vector2(520, 300);

				resultPosition[0] = new Vector2(100, 150);
				resultPosition[1] = new Vector2(300, 150);
		}
		public override void Update(GameTime delta) {
			bool requestPlay = false;
			// BEGIN ===================================
			for (int i = 0; i < noPlayers; ++i) {
				// Request exit
				requestPlay |= Host.KeyboardInput.IsTap(Host.Config.IO.Keyboard.Players[i].Rhythm.InputKeys[2]);

				if (TrackSelected[i] > 0 &&
					Host.KeyboardInput.IsTap(Host.Config.IO.Keyboard.Players[i].Movement.Up))
					--TrackSelected[i];
				if (TrackSelected[i] < Tracks.Count - 1 &&
					Host.KeyboardInput.IsTap(Host.Config.IO.Keyboard.Players[i].Movement.Down))
					++TrackSelected[i];

				if (Host.KeyboardInput.IsTap(Host.Config.IO.Keyboard.Players[i].Movement.Right)) {
					// Add to track selection
					TrackSelection selection = new TrackSelection(Tracks[TrackSelected[i]], Tracks[TrackSelected[i]].Diffs[DiffSelected[i]]);
					if (selection.Track != null && selection.Diff != null)
						SelectionList[i].AddLast(selection);
				}
				if (Host.KeyboardInput.IsTap(Host.Config.IO.Keyboard.Players[i].Movement.Left)) {
					// Remove from track selection
					if (SelectionList[i].Count > 0)
						SelectionList[i].RemoveLast();
				}

				if (Host.KeyboardInput.IsTap(Host.Config.IO.Keyboard.Players[i].Rhythm.InputKeys[0]) &&
					DiffSelected[i] > 0)
					--DiffSelected[i];
				if (Host.KeyboardInput.IsTap(Host.Config.IO.Keyboard.Players[i].Rhythm.InputKeys[1]) &&
					DiffSelected[i] < Tracks[TrackSelected[i]].Diffs.Count - 1)
					++DiffSelected[i];
			}

			if (requestPlay) {
				RequestStateChange(GameStateID.Playing);
			}
			// END ===================================
		}
		public override void Draw(SpriteBatch Sb, PrimitiveRenderer Pr) {
			StringBuilder str = new StringBuilder();
			// Draw track list
			str.AppendLine("Track List:");
			foreach (Track track in Tracks)
				str.AppendLine(track.Name);

			Sb.Begin();
			Sb.DrawString(Font, str, trackSelectListPosition, Color.White);

			// BEGIN ==================================
			for (int i = 0; i < noPlayers; ++i) {
				str.Clear();
				str.AppendLine(Tracks[TrackSelected[i]].Name);
				// Draw diff list
				foreach (TrackData diff in Tracks[TrackSelected[i]].Diffs)
					str.AppendLine(diff.Name);
				Sb.DrawString(Font, str, diffSelectListPosition[i], Color.Pink);

				// Draw selected list
				str.Clear();
				str.AppendLine("Selected:");
				foreach (TrackSelection selection in SelectionList[i])
					str.AppendFormat("{0} ({1})\n", selection.Track.Name, selection.Diff.Name);
				Sb.DrawString(Font, str, selectionListPosition[i], Color.Pink);

				// Draw track selected box
				Rectangle selectedRect = new Rectangle((int)trackSelectListPosition.X - 15 * (noPlayers - i), (int)trackSelectListPosition.Y + (1 + TrackSelected[i]) * Font.LineSpacing, 10, Font.LineSpacing);
				Pr.DrawRectFilled(selectedRect, 1, Color.Red, Color.Pink);

				// Draw diff selected box
				selectedRect = new Rectangle((int)diffSelectListPosition[i].X - 15, (int)diffSelectListPosition[i].Y + (1 + DiffSelected[i]) * Font.LineSpacing, 10, Font.LineSpacing);
				Pr.DrawRectFilled(selectedRect, 1, Color.Red, Color.Pink);
			}
			// END ==================================

			Sb.End();
		}
		public override GameState PerformStateChange() {
			GameState State = this;
			switch (RequestedState) {
				case GameStateID.Playing:
					bool selectionListFilled = true;
					for (int i = 0; i < noPlayers; ++i)
						selectionListFilled &= SelectionList[i].Count > 0;
					if (selectionListFilled) {
						State = new PlayingState(Host, Host.Config.IO.Keyboard.Left, Host.Config.IO.Keyboard.Right, SelectionList);
					}
					break;
			}
			RequestedState = GameStateID.Invalid;
			return State;
		}
	}
}
