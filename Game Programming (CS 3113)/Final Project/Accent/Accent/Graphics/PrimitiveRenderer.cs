using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Accent.Graphics {
	public class PrimitiveRenderer {
		public PrimitiveRenderer(SpriteBatch sb) {
			this.sb = sb;

			pixel = new Texture2D(sb.GraphicsDevice, 1, 1);
			Color[] color = new Color[1];
			color[0] = new Color(1.0f, 1.0f, 1.0f);
			pixel.SetData<Color>(color);
		}
		public void Begin() {
			sb.Begin();
		}

		public void Begin(Matrix transform) {
			sb.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, transform);
		}

		public void End() {
			sb.End();
		}

		public void DrawLine(Vector2 start, Vector2 end, float width, Color color) {
			Vector2 delta = (end - start);
			sb.Draw(pixel, start, null, color, (float)Math.Atan2(delta.Y, delta.X), Vector2.Zero, new Vector2(delta.Length(), width), SpriteEffects.None, 0);
		}

		public void DrawCircle(Vector2 midpoint, float radius, float thickness, Color color) {
			float radsq = (float)Math.Pow(radius, 2);
			Vector2 current = new Vector2(radius, 0);
			Vector2 currentInv = new Vector2(radius, 0);

			for (; current.X >= 0; --current.X, --currentInv.X) {
				while (current.LengthSquared() < radsq) {
					sb.Draw(pixel, midpoint + current, color);
					sb.Draw(pixel, midpoint - current, color);
					sb.Draw(pixel, midpoint + currentInv, color);
					sb.Draw(pixel, midpoint - currentInv, color);
					--current.Y;
					++currentInv.Y;
				}
				++current.Y;
				--currentInv.Y;
			}
		}

		public void DrawCircleFilled(Vector2 midpoint, float radius, float thickness, Color outline, Color fill) {
			float radsq = (float)Math.Pow(radius, 2);
			Vector2 current = new Vector2(radius, 0);
			Vector2 currentInv = new Vector2(radius, 0);
			for (; current.X > 0; --current.X, --currentInv.X) {
				DrawLine(midpoint + current, midpoint + currentInv, 1.0f, fill);
				DrawLine(midpoint - current, midpoint - currentInv, 1.0f, fill);
				while (current.LengthSquared() < radsq) {
					sb.Draw(pixel, midpoint + current, outline);
					sb.Draw(pixel, midpoint - current, outline);
					sb.Draw(pixel, midpoint + currentInv, outline);
					sb.Draw(pixel, midpoint - currentInv, outline);
					--current.Y;
					++currentInv.Y;
				}
				++current.Y;
				--currentInv.Y;
			}
			sb.Draw(pixel, midpoint + current, outline);
			sb.Draw(pixel, midpoint - current, outline);
			sb.Draw(pixel, midpoint + currentInv, outline);
			sb.Draw(pixel, midpoint - currentInv, outline);
		}

		public void DrawRect(Rectangle rect, float width, Color color) {
			Vector2 topleft = new Vector2(rect.X, rect.Y);
			Vector2 topright = new Vector2(rect.X + rect.Width, rect.Y);
			Vector2 botleft = new Vector2(rect.X, rect.Y + rect.Height);
			Vector2 botright = new Vector2(rect.X + rect.Width, rect.Y + rect.Height);

			DrawLine(topleft, topright, 1, color);
			DrawLine(topleft, botleft, 1, color);
			DrawLine(topright, botright, 1, color);
			DrawLine(botleft, botright, 1, color);
		}

		public void DrawRectFilled(Rectangle rect, float width, Color outline, Color fill) {
			Vector2 topleft = new Vector2(rect.X, rect.Y);
			Vector2 topright = new Vector2(rect.X + rect.Width, rect.Y);
			Vector2 botleft = new Vector2(rect.X, rect.Y + rect.Height);
			Vector2 botright = new Vector2(rect.X + rect.Width, rect.Y + rect.Height);

			sb.Draw(pixel, rect, fill);
			DrawLine(topleft, topright, 1, outline);
			DrawLine(topleft, botleft, 1, outline);
			DrawLine(topright, botright, 1, outline);
			DrawLine(botleft, botright, 1, outline);
		}

		SpriteBatch sb;
		Texture2D pixel;
	}
}
