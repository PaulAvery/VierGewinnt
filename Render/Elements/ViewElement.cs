namespace VierGewinnt.Render {
	public class ViewElement: WrapElement {
		public Renderer renderer;

		public ViewElement(Renderer renderer) {
			this.renderer = renderer;
		}

		public override void render() {
			this.renderer.render(this);
		}
	}
}
