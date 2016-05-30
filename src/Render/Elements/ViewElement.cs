using VierGewinnt.Render.Elements.Base;

namespace VierGewinnt.Render.Elements {
	/**
	 * Base element for a view.
	 * Instead of further bubbling up the render()
	 * call it tries to render itself into the provided renderer
	 */
	public class ViewElement: WrapElement {
		/** The renderer to draw ourselves into */
		public Renderer renderer;

		public ViewElement(Renderer renderer) {
			this.renderer = renderer;
		}

		/** Override render() to draw it */
		public override void render() {
			this.renderer.render(this);
		}
	}
}
