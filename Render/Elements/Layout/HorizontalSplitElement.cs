namespace VierGewinnt.Render {
	/* Element which spaces out its childs evenly */
	public class HorizontalSplitElement: WrapMultipleElement {
		public HorizontalSplitElement(Element[] children = null): base(children) {}

		public override void draw(Buffer canvas) {
			int width = canvas.width / this.children.Count;

			for(int i = 0; i < this.children.Count; i++) {
				this.children[i].draw(canvas.view(i * width, 0, width, canvas.height));
			}
		}
	}
}
