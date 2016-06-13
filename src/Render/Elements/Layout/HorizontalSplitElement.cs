using System.Collections.Generic;
using VierGewinnt.Render.Elements.Base;

namespace VierGewinnt.Render.Elements.Layout {
	/* Element which spaces out its childs evenly */
	public class HorizontalSplitElement: WrapMultipleElement {
		public HorizontalSplitElement(ICollection<Element> children): base(children) {}

		public override void draw(Buffer canvas) {
			int width = canvas.width / this.children.Count;

			int i = 0;
			foreach(Element child in this.children) {
				child.draw(canvas.view(i * width, 0, width, canvas.height));
				i++;
			}
		}
	}
}
