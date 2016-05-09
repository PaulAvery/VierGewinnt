using System;

namespace VierGewinnt {
	/* Simple player class which matches a player to a player color */
	public class Player {
		public readonly ConsoleColor color;
		public readonly string name;

		public Player(ConsoleColor color, string name) {
			this.name = name;
			this.color = color;
		}
	}
}
