using System;
using System.Linq;
using System.Collections.Generic;

using VierGewinnt.Render.Elements.Style;
using VierGewinnt.Render.Elements.Content;

namespace VierGewinnt {
	/** Simple representation of a player */
	public abstract class Player: ColorElement {
		/**
		 * All available colors
		 */
		private static Stack<ConsoleColor> colors = new Stack<ConsoleColor>(
			((ConsoleColor[]) Enum.GetValues(typeof(ConsoleColor))).Where(
				color => color != ConsoleColor.Black && color != ConsoleColor.White
			)
		);

		/**
		 * The grid element for the waiting coin
		 */
		public GridElement waitingGrid;

		/** Widht of the waitingGrid */
		protected int width;

		public Player(string name, int width): base(colors.Pop(), new TextElement(name)) {
			this.width = width;
			this.waitingGrid = new GridElement(width, 1);
		}

		/** Returns the players color */
		public ConsoleColor getColor() {
			return this.color;
		}

		/** Passes focus to the player and allows it to act */
		public abstract int act(Coin[,] board);
	}
}
