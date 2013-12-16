using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Accent.System.IO;
using Accent.Rhythm;

namespace Accent.Graphics {
	class TrackRenderer {
		public TrackRenderer(TrackPlayer host)
		:	this(new Rectangle(0, 0, 400, 300), host)	{}

		public TrackRenderer(Rectangle dimensions, TrackPlayer host) {
			Dimensions = dimensions;
			Host = host;
		}

		public void Draw(PrimitiveRenderer pr, SpriteBatch sb) {
			if (Host.Track != null) {
				int noteHeight = 10;
				// Draw track
				Color healthColor = Color.Lerp(Color.Red, Color.Green, Host.ScoreValue.Health);
				pr.Begin();
				pr.DrawRectFilled(Dimensions, 2, Color.White, healthColor * 0.5f);
				pr.End();

				pr.Begin(Matrix.CreateTranslation(Dimensions.X, Dimensions.Y, 0));
				// Draw notes
				for (int key = 0; key < Host.ActiveDiff.Keys; ++key) {
					LinkedList<Note> visibleNotes = Host.ActiveDiff.GetNotesInRange(Host.ActiveSectionNo, key, (int)Host.Time, (int)(Host.Time + VisibleRange));
					int noteWidth = Dimensions.Width / Host.ActiveDiff.Keys;
					Rectangle drawPos = new Rectangle(noteWidth * key, 0, noteWidth, noteHeight);
					double delta;
					drawPos.Y = 0;
					foreach (Note note in visibleNotes) {
						delta = note.Time - Host.Time;
						drawPos.Y = (delta <= 0) ? 0 :
										(int)(delta / VisibleRange * Dimensions.Height);
						switch (note.State) {
							default:
							case KeyState.Tap:
								drawPos.Height = noteHeight;
								pr.DrawRectFilled(drawPos, 1, Color.White, Color.Red);
								break;
							case KeyState.Hold:
								// If note passed already, fix it to the top of the track.
								// Otherwise, leave default
								if (delta < 0) {
									drawPos.Y = 0;
								}
								// figure out where to put end
								double endDelta = delta + note.Length;
								if (delta > 0 && endDelta < VisibleRange) {
									// In range 
									drawPos.Height = (int)(note.Length / VisibleRange * Dimensions.Height);
								} else if (delta < 0 && endDelta > VisibleRange) {
									// Extended range
									drawPos.Height = Dimensions.Height;
								} else if (delta > 0) {
									// Entering
									drawPos.Height = (int)((1 - delta / VisibleRange) * Dimensions.Height);
								} else {
									// Leaving
									drawPos.Height = (int)((note.Length + delta) / VisibleRange * Dimensions.Height);
								}
								pr.DrawRectFilled(drawPos, 1, Color.White, Color.Blue);
								break;
						}
					}
				}
				pr.End();

				// Draw track overlay
				Rectangle currentTimeRect = new Rectangle(0, 0, Dimensions.Width, noteHeight);
				pr.Begin(Matrix.CreateTranslation(Dimensions.X, Dimensions.Y, 0));
				pr.DrawRectFilled(currentTimeRect, 1, Color.White, Color.LightGray * 0.8f);
				pr.End();
			}

			// Debug overlay
			StringBuilder debug = new StringBuilder();
			debug.AppendFormat("Status: {0}\n", Host.State);
			debug.AppendFormat("Active Section: {0}\n", Host.ActiveSectionNo);
			if (Host.Time == TrackPlayer.InvalidTime)
				debug.AppendLine("Track time: -");
			else
				debug.AppendFormat("Track time: {0:F2} ms\n", Host.Time);
			debug.AppendFormat("Begin/End: {0:F2} ms, {1:F2} ms\n", Host.ActiveDiff.Sections[Host.ActiveSectionNo].Begin, Host.ActiveDiff.Sections[Host.ActiveSectionNo].End);
			debug.AppendFormat("Health/Score: {0:P2}/{1}\n", Host.ScoreValue.Health, Host.ScoreValue.Score);

			// Movement keys
			for (int i = 0; i < 6; ++i) {
				debug.AppendFormat("{0}", Host.Controller.States[i]);
			}
			debug.AppendLine();
			// Rhythm keys
			for (int i = 6; i < Host.Controller.States.Length; ++i) {
				debug.AppendFormat("{0}", Host.Controller.States[i]);
			}

			sb.Begin();
			sb.DrawString(font, debug, textPosition, textColor);
			sb.End();
		}

		VertexPositionColor[] bg = new VertexPositionColor[4];
		public Rectangle Dimensions {
			get { return dimensions; }
			set {
				dimensions = value;
				textPosition.X = dimensions.X;
				textPosition.Y = dimensions.Y + dimensions.Height;
			}
		}
		Rectangle dimensions;
		public double VisibleRange = 2000;
		public TrackPlayer Host;
		public SpriteFont font;
		Vector2 textPosition;
		Color textColor = new Color(1.0f, 1.0f, 1.0f) * 0.8f;
	}
}
