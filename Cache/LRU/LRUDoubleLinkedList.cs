namespace Cache.LRU
{
	public class LRUDoubleLinkedList
	{
		private LRUNode Head;
		private LRUNode Tail;

		public LRUDoubleLinkedList()
		{
			Head = new LRUNode();
			Tail = new LRUNode();
			Head.Next = Tail;
			Tail.Previous = Head;
		}

		public void AddToTop(LRUNode node) {
			node.Next = Head.Next;
			Head.Next.Previous = node;
			node.Previous = Head;
			Head.Next = node;
		}

		public void RemoveNode(LRUNode node) {
			node.Previous.Next = node.Next;
			node.Next.Previous = node.Previous;
			node.Next = null;
			node.Previous = null;
		}

		public LRUNode RemoveLRUNode() {
			LRUNode target = Tail.Previous;
			RemoveNode(target);
			return target;
		}
	}
}
