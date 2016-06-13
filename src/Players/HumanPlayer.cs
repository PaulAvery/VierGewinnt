using System;
using VierGewinnt.Render.Elements.Content;

namespace VierGewinnt.Players {
	/** Simple representation of a player */
	public class HumanPlayer: Player {
		public HumanPlayer(string name, int width): base(name, width) {}

		/** Passes focus to the player and allows it to act */
		public override int act(Coin[,] board) {
			int position = board.GetLength(1) / 2;

			while(true) {
				/* Draw to grid */
				for(int x = 0; x < width; x++) {
					if(x == position) {
						waitingGrid.put(x, 0, new Coin(this));
					} else {
						waitingGrid.put(x, 0, new TextElement(" "));
					}
				}

				this.render();
				
				/* Wait for user input */
				switch(Console.ReadKey().Key) {
					case ConsoleKey.LeftArrow:
						position--;
						if(position < 0) {
							position = width - 1;
						}
						break;

					case ConsoleKey.RightArrow:
						position++;
						if(position >= width) {
							position = 0;
						}
						break;

					case ConsoleKey.Enter:
						return position;
				}
			}
		}
	}
}
