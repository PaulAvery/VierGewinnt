using System;
using System.Diagnostics;

namespace VierGewinnt.Render {
	public class Renderer {
		public long lastFrameTime;
		private bool initialized = false;
		private Stopwatch timer = new Stopwatch();

		public void render(Element element) {
			init();
			timer.Restart();

			Buffer canvas = new Buffer(Console.BufferWidth, Console.BufferHeight);
			element.draw(canvas);

			for(int y = 0; y < canvas.height; y++) {
				for(int x = 0; x < canvas.width; x++) {
					Console.ResetColor();
					Console.SetCursorPosition(x, y);

					TerminalCharacter character = canvas.get(x, y);

					if(character.foreground.HasValue) {
						Console.ForegroundColor = character.foreground.Value;
					}

					if(character.background.HasValue) {
						Console.BackgroundColor = character.background.Value;
					}

					Console.Write(character.value);
				}
			}

			timer.Stop();
			this.lastFrameTime = timer.ElapsedMilliseconds;
		}

		private void init() {
			if(this.initialized) {
				return;
			}

			Console.Clear();
			this.initialized = true;
		}
	}
}
