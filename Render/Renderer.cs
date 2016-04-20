using System;
using System.Diagnostics;

namespace VierGewinnt.Render {
	public class Renderer {
		/* Contains the time of the last call to render() in milliseconds */
		public long lastFrameTime = 0;
		private Stopwatch timer = new Stopwatch();

		/* Render an element to the terminal */
		public void render(Element element) {
			timer.Restart();

			Buffer canvas = new Buffer(Console.BufferWidth, Console.BufferHeight);
			element.draw(canvas);

			for(int y = 0; y < canvas.height; y++) {
				for(int x = 0; x < canvas.width; x++) {
					/* All of this is probably horribly inefficient atm */
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
	}
}
