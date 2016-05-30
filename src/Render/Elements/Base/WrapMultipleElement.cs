using System.Collections.Generic;

namespace VierGewinnt.Render.Elements.Base {
	/** Base class to wrap multiple elements in another element */
	public class WrapMultipleElement: Element {
		/** The list of children */
		public List<Element> children = new List<Element>();

		public WrapMultipleElement(Element[] children = null) {
			if(children == null) {
				children = new Element[0];
			}

			foreach(Element element in children) {
				this.addChild(element);
			}
		}

		/** Add a child */
		public void addChild(Element child) {
			child.parent = this;
			this.children.Add(child);
		}

		/** Draw all children on top of each other */
		public override void draw(Buffer canvas) {
			foreach(Element child in this.children) {
				child.draw(canvas);
			}
		}
	}
}
