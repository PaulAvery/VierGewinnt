using System;
using System.Collections.Generic;
using VierGewinnt.Render;
using VierGewinnt.Views;

namespace VierGewinnt {
	public class Program {
		/* Whos turn is it? */
		private static int turn = 0;

		/* What position does the waiting coin have? */
		private static int waiting = 0;

		private static Renderer renderer = new Renderer();
		private static Board board = new Board(7, 6);
		private static List<Player> players = new List<Player>();

		public static void Main(string[] playerNames) {
			Console.CancelKeyPress += new ConsoleCancelEventHandler((object o, ConsoleCancelEventArgs e) => exit());

			/* Setup */
			createUsers(playerNames);
			Console.CursorVisible = false;
			Console.Clear();
			Console.Title = "4 Gewinnt";

			/* Instantiate views */
			ViewElement mainView = new MainView(renderer, board);

			/* Main game loop */
			while(true) {
				/* Render the board */
				mainView.render();

				/* Print frametimings for debug purposes */
				Console.ResetColor();
				Console.SetCursorPosition(0, 0);
				Console.Write(renderer.lastFrameTime.ToString());

				/* Wait for user input */
				handleKey(Console.ReadKey().Key);
			}
		}

		/* Cleanup on exit */
		private static void exit() {
			Console.Clear();
		}

		/* Create users and assign colors */
		private static void createUsers(string[] playerNames) {
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

		/* Handle user input */
		private static void handleKey(ConsoleKey key) {
			if(!board.status().done) {
				handleKeyInGame(key);
			}
		}

		/* Handle user input while game is running */
		private static void handleKeyInGame(ConsoleKey key) {
			switch(key) {
				case ConsoleKey.LeftArrow:
					moveLeft();
					break;
				case ConsoleKey.RightArrow:
					moveRight();
					break;
				case ConsoleKey.Enter:
					insertCoin();
					break;
			}
		}

		/**********************************************************************
		 * Key handlers
		 **********************************************************************/
		private static void moveLeft() {
			if(waiting == 0) {
				waiting = board.width - 1;
			} else {
				waiting--;
			}
		}

		private static void moveRight() {
			if(waiting == board.width - 1) {
				waiting = 0;
			} else {
				waiting++;
			}
		}

		private static void insertCoin() {
			board.insert(new Coin(players[turn]), waiting);

			turn++;
			if(turn >= players.Count) {
				turn = 0;
			}
		}
	}
}
