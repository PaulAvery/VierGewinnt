using System;

namespace VierGewinnt.Render {
	/*
	 * A class to represent a rectangular grid of TerminalCharacters
	 */
	public class Buffer {
		/* Minimal and maximal occupied positions (bounding rectangle for set chars) */
		public int minX = 0;
		public int maxX = 0;
		public int minY = 0;
		public int maxY = 0;

		/* Width and height of buffer */
		public readonly int width;
		public readonly int height;

		private int offsetX;
		private int offsetY;
		private Buffer parent;
		private TerminalCharacter[,] characters;

		/*
		 * Store if the given character was set before. Otherwise we would need
		 * to use nullable TerminalCharacters which prevents us from changing
		 * parts of them elsewhere.
		 */
		private bool[,] isSet;

		/* If a parent is supplied, reads and writes will be passed through to the parent buffer at given offsets */
		public Buffer(int width, int height, Buffer parent = null, int offsetX = 0, int offsetY = 0) {
			this.width = width;
			this.height = height;
			this.characters = new TerminalCharacter[width, height];
			this.isSet = new bool[width, height];
			this.parent = parent;
			this.offsetX = offsetX;
			this.offsetY = offsetY;
			this.minX = this.width - 1;
			this.minY = this.height - 1;
		}

		/* Set a terminal character at given position */
		public void set(int x, int y, TerminalCharacter character) {
			if(this.parent != null) {
				this.parent.set(x + this.offsetX, y + this.offsetY, character);
			} else if(x >= 0 && x < this.width && y >= 0 && y < this.height) {
				this.characters[x, y] = character;
				this.isSet[x, y] = true;

				this.maxX = Math.Max(x, this.maxX);
				this.minX = Math.Min(x, this.minX);
				this.maxY = Math.Max(y, this.maxY);
				this.minY = Math.Min(y, this.minY);
			}
		}

		/*
		 * Get the TerminalCharacter at position. If position is out of bounds
		 * or was not set before, an empty TerminalCharacter is returned.
		 */
		public TerminalCharacter get(int x, int y) {
			if(this.parent != null) {
				return this.parent.get(x + this.offsetX, y + this.offsetY);
			}

			if(x >= 0 && x < this.width && y >= 0 && y < this.height) {
				if(!isSet[x, y]) {
					/* Because someone might modify this character, we set it first so our min/max values fit */
					this.set(x, y, new TerminalCharacter(' '));
				}

				return this.characters[x, y];
			} else {
				return new TerminalCharacter(' ');
			}
		}

		/* Create a transparent view into the buffer */
		public Buffer view(int x, int y, int width, int height) {
			return new Buffer(width, height, this, x, y);
		}

		/* Copy another buffer into this buffer at a given offset */
		public void copy(Buffer buffer, int offsetX = 0, int offsetY = 0) {
			for(int x = 0; x < buffer.width; x++) {
				for(int y = 0; y < buffer.height; y++) {
					this.set(x + offsetX, y + offsetY, buffer.get(x, y));
				}
			}
		}
	}
}
