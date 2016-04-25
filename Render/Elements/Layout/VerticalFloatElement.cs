namespace VierGewinnt.Render {
	public class VerticalFloatElement: WrapMultipleElement {
		public VerticalFloatElement(Element[] children): base(children) {}

		public override void draw(Buffer canvas) {
			int top = 0;

			for(int i = 0; i < this.children.Length; i++) {
				if(top < canvas.height) {
					this.children[i].draw(canvas.view(0, top, canvas.width, canvas.height));

					top = canvas.maxY;
				}
			}
		}
	}
}
