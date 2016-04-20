using System;

namespace VierGewinnt.Render {
	public class Buffer {
		public int minX = 0;
		public int maxX = 0;
		public int minY = 0;
		public int maxY = 0;
		public readonly int width;
		public readonly int height;
		private int offsetX;
		private int offsetY;
		private Buffer parent;
		private TerminalCharacter[,] characters;
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

		public TerminalCharacter[,] getArray() {
			return this.characters;
		}

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

		public TerminalCharacter get(int x, int y) {
			if(this.parent != null) {
				return this.parent.get(x + this.offsetX, y + this.offsetY);
			}

			if(x >= 0 && x < this.width && y >= 0 && y < this.height) {
				if(!isSet[x, y]) {
					this.set(x, y, new TerminalCharacter(' '));
				}

				return this.characters[x, y];
			} else {
				return new TerminalCharacter(' ');
			}
		}

		public Buffer view(int x, int y, int width, int height) {
			return new Buffer(width, height, this, x, y);
		}

		public void copy(Buffer buffer, int offsetX = 0, int offsetY = 0) {
			for(int x = 0; x < buffer.width; x++) {
				for(int y = 0; y < buffer.height; y++) {
					this.set(x + offsetX, y + offsetY, buffer.get(x, y));
				}
			}
		}
	}
}
