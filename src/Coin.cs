using System;
using VierGewinnt.Render;

namespace VierGewinnt {
	/** Simple representation of a single coin/chip */
	public class Coin: Element {
		/** Player this coin belongs to */
		public readonly Player player;
		/** Is this coin part of the winning coinset? */
		public bool won;

		public Coin(Player player) {
			this.player = player;
			this.won = false;
		}

		public override void draw(Render.Buffer canvas) {
			canvas.set(0, 0, new TerminalCharacter('●', this.won ? ConsoleColor.White : this.player.getColor()));
		}
	}
}
