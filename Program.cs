using System;
using System.Collections.Generic;
using VierGewinnt.Render;
using VierGewinnt.Views;

namespace VierGewinnt {
	public class Program {
		private static Renderer renderer = new Renderer();
		private static List<Player> players = new List<Player>();

		public static void Main(string[] playerNames) {
			Console.CancelKeyPress += new ConsoleCancelEventHandler(exit);

			/* Setup */
			createPlayers(playerNames);
			Console.CursorVisible = false;
			Console.Clear();
			Console.Title = "4 Gewinnt";

			/* Instantiate views */
			MainView mainView = new MainView(renderer, players);

			/* Focus main view */
			mainView.focus();

			/* Wait for input before exiting */
			Console.ReadKey();
			Console.Clear();
		}

		/* Cleanup on exit */
		private static void exit(object o, ConsoleCancelEventArgs e) {
			Console.Clear();
			Environment.Exit(0);
		}

		/* Create users and assign colors */
		private static void createPlayers(string[] playerNames) {
			if(playerNames.Length < 2) {
				abort("Needs at least two player names as arguments");
				return;
			}

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
		}

		/* Print a message and abort the program */
		private static void abort(string message) {
			Console.Clear();
			Console.WriteLine(message);
			Environment.Exit(1);
		}
	}
}
