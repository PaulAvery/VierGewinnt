using System;
using VierGewinnt.Render.Buffers.Base;

namespace VierGewinnt.Render.Buffers.Style {
	public class BackgroundColorBuffer: WrapBuffer {
		private ConsoleColor color;

		public  BackgroundColorBuffer(ConsoleColor color, Buffer child, int width, int height, Buffer parent = null, int offsetX = 0, int offsetY = 0): base(child, width, height, parent, offsetX, offsetY) {
			this.color = color;
		}

		public override void set(int x, int y, TerminalCharacter character) {
			character.background = this.color;

			this.child.set(x, y, character);
		}
	}
}
