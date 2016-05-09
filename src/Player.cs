using System;

namespace VierGewinnt {
	/** Simple representation of a player */
	public class Player {
		/**
		 * Players color
		 * This will be used in rendering so it is a console color
		 */
		public readonly ConsoleColor color;
		/** Player name */
		public readonly string name;

		public Player(ConsoleColor color, string name) {
			this.name = name;
			this.color = color;
		}
	}
}
