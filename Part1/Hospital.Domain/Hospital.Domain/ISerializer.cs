    namespace Hospital.Domain
{
    public interface ISerializer
    {
        IEnumerable<Hospital> DeSerializeByLINQ(string fileName);
        IEnumerable<Hospital> DeSerializeXML(string fileName);
        IEnumerable<Hospital> DeSerializeJSON(string fileName);
        void SerializeByLINQ(IEnumerable<Hospital> Hospitals, string fileName);
        void SerializeXML(IEnumerable<Hospital> Hospitals, string fileName);
        void SerializeJSON(IEnumerable<Hospital> Hospitals, string fileName);
    }
}
