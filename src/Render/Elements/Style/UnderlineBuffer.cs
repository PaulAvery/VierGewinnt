namespace VierGewinnt.Render {
	public class UnderlineBuffer: WrapBuffer {
		public UnderlineBuffer(Buffer child, int width, int height, Buffer parent = null, int offsetX = 0, int offsetY = 0): base(child, width, height, parent, offsetX, offsetY) {
		}

		public override void set(int x, int y, TerminalCharacter character) {
			character.underline = true;

			this.child.set(x, y, character);
		}
	}
}
