using System;
using VierGewinnt.Render.Elements.Base;
using VierGewinnt.Render.Buffers.Style;

namespace VierGewinnt.Render.Elements.Style {
	public class ColorElement: WrapElement {
		protected ConsoleColor color;

		public ColorElement(ConsoleColor color, Element child = null): base(child) {
			this.color = color;
		}

		public override void draw(Buffer canvas) {
			ColorBuffer wrappedCanvas = new ColorBuffer(this.color, canvas, canvas.width, canvas.height);

			child.draw(wrappedCanvas);
		}
	}
}
