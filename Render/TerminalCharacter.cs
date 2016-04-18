using System;

namespace VierGewinnt.Render {
	public struct TerminalCharacter {
		public char value;
		public ConsoleColor foreground;
		public ConsoleColor background;

		public TerminalCharacter(char value, ConsoleColor foreground = ConsoleColor.Black, ConsoleColor background = ConsoleColor.White) {
			this.value = value;
			this.foreground = foreground;
			this.background = background;
		}
	}
}
