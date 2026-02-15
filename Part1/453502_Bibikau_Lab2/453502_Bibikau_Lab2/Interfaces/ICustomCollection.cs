namespace _453502_Bibikau_Lab2
{

    public interface ICustomCollection<T>
    {
        T this[int index] { get; set; }

        void Reset();

        void Next();

        T Current();

        int Count { get; }

        void Add(T item);

        void Remove(T item);

        T RemoveCurrent();
    }
}