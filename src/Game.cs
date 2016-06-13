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

		public static readonly int width = 7;
		public static readonly int height = 6;

		/** The game board */
		public Board board = new Board(width, height);

		/** Whos turn is it? */
		public int turn = 0;

		public Game(Renderer renderer, List<Player> players): base(renderer) {
			this.players = players;

			/* Create element to display names */
			Element names = new HorizontalSplitElement(
				this.players.Select(player => {
					return new CenterElement(
						new ConditionalWrapElement(
							() => currentPlayer().Equals(player),
							new BackgroundColorElement(ConsoleColor.DarkGray),
							player
						)
					);
				}).ToArray()
			);

			/* Center and align board and waiting row */
			Element boardGrid = new CenterElement(new VerticalFloatElement(new Element[] {
				new DynamicElement<GridElement>(() => currentPlayer().waitingGrid),
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
		
		public override void render() {
			base.render();

			/* Print frametimings for debug purposes */
			Console.ResetColor();
			Console.SetCursorPosition(0, 0);
			Console.Write(renderer.lastFrameTime.ToString());
		}

		/**
		 * Focus method to handle keypresses.
		 * Returns the games state once the game is done
		 */
		public Board.Status focus() {
			/* Render the board */
			this.render();

			/* Main game loop */
			while(true) {
				/* Check if somebody won, or the game is over. If so, return */
				if(this.board.status().done) {
					break;
				}

				this.insert(currentPlayer().act(board.toArray()));
			}

			/* Return status so we can handle the winner in the Main() function */
			return this.board.status();
		}
	}
}
