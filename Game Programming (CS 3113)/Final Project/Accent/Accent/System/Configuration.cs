using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Input;

namespace Accent.System {
	public class Configuration {
		// ==============================
		// Properties
		// ==============================
		public GraphicsConfig Graphics = new GraphicsConfig();
		public AudioConfig Audio;
		public InterfaceConfig Interface;
		public IOConfig IO = new IOConfig();
		public GameConfig Game = new GameConfig();
		static char[] propertyDelimiter = { '.' };
		static char[] assignmentDelimiter = { ' ', '=' };
		// ==============================
		// Methods
		// ==============================
		public static Configuration Load(string filename) {
			Configuration config = new Configuration();
			StreamReader file = File.OpenText(filename);
			// Begin reading
			Console.WriteLine("Loading configuration file.");
			//try {
			for (String str = file.ReadLine(); str != null; str = file.ReadLine()) {
				if (str.Length > 0) {
					String[] tokens = str.Split(assignmentDelimiter, StringSplitOptions.RemoveEmptyEntries);

					LinkedList<String> descriptionList = new LinkedList<string>(tokens[0].Split(propertyDelimiter, StringSplitOptions.RemoveEmptyEntries));
					String value = tokens[1];

					config.ParseConfigLine(descriptionList, value);
				}
			}

			//} catch {
			//    Console.Error.WriteLine("Corrupt config file!");
			//} finally {
			file.Close();
			//}
			return config;
		}
		void ParseConfigLine(LinkedList<String> descriptionList, string value) {
			// Value belongs somewhere else
			String description = descriptionList.First.Value;
			Console.Write(description);
			descriptionList.RemoveFirst();

			switch (description) {
				case "graphics":
					Graphics.ParseConfigLine(descriptionList, value);
					break;
				case "io":
					IO.ParseConfigLine(descriptionList, value);
					break;
				case "game":
					Game.ParseConfigLine(descriptionList, value);
					break;
				default:
					break;
			}
		}

		public class GraphicsConfig {
			public void ParseConfigLine(LinkedList<String> descriptionList, string value) {
				String description = descriptionList.First.Value;
				Console.Write('.' + description);
				descriptionList.RemoveFirst();
				if (descriptionList.Count == 0)
					Console.WriteLine(" = " + value);
				switch (description) {
					case "window":
						Window.ParseConfigLine(descriptionList, value);
						break;
				}
			}

			public WindowConfig Window = new WindowConfig();

			public class WindowConfig {
				public void ParseConfigLine(LinkedList<String> descriptionList, string value) {
					String description = descriptionList.First.Value;
					Console.Write('.' + description);
					descriptionList.RemoveFirst();
					if (descriptionList.Count == 0)
						Console.WriteLine(" = " + value);
					switch (description) {
						case "width":
							Width = int.Parse(value);
							break;
						case "height":
							Height = int.Parse(value);
							break;
					}
				}

				public int Width;
				public int Height;
			}
		}
		public class AudioConfig {
			
		}
		public class InterfaceConfig {
			

		}
		public class IOConfig {
			public void ParseConfigLine(LinkedList<String> descriptionList, string value) {
				String description = descriptionList.First.Value;
				Console.Write('.' + description);
				descriptionList.RemoveFirst();

				switch (description) {
					case "keyboard":
						Keyboard.ParseConfigLine(descriptionList, value);
						break;
					default:
						break;
				}
			}

			public KeyboardConfig Keyboard = new KeyboardConfig();
			public MouseConfig Mouse;

			public class KeyboardConfig {
				int noPlayers = 2;
				public PlayerKeyConfig[] Players;

				public PlayerKeyConfig Left {
					get { return Players[0]; }
					set { Players[0] = value; }
				}
				public PlayerKeyConfig Right {
					get { return Players[1]; }
					set { Players[1] = value; }
				}

				// Methods
				public KeyboardConfig() {
					Players = new PlayerKeyConfig[noPlayers];
					for (int i = 0; i < noPlayers; ++i)
						Players[i] = new PlayerKeyConfig();
				}
				public void ParseConfigLine(LinkedList<String> descriptionList, string value) {
					String description = descriptionList.First.Value;
					Console.Write('.' + description);
					descriptionList.RemoveFirst();

					switch (description) {
						case "left":
							Left.ParseConfigLine(descriptionList, value);
							break;
						case "right":
							Right.ParseConfigLine(descriptionList, value);
							break;
						default:
							break;
					}
				}

				public class PlayerKeyConfig {
					public void ParseConfigLine(LinkedList<String> descriptionList, string value) {
						String description = descriptionList.First.Value;
						Console.Write('.' + description);
						descriptionList.RemoveFirst();

						switch (description) {
							case "movement":
								Movement.ParseConfigLine(descriptionList, value);
								break;
							case "rhythm":
								Rhythm.ParseConfigLine(descriptionList, value);
								break;
							default:
								break;
						}
					}

					public MovementKeyConfig Movement = new MovementKeyConfig();
					public RhythmKeyConfig Rhythm = new RhythmKeyConfig();

					public class MovementKeyConfig {
						public void ParseConfigLine(LinkedList<String> descriptionList, string value) {
							String description = descriptionList.First.Value;
							Console.Write('.' + description);
							descriptionList.RemoveFirst();
							if (descriptionList.Count == 0)
								Console.WriteLine(" = " + value);
							switch (description) {
								case "up":
									Up = (Keys)int.Parse(value);
									break;
								case "down":
									Down = (Keys)int.Parse(value);
									break;
								case "left":
									Left = (Keys)int.Parse(value);
									break;
								case "right":
									Right = (Keys)int.Parse(value);
									break;
								case "counterclockwise":
									CCW = (Keys)int.Parse(value);
									break;
								case "clockwise":
									CW = (Keys)int.Parse(value);
									break;
								default:
									break;
							}
						}

						public Keys Up {
							get { return KeyList[0]; }
							set { KeyList[0] = value; }
						}
						public Keys Down {
							get { return KeyList[1]; }
							set { KeyList[1] = value; }
						}
						public Keys Left {
							get { return KeyList[2]; }
							set { KeyList[2] = value; }
						}
						public Keys Right {
							get { return KeyList[3]; }
							set { KeyList[3] = value; }
						}
						public Keys CCW {
							get { return KeyList[4]; }
							set { KeyList[4] = value; }
						}
						public Keys CW {
							get { return KeyList[5]; }
							set { KeyList[5] = value; }
						}

						public Keys[] KeyList {
							get { return keyList; }
							protected set {
								keyList = value;
							}
						}
							
						Keys[] keyList = new Keys[6];
					}
					public class RhythmKeyConfig {
						public void ParseConfigLine(LinkedList<String> descriptionList, string value) {
							String description = descriptionList.First.Value;
							Console.Write('.' + description);
							descriptionList.RemoveFirst();

							int keyNo;
							if (int.TryParse(description, out keyNo)) {
								if (descriptionList.Count == 0)
									Console.WriteLine(" = " + value);
								InputKeys[keyNo] = (Keys)int.Parse(value);
							} else {
								switch (description) {
									case "keycount":
										Console.WriteLine(" = " + value);
										KeyCount = int.Parse(value);
										InputKeys = new Keys[KeyCount];
										break;
								}
							}
						}

						public int KeyCount;
						public Keys[] InputKeys;
					}
				}
			}
			public class MouseConfig {
				
			}
		}
		public class GameConfig {
			public void ParseConfigLine(LinkedList<String> descriptionList, string value) {
				String description = descriptionList.First.Value;
				Console.Write('.' + description);
				descriptionList.RemoveFirst();

				switch (description) {
					case "track":
						Track.ParseConfigLine(descriptionList, value);
						break;
					default:
						break;
				}
			}

			public TrackConfig Track = new TrackConfig();
			public class TrackConfig {
				public void ParseConfigLine(LinkedList<String> descriptionList, string value) {
					String description = descriptionList.First.Value;
					Console.Write('.' + description);
					descriptionList.RemoveFirst();

					switch (description) {
						case "leadin":
							Console.Write(" = ");
							Console.WriteLine(value);
							LeadIn = double.Parse(value);
							break;
						case "fallout":
							Console.Write(" = ");
							Console.WriteLine(value);
							FallOut = double.Parse(value);
							break;
						default:
							break;
					}
				}
				public double LeadIn;
				public double FallOut;
			}
		}
	}
}
