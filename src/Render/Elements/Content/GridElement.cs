namespace VierGewinnt.Render {
	/* Element to render a grid of TerminalCharacters with borders */
	public class GridElement: Element {
		private int cellWidth = 4;
		private int cellHeight = 2;
		private TerminalCharacter[,] entries;

		/* Width and height are the number of cells the grid will possess */
		public GridElement(int width, int height) {
			this.entries = new TerminalCharacter[width, height];

			for(int x = 0; x < 0; x++) {
				for(int y = 0; y < 0; y++) {
					this.entries[x, y] = new TerminalCharacter(' ');
				}
			}
		}

		/* Set a character into a gridcell */
		public void put(int x, int y, TerminalCharacter value) {
			this.entries[x, y] = value;
		}

		public override void draw(Buffer canvas) {
			int width = this.entries.GetLength(0);
			int height = this.entries.GetLength(1);

			int targetWidth = canvas.width;
			int targetHeight = canvas.height;

			/* Loop through grid cells and put all borders and values */
			for(int y = 0; y < height; y++) {
				for(int x = 0; x < width; x++) {
					char topLeft = '┼';

					if(x == 0 && y == 0) {
						topLeft = '┌';
					} else if(x == 0) {
						topLeft = '├';
					} else if(y == 0) {
						topLeft = '┬';
					}

					canvas.set(x * cellWidth, y * cellHeight, new TerminalCharacter(topLeft));

					for(int innerX = 1; innerX < cellWidth; innerX++) {
						canvas.set(x * cellWidth + innerX, y * cellHeight, new TerminalCharacter('─'));
					}

					for(int innerY = 1; innerY < cellHeight; innerY++) {
						canvas.set(x * cellWidth, y * cellHeight + innerY, new TerminalCharacter('│'));
					}

					canvas.set(x * cellWidth + cellWidth / 2, y * cellHeight + cellHeight / 2, this.entries[x, y]);
				}

				canvas.set(width * cellWidth, y * cellHeight, new TerminalCharacter(y == 0 ? '┐' : '┤'));
				for(int innerY = 1; innerY < cellHeight; innerY++) {
					canvas.set(width * cellWidth, y * cellHeight + innerY, new TerminalCharacter('│'));
				}
			}

			for(int x = 0; x < width; x++) {
				canvas.set(x * cellWidth, height * cellHeight, new TerminalCharacter(x == 0 ? '└' : '┴'));

				for(int innerX = 1; innerX < cellWidth; innerX++) {
					canvas.set(x * cellWidth + innerX, height * cellHeight, new TerminalCharacter('─'));
				}
			}

			canvas.set(width * cellWidth, height * cellHeight, new TerminalCharacter('┘'));
		}
	}
}
