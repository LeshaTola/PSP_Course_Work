namespace Server.DAO
{
	public abstract class DAO<T>
	{
		public abstract List<T> GetAll();
		public abstract T Get(int id);
		public abstract void Add(T item);
		public abstract void Remove(T item);
		public abstract void Update(T item);
	}
}
