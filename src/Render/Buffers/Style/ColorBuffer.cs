using System;
using VierGewinnt.Render.Buffers.Base;

namespace VierGewinnt.Render.Buffers.Style {
	/** Buffer which sets the foreground color of its childbuffer */
	public class ColorBuffer: WrapBuffer {
		private ConsoleColor color;

		public ColorBuffer(ConsoleColor color, Buffer child, int width, int height, Buffer parent = null, int offsetX = 0, int offsetY = 0): base(child, width, height, parent, offsetX, offsetY) {
			this.color = color;
		}

		public override void set(int x, int y, TerminalCharacter character) {
			character.foreground = this.color;

			this.child.set(x, y, character);
		}
	}
}
