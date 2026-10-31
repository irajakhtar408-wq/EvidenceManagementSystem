using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace EvidenceManagementSystem.EVIDANCE_BUSSINESS_LAYER
{
    public  class EvidenceBLL
    {

        
        
            // The BLL creates one DAL object and uses it to store and read data
            private EvidenceDAL dal = new EvidenceDAL();

            // STATIC FUNCTION: checks if a text is empty or only spaces
            public static bool IsEmpty(string text)
            {
                if (text == null || text.Trim() == "")
                {
                    return true;
                }
                return false;
            }

            // Checks all text fields. Returns "" if everything is fine,
            // otherwise returns the error message.
            private string ValidateFields(string name, string type, string description, string location)
            {
                if (IsEmpty(name)) return "Evidence name cannot be empty.";
                if (IsEmpty(type)) return "Evidence type cannot be empty.";
                if (IsEmpty(description)) return "Description cannot be empty.";
                if (IsEmpty(location)) return "Location cannot be empty.";

                // The | symbol is used to separate values in the text file,
                // so it is not allowed inside the data.
                if (name.Contains("|") || type.Contains("|") ||
                    description.Contains("|") || location.Contains("|"))
                {
                    return "The character | is not allowed.";
                }

                return "";
            }

            public string AddEvidence(int id, string name, string type, string description, string location)
            {
                if (id <= 0)
                {
                    return "Error: Evidence ID must be greater than 0.";
                }

                if (dal.FindById(id) != null)
                {
                    return "Error: An evidence with this ID already exists.";
                }

                string error = ValidateFields(name, type, description, location);
                if (error != "")
                {
                    return "Error: " + error;
                }

                Evidence evidence = new Evidence(id, name.Trim(), type.Trim(), description.Trim(), location.Trim());
                dal.AddEvidence(evidence);
                return "Success: Evidence added.";
            }

            public List<Evidence> GetAllEvidence()
            {
                return dal.GetAllEvidence();
            }

            public Evidence SearchEvidence(int id)
            {
                return dal.FindById(id);
            }

            public string UpdateEvidence(int id, string name, string type, string description, string location)
            {
                string error = ValidateFields(name, type, description, location);
                if (error != "")
                {
                    return "Error: " + error;
                }

                bool updated = dal.UpdateEvidence(id, name.Trim(), type.Trim(), description.Trim(), location.Trim());

                if (updated)
                {
                    return "Success: Evidence updated.";
                }
                return "Error: Evidence not found.";
            }

            public string DeleteEvidence(int id)
            {
                bool deleted = dal.DeleteEvidence(id);

                if (deleted)
                {
                    return "Success: Evidence deleted.";
                }
                return "Error: Evidence not found.";
            }

            // OBJECT COPYING: makes a real copy of an evidence using the copy constructor
            public string CopyEvidence(int oldId, int newId)
            {
                Evidence original = dal.FindById(oldId);

                if (original == null)
                {
                    return "Error: Original evidence not found.";
                }

                if (newId <= 0)
                {
                    return "Error: New ID must be greater than 0.";
                }

                if (dal.FindById(newId) != null)
                {
                    return "Error: The new ID already exists.";
                }

                // Create a NEW separate object using the copy constructor
                Evidence copy = new Evidence(original);
                copy.Id = newId;
                copy.Name = original.Name + " (Copy)";

                dal.AddEvidence(copy);
                return "Success: Evidence copied with new ID " + newId + ".";
            }

            public string SaveEvidence()
            {
                dal.SaveToFile();
                int count = dal.GetAllEvidence().Count;
                return "Success: " + count + " record(s) saved to " + EvidenceDAL.FilePath;
            }

            public string LoadEvidence()
            {
                int count = dal.LoadFromFile();

                if (count == -1)
                {
                    return "Error: File not found. Save some records first.";
                }
                return "Success: " + count + " record(s) loaded from " + EvidenceDAL.FilePath;
            }
        }
    }

