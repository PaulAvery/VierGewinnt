using System;
using System.Collections.Generic;
using VierGewinnt.Render;
using VierGewinnt.Views;

namespace VierGewinnt {
	public class Program {
		private static int turn = 0;
		private static int waiting = 0;
		private static Renderer renderer = new Renderer();
		private static Board board = new Board(7, 6);
		private static List<Player> players = new List<Player>();

		public static void Main(string[] playerNames) {
			AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnExit);

			createUsers(playerNames);
			Console.CursorVisible = false;
			Console.Title = "4 Gewinnt";

			ViewElement mainView = new MainView(renderer);

			while(true) {
				mainView.render();

				Console.ResetColor();
				Console.SetCursorPosition(0, 0);
				Console.Write(renderer.lastFrameTime.ToString());
;
				handleKey(Console.ReadKey().Key);
			}
		}

		private static void OnExit(object sender, EventArgs e) {
			Console.Clear();
		}

		private static void handleKey(ConsoleKey key) {
			if(!board.status().done) {
				handleKeyInGame(key);
			}
		}

		private static void handleKeyInGame(ConsoleKey key) {
			switch(key) {
				case ConsoleKey.LeftArrow:
					moveLeft();
					break;
				case ConsoleKey.RightArrow:
					moveRight();
					break;
			}
		}

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

		private static void abort(string message) {
			Console.WriteLine(message);
			Environment.Exit(1);
		}

		private static void createUsers(string[] playerNames) {
			if(playerNames.Length < 2) {
				abort("Needs at least two player names as arguments");
				return;
			}

			Stack<ConsoleColor> colors = new Stack<ConsoleColor>((ConsoleColor[]) Enum.GetValues(typeof(ConsoleColor)));

			foreach(string playerName in playerNames) {
				ConsoleColor color = colors.Pop();

				if(color == ConsoleColor.Black) {
					color = colors.Pop();
				}

				if(color == ConsoleColor.White) {
					color = colors.Pop();
				}

				players.Add(new Player(color, playerName));
			}
		}
	}
}
