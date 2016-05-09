using System;
using System.Diagnostics;

namespace VierGewinnt.Render {
	/**
	 * A class to render a given Element instance to the console
	 */
	public class Renderer {
		/** Duration of the last call to render() in milliseconds */
		public long lastFrameTime = 0;
		/** Stopwatch to measure render time */
		private Stopwatch timer = new Stopwatch();

		/** The window title */
		private String title;

		public Renderer(String title) {
			this.title = title;
		}

		/** Prepare console */
		public void init() {
			Console.CursorVisible = false;
			Console.Clear();
			Console.Title = this.title;
		}

		/** Reset console */
		public void destroy() {
			Console.CursorVisible = true;
			Console.ResetColor();
			Console.Clear();
		}

		/**
		 * Render an element to the terminal
		 * @todo This is horribly inefficient atm
		 */
		public void render(Element element) {
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
	}
}
