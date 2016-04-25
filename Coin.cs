namespace VierGewinnt {
	public class Coin {
		public readonly Player player;
		public bool won;

		public Coin(Player player) {
			this.player = player;
			this.won = false;
		}
	}
}
