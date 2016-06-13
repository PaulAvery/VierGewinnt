using System;

namespace VierGewinnt.Render.Elements.Base {
	/**
	 * An element to conditionally wrap a child element in a WrapElement
	 */
	public class DynamicElement<T>: Element where T: Element {
		/**
		 * Lambda which outputs the current element on draw
		 */
		private Func<T> lambda;

		public DynamicElement(Func<T> lambda): base() {
			this.lambda = lambda;
		}

		/**
		 * Drawing logic.
		 * Evaluates the condition and draws the child either inside or outside the wrapper
		 */
		public override void draw(Buffer canvas) {
			this.lambda().draw(canvas);
		}
	}
}
