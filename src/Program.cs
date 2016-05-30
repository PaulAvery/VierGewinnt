using System;
using System.Collections.Generic;

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
			/* Handle Ctrl-C and exit gracefully */
			Console.CancelKeyPress += new ConsoleCancelEventHandler(exit);

			/* Setup */
			renderer.init();
			game = new Game(renderer, createPlayers(playerNames));

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

		/** Print a message and abort the program */
		private static void abort(string message) {
			Console.Clear();
			Console.WriteLine(message);
			Environment.Exit(1);
		}

		/** Create users and assign colors */
		private static List<Player> createPlayers(string[] playerNames) {
			if(playerNames.Length < 2) {
				abort("Needs at least two player names as arguments");
			}

			int colorCount = Enum.GetValues(typeof(ConsoleColor)).Length - 2;
			if(playerNames.Length > colorCount) {
				abort("Maximum number of players: " + colorCount);
			}

			List<Player> players = new List<Player>();
			Stack<ConsoleColor> colors = new Stack<ConsoleColor>((ConsoleColor[]) Enum.GetValues(typeof(ConsoleColor)));

			foreach(string playerName in playerNames) {
				ConsoleColor color = colors.Pop();

				/* Skip black color */
				if(color == ConsoleColor.Black) {
					color = colors.Pop();
				}

				/* Skip white color */
				if(color == ConsoleColor.White) {
					color = colors.Pop();
				}

				players.Add(new Player(color, playerName));
			}

			return players;
		}
	}
}
