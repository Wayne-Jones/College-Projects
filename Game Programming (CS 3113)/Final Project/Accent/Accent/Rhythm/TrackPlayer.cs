using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Accent.System.IO;

namespace Accent.Rhythm {
	class TrackPlayer {
		public bool ShowDebug = false;
		public bool ShowPointNotice = false;
		/// <summary>
		/// Creates a new Trackplayer instance.
		/// </summary>
		public TrackPlayer()
			: this(null) { }
		/// <summary>
		/// Creates a new Trackplayer instance.
		/// </summary>
		/// <param name="controller">The controller to interface against.</param>
		public TrackPlayer(Controller controller) {
			Controller = controller;
		}
		public void Update(GameTime gameTime) {
			if (State == PlayerState.Playing) {
				Time += gameTime.ElapsedGameTime.TotalMilliseconds;
			}
		}
		/// <summary>
		/// Checks the current input device against the game state.
		/// </summary>
		/// <returns>Value of current input.</returns>
		public RhythmState CheckState() {
			RhythmState state = new RhythmState();

			if (ActiveDiff != null) {
				int[] timingWindows = { 100, 200 };
				int[] scoreValues = { 100, 50 };
				float[] healthGain = {0.10f, 0.05f};
				float HealthLoss = 0.25f;

				// Perform note checking
				if (Controller != null) {
					for (int key = 0; key < ActiveDiff.Keys; ++key) {
						bool handled = false;
						if (activeNotes[key] != null) {
							double deltaTime = activeNotes[key].Value.Time - Time;
							switch (activeNotes[key].Value.State) {
								case KeyState.Tap:
									for (int i = 0; !handled && i < timingWindows.Count(); ++i) {
										if (Math.Abs(deltaTime) < timingWindows[i]) {
											if (Controller[key] == activeNotes[key].Value.State) {
												if (ShowDebug) {
													Console.Write(String.Format("Hit {0}, offset {1:F2}\n", key, Time - activeNotes[key].Value.Time));
												}
												activeNotes[key] = activeNotes[key].Next;
												state.Score += scoreValues[i];
												state.Health += healthGain[i];
												handled = true;
											}
										}
									}
									if (!handled && deltaTime < -timingWindows.Last()) {
										if (ShowDebug){
											Console.Write(String.Format("Miss {0}, offset {1:F2}\n", key, Time - activeNotes[key].Value.Time));
										}
										state.Health -= HealthLoss;
										activeNotes[key] = activeNotes[key].Next;
									}
									break;
								case KeyState.Hold:
									// Initiate hold
									if (!holding[key]) {
										for (int i = 0; i < timingWindows.Count() && !handled; ++i) {
											if (Math.Abs(deltaTime) < timingWindows[i]) {
												if (Controller[key] == KeyState.Tap) {
													holding[key] = true;
													handled = true;
													if (ShowDebug)
														Console.Write(String.Format("Holding {0}, offset {1:F2}\n", key, Time - activeNotes[key].Value.Time));
												}
											}
										}
										if (!handled && deltaTime < -timingWindows.Last()) {
											if (ShowDebug) {
												Console.Write(String.Format("Miss {0}, offset {1:F2}\n", key, Time - activeNotes[key].Value.Time));
											}
											state.Health -= HealthLoss;
											activeNotes[key] = activeNotes[key].Next;
										}
									} else {
										// Terminate hold
										double endDeltaTime = deltaTime + activeNotes[key].Value.Length;
										if (Controller[key] == KeyState.Release) {
											for (int i = 0; i < timingWindows.Count() && !handled; ++i) {
												if (Math.Abs(endDeltaTime) < timingWindows[i]) {
													if (ShowDebug) {
														Console.Write(String.Format("Hit {0}, offset {1}\n", key, Time - activeNotes[key].Value.Time - activeNotes[key].Value.Length));
													}
													activeNotes[key] = activeNotes[key].Next;
													state.Score += scoreValues[i];
													state.Health += healthGain[i];
													handled = true;
												}
											}
											if (!handled) {
												if (ShowDebug) {
													Console.Write(String.Format("Miss {0}, offset {1}\n", key, Time - activeNotes[key].Value.Time - activeNotes[key].Value.Length));
												}
												state.Health -= HealthLoss;
												activeNotes[key] = activeNotes[key].Next;
											}
										} else if (Controller[key] == KeyState.Hold) {
											if (endDeltaTime < -timingWindows.Last()) {
												if (ShowDebug) {
													Console.Write("Miss: ");
													Console.WriteLine(key);
												}
												state.Health -= HealthLoss;
												activeNotes[key] = activeNotes[key].Next;
											}
										}
									}

									break;
								default:
									break;
							}
						}
					}
				}

				if (ShowDebug && ShowPointNotice && state != RhythmState.Zero) {
					StringBuilder pointNotice = new StringBuilder();
					pointNotice.AppendLine(state.ToString());
					Console.WriteLine(pointNotice);
				}
			}
			ScoreValueDelta = state;
			ScoreValue += state;
			return state;
		}

		public void Shift(double delta) {
			Time += delta;
		}

		/// <summary>
		/// Starts playback.
		/// </summary>
		public void Play() {
			Stop();
			State = PlayerState.Playing;
		}
		/// <summary>
		/// Pauses playback.
		/// </summary>
		public void Pause() {
			switch (State) {
				case PlayerState.Playing:
					State = PlayerState.Paused;
					break;
				case PlayerState.Paused:
					State = PlayerState.Playing;
					break;
			}
		}
		/// <summary>
		/// Stops playback and resets the internal timer to 0.
		/// </summary>
		public void Stop() {
			State = PlayerState.Stopped;
			if (track != null) {
				Time = InvalidTime;
				resetActiveNotes();
			}
		}

		// ==============================
		// Public properties
		// ==============================
		/// <summary>
		/// Track data that will be used by the player.
		/// Assigning to this value will reset the internal clock.
		/// </summary>
		public Track Track {
			get {
				return track;
			}
			set {
				track = value;
			}
		}

		public int ActiveSectionNo {
			get { return activeSectionNo; }
			set {
				activeSectionNo = value;
				resetActiveNotes();
			}
		}

		int activeSectionNo;

		/// <summary>
		/// Current time of the player.
		/// </summary>
		public double Time {
			get;
			set;
		}
		/// <summary>
		/// The current state of the player. 
		/// </summary>
		public PlayerState State {
			get;
			protected set;
		}

		public int VisibleRange {
			get;
			set;
		}

		/// <summary>
		/// The controller to interface against. Null indicates no controller.
		/// </summary>
		public Controller Controller;

		// ==============================
		// Private properties
		// ==============================
		public TrackData ActiveDiff {
			get { return activeDiff; }
			set {
				Time = InvalidTime;
				activeDiff = value;
				resetActiveNotes();
			}
		}

		TrackData activeDiff;

		public RhythmState ScoreValue {
			get { return scoreValue; }
			set {
				scoreValue = value;
				scoreValue.Health = MathHelper.Clamp(scoreValue.Health, float.MinValue, 1.0f);
			}
		}

		public RhythmState scoreValue;
		public RhythmState ScoreValueDelta {
			get;
			protected set;
		}
		Track track;

		public LinkedListNode<Note>[] ActiveNotes {
			get { return activeNotes; }
		}

		LinkedListNode<Note>[] activeNotes;
		bool[] holding;

		// ==============================
		// Private Implementations
		// ==============================
		void resetActiveNotes() {
			if (ActiveDiff != null) {
				activeNotes = new LinkedListNode<Note>[ActiveDiff.Keys];
				for (int i = 0; i < ActiveDiff.Keys; ++i)
					activeNotes[i] = ActiveDiff.Sections[ActiveSectionNo].Notes[i].First;
				holding = new bool[ActiveDiff.Keys];
			}
		}

		public static double InvalidTime = double.MinValue;
	}

	public struct RhythmState: IEquatable<RhythmState> {
		public RhythmState(int Score, float Health) {
			this.Score = Score;
			health = Health;
		}
		public int Score;
		public float Health {
			get { return health; }
			set {
				health = MathHelper.Clamp(value, -1.0f, 1.0f);
			}
		}

		float health;

		public static RhythmState operator+ (RhythmState lhs, RhythmState rhs) {
			return new RhythmState(lhs.Score + rhs.Score, lhs.Health + rhs.Health);
		}
		public static RhythmState Zero {
			get { return new RhythmState(); }
		}
		public bool Equals(RhythmState obj) {
			return Score == obj.Score && Health == obj.Health;
		}

		public override bool Equals(object obj) {
			if (obj.GetType() != typeof(RhythmState))
				return false;
			return Equals(this, (RhythmState)obj);
		}

		public override int GetHashCode() {
			return base.GetHashCode();
		}

		public override string ToString() {
			return String.Format("{0}, {1}", Score, Health);
		}

		public static bool operator ==(RhythmState lhs, RhythmState rhs) {
			if (object.ReferenceEquals(lhs, rhs)) return true;
			return rhs.Equals(lhs);
		}
		public static bool operator !=(RhythmState lhs, RhythmState rhs) {
			if (object.ReferenceEquals(lhs, rhs)) return false;
			return !rhs.Equals(lhs);
		}
	}

	/// <summary>
	/// Valid states for a TrackPlayer.
	/// </summary>
	public enum PlayerState {
		Stopped,
		Playing,
		Paused
	}
}
