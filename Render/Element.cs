namespace VierGewinnt.Render {
	/*
	 * A base class to represent an element to render to the terminal
	 * Calls to render() chain upwards by default
	 * The draw() method should render neccessary characters to the provided buffer
	 */
	public abstract class Element {
		public Element parent;

		public virtual void render() {
			if(this.parent != null) {
				this.parent.render();
			}
		}

		public abstract void draw(Buffer canvas);
	}
}
