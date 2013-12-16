using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using Accent.Graphics;
using Accent.System.IO;

namespace Accent.Entity {
	/// <summary>
	/// Base GameObject class. All game objects inherit from this.
	/// </summary>
	public class GameObject {
		public GameObject() {
			localToWorld = Matrix.Identity;
			spriteToLocal = Matrix.Identity;
			scale = new Vector2(1, 1);

		}

		// ==============================
		// Public Methods
		// ==============================
		/// <summary>
		/// Registers a transformation.
		/// </summary>
		/// <param name="dPosition">Positional shift vector</param>
		/// <param name="angle">Angle of rotation</param>
		public void Move(Vector2 shift, float angle) {
			Move(shift.X, shift.Y, angle);
		}

		/// <summary>
		/// Registers a transformation.
		/// </summary>
		/// <param name="xShift">Change in X</param>
		/// <param name="yShift">Change in Y</param>
		/// <param name="angle"></param>
		public void Move(float xShift, float yShift, float angle) {
			dPosition.X += xShift;
			dPosition.Y += yShift;
			dAngle += angle;
		}
		/// <summary>
		/// Runs an update cycle. This function should be called after
		/// other state changes have occured.
		/// </summary>
		/// <param name="time">Delta time</param>
		public virtual void Update(GameTime time) {
			//checkInput();
			position += dPosition;
			angle += dAngle;

			// Does not work with rotations.
			//localToWorld = Matrix.CreateRotationZ(angle) * Matrix.CreateTranslation(new Vector3(position, 0));
			calculateTransform();
			float oneOverSecondsPassed = (float)(1000 / time.ElapsedGameTime.TotalMilliseconds);
			velocity = dPosition * oneOverSecondsPassed;
			angularVelocity = dAngle * oneOverSecondsPassed;
			// Reset delta position
			dPosition.X = 0;
			dPosition.Y = 0;
			dAngle = 0;
		}

		/// <summary>
		/// Checks for a collision between this object and another.
		/// </summary>
		/// <param name="other"></param>
		/// <returns>Collision detected</returns>
		public bool CheckCollide(GameObject other) {
			if ((other.Position - Position).LengthSquared() > other.Sprite.RadiusSquared + Sprite.RadiusSquared) {
				return false;
			}

			GameObject small, large;
			if (Sprite.Dimensions.Width * Sprite.Dimensions.Height < other.Sprite.Dimensions.Width * other.Sprite.Dimensions.Height) {
				small = this;
				large = other;
			} else {
				large = this;
				small = other;
			}

			// Matrix to convert from small-space to large-space, the local space of the respective objects.
			Matrix s2l = small.Transform * Matrix.Invert(large.Transform);

			Color[] sColors = new Color[small.Sprite.Dimensions.Width * small.Sprite.Dimensions.Height];
			Color[] lColors = new Color[large.Sprite.Dimensions.Width * large.Sprite.Dimensions.Height];

			small.Sprite.GetColorData(ref sColors);
			large.Sprite.GetColorData(ref lColors);

			Vector3 sPos = new Vector3(0, 0, 1);
			Vector3 lPos;
			int sIndex = 0;
			int lIndex;
			for (sPos.Y = 0; sPos.Y < small.Sprite.Dimensions.Height; ++sPos.Y)
				for (sPos.X = 0; sPos.X < small.Sprite.Dimensions.Width; ++sPos.X, ++sIndex) {
					if (sColors[sIndex].A > 0) {
						Vector3.Transform(ref sPos, ref s2l, out lPos);
						lIndex = large.Sprite.Sheet.VectorToIndex(lPos);
						if (lIndex >= 0 && lColors[lIndex].A > 0)
							return true;
						}
				}
			return false;
		}

		// ==============================
		// Properties
		// ==============================
		/// <summary>
		/// Current position of the object.
		/// </summary>
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

		/// <summary>
		/// Transformation matrix taking into account the sprite's origin.
		/// </summary>
		public Matrix Transform {
			get { return localToWorld; }
		}
		/// <summary>
		/// Absolute transformation matrix
		/// </summary>
		public Matrix AbsoluteTransform {
			get;
			protected set;
		}

		public Vector2 Scale {
			get { return Vector2.One; }
		}

		public float Angle {
			get { return angle; }
            set { angle = value; }
		}

		public Sprite Sprite {
			get;
			set;
			// TODO: Limit set to protected after debugging
		}

		public Vector2 Velocity {
			get {
				return velocity;
			}
		}	// TODO: will be used for particle initial velocity
			// Try to get some degree of accuracy, doesn't have to be perfect
		public float AngularVelocity {
			get {
				return angularVelocity;
			}
		}

		public Controller controller;

		// ==============================
		// Private Implementation Fields
		// ==============================
		/// <summary>
		/// Matrix representing the transformation from local space to 
		/// world space.
		/// </summary>
		Matrix localToWorld;
		/// <summary>
		/// Matrix representing the transformation from sprite space to
		/// local space.
		/// TODO: Is this needed?
		/// </summary>
		Matrix spriteToLocal;
		/// <summary>
		/// Amount to move by on the next update cycle.
		/// </summary>
		Vector2 dPosition;
		float dAngle;
		
		public Vector2 position;
		public float angle;
		Vector2 scale;

		Vector2 velocity;
		float angularVelocity;

		// ==============================
		// Private Methods
		// ==============================
		void calculateTransform() {
			AbsoluteTransform = Matrix.CreateRotationZ(angle) * Matrix.CreateScale(new Vector3(scale, 1)) * Matrix.CreateTranslation(new Vector3(position, 1));
			localToWorld = Matrix.CreateTranslation(new Vector3(-Sprite.Origin, 1)) * AbsoluteTransform;
		}
	}
}
