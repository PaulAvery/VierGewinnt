using System;
using System.Linq;
using VierGewinnt.Render;

namespace VierGewinnt.Views {
	/**
	 * The games main view.
	 * It renders the active player as well as the board
	 */
	public class MainView: ViewElement {
		/** The game object */
		private Game game;

		/** Position of the waiting coin */
		public int waiting;

		/**
		 * The grid element for the waiting coin
		 * We save it, because we have to modify in .draw()
		 */
		private GridElement waitingGrid;

		public MainView(Renderer renderer, Game game): base(renderer) {
			this.game = game;

			/* Create main elements with correct sizes */
			this.waitingGrid = new GridElement(game.board.width, 1);

			/* Create element to display names */
			Element names = new HorizontalSplitElement(
				game.players.Select(player => {
					return new ColorElement(player.color,
						new CenterElement(
							new ConditionalWrapElement(
								() => game.currentPlayer().Equals(player),
								new BackgroundColorElement(ConsoleColor.DarkGray),
								new TextElement(
									player.name
								)
							)
						)
					);
				}).ToArray()
			);

			/* Center and align board and waiting row */
			Element boardGrid = new CenterElement(new VerticalFloatElement(new Element[] {
				this.waitingGrid,
				game.board
			}));

			/* Add all elements to ourselves */
			setChild(new VerticalFloatElement(new Element[] {
				new FixedHeightElement(5, names),
				boardGrid
			}));
		}

		/** Override drawing function to set all neccessary data in elements */
		public override void draw(Render.Buffer canvas) {
			for(int x = 0; x < game.board.width; x++) {
				/* Draw waiting coin */
				if(this.waiting == x) {
					waitingGrid.put(x, 0, () => new TerminalCharacter('●', game.players[game.turn].color));
				} else {
					waitingGrid.put(x, 0, () => new TerminalCharacter(' '));
				}
			}

			base.draw(canvas);
		}

		/**
		 * Focus method to handle keypresses.
		 * Returns the games state once the game is done
		 */
		public Board.Status focus() {
			/* Main game loop */
			while(true) {
				/* Render the board */
				render();

				/* Print frametimings for debug purposes */
				Console.ResetColor();
				Console.SetCursorPosition(0, 0);
				Console.Write(renderer.lastFrameTime.ToString());

				/* Check if somebody won, or the game is over. If so, return */
				if(game.board.status().done) {
					break;
				}

				/* Wait for user input */
				handleKey(Console.ReadKey().Key);
			}

			/* Return status so we can handle the winner in the Main() function */
			return game.board.status();
		}

		/** Handle user input while game is running */
		private void handleKey(ConsoleKey key) {
			switch(key) {
				case ConsoleKey.LeftArrow:
					moveLeft();
					break;
				case ConsoleKey.RightArrow:
					moveRight();
					break;
				case ConsoleKey.Enter:
					insertCoin();
					break;
			}
		}

		/**********************************************************************
		 * Key handlers
		 **********************************************************************/
		/** Select previous column */
		private void moveLeft() {
			if(this.waiting == 0) {
				this.waiting = this.game.board.width - 1;
			} else {
				this.waiting--;
			}
		}

		/** Select next column */
		private void moveRight() {
			if(this.waiting == this.game.board.width - 1) {
				this.waiting = 0;
			} else {
				this.waiting++;
			}
		}

		/** Insert coin into currently selected column */
		private void insertCoin() {
			game.insert(this.waiting);
		}
	}
}
