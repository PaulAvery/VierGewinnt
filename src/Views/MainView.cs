using System;
using System.Linq;
using VierGewinnt.Render;

namespace VierGewinnt.Views {
	/* The games main view, rendering the active player as well as the board */
	public class MainView: ViewElement {
		/* The game object */
		private Game game;

		/* The elements we have to modify in .draw() */
		private GridElement grid;
		private GridElement waitingGrid;

		public MainView(Renderer renderer, Game game): base(renderer) {
			this.game = game;
			game.board.player = game.players[0];

			/* Create main elements with correct sizes */
			this.grid = new GridElement(game.board.width, game.board.height);
			this.waitingGrid = new GridElement(game.board.width, 1);

			/* Create element to display names */
			Element names = new HorizontalSplitElement(
				game.players.Select(player => {
					return new ColorElement(player.color,
						new CenterElement(
							new ConditionalWrapElement(
								() => game.board.player.Equals(player),
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
				this.grid
			}));

			/* Add all elements to ourselves */
			setChild(new VerticalFloatElement(new Element[] {
				new FixedHeightElement(5, names),
				boardGrid
			}));
		}

		public override void draw(Render.Buffer canvas) {
			for(int x = 0; x < game.board.width; x++) {
				/* Draw waiting coin */
				if(game.board.waiting == x) {
					waitingGrid.put(x, 0, new TerminalCharacter('●', game.players[game.turn].color));
				} else {
					waitingGrid.put(x, 0, new TerminalCharacter(' '));
				}

				for(int y = 0; y < game.board.height; y++) {
					Coin coin = game.board.getPosition(x, y);

					/* Draw each available coin to the grid */
					if(coin == null) {
						grid.put(x, game.board.height - y - 1, new TerminalCharacter(' '));
					} else {
						TerminalCharacter character = new TerminalCharacter('●', coin.won ? ConsoleColor.White : coin.player.color);
						grid.put(x, game.board.height - y - 1, character);
					}
				}
			}

			base.draw(canvas);
		}

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

		/* Handle user input while game is running */
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
		private void moveLeft() {
			game.board.selectPrevious();
		}

		private void moveRight() {
			game.board.selectNext();
		}

		private void insertCoin() {
			if(game.board.insert()) {
				game.turn++;
				if(game.turn >= game.players.Count) {
					game.turn = 0;
				}

				game.board.player = game.players[game.turn];
			}
		}
	}
}
