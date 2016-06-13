using System.Collections.Generic;

namespace VierGewinnt.Render.Elements.Base {
	/** Base class to wrap multiple elements in another element */
	public class WrapMultipleElement: Element {
		/** The list of children */
		public ICollection<Element> children;

		public WrapMultipleElement(ICollection<Element> children) {
			this.children = children;
		}

		/** Draw all children on top of each other */
		public override void draw(Buffer canvas) {
			foreach(Element child in this.children) {
				child.draw(canvas);
			}
		}
	}
}
