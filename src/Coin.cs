namespace VierGewinnt {
	/** Simple representation of a single coin/chip */
	public class Coin {
		/** Player this coin belongs to */
		public readonly Player player;
		/** Is this coin part of the winning coinset? */
		public bool won;

		public Coin(Player player) {
			this.player = player;
			this.won = false;
		}
	}
}
