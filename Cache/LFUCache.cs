using Cache.LFU;
using System;
using System.Collections.Generic;

namespace Cache
{
	public class LFUCache : ICache
	{
		private int count, capacity, minFrequency;
		Dictionary<int, LFUNode> map;
		Dictionary<int, LFUDoubleLinkedList> frequencyMap;

		public LFUCache(int capacity)
		{
			this.capacity = capacity;
			this.count = 0;
			map = new Dictionary<int, LFUNode>();
			frequencyMap = new Dictionary<int, LFUDoubleLinkedList>();
			this.minFrequency = Int32.MaxValue;
		}

		// get the value and then promote the node
		public int Get(int key)
		{
			if (!map.ContainsKey(key)) return -1;
			LFUNode node = map[key];
			PromoteNode(node);
			return node.Value;
		}

		public void Add(int key, int value)
		{
			if (capacity == 0) return;

			// update the value of that node and then promote the node
			if (map.ContainsKey(key))
			{
				LFUNode node = map[key];
				node.Value = value;
				PromoteNode(node);
			}
			else
			{
				// remove the LFU node
				if (count == capacity)
				{
					LFUDoubleLinkedList dll = frequencyMap[minFrequency];
					LFUNode lfuNode = dll.RemoveLFUNode();
					if (dll.Count == 0) frequencyMap.Remove(minFrequency);
					map.Remove(lfuNode.Key);
					count--;
				}

				// add a new node
				LFUNode node = new LFUNode(key, value);
				minFrequency = node.Frequency;
				map[key] = node;
				if (!frequencyMap.ContainsKey(node.Frequency)) frequencyMap[node.Frequency] = new LFUDoubleLinkedList();
				frequencyMap[node.Frequency].AddToTop(node);
				count++;
			}
		}

		// remove target node from current frequency double-linked list,
		// and then promote it to higher frequency double-linked list
		private void PromoteNode(LFUNode node) {
			LFUDoubleLinkedList dll = frequencyMap[node.Frequency];
			dll.RemoveNode(node);
			if (dll.Count == 0) {
				frequencyMap.Remove(node.Frequency);
				if (minFrequency == node.Frequency) minFrequency++;
			}
			node.Frequency++;
			if (!frequencyMap.ContainsKey(node.Frequency)) {
				frequencyMap[node.Frequency] = new LFUDoubleLinkedList();
			}
			frequencyMap[node.Frequency].AddToTop(node);
		}
	}
}
