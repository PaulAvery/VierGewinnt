namespace VierGewinnt.Render.Elements.Content {
	/** Element to render a block of text */
	public class TextElement: Element {
		/** The text to print */
		public string text;

		public TextElement(string text = "") {
			this.text = text;
		}

		/**
		 * Draw text.
		 * Breaks primitively at end of buffer
		 */
		public override void draw(Buffer canvas) {
			int position = 0;

			/* Loop line-by-line and cell-by-cell to allow linebreaks */
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
