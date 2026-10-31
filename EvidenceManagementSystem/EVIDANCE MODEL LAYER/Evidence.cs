using System;

namespace EvidenceManagementSystem
{
    public class Evidence
    {
        // ATTRIBUTES
        public int Id;
        public string Name;
        public string Type;
        public string Description;
        public string Location;

        // CONSTRUCTOR 1: default constructor
        public Evidence()
        {
            Id = 0;
            Name = "";
            Type = "";
            Description = "";
            Location = "";
        }

        // CONSTRUCTOR 2: parameterized constructor
        public Evidence(int id, string name, string type, string description, string location)
        {
            Id = id;
            Name = name;
            Type = type;
            Description = description;
            Location = location;
        }

        // CONSTRUCTOR 3: copy constructor (object copying)
        public Evidence(Evidence other)
        {
            Id = other.Id;
            Name = other.Name;
            Type = other.Type;
            Description = other.Description;
            Location = other.Location;
        }
    }
}