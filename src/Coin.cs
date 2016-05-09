namespace VierGewinnt {
	/* Simple class to represent a single coin which will be inserted into the board */
	public class Coin {
		public readonly Player player;
		public bool won;

		public Coin(Player player) {
			this.player = player;
			this.won = false;
		}
	}
}
