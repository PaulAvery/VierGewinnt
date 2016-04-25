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
		private int waiting;
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
			Coin coin = this.board[x, y];

			if(coin != null) {
				Player player = coin.player;
				List<Coin> coins = new List<Coin>();
				coins.Add((Coin) coin);

				/* check horizontally */
				for(int xPos = x - 1; xPos >= 0; xPos--) {
					Coin foundCoin = this.board[xPos, y];

					if(foundCoin != null && foundCoin.player.Equals(player)) {
						coins.Add((Coin) foundCoin);
					} else {
						break;
					}
				}

				for(int xPos = x + 1; xPos < this.width; xPos++) {
					Coin foundCoin = this.board[xPos, y];

					if(foundCoin != null && foundCoin.player.Equals(player)) {
						coins.Add((Coin) foundCoin);
					} else {
						break;
					}
				}

				if(coins.Count >= 4) {
					/* Highlight winning coins */
					for(int i = 0; i < coins.Count; i++) {
						Coin foundCoin = coins[i];
						foundCoin.won = true;
					}

					return true;
				} else {
					/* Reset our found coins */
					coins.Clear();
					coins.Add((Coin) coin);
				}

				/* TODO */
			}

			return false;
		}
	}
}
