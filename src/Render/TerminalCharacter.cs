using System;

namespace VierGewinnt.Render {
	/**
	 * A representation of a character to be printed to the terminal.
	 * It has a char value and optional foreground and background colors
	 */
	public struct TerminalCharacter {
		/** Char to be represented */
		public char value;
		/** Foreground color */
		public ConsoleColor ?foreground;
		/** Background color */
		public ConsoleColor ?background;
		/**
		 * Should this character be underlined?
		 * Currently not used
		 */
		public bool underline;

		public TerminalCharacter(char value, ConsoleColor ?foreground = null, ConsoleColor ?background = null, bool underline = false) {
			this.value = value;
			this.underline = underline;
			this.foreground = foreground;
			this.background = background;
		}
	}
}
