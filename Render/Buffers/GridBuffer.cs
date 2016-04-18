namespace VierGewinnt.Render {
	public class GridBuffer: Buffer {
		private TerminalCharacter[,] entries;

		public GridBuffer(int width, int height) {
			this.entries = new TerminalCharacter[width, height];
		}

		public override void draw(TerminalCharacter[,] canvas) {
			int width = this.entries.GetLength(0);
			int height = this.entries.GetLength(1);

			int targetWidth = canvas.GetLength(0);
			int targetHeight = canvas.GetLength(1);

			int cellSize = 4;

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

					canvas[x, y].value = topLeft;

					for(int innerX = 1; innerX < cellSize; innerX++) {
						canvas[x * cellSize + innerX, y * cellSize].value = '─';
					}

					for(int innerY = 1; innerY < cellSize; innerY++) {
						canvas[x * cellSize, y * cellSize + innerY].value = '│';
					}

					canvas[x * cellSize + cellSize / 2, y * cellSize + cellSize / 2] = this.entries[x, y];
				}

				canvas[width * cellSize + 1, y * cellSize].value = y == 0 ? '┐' : '┤';
				for(int innerY = 1; innerY < cellSize; innerY++) {
					canvas[width * cellSize + 1, y * cellSize + innerY].value = '│';
				}
			}

			for(int x = 0; x < width; x++) {
				canvas[x * cellSize, height * cellSize + 1].value = x == 0 ? '└' : '┴';
				for(int innerX = 1; innerX < cellSize; innerX++) {
					canvas[x * cellSize + innerX, height * cellSize + 1].value = '─';
				}
			}
		}
	}
}
