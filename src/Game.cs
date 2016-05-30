using System;
using System.Linq;
using System.Collections.Generic;

using VierGewinnt.Render;
using VierGewinnt.Render.Elements;
using VierGewinnt.Render.Elements.Base;
using VierGewinnt.Render.Elements.Content;
using VierGewinnt.Render.Elements.Layout;
using VierGewinnt.Render.Elements.Style;

namespace VierGewinnt {
	/**
	 * The games main view.
	 * It renders the active player as well as the board
	 */
	public class Game: ViewElement {
		/** The list of players */
		public List<Player> players = new List<Player>();

		/** The game board */
		public Board board = new Board(7, 6);

		/** Whos turn is it? */
		public int turn = 0;

		/** Position of the waiting coin */
		public int waiting;

		/**
		 * The grid element for the waiting coin
		 * We save it, because we have to modify in .draw()
		 */
		private GridElement waitingGrid;

		public Game(Renderer renderer, List<Player> players): base(renderer) {
			this.players = players;

			/* Create main elements with correct sizes */
			this.waitingGrid = new GridElement(this.board.width, 1);

			/* Create element to display names */
			Element names = new HorizontalSplitElement(
				this.players.Select(player => {
					return new ColorElement(player.color,
						new CenterElement(
							new ConditionalWrapElement(
								() => currentPlayer().Equals(player),
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
				this.board
			}));

			/* Add all elements to ourselves */
			setChild(new VerticalFloatElement(new Element[] {
				new FixedHeightElement(5, names),
				boardGrid
			}));
		}

		/** Get the player whos turn it is */
		public Player currentPlayer() {
			return this.players[turn];
		}

		/** Insert coin of current player into board */
		public void insert(int position) {
			bool success = this.board.insert(position, new Coin(currentPlayer()));

			if(success) {
				if(turn == players.Count - 1) {
					turn = 0;
				} else {
					turn++;
				}
			}
		}

		/** Override drawing function to set all neccessary data in elements */
		public override void draw(Render.Buffer canvas) {
			for(int x = 0; x < this.board.width; x++) {
				/* Draw waiting coin */
				if(this.waiting == x) {
					waitingGrid.put(x, 0, () => new TerminalCharacter('●', this.players[this.turn].color));
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
				if(this.board.status().done) {
					break;
				}

				/* Wait for user input */
				handleKey(Console.ReadKey().Key);
			}

			/* Return status so we can handle the winner in the Main() function */
			return this.board.status();
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
				this.waiting = this.board.width - 1;
			} else {
				this.waiting--;
			}
		}

		/** Select next column */
		private void moveRight() {
			if(this.waiting == this.board.width - 1) {
				this.waiting = 0;
			} else {
				this.waiting++;
			}
		}

		/** Insert coin into currently selected column */
		private void insertCoin() {
			this.insert(this.waiting);
		}
	}
}
