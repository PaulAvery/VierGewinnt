using System;

namespace VierGewinnt {
	public class Player {
		public readonly ConsoleColor color;
		public readonly string name;

		public Player(ConsoleColor color, string name) {
			this.name = name;
			this.color = color;
		}
	}
}
