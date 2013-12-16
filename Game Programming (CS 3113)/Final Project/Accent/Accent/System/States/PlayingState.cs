using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using Accent.AI;
using Accent.Entity;
using Accent.Rhythm;
using Accent.Graphics;
using Accent.System.IO;

namespace Accent.System.States {
	class PlayingState : GameState {
		// Debug settings
		int playerShift = 10;

		PlayerIndex ActivePlayer;
		int noPlayers = 2;

		const int atk = 0;
		const int def = 1;

		Fighter[] Fighters;
		SpriteSheet[] FighterSpriteSheets;

		Vector2[] InitialPositions;
		Rectangle[] ValidRects;

		Bullet bullet;
		LinkedList<Bullet> BulletList = new LinkedList<Bullet>();

		public Controller LeftController {
			get {
				return Re.LeftController;
			}
			set {
				Re.LeftController = value;
				Fighters[0].controller = value;
			}
		}

		public Controller RightController {
			get {
				return Re.RightController;
			}
			set {
				Re.RightController = value;
				Fighters[1].controller = value;
			}
		}


		Configuration.IOConfig.KeyboardConfig.PlayerKeyConfig[] Keyconfigs;
		
		// Engines
		RhythmEngine Re;
		ParticleSystem Ps;
		RhythmRenderer Rr;

		// Methods
		public PlayingState(ActGame Host, Configuration.IOConfig.KeyboardConfig.PlayerKeyConfig LeftControlConfig, Configuration.IOConfig.KeyboardConfig.PlayerKeyConfig RightControlConfig, LinkedList<TrackSelection>[] Selections)
			: base(Host) {
				FighterSpriteSheets = new SpriteSheet[noPlayers];
				FighterSpriteSheets[atk] = Host.fighterSpriteSheet;
				FighterSpriteSheets[def] = Host.fighterSpriteSheet;

				InitialPositions = new Vector2[noPlayers];
				InitialPositions[atk] = new Vector2(400, 400);
				InitialPositions[def] = new Vector2(400, 200);

				ValidRects = new Rectangle[noPlayers];
				ValidRects[atk] = new Rectangle(250, 250, 300, 150);
				ValidRects[def] = new Rectangle(250, 50, 300, 150);

				bullet = new Bullet();
				bullet.Sprite = Host.bulletSpriteSheet.FrameSprite(0);

				Fighters = new Fighter[noPlayers];
				for (int i = 0; i < noPlayers; ++i) {
					Fighters[i] = new Fighter(Host, ValidRects[i], bullet, RegisterBullet);
					Fighters[i].Sprite = FighterSpriteSheets[i].FrameSprite(0);
				}

				Keyconfigs = new Configuration.IOConfig.KeyboardConfig.PlayerKeyConfig[noPlayers];
				Keyconfigs[atk] = LeftControlConfig;
				Keyconfigs[def] = RightControlConfig;

				SetActivePlayer(PlayerIndex.One);

				Ps = new ParticleSystem();

				Re = new RhythmEngine(Host.Config);
				Re.SelectionList = Selections;
				
				Re.LeftPlayer.VisibleRange = 2000;
				Re.RightPlayer.VisibleRange = 2000;

				Re.LeftPlayer.ShowDebug = false;
				Re.RightPlayer.ShowDebug = true;

				Rr = new RhythmRenderer(Re);
				Rr.Font = Host.sysFont;

				Rr.LeftDrawRect = new Rectangle(8, 8, 192, 350);
				Rr.RightDrawRect = new Rectangle(600, 8, 192, 350);

				LeftController = Host.LeftController;
				RightController = Host.RightController;

				// TODO: Debug AI
				//LeftController = new AiController(Host.Config.IO.Keyboard.Left.Rhythm.KeyCount, Fighters[atk], Re.players[atk]);
				RightController = new AiController(Host.Config.IO.Keyboard.Right.Rhythm.KeyCount, Fighters[def], Re.players[def]);

				Re.Lock();
				Re.Play();

				Particle particle = new Particle(Host.particle1SpriteSheet);

				GraphicsObject[] FighterGraphicObjs = new GraphicsObject[noPlayers];
				for (int i = 0; i < noPlayers; ++i) {
					FighterGraphicObjs[i] = new GraphicsObject(Fighters[i]);
				}

				foreach (GraphicsObject obj in FighterGraphicObjs) {
					obj.CreateEmitter(new Vector2(0, 5), 0.15f, -MathHelper.PiOver2, MathHelper.PiOver4, 100, 250, 150, particle, true);
					obj.CreateEmitter(new Vector2(-5, 2), 0.15f, -0.6f * MathHelper.PiOver2, MathHelper.PiOver4 / 2.0f, 50, 250, 150, particle, true);
					obj.CreateEmitter(new Vector2(5, 2), 0.15f, -1.4f * MathHelper.PiOver2, MathHelper.PiOver4 / 2.0f, 50, 250, 150, particle, true);

					Ps.RegisterGraphicsObject(obj);
				}
		}

		void UpdateGraphicsSystem(GameTime gameTime) {
			Ps.Update(gameTime);
		}

		internal void RegisterBullet(Bullet bullet) {
			BulletList.AddLast(bullet);
		}

		void UpdateGameState(GameTime gameTime) {
			// Update rhythm engine
			Re.Update(gameTime);
			Re.CheckState();

			if (Re.Switched) {
				// Switch defender/attacker
				if (ActivePlayer == PlayerIndex.One) {
					SetActivePlayer(PlayerIndex.Two);
				} else {
					SetActivePlayer(PlayerIndex.One);
				}
			}

			// Update fighters
			if (Re.State == RhythmEngine.RhythmEngineState.Playing) {
				for (int i = 0; i < noPlayers; ++i) {
					Fighters[i].Power = Re.players[i].ScoreValue.Health;
					Fighters[i].Update(gameTime);
				}
				updateBullets(gameTime);
			}
		}

		void updateBullets(GameTime gameTime) {
			if (Re.State == RhythmEngine.RhythmEngineState.Playing) {
				Stack<LinkedListNode<Bullet>> removeStack = new Stack<LinkedListNode<Bullet>>();

				for (LinkedListNode<Bullet> node = BulletList.First; node != null; node = node.Next) {
					for (int i = 0; i < noPlayers; ++i) {
						if (node.Value.CheckCollide(Fighters[i])) {
							Fighters[i].Damage += node.Value.Damage;
							removeStack.Push(node);
							node.Value.isAlive = false;
						}
					}
					if (node.Value.isAlive)
						node.Value.Update(gameTime);
				}

				for (LinkedListNode<Bullet> node; removeStack.Count > 0; ) {
					node = removeStack.Pop();
					BulletList.Remove(node);
				}
			}
		}

		void SetActivePlayer(PlayerIndex PlayerNo) {
			Fighter[] fighters = null;
			switch (PlayerNo) {
				case PlayerIndex.One:
					fighters = new Fighter[noPlayers];
					fighters[0] = Fighters[0];
					fighters[1] = Fighters[1];
					break;
				case PlayerIndex.Two:
					fighters = new Fighter[noPlayers];
					fighters[0] = Fighters[1];
					fighters[1] = Fighters[0];
					break;
				default:
					break;
			}
			if (fighters != null) {
				for (int i = 0; i < noPlayers; ++i) {
					fighters[i].CurrentState = 
						(i == 0) ? Fighter.State.Attack : Fighter.State.Defend;
					fighters[i].ValidRect = ValidRects[i];
					fighters[i].Position = InitialPositions[i];
				}
			}
			
		}

		public void HandleInput(GameTime gameTime) {
			if (Re.State == RhythmEngine.RhythmEngineState.Playing)
			{
				LeftController.Update(gameTime);
				RightController.Update(gameTime);
			}
			KeyboardState Current = Host.KeyboardInput.Current;
			if (Host.Debugging) {
				// Rhythm engine
				if (Host.KeyboardInput.IsTap(Keys.OemOpenBrackets)) {
					Re.Stop();
				}
				if (Host.KeyboardInput.IsTap(Keys.OemCloseBrackets)) {
					Re.Play();
				}
				if (Host.KeyboardInput.IsTap(Keys.OemPipe)) {
					Re.Pause();
				}
				if (Current.IsKeyDown(Keys.OemPlus))
				{
					Re.Shift(playerShift);
				}
				if (Current.IsKeyDown(Keys.OemMinus))
				{
					Re.Shift(-playerShift);
				}
			}
		}

		public override void Update(GameTime gameTime) {
			if (Re.Complete) {
				Re.Stop();
				RequestStateChange(GameStateID.Results);
			} else {
				UpdateGameState(gameTime);
				UpdateGraphicsSystem(gameTime);
				if (Re.Switched) {
					if (Re.ActivePlayer == RhythmEngine.PlayerID.Right) {
						SetActivePlayer(PlayerIndex.Two);
					} else {
						SetActivePlayer(PlayerIndex.One);
					}
				} else {
					HandleInput(gameTime);
					for (int i = 0; i < noPlayers; ++i) {
						Fighters[i].Power = Re.players[i].ScoreValue.Health; 
						Fighters[i].Update(gameTime);
					}
				}
			}
		}
		public override void Draw(SpriteBatch Sb, PrimitiveRenderer Pr) {
			Ps.Draw(Sb);

			Sb.Begin();
			// Draw enemy projectiles
			// Draw player projectiles
			foreach (Bullet bullet in BulletList) {
				if (bullet.isAlive)
					bullet.Sprite.Draw(Sb, bullet.position, bullet.Angle, bullet.Scale, Color.White);
			}
			// Draw enemy slaves
			// Draw player slaves

			// Draw fighters
			foreach (Fighter fighter in Fighters) {
				if (fighter.CurrentState == Fighter.State.Defend)
					Pr.DrawCircle(fighter.Position, 50, 1.0f, Color.White * fighter.ShieldStrength);
				fighter.Sprite.Draw(Sb, fighter.Position, fighter.Angle, fighter.Scale, Color.White);
			}
			Sb.End();
			Rr.Draw(Pr, Sb);
		}
		public override GameState PerformStateChange() {
			GameState State = this;
			switch (RequestedState) {
				case GameStateID.Results:
					ScoreReport Left = new ScoreReport();
					Left.Health = Re.LeftPlayer.ScoreValue.Health;
					Left.Score = Re.LeftPlayer.ScoreValue.Score;
					Left.Damage = -Fighters[1].health;

					ScoreReport Right = new ScoreReport();
					Right.Health = Re.RightPlayer.ScoreValue.Health;
					Right.Score = Re.RightPlayer.ScoreValue.Score;
					Right.Damage = -Fighters[0].health;

					State = new ResultsState(Host, Left, Right, Host.sysFont);
					break;
			}
			RequestedState = GameStateID.Invalid;
			return State;
		}
	}

	public struct ScoreReport {
		public string Name;
		public int Score;
		public float Health;
		/// <summary>
		/// Damage received
		/// </summary>
		public float Damage;
	}
}
