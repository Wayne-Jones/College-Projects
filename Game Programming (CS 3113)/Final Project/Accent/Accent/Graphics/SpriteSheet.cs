using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using Accent;

namespace Accent.Graphics {
	/// <summary>
	/// Represents a sprite object, containing display data for an 
	/// entity capable of being drawn on to a display.
	/// </summary>
	public class SpriteSheet {
		/// <summary>
		/// Creates a new Sprite object.
		/// </summary>
		/// <param name="frameDim">Rectangle containing the width/height of each sprite.</param>
		/// <param name="spritesheet">Texture representing the spritesheet.</param>
		public SpriteSheet(Rectangle frameDim, Texture2D spritesheet) {
			Texture = spritesheet;
			dimension = frameDim;
			origin = new Vector2(dimension.Width * 0.5f, dimension.Height * 0.5f);
			calculateRadius();
		}

		// ==============================
		// -- Public Properties ---------
		// ==============================
		public Texture2D Texture {
			get {
				return sheet;	
			}
			set {
				sheet = value;
				calculateBoundaries();
				calculateRadius();
			}
		}

		/// <summary>
		/// Returns the dimensions of each sprite in the sprite sheet.
		/// X/Y values are not used.
		/// </summary>
		public Rectangle Dimension {
			get { return dimension; }
		}

		public Vector2 Origin {
			get { return origin; }
		}
		// ==============================
		// -- Public Methods ---------
		// ==============================
		/// <summary>
		/// Returns a SpriteRecord representing the selected frame.
		/// </summary>
		/// <param name="FrameNo">Frame to extract</param>
		/// <returns>SpriteRecord object</returns>
		public Sprite FrameSprite(int FrameNo) {
			Rectangle recDim = dimension;
			if (cols > 0) {
				dimension.X = (FrameNo / cols) * dimension.Width;
				dimension.Y = (FrameNo % cols) * dimension.Height;
			}
			if (radiiSquared.Count() > 0)
				return new Sprite(this, recDim, origin, radiiSquared[FrameNo]);
			return new Sprite(this, recDim, origin);
		}

		public int VectorToIndex(Vector3 vector) {
			if (0 <= vector.X && vector.X < dimension.Width &&
				0 <= vector.Y && vector.Y < dimension.Height)
				return (int)(vector.Y) * dimension.Width + (int)vector.X;
			return -1;
		}

		public float[] RadiiSquared {
			get { return radiiSquared; }
		}

		// ==============================
		// -- Private Fields
		// ==============================
		Rectangle dimension;
		Vector2 origin;
		Texture2D sheet;

		int cols;
		int rows;
		float[] radiiSquared;

		// ==============================
		// -- Private Methods ---------
		// ==============================
		void calculateBoundaries() {
			if (sheet != null) {
				cols = sheet.Width / dimension.Width;
				rows = sheet.Height / dimension.Height;
			} else {
				cols = 0;
				rows = 0;
			}
		}

		void calculateRadius() {
			radiiSquared = new float[cols * rows];
			int frameNo = 0;
			for (int y = 0; y < rows; ++y) {
				for (int x = 0; x < cols; ++x, ++frameNo) {
					radiiSquared[frameNo] = 0;
					if (Texture != null) {
						Rectangle dimension = this.dimension;
						dimension.X = this.dimension.Width * x;
						dimension.Y = this.dimension.Height * y;
						Color[] colors = new Color[dimension.Width * dimension.Height];
						Texture.GetData<Color>(0, dimension, colors, 0, colors.Count());
						Vector2 pos = new Vector2(0, 0);
						
						// Begin radius check loop
						for (int i = 0; pos.X < dimension.Width; ++pos.X)
							for (pos.Y = 0; pos.Y < dimension.Height; ++pos.Y, ++i) {
								if (colors[i].A > 0)
									radiiSquared[frameNo] = Math.Max(radiiSquared[frameNo], (pos - Origin).LengthSquared());
							}
					}
				}
			}
		}
	}

	public class Sprite {
		public Sprite()
			: this(null, Rectangle.Empty, Vector2.Zero) { }
		public Sprite(SpriteSheet sheet, Rectangle dim, Vector2 origin)
			: this(sheet, dim, origin, 0) { }
		public Sprite(SpriteSheet sheet, Rectangle dim, Vector2 origin, float radiusSq) {
			Sheet = sheet;
			Dimensions = dim;
			Origin = origin;
			RadiusSquared = radiusSq;
		}

		public void Draw(SpriteBatch sb, Vector2 position, float rotation, Vector2 scale, Color color) {
			sb.Draw(Sheet.Texture, position, Sheet.Dimension, color, rotation, Origin, scale, SpriteEffects.None, 0);
		}

		public void GetColorData(ref Color[] array) {
			Sheet.Texture.GetData<Color>(0, Dimensions, array, 0, array.Length);
		}

		public SpriteSheet Sheet;
		public Vector2 Origin;
		public Rectangle Dimensions;
		public float RadiusSquared {
			get;
			protected set;
		}
	}
}
