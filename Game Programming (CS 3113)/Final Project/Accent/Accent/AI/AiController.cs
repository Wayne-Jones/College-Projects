using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Accent.System.IO;
using Accent.Rhythm;
using Accent.Entity;

namespace Accent.AI {
	class AiController : Controller {
		// ==============================
		// Movement fields
		// ==============================
		/// <summary>
		/// How precise the AI's movements are.
		/// Higher values make AI runtime slower.
		/// </summary>
		public int MovementDegree;
		/// <summary>
		/// How accurate the AI's attacks are.
		/// </summary>
		public int MovementAttackSkill;
		/// <summary>
		/// The number of levels that 
		/// </summary>
		public int MovementLevel;
		/// <summary>
		/// How well the AI dodges.
		/// </summary>
		public int MovementDodgeSkill;
		// ==============================
		// Rhythm fields
		// ==============================
		/// <summary>
		/// How confident the AI is.
		/// Higher confidence = less frequent rhythm changes.
		/// </summary>
		public double RhythmConfidence {
			get { return confidence; }
			set { confidence = value;
			refreshRhythmPeriod = value * 1000;
			}
		}
		/// <summary>
		/// Higher Accuracy = Less deviation about note value.
		/// </summary>
		public double RhythmAccuracy = 100;
		/// <summary>
		/// Higher precision = Less rhythmic scatter.
		/// </summary>
		public double RhythmPrecision = 100;

		double confidence;

		// ==============================
		// Interface fields
		// ==============================
		/// <summary>
		/// The fighter to use to read against. Do not change.
		/// </summary>
		readonly Fighter fighter;
		/// <summary>
		/// The player to interface against. Do not modify.
		/// </summary>
		readonly TrackPlayer player;

		// ==============================
		// AI
		// ==============================
		double timeSinceRhythmRefresh = 0;
		double refreshRhythmPeriod = 1000;
		double nextNoteOffset;
		double rhythmAccuracyOffset;

		LinkedListNode<Note>[] LastNotes;

		static Random rand = new Random();

		// ==============================
		// Methods
		// ==============================
		public AiController(int noRhythmKeys, Fighter fighter, TrackPlayer player)
			: base(noRhythmKeys) {
			this.fighter = fighter;
			this.player = player;
			LastNotes = new LinkedListNode<Note>[noRhythmKeys];
		}

		/// <summary>
		/// Run one iteration of the AI.
		/// </summary>
		public override void Update(GameTime gameTime) {
			UpdateRhythm(gameTime);
			UpdateMovement(gameTime);
		}

		void UpdateMovement(GameTime gameTime) {

		}

		void UpdateRhythm(GameTime gameTime) {
			// Handle rhythm
			timeSinceRhythmRefresh += gameTime.ElapsedGameTime.TotalMilliseconds;
			if (timeSinceRhythmRefresh > refreshRhythmPeriod)
			{
				RefreshRhythmAccuracy();
				timeSinceRhythmRefresh = 0;
			}

			for (int i = 0; i < noRhythmKeys; ++i)
			{
				if (player.ActiveNotes[i] != null)
				{
					if (player.ActiveNotes[i].Value.Time + nextNoteOffset < player.Time)
					{
						// After note started
						switch (this[i])
						{
							case KeyState.None:
								SetRhythmState(i, KeyState.Tap);
								GenerateNextNoteOffset();
								break;
							case KeyState.Tap:
								if (player.ActiveNotes[i].Value.Length != 0 &&
									player.ActiveNotes[i].Value.Time + player.ActiveNotes[i].Value.Length + nextNoteOffset > player.Time)
								{
									SetRhythmState(i, KeyState.Hold);
								}
								else
								{
									SetRhythmState(i, KeyState.Release);
								}
								break;
							case KeyState.Hold:
								if (player.ActiveNotes[i].Value.Length != 0 &&
									player.ActiveNotes[i].Value.Time + player.ActiveNotes[i].Value.Length + nextNoteOffset > player.Time)
								{
									SetRhythmState(i, KeyState.Hold);
								}
								else
								{
									SetRhythmState(i, KeyState.Release);
									GenerateNextNoteOffset();
								}
								break;
						}
					}
					else
					{
						// Before note started
						switch (this[i])
						{
							case KeyState.Tap:
							case KeyState.Hold:
								SetRhythmState(i, KeyState.Release);
								break;
							case KeyState.Release:
								SetRhythmState(i, KeyState.None);
								break;
						}
					}
				}
				else
				{
					SetRhythmState(i, KeyState.None);
				}
			}

		}

		void RefreshRhythmAccuracy() {
			rhythmAccuracyOffset = (RhythmAccuracy == 0) ?
				0 : (rand.NextDouble() * 2 - 1) * RhythmAccuracy;
			Console.WriteLine(String.Format("Accuracy offset: {0:F2}", rhythmAccuracyOffset));
		}

		void GenerateNextNoteOffset() {
			nextNoteOffset = ((RhythmPrecision == 0) ?
				0 : (rand.NextDouble() * 2 - 1) * RhythmPrecision) + rhythmAccuracyOffset;
		}
	}
}
