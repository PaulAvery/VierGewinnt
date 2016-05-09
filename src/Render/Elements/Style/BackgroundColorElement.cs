using System;

namespace VierGewinnt.Render {
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
