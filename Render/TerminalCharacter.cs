using System;

namespace VierGewinnt.Render {
	public struct TerminalCharacter {
		public char value;
		public ConsoleColor ?foreground;
		public ConsoleColor ?background;

		public TerminalCharacter(char value, ConsoleColor ?foreground = null, ConsoleColor ?background = null) {
			this.value = value;
			this.foreground = foreground;
			this.background = background;
		}
	}
}
