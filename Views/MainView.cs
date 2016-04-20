using VierGewinnt.Render;

namespace VierGewinnt.Views {
	public class MainView: ViewElement {
		public MainView(Renderer renderer): base(renderer) {
			setChild(new CenterElement(new GridElement(7, 6)));
		}
	}
}
