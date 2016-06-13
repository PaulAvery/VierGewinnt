namespace VierGewinnt.Render.Elements.Base {
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
			this.child = child;
		}

		/** Call the childs draw() by default */
		public override void draw(Buffer canvas) {
			child.draw(canvas);
		}
	}
}
