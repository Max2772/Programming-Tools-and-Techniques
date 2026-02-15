namespace Hospital.Domain
{
    [Serializable]
    public class Hospital : IEquatable<Hospital>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public List<Department> Departments { get; set; }

        public Hospital()
        {
            Name = string.Empty;
            Address = string.Empty;
            Phone = string.Empty;
            Departments = new List<Department>();
        } 
        
        public Hospital(string name, string address, string phone)
        {
            Name = name;
            Address = address;
            Phone = phone;
            Departments = new List<Department>();
        }

        public bool Equals(Hospital? other)
        {
            if (other == null) return false;
            return Name == other.Name 
                   && Address == other.Address 
                   && Departments.SequenceEqual(other.Departments);
        }

    }
}
