using System;

namespace VierGewinnt.Render {
	public class ColorElement: WrapElement {
		private ConsoleColor color;

		public ColorElement(ConsoleColor color, Element child = null): base(child) {
			this.color = color;
		}

		public override void draw(Buffer canvas) {
			ColorBuffer wrappedCanvas = new ColorBuffer(this.color, canvas, canvas.width, canvas.height);

			child.draw(wrappedCanvas);
		}
	}
}
