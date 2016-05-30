using System;
using System.Linq;

using VierGewinnt.Render;

namespace VierGewinnt {
	/** The main application class */
	public class Program {
		/** The renderer which will draw our UI */
		private static Renderer renderer = new Renderer("4 Gewinnt");

		/** The game object */
		private static Game game;

		/** The entry method */
		public static void Main(string[] playerNames) {
			if(playerNames.Length < 2) {
				Console.WriteLine("Needs at least two player names as arguments");
				Environment.Exit(1);
			}

			/* Handle Ctrl-C and exit gracefully */
			Console.CancelKeyPress += new ConsoleCancelEventHandler(exit);

			/* Setup */
			renderer.init();
			game = new Game(renderer, playerNames.Select(name => new Player(name)).ToList());

			/* Pass focus to main view */
			game.focus();

			/* Wait for input before exiting */
			Console.ReadKey();
			renderer.destroy();
		}

		/** Cleanup on exit */
		private static void exit(object o, ConsoleCancelEventArgs e) {
			renderer.destroy();
			Environment.Exit(0);
		}
	}
}
