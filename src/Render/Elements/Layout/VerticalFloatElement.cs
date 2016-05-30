using VierGewinnt.Render.Elements.Base;

namespace VierGewinnt.Render.Elements.Layout {
	/**
	 * Renders its childrens beneath each other, as tightly spaced as possible
	 */
	public class VerticalFloatElement: WrapMultipleElement {
		public VerticalFloatElement(Element[] children = null): base(children) {}

		public override void draw(Buffer canvas) {
			int top = 0;

			foreach(Element child in this.children) {
				if(top < canvas.height) {
					child.draw(canvas.view(0, top + 1, canvas.width, canvas.height - top - 1));

					top = canvas.maxY;
				}
			}
		}
	}
}
