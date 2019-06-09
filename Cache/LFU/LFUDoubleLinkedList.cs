namespace Cache.LFU
{
	public class LFUDoubleLinkedList
	{
		private LFUNode Head;
		private LFUNode Tail;
		public int Count { get; private set; }
		public LFUDoubleLinkedList()
		{
			Head = new LFUNode();
			Tail = new LFUNode();
			Head.Next = Tail;
			Tail.Previous = Head;
			Count = 0;
		}

		public void AddToTop(LFUNode node) {
			node.Previous = Head;
			node.Next = Head.Next;
			node.Next.Previous = node;
			Head.Next = node;
			Count++;
		}

		public void RemoveNode(LFUNode node) {
			node.Previous.Next = node.Next;
			node.Next.Previous = node.Previous;
			node.Previous = null;
			node.Next = null;
			Count--;
		}

		public LFUNode RemoveLFUNode() {
			LFUNode target = Tail.Previous;
			RemoveNode(target);
			return target;
		}
	}
}
