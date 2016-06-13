using System;
using System.Collections;
using System.Collections.Generic;

namespace VierGewinnt.Render.Elements.Base {
	/**
	 * An element to conditionally wrap a child element in a WrapElement
	 */
	public class DynamicElementCollection: ICollection {
		/**
		 * Lambda which outputs the current element on draw
		 */
		private Func<ICollection<Element>> lambda;

		public DynamicElementCollection(Func<ICollection<Element>> lambda): base() {
			this.lambda = lambda;
		}

		public int Count {
			get { return this.lambda().Count; }
		}

		public object SyncRoot {
			get { return  ((ICollection) this.lambda()).SyncRoot; }
		}

		public bool IsSynchronized {
			get {return ((ICollection) this.lambda()).IsSynchronized; }
		}

		public bool IsReadOnly {
			get {return this.lambda().IsReadOnly; }
		}

		public void CopyTo(Array arr, int i) {
			this.lambda().CopyTo((Element[]) arr, i);
		}

		public void Add(Element el) {
			this.lambda().Add(el);
		}

		public void Clear() {
			this.lambda().Clear();
		}

		public bool Contains(Element el) {
			return this.lambda().Contains(el);
		}

		public bool Remove(Element el) {
			return this.lambda().Remove(el);
		}

		public IEnumerator GetEnumerator() {
			return this.lambda().GetEnumerator();
		}
	}
}
