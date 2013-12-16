using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Accent.System;
using Accent.System.States;
using Accent.System.IO;
using Accent.Entity;
using Accent.Graphics;
using Accent.Graphics.Particles;
using Accent.Graphics.Particles.Effectors;
using Accent.Rhythm;


namespace Accent {
	public class ActGame : Microsoft.Xna.Framework.Game {
		// ==============================
		// Debug Settings
		// ==============================
		internal bool Debugging = true;
		bool ForceStateChange = false;
		Vector2 DebugOverlayPosition = new Vector2(2, 0);
		Color DebugOverlayColor = new Color(1f, 1f, 1f) * 0.5f;
		int noRhythmKeys = 3;
		// ==============================
		// Properties
		// ==============================

		/// <summary>
		/// Attempts to change the game's state.
		/// </summary>
		void PerformStateChange() {
			bool validSwitch = ForceStateChange;

			OldState = State;
			State = State.PerformStateChange();

			if (State != OldState) {
				// TODO: Start a tween
			}
		}

		public ActGame() {
			// ==============================
			// -- Prepare filesystems
			// ==============================
			Directory.SetCurrentDirectory(@"..\..\..\..\");
			root = Directory.GetCurrentDirectory();
			Console.WriteLine("Current working directory is:");
			Console.WriteLine(root);

			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			// Load configuration files
			config = Configuration.Load(@"master.conf");
			IsMouseVisible = true;

			// Initialize controllers
			keyboardInput = new KeyboardInput();
			leftController = new KeyboardController(noRhythmKeys, keyboardInput, config.IO.Keyboard.Left);
			rightController = new KeyboardController(noRhythmKeys, keyboardInput, config.IO.Keyboard.Right);
		}
		protected override void Initialize() {
			base.Initialize();
		}
		protected override void LoadContent() {
			// ==============================
			// -- Prepare graphic systems
			// ==============================
			spriteBatch = new SpriteBatch(GraphicsDevice);
			particleSystem = new ParticleSystem();
			primitiveRenderer = new PrimitiveRenderer(spriteBatch);

			// ==============================
			// -- Object pre-initialization
			// ==============================
			particle = new Particle(particle1SpriteSheet);
			beam = new Particle(beam1SpriteSheet);

			// ==============================
			// -- Load fonts
			// ==============================
			sysFont = Content.Load<SpriteFont>(@"Fonts\System");
			// ==============================
			// -- Load graphics
			// ==============================
			fighterSpriteSheet.Texture = Content.Load<Texture2D>(@"Sprites\Fighter1");
			particle1SpriteSheet.Texture = Content.Load<Texture2D>(@"Particles\Particle1");
			beam1SpriteSheet.Texture = Content.Load<Texture2D>(@"Particles\Beam1");
			bulletSpriteSheet.Texture = Content.Load<Texture2D>(@"Sprites\bullet");
			// ==============================
			// -- Object post-initialization
			// =============================
			LoadTracks();

			State = new TitleState(this, sysFont);
		}
		void LoadTracks() {
			string[] trackDirectories = Directory.GetFileSystemEntries(root + @"\Tracks");
			tracks = new List<Track>(trackDirectories.Count());
			for (int i = 0; i < trackDirectories.Count(); ++i)
				tracks.Add(Track.LoadTrackData(trackDirectories[i]));
		}
		protected override void UnloadContent() {
		}
		protected override void Update(GameTime gameTime) {
			UpdateInputDevices();
			State.Update(gameTime);
			PerformStateChange();
			base.Update(gameTime);
		}
		
		void UpdateInputDevices() {
			keyboardInput.Update(Keyboard.GetState());
			// mouseInput.Update(Mouse.GetState());
		}
		protected override void Draw(GameTime gameTime) {
			GraphicsDevice.Clear(Color.Black);
			State.Draw(spriteBatch, primitiveRenderer);
			base.Draw(gameTime);
		}
		
		// ==============================
		// System Variables
		// ==============================

		public Configuration Config {
			get { return config; }
		}

		GameState State;
		GameState OldState;

		Configuration config;
		// ==============================
		// I/O Variables
		// ==============================
		public KeyboardInput KeyboardInput {
			get { return keyboardInput; }
		}
		KeyboardInput keyboardInput;
		// MouseInput mouseInput;
		Controller leftController;
		Controller rightController;

		internal Controller LeftController {
			get { return leftController; }
		}
		internal Controller RightController {
			get { return rightController; }
		}
		string root;
		// ==============================
		// Graphics Variables
		// ==============================
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		internal SpriteFont sysFont;
		PrimitiveRenderer primitiveRenderer;
		ParticleSystem particleSystem;

		internal SpriteSheet fighterSpriteSheet = new SpriteSheet(new Rectangle(0, 0, 64, 64), null);
		internal SpriteSheet particle1SpriteSheet = new SpriteSheet(new Rectangle(0, 0, 4, 4), null);
		SpriteSheet beam1SpriteSheet = new SpriteSheet(new Rectangle(0, 0, 4, 8), null);
		internal SpriteSheet bulletSpriteSheet = new SpriteSheet(new Rectangle(0, 0, 15, 28), null);
		// ==============================
		// Audio Variables
		// ==============================
		List<Track> tracks;
		internal List<Track> Tracks {
			get { return tracks; }
		}

		// ==============================
		// Game Variables
		// ==============================
		List<Bullet> bulletlist = new List<Bullet>();


		// ==============================
		// Debug Variables
		// ==============================
		Particle particle;
		Particle beam;

		NetworkState NetState = NetworkState.Offline;

		enum NetworkState {
			Offline,
			Server,
			Client
		}
	}
}
