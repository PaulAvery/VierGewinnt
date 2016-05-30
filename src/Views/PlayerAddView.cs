using System.Collections.Generic;
using VierGewinnt.Render;
using VierGewinnt.Render.Elements;
using VierGewinnt.Render.Elements.Layout;

namespace VierGewinnt.Views {
	public class PlayerAddView: ViewElement {
		private List<Player> players = new List<Player>();
		private VerticalFloatElement names = new VerticalFloatElement();

		public PlayerAddView(Renderer renderer): base(renderer) {}

		public List<Player> focus() {
			return this.players;
		}
	}
}
