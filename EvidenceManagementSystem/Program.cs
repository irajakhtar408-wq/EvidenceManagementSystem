using EvidenceManagementSystem.EVIDANCE_BUSSINESS_LAYER;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace EvidenceManagementSystem
{
    public  class Program
        {
            // STATIC VARIABLE: one BLL object shared by all menu functions
            static EvidenceBLL bll = new EvidenceBLL();

            static void Main(string[] args)
            {
                bool running = true;

                while (running)
                {
                    ShowMenu();
                    string choice = Console.ReadLine();
                    Console.WriteLine();

                    if (choice == "1") AddEvidenceMenu();
                    else if (choice == "2") DisplayAllMenu();
                    else if (choice == "3") SearchMenu();
                    else if (choice == "4") UpdateMenu();
                    else if (choice == "5") DeleteMenu();
                    else if (choice == "6") CopyMenu();
                    else if (choice == "7") Console.WriteLine(bll.SaveEvidence());
                    else if (choice == "8") Console.WriteLine(bll.LoadEvidence());
                    else if (choice == "0")
                    {
                        running = false;
                        Console.WriteLine("Goodbye!");
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice. Please enter a number from the menu.");
                    }
                }
            }

        // STATIC FUNCTION: prints the main menu
        static void ShowMenu()
        {
            Console.Clear();

            Console.WriteLine("+============================================================================+");
            Console.WriteLine("|                                                                            |");
            Console.WriteLine("|                        EVIDENCE MANAGEMENT SYSTEM                          |");
            Console.WriteLine("|                                MAIN MENU                                   |");
            Console.WriteLine("|                                                                            |");
            Console.WriteLine("+============================================================================+");
            Console.WriteLine("|                                                                            |");
            Console.WriteLine("|      [1]  Add new evidence                                                 |");
            Console.WriteLine("|      [2]  Display all evidence                                             |");
            Console.WriteLine("|      [3]  Search evidence by ID                                            |");
            Console.WriteLine("|      [4]  Update evidence                                                  |");
            Console.WriteLine("|      [5]  Delete evidence                                                  |");
            Console.WriteLine("|      [6]  Copy evidence                                                    |");
            Console.WriteLine("|      [7]  Save records to file                                             |");
            Console.WriteLine("|      [8]  Read records from file                                           |");
            Console.WriteLine("|                                                                            |");
            Console.WriteLine("|----------------------------------------------------------------------------|");
            Console.WriteLine("|      [0]  Exit                                                             |");
            Console.WriteLine("|                                                                            |");
            Console.WriteLine("+============================================================================+");
            Console.WriteLine();
            Console.Write("  Enter your choice (0-8): ");
        }

        // STATIC FUNCTION with parameter and return value:
        // keeps asking until the user types a valid whole number
        static int ReadInt(string message)
            {
                while (true)
                {
                    Console.Write(message);
                    string text = Console.ReadLine();
                    int number;

                    if (int.TryParse(text, out number))
                    {
                        return number;
                    }
                    Console.WriteLine("Please enter a valid whole number.");
                }
            }

            // STATIC FUNCTION: shows one evidence record
            static void ShowEvidence(Evidence evidence)
            {
                Console.WriteLine("ID          : " + evidence.Id);
                Console.WriteLine("Name        : " + evidence.Name);
                Console.WriteLine("Type        : " + evidence.Type);
                Console.WriteLine("Description : " + evidence.Description);
                Console.WriteLine("Location    : " + evidence.Location);
            }

            static void AddEvidenceMenu()
            {
                Console.WriteLine("--- Add New Evidence ---");
                int id = ReadInt("Enter Evidence ID: ");
                Console.Write("Enter Evidence Name: ");
                string name = Console.ReadLine();
                Console.Write("Enter Evidence Type: ");
                string type = Console.ReadLine();
                Console.Write("Enter Description: ");
                string description = Console.ReadLine();
                Console.Write("Enter Location: ");
                string location = Console.ReadLine();

                string result = bll.AddEvidence(id, name, type, description, location);
                Console.WriteLine(result);
            }

            static void DisplayAllMenu()
            {
                Console.WriteLine("--- All Evidence Records ---");
                List<Evidence> list = bll.GetAllEvidence();

                if (list.Count == 0)
                {
                    Console.WriteLine("No evidence records found.");
                }
                else
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        Console.WriteLine("Record " + (i + 1));
                        ShowEvidence(list[i]);
                        Console.WriteLine("-------------------------");
                    }
                }

                // Using the static function of the Evidence class
              
            }

            static void SearchMenu()
            {
                Console.WriteLine("--- Search Evidence ---");
                int id = ReadInt("Enter Evidence ID to search: ");

                Evidence found = bll.SearchEvidence(id);

                if (found == null)
                {
                    Console.WriteLine("Evidence not found.");
                }
                else
                {
                    Console.WriteLine("Evidence found:");
                    ShowEvidence(found);
                }
            }

            static void UpdateMenu()
            {
                Console.WriteLine("--- Update Evidence ---");
                int id = ReadInt("Enter Evidence ID to update: ");

                Evidence found = bll.SearchEvidence(id);

                if (found == null)
                {
                    Console.WriteLine("Evidence not found.");
                    return;
                }

                Console.WriteLine("Current record:");
                ShowEvidence(found);
                Console.WriteLine();
                Console.WriteLine("Enter the new information:");

                Console.Write("New Name: ");
                string name = Console.ReadLine();
                Console.Write("New Type: ");
                string type = Console.ReadLine();
                Console.Write("New Description: ");
                string description = Console.ReadLine();
                Console.Write("New Location: ");
                string location = Console.ReadLine();

                string result = bll.UpdateEvidence(id, name, type, description, location);
                Console.WriteLine(result);
            }

            static void DeleteMenu()
            {
                Console.WriteLine("--- Delete Evidence ---");
                int id = ReadInt("Enter Evidence ID to delete: ");

                string result = bll.DeleteEvidence(id);
                Console.WriteLine(result);
            }

            static void CopyMenu()
            {
                Console.WriteLine("--- Copy Evidence ---");
                int oldId = ReadInt("Enter ID of the evidence to copy: ");
                int newId = ReadInt("Enter new ID for the copy: ");

                string result = bll.CopyEvidence(oldId, newId);
                Console.WriteLine(result);
            }
        }
    }

