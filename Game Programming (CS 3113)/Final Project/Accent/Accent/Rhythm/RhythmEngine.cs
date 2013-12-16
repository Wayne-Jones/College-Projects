using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using System.Diagnostics;

using Microsoft.Xna.Framework;

using IrrKlang;

using Accent.System;
using Accent.System.IO;

namespace Accent.Rhythm {
	class RhythmEngine {
		public RhythmEngine(Configuration config) {
			Config = config;
			fadeInTimeInterval = (float)(Config.Game.Track.LeadIn * fadeVolumeInterval);
			fadeOutTimeInterval = (float)(Config.Game.Track.FallOut * fadeVolumeInterval);
			State = RhythmEngineState.Stopped;
			for (int i = 0; i < NoPlayers; ++i)
				players[i] = new TrackPlayer();
			ActivePlayer = PlayerID.Left;
		}

		public void Update(GameTime gameTime) {
			Switched = false;
			if (State == RhythmEngineState.Playing)
			{
				double currentBegin = currentSelection[(int)ActivePlayer].Value.Diff.Sections[ActiveSectionNo[(int)ActivePlayer]].Begin;
				double currentEnd = currentSelection[(int)ActivePlayer].Value.Diff.Sections[ActiveSectionNo[(int)ActivePlayer]].End;
				double fadein = Config.Game.Track.LeadIn;
				double fadeout = Config.Game.Track.FallOut;
				double timeBefore = Time;
				if (!song.Paused)
					Time = song.PlayPosition;
				else
					Time += gameTime.ElapsedGameTime.TotalMilliseconds;
				if (song.Paused)
				{
					if (Time > 0)
					{
						song.PlayPosition = (uint)Time;
						song.Paused = false;
					}
				}

				if (currentBegin - fadein <= Time && Time < currentBegin)
				{
					// Fading in
					timeSinceFadeStep += gameTime.ElapsedGameTime.TotalMilliseconds;
					if (timeSinceFadeStep >= fadeInTimeInterval)
					{
						song.Volume += fadeVolumeInterval;
						timeSinceFadeStep -= fadeInTimeInterval;
					}
				} else if (Time > currentEnd && Time < currentEnd + fadeout)
				{
					// Fading out
					timeSinceFadeStep += gameTime.ElapsedGameTime.TotalMilliseconds;
					if (timeSinceFadeStep >= fadeOutTimeInterval)
					{
						song.Volume -= fadeVolumeInterval;
						timeSinceFadeStep -= fadeOutTimeInterval;
					}
				} else if (Time > currentEnd + fadeout)
				{
					// End playback
					song.Stop();
					++ActiveSectionNo[(int)ActivePlayer];
					if (ActiveSectionNo[(int)ActivePlayer] >= currentSelection[(int)ActivePlayer].Value.Diff.Sections.Count)
					{
						currentSelection[(int)ActivePlayer] = currentSelection[(int)ActivePlayer].Next;
						ActiveSectionNo[(int)ActivePlayer] = 0;
					}
					ActivePlayer = (ActivePlayer == PlayerID.Left) ? PlayerID.Right : PlayerID.Left;

					if (currentSelection[(int)ActivePlayer] == null)
					{
						// No more songs left!
						Unlock();
						return;
					}
					Time = currentSelection[(int)ActivePlayer].Value.Diff.Sections[ActiveSectionNo[(int)ActivePlayer]].Begin - Config.Game.Track.LeadIn;
					foreach (TrackPlayer player in players)
					{
						player.Track = currentSelection[(int)ActivePlayer].Value.Track;
						player.ActiveDiff = currentSelection[(int)ActivePlayer].Value.Diff;
						player.ActiveSectionNo = activeSectionNo[(int)ActivePlayer];
					}
					song = soundengine.Play2D(players[(int)ActivePlayer].Track.FilePath, false, true);
					song.Volume = 0;
					Switched = true;
				} else
				{
					// Normal playback
					song.Volume = 1.0f;
				}
			}
			foreach (TrackPlayer player in players)
				player.Update(gameTime);
		}
		public void Lock() {
			locked = true;
			if (LeftSelectionList.Count > 0 && RightSelectionList.Count > 0)
			{
				for (int i = 0; i < NoPlayers; ++i)
				{
					currentSelection[i] = SelectionList[i].First;
					players[i].Track = LeftSelectionList.First().Track;
					players[i].ActiveDiff = LeftSelectionList.First().Diff;
					players[i].ScoreValue = new RhythmState(0, -1.0f);
				}
			}
		}
		public void Unlock() {
			locked = false;
		}
		public void CheckState() {
			// TODO: DEBUG
			for (int i = 0; i < NoPlayers; ++i)
			{
				players[i].CheckState();
			}
		}

		public void Play() {
			if (song != null)
				song.Stop();
			song = soundengine.Play2D(ActiveTrack.FileDirectory + @"\" + ActiveTrack.AudioFilename, false, true);
			State = RhythmEngineState.Playing;
			for (int i = 0; i < NoPlayers; ++i)
			{
				players[i].Play();
			}
			Time = currentSelection[(int)ActivePlayer].Value.Diff.Sections[ActiveSectionNo[(int)ActivePlayer]].Begin - Config.Game.Track.LeadIn;
		}
		public void Pause() {
			switch (State)
			{
				case RhythmEngineState.Playing:
					song.Paused = true;
					State = RhythmEngineState.Paused;
					foreach (TrackPlayer player in players)
						player.Pause();
					break;
				case RhythmEngineState.Paused:
					if (Time >= 0)
						song.Paused = false;
					State = RhythmEngineState.Playing;
					foreach (TrackPlayer player in players)
						player.Pause();
					break;
			}
		}
		public void Stop() {
			if (song != null)
				song.Stop();
			State = RhythmEngineState.Stopped;
			foreach (TrackPlayer player in players)
				player.Stop();
		}
		public void Shift(double delta) {
			Time += delta;
			foreach (TrackPlayer player in players)
				player.Shift(delta);
		}

		public RhythmEngineState State {
			get;
			protected set;
		}
		public Track ActiveTrack {
			get {
				return currentSelection[(int)ActivePlayer].Value.Track;
			}
		}

		Configuration Config;
		public double Time {
			get {
				return time;
			}
			protected set {
				time = value;
				LeftPlayer.Time = value;
				RightPlayer.Time = value;
			}
		}

		double time;

		public TrackPlayer LeftPlayer {
			get { return players[(int)PlayerID.Left]; }
			protected set { players[(int)PlayerID.Left] = value; }
		}
		public Controller LeftController {
			get { return players[(int)PlayerID.Left].Controller; }
			set { players[(int)PlayerID.Left].Controller = value; }
		}
		public RhythmState LhsState {
			get { return players[(int)PlayerID.Left].ScoreValue; }
		}
		public RhythmState LhsStateDelta {
			get { return players[(int)PlayerID.Left].ScoreValueDelta; }
		}
		public TrackData LeftDiff {
			get { return LeftPlayer.ActiveDiff; }
			set { LeftPlayer.ActiveDiff = value; }
		}

		public TrackPlayer RightPlayer {
			get { return players[(int)PlayerID.Right]; }
			protected set { players[(int)PlayerID.Right] = value; }
		}
		public Controller RightController {
			get { return RightPlayer.Controller; }
			set { RightPlayer.Controller = value; }
		}

		//RhythmState[] playerValue = new RhythmState[2];
		//RhythmState[] playerValueDelta = new RhythmState[2];

		public RhythmState RhsState {
			get {
				return players[(int)PlayerID.Right].ScoreValue;
			}
		}
		public RhythmState RhsStateDelta {
			get { return players[(int)PlayerID.Right].ScoreValueDelta; }
		}
		public TrackData RightDiff {
			get { return RightPlayer.ActiveDiff; }
			set { RightPlayer.ActiveDiff = value; }
		}

		public int[] ActiveSectionNo {
			get { return activeSectionNo; }
		}

		int[] activeSectionNo = new int[2];

		public ISound Song {
			get { return song; }
		}
		ISound song;
		ISoundEngine soundengine = new ISoundEngine();

		public TrackPlayer[] players = new TrackPlayer[2];

		public LinkedList<TrackSelection> LeftSelectionList {
			get {
				return SelectionList[(int)PlayerID.Left];
			}
			set {
				if (locked)
					throw new InvalidOperationException("Cannot change selection while game is running.");
				SelectionList[(int)PlayerID.Left] = value;
			}
		}
		public LinkedList<TrackSelection> RightSelectionList {
			get {
				return SelectionList[(int)PlayerID.Right];
			}
			set {
				if (locked)
					throw new InvalidOperationException("Cannot change selection while game is running.");
				SelectionList[(int)PlayerID.Right] = value;
			}
		}

		public PlayerID ActivePlayer {
			get;
			protected set;
		}
		public bool Complete {
			get { return !locked; }
		}
		bool locked = false;
		public LinkedList<TrackSelection>[] SelectionList = new LinkedList<TrackSelection>[2];
		LinkedListNode<TrackSelection>[] currentSelection = new LinkedListNode<TrackSelection>[2];

		public bool Switched {
			get;
			protected set;
		}

		public enum RhythmEngineState {
			Stopped,
			Playing,
			Paused
		}

		// Not implemented, needs refactoring
		public enum PlayerID : int {
			Left = 0,
			Right = 1
		}
		public int NoPlayers = 2;

		double timeSinceFadeStep = 0;
		float fadeVolumeInterval = 0.01f;
		float fadeInTimeInterval;
		float fadeOutTimeInterval;
	}
}
