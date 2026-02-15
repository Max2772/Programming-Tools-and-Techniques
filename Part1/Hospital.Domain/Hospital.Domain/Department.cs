namespace Hospital.Domain
{
    [Serializable]
    public class Department : IEquatable<Department>
    {
        public int Id { get; set; }
        public string Name { get; set; }      
        public string Head { get; set; }
        public string Phone { get; set; }
        public int StaffCount { get; set; }
        public int Capacity { get; set; }

        public Department()
        {
            Id = 0;
            Name = string.Empty;
            Head = string.Empty;
            Phone = string.Empty;
            StaffCount = 0;
            Capacity = 0;
        }

        public Department(int id, string name, string head, string phone, int staffCount, int capacity)
        {
            Id = id;
            Name = name;
            Head = head;
            Phone = phone;
            StaffCount = staffCount;
            Capacity = capacity;
        }

        public bool Equals(Department? other)
        {
            if (other == null) return false;
            return Id == other.Id
                   && Name == other.Name
                   && Head == other.Head
                   && Phone == other.Phone
                   && StaffCount == other.StaffCount
                   && Capacity == other.Capacity;
        }
    }
}
