using System;
using System.Linq;
using System.Collections.Generic;
using VierGewinnt.Render;

namespace VierGewinnt.Views {
	public class MainView: ViewElement {
		private Board board;
		private GridElement grid;
		private HorizontalSplitElement names;

		public MainView(Renderer renderer, Board board, List<Player> players): base(renderer) {
			this.board = board;
			this.grid = new GridElement(board.width, board.height);

			Element names = new HorizontalSplitElement(
				players.Select(player => {
					return new ConditionalWrapElement(
						() => board.player.Equals(player),
						new ColorElement(ConsoleColor.White),
						new ColorElement(player.color,
							new CenterElement(
								new TextElement(
									player.name
								)
							)
						)
					);
				}).ToArray()
			);

			Element boardGrid = new CenterElement(this.grid);

			setChild(new VerticalFloatElement(new Element[] {
				new FixedHeightElement(5, names),
				boardGrid
			}));
		}

		public override void draw(Render.Buffer canvas) {
			for(int x = 0; x < board.width; x++) {
				for(int y = 0; y < board.height; y++) {
					Coin coin = board.getPosition(x, y);

					if(coin == null) {
						grid.put(x, board.height - y - 1, new TerminalCharacter(' '));
					} else {
						TerminalCharacter character = new TerminalCharacter('●', coin.won ? ConsoleColor.White : coin.player.color);
						grid.put(x, board.height - y - 1, character);
					}
				}
			}

			base.draw(canvas);
		}
	}
}
