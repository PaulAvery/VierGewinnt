using System;

namespace VierGewinnt.Render {
	/**
	 * An element to conditionally wrap a child element in a WrapElement
	 */
	public class ConditionalWrapElement: WrapElement {
		/**
		 * Condition deciding if element should be wrapped.
		 * Function will be called on each call to draw()
		 */
		private Func<bool> condition;
		/** Child wrapped into given WrapElement */
		private WrapElement wrappedChild;

		public ConditionalWrapElement(Func<bool> condition, WrapElement wrapper, Element child): base(child) {
			this.condition = condition;
			this.wrappedChild = wrapper;

			wrapper.setChild(child);
		}

		/**
		 * Drawing logic.
		 * Evaluates the condition and draws the child either inside or outside the wrapper
		 */
		public override void draw(Buffer canvas) {
			if(this.condition()) {
				wrappedChild.draw(canvas);
			} else {
				child.draw(canvas);
			}
		}
	}
}
