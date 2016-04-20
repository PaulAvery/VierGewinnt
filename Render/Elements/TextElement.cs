namespace VierGewinnt.Render {
	public class TextElement: Element {
		public string text;

		public TextElement(string text = "") {
			this.text = text;
		}

		public override void draw(Buffer canvas) {
			int position = 0;
			for(int y = 0; y < canvas.height; y++) {
				for(int x = 0; x < canvas.width; x++) {
					if(position < this.text.Length) {
						canvas.set(x, y, new TerminalCharacter(this.text[position]));
					}

					position++;
				}
			}
		}
	}
}
