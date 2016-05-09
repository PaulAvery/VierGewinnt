namespace VierGewinnt.Render {
	public class UnderlineElement: WrapElement {
		public UnderlineElement(Element child = null): base(child) {}

		public override void draw(Buffer canvas) {
			UnderlineBuffer wrappedCanvas = new UnderlineBuffer(canvas, canvas.width, canvas.height);

			child.draw(wrappedCanvas);
		}
	}
}
