using System;
using System.Collections.Generic;
using System.IO;

namespace EvidenceManagementSystem
{
    public class EvidenceDAL
    {
        // STATIC VARIABLE: the file name used for saving and reading
        public static string FilePath = "evidence.txt";

        // List that stores all evidence records in memory
        private List<Evidence> evidenceList = new List<Evidence>();

        public void AddEvidence(Evidence evidence)
        {
            evidenceList.Add(evidence);
        }

        public List<Evidence> GetAllEvidence()
        {
            return evidenceList;
        }

        // Finds one evidence by ID. Returns null if not found.
        public Evidence FindById(int id)
        {
            for (int i = 0; i < evidenceList.Count; i++)
            {
                if (evidenceList[i].Id == id)
                {
                    return evidenceList[i];
                }
            }
            return null;
        }

        // Updates an existing evidence. Returns true if updated, false if not found.
        public bool UpdateEvidence(int id, string name, string type, string description, string location)
        {
            Evidence existing = FindById(id);

            if (existing == null)
            {
                return false;
            }

            existing.Name = name;
            existing.Type = type;
            existing.Description = description;
            existing.Location = location;
            return true;
        }

        // Deletes an evidence by ID. Returns true if deleted, false if not found.
        public bool DeleteEvidence(int id)
        {
            Evidence existing = FindById(id);

            if (existing == null)
            {
                return false;
            }

            evidenceList.Remove(existing);
            return true;
        }

        // FILE HANDLING (WRITING): each record is saved as 5 lines
        public void SaveToFile()
        {
            StreamWriter writer = new StreamWriter(FilePath);

            for (int i = 0; i < evidenceList.Count; i++)
            {
                writer.WriteLine(evidenceList[i].Id);
                writer.WriteLine(evidenceList[i].Name);
                writer.WriteLine(evidenceList[i].Type);
                writer.WriteLine(evidenceList[i].Description);
                writer.WriteLine(evidenceList[i].Location);
            }

            writer.Close();
        }

        // FILE HANDLING (READING): reads 5 lines for each record
        // Returns the number of records loaded, or -1 if the file does not exist.
        public int LoadFromFile()
        {
            if (!File.Exists(FilePath))
            {
                return -1;
            }

            evidenceList.Clear();

            StreamReader reader = new StreamReader(FilePath);
            string line = reader.ReadLine();

            while (line != null)
            {
                int id = Convert.ToInt32(line);
                string name = reader.ReadLine();
                string type = reader.ReadLine();
                string description = reader.ReadLine();
                string location = reader.ReadLine();

                Evidence evidence = new Evidence(id, name, type, description, location);
                evidenceList.Add(evidence);

                line = reader.ReadLine();
            }

            reader.Close();
            return evidenceList.Count;
        }
    }
}