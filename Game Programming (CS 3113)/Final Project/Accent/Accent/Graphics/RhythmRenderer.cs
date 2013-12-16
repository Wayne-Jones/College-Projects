using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Accent.Rhythm;

namespace Accent.Graphics {
	class RhythmRenderer {
		public RhythmRenderer(RhythmEngine engine) {
			host = engine;
			leftRender = new TrackRenderer(host.LeftPlayer);
			rightRender = new TrackRenderer(host.RightPlayer);
		}

		public Rectangle LeftDrawRect {
			get { return leftRender.Dimensions; }
			set { leftRender.Dimensions = value; }
		}

		public Rectangle RightDrawRect {
			get { return rightRender.Dimensions; }
			set { rightRender.Dimensions = value; }
		}

		public SpriteFont Font {
			get { return font; }
			set {
				leftRender.font = value;
				rightRender.font = value;
				font = value;
			}
		}

		public void Draw(PrimitiveRenderer pr, SpriteBatch sb) {
			leftRender.Draw(pr, sb);
			rightRender.Draw(pr, sb);

			if (font != null) {
				sb.Begin();
				debug.Clear();
				debug.AppendFormat("Active Player: {0}\n", host.ActivePlayer);
				debug.AppendFormat("Time: {0:F2}\n", host.Time);
				debug.AppendFormat("Track time: {0:F2}\n", host.Song.PlayPosition);
				debug.AppendFormat("Active Section: {0}\n", host.ActiveSectionNo[(int)host.ActivePlayer]);
				sb.DrawString(font, debug, debugTextPos, Color.White);
				sb.End();
			}
		}

		Vector2 debugTextPos = new Vector2(200, 0);
		StringBuilder debug = new StringBuilder();
		SpriteFont font;
		TrackRenderer leftRender;
		TrackRenderer rightRender;

		RhythmEngine host;
	}
}