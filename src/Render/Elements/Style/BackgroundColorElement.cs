using System;
using VierGewinnt.Render.Elements.Base;
using VierGewinnt.Render.Buffers.Style;

namespace VierGewinnt.Render.Elements.Style {
	/** An element to set a fixed background color on a child element */
	public class  BackgroundColorElement: WrapElement {
		private ConsoleColor color;

		public  BackgroundColorElement(ConsoleColor color, Element child = null): base(child) {
			this.color = color;
		}

		public override void draw(Buffer canvas) {
			 BackgroundColorBuffer wrappedCanvas = new  BackgroundColorBuffer(this.color, canvas, canvas.width, canvas.height);

			child.draw(wrappedCanvas);
		}
	}
}
