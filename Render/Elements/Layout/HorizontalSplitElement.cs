namespace VierGewinnt.Render {
	/* Element which spaces out its childs evenly */
	public class HorizontalSplitElement: WrapMultipleElement {
		public HorizontalSplitElement(Element[] children): base(children) {}

		public override void draw(Buffer canvas) {
			int width = canvas.width / this.children.Length;

			for(int i = 0; i < this.children.Length; i++) {
				this.children[i].draw(canvas.view(i * width, 0, width, canvas.height));
			}
		}
	}
}
