namespace VierGewinnt.Render.Elements.Content {
	/** Element to render a grid of TerminalCharacters with borders */
	public class GridElement: Element {
		/**
		 * Char-width of a cell.
		 * 2 borders and 2 spaces
		 */
		private int cellWidth = 4;
		/**
		 * Char-height of a cell.
		 * 2 borders
		 */
		private int cellHeight = 2;
		/** Representation of cells */
		private Element[,] entries;

		/* Width and height are the number of cells the grid will possess */
		public GridElement(int width, int height) {
			this.entries = new Element[width, height];

			for(int x = 0; x < 0; x++) {
				for(int y = 0; y < 0; y++) {
					this.entries[x, y] = new TextElement(" ");
				}
			}
		}

		/** Set a character into a gridcell */
		public void put(int x, int y, Element value) {
			this.entries[x, y] = value;
		}

		/** Draw all characters and borders */
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

					if(this.entries[x, y] != null) {
						this.entries[x, y].draw(new Buffer(1, 1, canvas, x * cellWidth + cellWidth / 2, y * cellHeight + cellHeight / 2));	
					}
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
