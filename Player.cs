using System;

namespace VierGewinnt {
	public struct Player {
		public readonly ConsoleColor color;
		public readonly string name;

		public Player(ConsoleColor color, string name) {
			this.color = color;
			this.name = name;
		}
	}
}
