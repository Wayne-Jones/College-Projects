using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Accent.Rhythm {
	public class Track {
		/// <summary>
		/// Loads a track from the passed in directory. The directory should 
		/// have all the files that is necessary for the loading to complete.
		/// </summary>
		/// <param name="songDirectory">The absolute path to the directory of which all the song definitions and difficulty definitions exist.</param>
		/// <returns></returns>
		static public Track LoadTrackData(string songDirectory) {
			Console.Write("Loading song from directory: ");
			Console.WriteLine(songDirectory);
			char[] splitter = {':'};
			string[] trackFiles = Directory.GetFiles(songDirectory, "*.tdef");
			StreamReader file = File.OpenText(trackFiles[0]);
			Track track = new Track();

			track.FileDirectory = songDirectory;
			// Begin reading file
			for (string[] line; !file.EndOfStream;) {
				// Splits data into two usable portions.
				// [0]: Data type
				// [1]: Data value
				line = file.ReadLine().Split(splitter, 2);
				// Data type
				switch (line[0]) {
					case "Name":
						track.Name = line[1];
						break;
					case "Artist":
						track.Artist = line[1];
						break;
					case "Audio":
						track.AudioFilename = line[1];
						break;
				}
			}

			Queue<string> trackDiffs = new Queue<string>(Directory.GetFiles(songDirectory, "*.tdiff"));
			track.Diffs = new List<TrackData>(trackDiffs.Count());
			for (; trackDiffs.Count > 0; ) {
				string trackDiff = trackDiffs.Dequeue();
				track.Diffs.Add(TrackData.LoadTrackFromFile(trackDiff));
			}

			return track;
		}

		private Track() {}

		public string Name {
			get;
			protected set;
		}

		public string Artist {
			get;
			protected set;
		}
		public string AudioFilename {
			get;
			protected set;
		}
		public string FileDirectory {
			get;
			protected set;
		}

		public string FilePath {
			get { return FileDirectory + @"\" + AudioFilename; }
		}

		public List<TrackData> Diffs;
	}

	public class TrackSelection {
		public TrackSelection(Track track, TrackData diff) {
			Track = track;
			Diff = diff;
		}
		public Track Track;
		public TrackData Diff {
			get { return diff; }
			set {
				if (Track != null && Track.Diffs.Contains(value))
					diff = value;
				else 
					diff = null;
			}
		}

		TrackData diff;
	}
}
