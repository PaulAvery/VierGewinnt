namespace VierGewinnt.Render {
	public abstract class Buffer {
		public Buffer parent;

		public virtual void render() {
			if(this.parent != null) {
				this.parent.render();
			}
		}

		public abstract void draw(TerminalCharacter[,] canvas);
	}
}
