using System;

namespace VierGewinnt.Render {
	public class Renderer {
		private bool initialized = false;
		private TerminalCharacter[,] frame;

		public void render(Buffer buffer) {
			init();

			TerminalCharacter[,] canvas = emptyBuffer();
			buffer.draw(canvas);

			/* TODO */
		}

		private void init() {
			if(this.initialized) {
				return;
			}

			Console.Clear();
			this.frame = emptyBuffer();
			this.initialized = true;
		}

		private TerminalCharacter[,] emptyBuffer() {
			TerminalCharacter[,] buffer = new TerminalCharacter[Console.BufferWidth, Console.BufferHeight];

			for(int x = 0; x < Console.BufferWidth; x++) {
				for(int y = 0; y < Console.BufferHeight; y++) {
					buffer[x, y] = new TerminalCharacter(' ');
				}
			}

			return buffer;
		}
	}
}
