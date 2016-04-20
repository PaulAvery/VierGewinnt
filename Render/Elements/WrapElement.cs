namespace VierGewinnt.Render {
	public class WrapElement: Element {
		public Element child;

		public WrapElement(Element child = null) {
			if(child != null) {
				this.setChild(child);
			}
		}

		public void setChild(Element child) {
			if(this.child != null) {
				this.child.parent = null;
			}

			this.child = child;
			child.parent = this;
		}

		public override void draw(Buffer canvas) {
			child.draw(canvas);
		}
	}
}
