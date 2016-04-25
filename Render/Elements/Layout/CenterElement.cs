namespace VierGewinnt.Render {
	/* Element which centers its child element horizontally and vertically */
	public class CenterElement: WrapElement {
		public CenterElement(Element child = null): base(child) {}

		public override void draw(Buffer canvas) {
			Buffer innerBuffer = new Buffer(canvas.width, canvas.height);
			child.draw(innerBuffer);

			int offsetX = (canvas.width - (innerBuffer.maxX - innerBuffer.minX)) / 2;
			int offsetY = (canvas.height - (innerBuffer.maxY - innerBuffer.minY)) / 2;

			canvas.copy(innerBuffer, offsetX, offsetY);
		}
	}
}
