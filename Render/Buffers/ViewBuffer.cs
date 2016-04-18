namespace VierGewinnt.Render {
	public class ViewBuffer: Buffer {
		public Buffer child;
		public Renderer renderer;

		public ViewBuffer(Renderer renderer, Buffer child) {
			this.renderer = renderer;
			this.child = child;
			child.parent = this;
		}

		public override void render() {
			this.renderer.render(this);
		}

		public override void draw(TerminalCharacter[,] canvas) {
			child.draw(canvas);
		}
	}
}
