namespace VierGewinnt.Render {
	/*
	 * Base class to wrap a buffer inside of another buffer.
	 * Useful to add properties to set TerminalCharacters etc.
	 */
	public class WrapBuffer: Buffer {
		protected Buffer child;

		public WrapBuffer(Buffer child, int width, int height, Buffer parent = null, int offsetX = 0, int offsetY = 0): base(width, height, parent, offsetX, offsetY) {

			this.child = child;
		}

		public override void set(int x, int y, TerminalCharacter character) {
			this.child.set(x, y, character);
		}

		public override TerminalCharacter get(int x, int y) {
			return this.child.get(x, y);
		}
	}
}
