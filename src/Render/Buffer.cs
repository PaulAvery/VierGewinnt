using System;

namespace VierGewinnt.Render {
	/**
	 * A class to represent a rectangular grid of TerminalCharacters
	 */
	public class Buffer {
		/** Lowest occupied x position */
		public int minX = 0;
		/** Highest occupied x position */
		public int maxX = 0;
		/** Lowest occupied y position */
		public int minY = 0;
		/** Highest occupied y position */
		public int maxY = 0;

		/** Width of buffer */
		public readonly int width;
		/** Height of buffer */
		public readonly int height;

		/** X offset inside parent buffer */
		private int offsetX;
		/** Y offset inside parent buffer */
		private int offsetY;
		/**
		 * Parent buffer.
		 * Writes will pass through if possible
		 */
		private Buffer parent;
		/** Internal representation of the characters */
		private TerminalCharacter[,] characters;

		/**
		 * Store if a character was set before.
		 * Otherwise we would need to use nullable TerminalCharacters,
		 * which prevents us from changing parts of them elsewhere.
		 */
		private bool[,] isSet;

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

		/**
		 * Set a terminal character at given position
		 * Writes will pass through to the parent if one is available
		 */
		public virtual void set(int x, int y, TerminalCharacter character) {
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

		/**
		 * Get the TerminalCharacter at given position.
		 * If position is out of bounds or was not set before,
		 * an empty TerminalCharacter is returned.
		 */
		public virtual TerminalCharacter get(int x, int y) {
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

		/** Create a transparent view into the buffer */
		public Buffer view(int x, int y, int width, int height) {
			return new Buffer(width, height, this, x, y);
		}

		/** Copy another buffer into this buffer at a given offset */
		public void copy(Buffer buffer, int offsetX = 0, int offsetY = 0) {
			for(int x = 0; x < buffer.width; x++) {
				for(int y = 0; y < buffer.height; y++) {
					this.set(x + offsetX, y + offsetY, buffer.get(x, y));
				}
			}
		}
	}
}
