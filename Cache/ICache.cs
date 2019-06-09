namespace Cache
{
	public interface ICache
	{
		// return the value if key exists in cache, or -1 if not. 
		int Get(int key);

		// add a new data entry to cache
		void Add(int key, int value);
	}
}
