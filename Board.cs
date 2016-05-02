using System.Collections.Generic;

namespace VierGewinnt {
	public class Board {
		public struct Status {
			public readonly Player winner;
			public readonly bool done;

			public Status(Player winner, bool done) {
				this.winner = winner;
				this.done = done;
			}
		}

		public readonly int width;
		public readonly int height;

		/* Use nullable type so we can have empty cells */
		private Coin[,] board;
		private Status state;
		public int waiting;
		public Player player;

		public Board(int width, int height) {
			this.width = width;
			this.height = height;

			this.board = new Coin[width, height];
			this.state = new Status(null, false);
		}

		/* Return current Status */
		public Status status() {
			return this.state;
		}

		/* Get coin from single position */
		public Coin getPosition(int x, int y) {
			return this.board[x, y];
		}

		/* Select next column */
		public void selectPrevious() {
			if(this.waiting == 0) {
				this.waiting = this.width - 1;
			} else {
				this.waiting--;
			}
		}

		/* Select previous column */
		public void selectNext() {
			if(this.waiting == this.width - 1) {
				this.waiting = 0;
			} else {
				this.waiting++;
			}
		}

		/* Try to insert a coin into the current column*/
		public bool insert() {
			Coin coin = new Coin(this.player);

			/* Find lowest empty slot */
			for(int i = 0; i < this.height; i++) {
				if(this.board[this.waiting, i] == null) {
					/* Save the coin */
					this.board[this.waiting, i] = coin;

					/* Check if this resulted in a win for anyone */
					this.state = this.checkStatus(this.waiting, i);
					return true;
				}
			}

			/* Our column is full, so fail */
			return false;
		}

		/* Generate a new status based on the given center coin */
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

		/* Check if the entire board is full */
		private bool isFull() {
			for(int x = 0; x < this.width; x++) {
				if(this.board[x, this.height - 1] == null) {
					return false;
				}
			}

			return true;
		}

		/* Check if 4 are matched through specific cell */
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

		/* Mark all given coins as won to highlight them later */
		private void showWinningCoins(List<Coin> coins) {
			foreach(Coin coin in coins) {
				coin.won = true;
			}
		}

		/* Check if a horizontal match was found */
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

		/* Check if a vertical match was found */
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

			for(int yPos = y + 1; yPos < this.width; yPos++) {
				Coin foundCoin = this.board[x, yPos];

				if(foundCoin != null && foundCoin.player.Equals(coin.player)) {
					coins.Add(foundCoin);
				} else {
					break;
				}
			}

			return coins;
		}

		/* Check if a diagonal match was found from bottom left to top right */
		private List<Coin> matchedFourDiagonalBottomToTop(int x, int y) {
			/* ToDo */
			return new List<Coin>();
		}

		/* Check if a diagonal match was found from bottom left to top right */
		private List<Coin> matchedFourDiagonalTopToBottom(int x, int y) {
			/* ToDo */
			return new List<Coin>();
		}
	}
}
