using System;
using System.Linq;
using System.Collections.Generic;

using VierGewinnt.Render.Elements.Style;
using VierGewinnt.Render.Elements.Content;

namespace VierGewinnt {
	/** Simple representation of a player */
	public class Player: ColorElement {
		/**
		 * All available colors
		 */
		private static Stack<ConsoleColor> colors = new Stack<ConsoleColor>(
			((ConsoleColor[]) Enum.GetValues(typeof(ConsoleColor))).Where(
				color => color != ConsoleColor.Black && color != ConsoleColor.White
			)
		);

		public Player(string name): base(colors.Pop(), new TextElement(name)) {}

		/** Returns the players color */
		public ConsoleColor getColor() {
			return this.color;
		}
	}
}
