using VierGewinnt.Render.Elements.Base;

namespace VierGewinnt.Render.Elements.Layout {
	/* Limited the height of an element */
	public class FixedHeightElement: WrapElement {
		private int height;

		public FixedHeightElement(int height, Element child): base(child) {
			this.height = height;
		}

		public override void draw(Buffer canvas) {
			this.child.draw(canvas.view(0, 0, canvas.width, this.height));

			canvas.get(0, this.height - 1);
		}
	}
}
