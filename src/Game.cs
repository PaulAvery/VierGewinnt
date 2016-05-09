using System.Collections.Generic;

namespace VierGewinnt {
	/* Class which represents the game and its internal state */
	public class Game {
		/* The list of players */
		public List<Player> players = new List<Player>();

		/* The board */
		public

		Game(List<Player> players) {
			this.players = players;
		}
	}
}
