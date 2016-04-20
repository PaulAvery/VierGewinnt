using System;

namespace VierGewinnt.Render {
	/*
	 * A representation of a character to be printed to the terminal.
	 * It has a char value and optional foreground and background colors
	 */
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
