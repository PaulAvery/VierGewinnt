namespace VierGewinnt.Render {
	/* Base class to wrap an element in another element */
	public class WrapMultipleElement: Element {
		public Element[] children;

		public WrapMultipleElement(Element[] children) {
			this.children = children;

			for(int i = 0; i < children.Length; i++) {
				this.children[i].parent = this;
			}
		}

		public override void draw(Buffer canvas) {
			for(int i = 0; i < this.children.Length; i++) {
				this.children[i].draw(canvas);
			}
		}
	}
}
