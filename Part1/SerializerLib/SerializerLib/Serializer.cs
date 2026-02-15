using System.Text.Json;
using System.Xml.Linq;
using System.Xml.Serialization;
using Hospital.Domain;

namespace SerializerLib
{
    public class Serializer : ISerializer
    {
        public void SerializeByLINQ(IEnumerable<Hospital.Domain.Hospital> hospitals, string fileName)
        {
            var file = new XDocument(
                new XElement("Hospitals",
                    hospitals.Select(h =>
                        new XElement("Hospital",
                            new XAttribute("Name", h.Name),
                            new XElement("Address", h.Address),
                            new XElement("Phone", h.Phone),
                            new XElement("Departments",
                                h.Departments.Select(d =>
                                    new XElement("Department",
                                        new XAttribute("Id", d.Id),
                                        new XElement("Name", d.Name),
                                        new XElement("Head", d.Head),
                                        new XElement("Phone", d.Phone),
                                        new XElement("StaffCount", d.StaffCount),
                                        new XElement("Capacity", d.Capacity)
                                    )
                                )
                            )
                        )
                    )
                )
            );

            file.Save($"{fileName}.xml");
        }
        
        public IEnumerable<Hospital.Domain.Hospital> DeSerializeByLINQ(string fileName)
        {
            var file = XDocument.Load($"{fileName}.xml");

            return file.Root?
                       .Elements("Hospital")
                       .Select(h => new Hospital.Domain.Hospital(
                           (string?)h.Attribute("Name") ?? "",
                           (string?)h.Element("Address") ?? "",
                           (string?)h.Element("Phone") ?? ""
                       )
                       {
                           Departments = h.Element("Departments")?
                               .Elements("Department")
                               .Select(d => new Department(
                                   int.TryParse(d.Attribute("Id")?.Value, out var id) ? id : 0,
                                   (string?)d.Element("Name") ?? "",
                                   (string?)d.Element("Head") ?? "",
                                   (string?)d.Element("Phone") ?? "",
                                   int.TryParse(d.Element("StaffCount")?.Value, out var staffCount) ? staffCount : 0,
                                   int.TryParse(d.Element("Capacity")?.Value, out var capacity) ? capacity : 0
                               ))
                               .ToList() ?? new List<Department>()
                       })
                       .ToList()
                   ?? new List<Hospital.Domain.Hospital>();
        }

        public void SerializeXML(IEnumerable<Hospital.Domain.Hospital> hospitals, string fileName)
        {
            var serializer = new XmlSerializer(typeof(List<Hospital.Domain.Hospital>));
            using var stream = File.Create($"{fileName}.xml");
            serializer.Serialize(stream, hospitals.ToList());
        }

        public IEnumerable<Hospital.Domain.Hospital> DeSerializeXML(string fileName)
        {
            var serializer = new XmlSerializer(typeof(List<Hospital.Domain.Hospital>));
            using var stream = File.OpenRead($"{fileName}.xml");
            return serializer.Deserialize(stream) as List<Hospital.Domain.Hospital> 
                   ?? new List<Hospital.Domain.Hospital>();
        }

        public void SerializeJSON(IEnumerable<Hospital.Domain.Hospital> hospitals, string fileName)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var jsonString = JsonSerializer.Serialize(hospitals.ToList(), options);
            File.WriteAllText($"{fileName}.json", jsonString);
        }

        public IEnumerable<Hospital.Domain.Hospital> DeSerializeJSON(string fileName)
        {
            var jsonString = File.ReadAllText($"{fileName}.json");
            return JsonSerializer.Deserialize<List<Hospital.Domain.Hospital>>(jsonString)
                   ?? new List<Hospital.Domain.Hospital>();
        }
    }
}
