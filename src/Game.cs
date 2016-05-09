using System.Collections.Generic;

namespace VierGewinnt {
	/* Class which represents the game and its internal state */
	public class Game {
		/* The list of players */
		public List<Player> players = new List<Player>();

		/* The game board */
		public Board board = new Board(7, 6);

		/* Whos turn is it? */
		public int turn = 0;

		public Game(List<Player> players) {
			this.players = players;
		}

		/* Get the player whos turn it is */
		public Player currentPlayer() {
			return this.players[turn];
		}

		/* Insert coin of current player into board */
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
	}
}
