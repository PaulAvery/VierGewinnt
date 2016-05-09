using System;
using System.Linq;
using System.Collections.Generic;
using VierGewinnt.Render;

namespace VierGewinnt.Views {
	/* The games main view, rendering the active player as well as the board */
	public class MainView: ViewElement {
		/* Whos turn is it? */
		private int turn = 0;

		/* The game board */
		private Board board = new Board(7, 6);

		/* The list of players */
		private List<Player> players;

		/* The elements we have to modify in .draw() */
		private GridElement grid;
		private GridElement waitingGrid;

		public MainView(Renderer renderer, List<Player> players): base(renderer) {
			this.players = players;
			this.board.player = players[0];

			/* Create main elements with correct sizes */
			this.grid = new GridElement(board.width, board.height);
			this.waitingGrid = new GridElement(board.width, 1);

			/* Create element to display names */
			Element names = new HorizontalSplitElement(
				players.Select(player => {
					return new ColorElement(player.color,
						new CenterElement(
							new ConditionalWrapElement(
								() => board.player.Equals(player),
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
			for(int x = 0; x < board.width; x++) {
				/* Draw waiting coin */
				if(board.waiting == x) {
					waitingGrid.put(x, 0, new TerminalCharacter('●', players[turn].color));
				} else {
					waitingGrid.put(x, 0, new TerminalCharacter(' '));
				}

				for(int y = 0; y < board.height; y++) {
					Coin coin = board.getPosition(x, y);

					/* Draw each available coin to the grid */
					if(coin == null) {
						grid.put(x, board.height - y - 1, new TerminalCharacter(' '));
					} else {
						TerminalCharacter character = new TerminalCharacter('●', coin.won ? ConsoleColor.White : coin.player.color);
						grid.put(x, board.height - y - 1, character);
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
				if(board.status().done) {
					break;
				}

				/* Wait for user input */
				handleKey(Console.ReadKey().Key);
			}

			/* Return status so we can handle the winner in the Main() function */
			return board.status();
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
			board.selectPrevious();
		}

		private void moveRight() {
			board.selectNext();
		}

		private void insertCoin() {
			if(board.insert()) {
				turn++;
				if(turn >= players.Count) {
					turn = 0;
				}

				board.player = players[turn];
			}
		}
	}
}
