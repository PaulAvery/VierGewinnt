namespace VierGewinnt.Render {
	public class TextBuffer: Buffer {
		public string text;

		public TextBuffer(string text = "") {
			this.text = text;
		}

		public override void draw(TerminalCharacter[,] canvas) {
			int width = canvas.GetLength(0);
			int height = canvas.GetLength(1);

			int position = 0;
			for(int y = 0; y < height; y++) {
				for(int x = 0; x < width; x++) {
					if(position < this.text.Length) {
						canvas[x, y].value = this.text[position];
					}

					position++;
				}
			}
		}
	}
}
