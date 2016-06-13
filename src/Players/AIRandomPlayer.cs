using System;

namespace VierGewinnt.Players {
	/** An AI player picking moves at random */
	public class AIRandomPlayer: Player {
		public AIRandomPlayer(int width): base("Random AI", width) {}

		public override int act(Coin[,] board) {
			/* Get a random column in the board */
			return new Random().Next(0, board.GetLength(1) - 1);
		}
	}
}
