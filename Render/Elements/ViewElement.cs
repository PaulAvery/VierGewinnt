namespace VierGewinnt.Render {
	/*
	 * Base element for a view. Instead of further bubbling up the render()
	 * call it tries to render itself into the provided renderer
	 */
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
