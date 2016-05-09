namespace VierGewinnt.Render {
	/**
	 * A base class to represent an element to render to the terminal.
	 */
	public abstract class Element {
		public Element parent;

		/**
		 * Simply chain upward by default.
		 * Allows us to only implement proper rendering on root element
		 */
		public virtual void render() {
			if(this.parent != null) {
				this.parent.render();
			}
		}

		/**
		 * This method should be implemented to draw the elements contenst.
		 * The given buffer should be modified in whatever way the element sees fit.
		 */
		public abstract void draw(Buffer canvas);
	}
}
