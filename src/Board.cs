using System;
using System.Collections.Generic;
using VierGewinnt.Render;

namespace VierGewinnt {
	/** Representation of the game board */
	public class Board: GridElement{
		/** Utility Struct to represent game state */
		public struct Status {
			/** Winner or null if not done yet/tied */
			public readonly Player winner;
			/** Is the game over (somebody won or no spaces left) */
			public readonly bool done;

			public Status(Player winner, bool done) {
				this.winner = winner;
				this.done = done;
			}
		}

		/** Width of the board */
		public readonly int width;
		/** Height of the board */
		public readonly int height;

		/** Internal representation of the board */
		private Coin[,] board;
		/** Internal representation of the games state */
		private Status state;

		public Board(int width, int height): base(width, height) {
			this.width = width;
			this.height = height;

			this.board = new Coin[width, height];
			this.state = new Status(null, false);
		}

		/** Return current Status */
		public Status status() {
			return this.state;
		}

		/** Get coin from single position */
		public Coin getPosition(int x, int y) {
			return this.board[x, y];
		}

		/**
		 * Try to insert a coin into a column
		 * @returns bool True on success, false on full column.
		 */
		public bool insert(int Position, Coin coin) {
			/* Find lowest empty slot */
			for(int i = 0; i < this.height; i++) {
				if(this.board[Position, i] == null) {
					/* Save the coin */
					this.board[Position, i] = coin;
					this.put(Position, this.height - i - 1, () => new TerminalCharacter('●', coin.won ? ConsoleColor.White : coin.player.color));

					/* Check if this resulted in a win for anyone */
					this.state = this.checkStatus(Position, i);
					return true;
				}
			}

			/* Our column is full, so fail */
			return false;
		}

		/** Generate a new status based on the given center coin */
		private Status checkStatus(int x, int y) {
			if(this.matchedFour(x, y)) {
				return new Status(this.board[x, y].player, true);
			} else if(this.isFull()) {
				/* Our board is full, tie */
				return new Status(null, true);
			} else {
				/* Nobody can have won */
				return new Status(null, false);
			}
		}

		/** Check if the entire board is full */
		private bool isFull() {
			for(int x = 0; x < this.width; x++) {
				if(this.board[x, this.height - 1] == null) {
					return false;
				}
			}

			return true;
		}

		/** Check if 4 are matched through specific cell */
		private bool matchedFour(int x, int y) {
			if(this.board[x, y] != null) {
				List<Coin> coins;

				coins = matchedFourHorizontal(x, y);
				if(coins.Count >= 4) {
					showWinningCoins(coins);
					return true;
				}

				coins = matchedFourVertical(x, y);
				if(coins.Count >= 4) {
					showWinningCoins(coins);
					return true;
				}

				coins = matchedFourDiagonalBottomToTop(x, y);
				if(coins.Count >= 4) {
					showWinningCoins(coins);
					return true;
				}

				coins = matchedFourDiagonalTopToBottom(x, y);
				if(coins.Count >= 4) {
					showWinningCoins(coins);
					return true;
				}
			}

			return false;
		}

		/** Mark all given coins as won to highlight them later */
		private void showWinningCoins(List<Coin> coins) {
			foreach(Coin coin in coins) {
				coin.won = true;
			}
		}

		/** Check if a horizontal match was found */
		private List<Coin> matchedFourHorizontal(int x, int y) {
			Coin coin = this.board[x, y];
			List<Coin> coins = new List<Coin>();

			coins.Add(coin);

			for(int xPos = x - 1; xPos >= 0; xPos--) {
				Coin foundCoin = this.board[xPos, y];

				if(foundCoin != null && foundCoin.player.Equals(coin.player)) {
					coins.Add((Coin) foundCoin);
				} else {
					break;
				}
			}

			for(int xPos = x + 1; xPos < this.width; xPos++) {
				Coin foundCoin = this.board[xPos, y];

				if(foundCoin != null && foundCoin.player.Equals(coin.player)) {
					coins.Add(foundCoin);
				} else {
					break;
				}
			}

			return coins;
		}

		/** Check if a vertical match was found */
		private List<Coin> matchedFourVertical(int x, int y) {
			Coin coin = this.board[x, y];
			List<Coin> coins = new List<Coin>();

			coins.Add(coin);

			for(int yPos = y - 1; yPos >= 0; yPos--) {
				Coin foundCoin = this.board[x, yPos];

				if(foundCoin != null && foundCoin.player.Equals(coin.player)) {
					coins.Add((Coin) foundCoin);
				} else {
					break;
				}
			}

			for(int yPos = y + 1; yPos < this.height; yPos++) {
				Coin foundCoin = this.board[x, yPos];

				if(foundCoin != null && foundCoin.player.Equals(coin.player)) {
					coins.Add(foundCoin);
				} else {
					break;
				}
			}

			return coins;
		}

		/**
		 * Check if a diagonal match was found from bottom left to top right
		 */
		private List<Coin> matchedFourDiagonalBottomToTop(int x, int y) {
			Coin coin = this.board[x, y];
			List<Coin> coins = new List<Coin>();

			coins.Add(coin);

			for(int offset = -1; y + offset >= 0 && x + offset >= 0; offset--) {
				Coin foundCoin = this.board[x + offset, y + offset];

				if(foundCoin != null && foundCoin.player.Equals(coin.player)) {
					coins.Add(foundCoin);
				} else {
					break;
				}
			}

			for(int offset = 1; y + offset < this.height && x + offset < this.width; offset++) {
				Coin foundCoin = this.board[x + offset, y + offset];

				if(foundCoin != null && foundCoin.player.Equals(coin.player)) {
					coins.Add(foundCoin);
				} else {
					break;
				}
			}

			return coins;
		}

		/**
		 * Check if a diagonal match was found from bottom left to top right
		 */
		private List<Coin> matchedFourDiagonalTopToBottom(int x, int y) {
			Coin coin = this.board[x, y];
			List<Coin> coins = new List<Coin>();

			coins.Add(coin);

			for(int offset = -1; y - offset < this.height && x + offset >= 0; offset--) {
				Coin foundCoin = this.board[x + offset, y - offset];

				if(foundCoin != null && foundCoin.player.Equals(coin.player)) {
					coins.Add(foundCoin);
				} else {
					break;
				}
			}

			for(int offset = 1; y - offset >= 0 && x + offset < this.width; offset++) {
				Coin foundCoin = this.board[x + offset, y - offset];

				if(foundCoin != null && foundCoin.player.Equals(coin.player)) {
					coins.Add(foundCoin);
				} else {
					break;
				}
			}

			return coins;
		}
	}
}
