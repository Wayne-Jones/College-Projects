using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Accent.System.IO;

namespace Accent.Rhythm {
	public class TrackData {
		public string Name {
			get;
			protected set;
		}
		/// <summary>
		/// Number of keys in the track.
		/// </summary>
		public int Keys {
			get;
			protected set;
		}
		public List<TrackSection> Sections {
			get { return sections; }
		}
		List<TrackSection> sections;

		TrackData(string name, int noKeys) {
			Name = name;
			Keys = noKeys;
		}
		public static TrackData LoadTrackFromFile(string fileName) {
			StreamReader stream = File.OpenText(fileName);
			TrackData data;
			string[] tokens;
			char[] splitChar = { ':' };
			//try {
			tokens = stream.ReadLine().Split(splitChar, 2, StringSplitOptions.None);
			string name = tokens[1];

			// Read key data
			tokens = stream.ReadLine().Split(splitChar);
			int noKeys = int.Parse(tokens[1]);
			Console.WriteLine(String.Format("Using {0} key(s).", noKeys));

			data = new TrackData(name, noKeys);

			tokens = stream.ReadLine().Split(splitChar);
			data.sections = new List<TrackSection>(int.Parse(tokens[1]));
			Console.WriteLine(String.Format("Loading {0} section(s)...", data.sections.Capacity));

			// Read note data
			// Temporary holder for held notes
			Note[] holdNotes = new Note[data.Keys];

			for (string line; !stream.EndOfStream; ) {
				if ((char)stream.Peek() == '[')
					data.sections.Add(TrackSection.LoadTrackSection(stream, noKeys));
				else 
					line = stream.ReadLine();
			}
			for (int i = 0; i < data.Keys; ++i)
				Console.WriteLine(String.Format("[{0}]: {1} notes.", i, data.Sections.Last().Notes[i].Count));
			//} catch {
			//    Console.Error.WriteLine("Corrupt file!");
			//    data = new TrackData(0);
			//} finally {
			stream.Close();
			//}

			return data;
		}

		public LinkedList<Note> GetNotesInRange(int sectionNo, int key, int begin, int end) {
			LinkedList<Note> list = new LinkedList<Note>();
			LinkedListNode<Note> item = sections[sectionNo].Notes[key].First;
			while (item != null && item.Value.Time + item.Value.Length < begin)
				item = item.Next;
			for (; item != null && item.Value.Time < end; item = item.Next)
				list.AddLast(item.Value);
			return list;
		}
	}

	public class TrackSection {
		TrackSection(int beginTime) {
			Begin = beginTime;
		}

		public void initNoteLists(int keys) {
			notes = new LinkedList<Note>[keys];
			for (int i = 0; i < keys; ++i)
				notes[i] = new LinkedList<Note>();
		}

		public static TrackSection LoadTrackSection(StreamReader stream, int keys) {
			TrackSection section;
			try {
				string line = stream.ReadLine();
				int noChrs = line.Count();

				section = new TrackSection(int.Parse(line.Substring(1, noChrs - 2)));
				section.initNoteLists(keys);

				Console.WriteLine(String.Format("Begin section at: {0}ms.", section.Begin));
				string[] tokens;

				// Init temp array to test for non-terminated hold notes
				int[] holdNoteStartTimes = new int[3];
				for (int i = 0; i < holdNoteStartTimes.Length; ++i)
					holdNoteStartTimes[i] = InvalidTime;

				while (!stream.EndOfStream) {
					if ((char)stream.Peek() == ']') {
						// End of a track section
						stream.ReadLine();
						break;
					}
					line = stream.ReadLine();
					tokens = line.Split(',');

					// Get time
					int time = int.Parse(tokens[0]);
					Console.Write(String.Format("Time: {0};", time));

					// Extract state
					for (int i = 0; i < keys; ++i) {
						KeyState stateID = (KeyState)int.Parse(tokens[i + 1]);
						switch (stateID) {
							case KeyState.Tap:
								section.notes[i].AddLast(new Note(time));
								break;
							case KeyState.Hold:
								holdNoteStartTimes[i] = time;
								break;
							case KeyState.Release:
								section.notes[i].AddLast(new Note(holdNoteStartTimes[i], time - holdNoteStartTimes[i]));
								holdNoteStartTimes[i] = InvalidTime;
								break;
						}
						section.End = time;
						Console.Write(String.Format(" {0}", stateID));
					}
					Console.WriteLine();
				}
				bool holdsLeft = false;
				for (int i = 0; !holdsLeft && i < keys; ++i)
					holdsLeft |= holdNoteStartTimes[i] != InvalidTime;
				if (holdsLeft)
					throw new InvalidDataException("Corrupt section!");
			} catch (Exception e) {
				Console.Error.WriteLine(e.Message);
				section = null;
			}
			return section;
		}

		public int Begin {
			get;
			protected set;
		}
		public int End {
			get;
			protected set;
		}
		public LinkedList<Note>[] Notes {
			get { return notes; }
		}
		LinkedList<Note>[] notes;
		// Sentinel value for invalid time.
		static int InvalidTime = -1;
	}
}
