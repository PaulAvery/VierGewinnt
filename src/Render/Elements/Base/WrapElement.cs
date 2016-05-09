namespace VierGewinnt.Render {
	/** Base class to wrap an element in another element */
	public class WrapElement: Element {
		/** The child element */
		public Element child;

		public WrapElement(Element child = null) {
			if(child != null) {
				this.setChild(child);
			}
		}

		/**
		 * Sets the child element.
		 * Properly assigns its parent
		 */
		public void setChild(Element child) {
			if(this.child != null) {
				this.child.parent = null;
			}

			this.child = child;
			child.parent = this;
		}

		/** Call the childs draw() by default */
		public override void draw(Buffer canvas) {
			child.draw(canvas);
		}
	}
}
