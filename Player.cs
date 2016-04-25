using System;

namespace VierGewinnt {
	public struct Player {
		public readonly ConsoleColor color;
		public readonly string name;
		public bool active;

		public Player(ConsoleColor color, string name) {
			this.name = name;
			this.color = color;
			this.active = false;
		}
	}
}
