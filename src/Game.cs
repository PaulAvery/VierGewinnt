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
	}
}
