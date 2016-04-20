using VierGewinnt.Render;

namespace VierGewinnt.Views {
	public class MainView: ViewElement {
		private Board board;
		private GridElement grid = new GridElement(7, 6);

		public MainView(Renderer renderer, Board board): base(renderer) {
			setChild(new CenterElement(grid));
			this.board = board;
		}

		public override void draw(Buffer canvas) {
			for(int x = 0; x < board.width; x++) {
				for(int y = 0; y < board.height; y++) {
					Coin? coin = board.getPosition(x, y);

					if(!coin.HasValue) {
						grid.put(x, y, new TerminalCharacter(' '));
					} else {
						TerminalCharacter character = new TerminalCharacter('o', coin.Value.player.color);
						grid.put(x, y, character);
					}
				}
			}

			base.draw(canvas);
		}
	}
}
