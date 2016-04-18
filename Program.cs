using System;
using System.Collections.Generic;
using VierGewinnt.Render;

namespace VierGewinnt {
	public class Program {
		private static int turn = 0;
		private static int waiting = 0;
		private static Renderer renderer = new Renderer();
		private static Board board = new Board(7, 6);
		private static List<Player> players = new List<Player>();

		public static void Main(string[] playerNames) {
			createUsers(playerNames);

			ViewBuffer mainView = new ViewBuffer(renderer, new GridBuffer(6, 7));

			while(true) {
				mainView.render();
				handleKey(Console.ReadKey().Key);
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

				players.Add(new Player(color, playerName));
			}
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

		private static void render() {
			int width = Console.BufferWidth;
			int height = Console.BufferHeight;

			int offsetX = (width - (board.width * 4) + 1) / 2;
			int offsetY = (height - (board.height * 2) + 1) / 2;

			Console.Clear();
			Console.ResetColor();

			printWaiting(offsetX, offsetY);
			printBoard(offsetX, offsetY + 1);

			Console.SetCursorPosition(width - 1, height - 1);
		}

		private static void printWaiting(int offsetX, int offsetY) {
			Console.SetCursorPosition(offsetX + (waiting * 4) + 2, offsetY);
			Console.ForegroundColor = players[turn].color;
			Console.Write("●");
		}

		private static void printBoard(int offsetX, int offsetY) {
			for(int x = 0; x < board.width * 4; x+=4) {
				for(int y = (board.height - 1) * 2; y >= 0; y-=2) {
					Coin? coin = board.getPosition(x/4, y/2);

					Console.SetCursorPosition(offsetX + x, offsetY + y);
					Console.Write("┼───┼");

					Console.SetCursorPosition(offsetX + x, offsetY + y + 1);
					Console.Write("│ ");

					if(coin.HasValue) {
						Console.ForegroundColor = coin.Value.won ? ConsoleColor.Black : coin.Value.player.color;
						Console.Write("●");
						Console.ResetColor();
					} else {
						Console.Write(" ");
					}

					Console.Write(" │");
				}

				Console.SetCursorPosition(offsetX + x, offsetY + board.height * 2);
				Console.Write("┴───┴");

				Console.SetCursorPosition(offsetX + x, offsetY);
				Console.Write("┬───┬");
			}

			for(int y = board.height * 2; y >= 0; y-=2) {
				Console.SetCursorPosition(offsetX, offsetY + y);
				if(y == 0) {
					Console.Write("┌");
				} else if(y == board.height * 2) {
					Console.Write("└");
				} else {
					Console.Write("├");
				}

				Console.SetCursorPosition(offsetX + board.width * 4, offsetY + y);
				if(y == 0) {
					Console.Write("┐");
				} else if(y == board.height * 2) {
					Console.Write("┘");
				} else {
					Console.Write("┤");
				}
			}
		}
	}
}
