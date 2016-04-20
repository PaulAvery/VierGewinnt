namespace VierGewinnt.Render {
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
