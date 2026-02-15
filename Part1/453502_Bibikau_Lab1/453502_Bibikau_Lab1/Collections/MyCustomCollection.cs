    namespace _453502_Bibikau_Lab1
{
    public class MyCustomCollection<T> : ICustomCollection<T>
    {
        public class Node
        {
            public Node? Next { get; set; }
            public T Value { get; set; }
            public Node(T value, Node? next = null)
            {
                Value = value;
                Next = next;
            }
        }

        private Node? _root;
        private Node? _currentNode;
        private Node? _end;
        private int _count;
        public int Count => _count;

        public MyCustomCollection()
        {
            _root = null;
            _currentNode = null;
            _end = null;
            _count = 0;
        }

        public void Reset() => _currentNode = _root;

        public void Next()
        {
            if (_currentNode?.Next != null)
                _currentNode = _currentNode.Next;
        }

        public T Current()
        {
            if (_currentNode == null) 
                throw new NullReferenceException("Current node is null.");
            return _currentNode.Value;
        }        
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= _count) throw new IndexOutOfRangeException();

                Node? temp = _root;
                for (int i = 0; i < index; i++)
                    temp = temp.Next;

                return temp.Value;
            }
            set
            {
                if (index < 0 || index >= _count) throw new IndexOutOfRangeException();

                Node? temp = _root;
                for (int i = 0; i < index; i++)
                    temp = temp.Next;

                temp.Value = value;
            }
        }

        public void Add(T item)
        {
            if (item == null)
                throw new ArgumentNullException();
            
            Node newNode = new(item);
            if (_root == null)
            {
                _root = newNode;
                _currentNode = _root;
                _end = _root;
            }
            else
            {
                _end!.Next = newNode;
                _end = newNode;
            }
            _count++;
        }

        public void Remove(T item)
        {
            if (_root is null) throw new NullReferenceException();
            if (item == null) return;

			if (_root.Value.Equals(item))
            {
                _root = _root.Next;
                _count--;
                if (_root == null) _end = _currentNode = null;
                
                return;
            }

            Node? temp = _root;
            while (temp.Next != null)
            {
				if (temp.Next.Value.Equals(item))
                {
                    temp.Next = temp.Next.Next;
                    if (temp.Next == null) _end = temp;
                    _count--;
                    return;
                }
                temp = temp.Next;
            }
        }

        public T RemoveCurrent()
        {
            if (_currentNode is null) throw new NullReferenceException();

            T removed = _currentNode.Value;

            if (_currentNode.Next != null)
            {
                _currentNode.Value = _currentNode.Next.Value;
                _currentNode.Next = _currentNode.Next.Next;
            }
            else
            {
                if (_root == _currentNode)
                {
                    _root = null;
                    _end = null;
                    _currentNode = null;
                }
                else
                {
                    Node? prev = _root;
                    
                    while (prev!.Next != _currentNode)
                        prev = prev.Next;

                    prev.Next = _currentNode.Next;
                    
                    if(_currentNode == _end)
                        _end = prev;

                    _currentNode = prev.Next;
                }
            }

            _count--;
            return removed;
        }
    }
}