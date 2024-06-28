﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using Microsoft.VisualBasic;
using System.Text.RegularExpressions;

namespace DiaryManager
{

    public class DailyDiary
    {
        private const string DiaryFilePath = "../../../diarydata.txt";
        private const string DateFormat = @"^\d{4}-\d{2}-\d{2}$";

        public void RunApp()
        {
            string choice = string.Empty;
            while (choice != "E")
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("R. Read Diary");
                Console.WriteLine("A. Add New Entry");
                Console.WriteLine("D. Delete Entry");
                Console.WriteLine("C. Count All Lines");
                Console.WriteLine("S. Search Entries");
                Console.WriteLine("E. Exit");

                choice = Console.ReadLine().ToUpper();

                switch (choice)
                {
                    case "R":
                        ReadDiaryFile();
                        break;
                    case "A":
                        AddEntry();
                        break;
                    case "D":
                        DeleteEntry();
                        break;
                    case "C":
                        CountLines();
                        break;
                    case "S":
                        SearchEntries();
                        break;
                    case "E":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }


        private bool IsValidDate(string date) => Regex.IsMatch(date, DateFormat);

        public void ReadDiaryFile()
        {
            try
            {
                if (File.Exists(DiaryFilePath))
                {
                    string[] lines = File.ReadAllLines(DiaryFilePath);
                    foreach (string line in lines)
                    {
                        Console.WriteLine(line);
                    }
                }
                else
                {
                    Console.WriteLine("Diary file not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading diary file: {ex.Message}");
            }
        }

        public void AddEntry()
        {
            try
            {
                Console.WriteLine("Enter the date (YYYY-MM-DD):");
                string date = Console.ReadLine();

                while (!IsValidDate(date))
                {
                    Console.WriteLine("Invalid date format. Please use YYYY-MM-DD.");
                    date = Console.ReadLine();
                }

                Console.WriteLine("Enter the content:");
                string content = Console.ReadLine();

                string formattedEntry = $"{Environment.NewLine}{date}{Environment.NewLine}{content}{Environment.NewLine}";
                File.AppendAllText(DiaryFilePath, formattedEntry);
                Console.WriteLine("Entry added successfully.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding entry: {ex.Message}");
            }
        }

        public void DeleteEntry()
        {
            try
            {
                Console.WriteLine("Enter the date (YYYY-MM-DD) of the entry to delete:");
                string date = Console.ReadLine();

                while (!IsValidDate(date))
                {
                    Console.WriteLine("Invalid date format. Please use YYYY-MM-DD.");
                    date = Console.ReadLine();
                }

                if (File.Exists(DiaryFilePath))
                {
                    var lines = File.ReadAllLines(DiaryFilePath).ToList();
                    bool entryFound = false;

                    for (int i = 0; i < lines.Count; i++)
                    {
                        if (lines[i].StartsWith(date))
                        {
                            entryFound = true;
                            lines.RemoveAt(i); // Remove the date line
                            if (i < lines.Count && !IsValidDate(lines[i]))
                            {
                                lines.RemoveAt(i); // Remove the content line
                            }
                            break;
                        }
                    }

                    if (!entryFound)
                    {
                        Console.WriteLine("No entry found for the specified date.");
                        return;
                    }

                    File.WriteAllLines(DiaryFilePath, lines);
                    Console.WriteLine("Entry deleted successfully.\n");
                }
                else
                {
                    Console.WriteLine("Diary file not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting entry: {ex.Message}");
            }
        }

        public void CountLines()
        {
            try
            {
                if (File.Exists(DiaryFilePath))
                {
                    int lineCount = File.ReadAllLines(DiaryFilePath).Length;
                    Console.WriteLine($"Total number of lines: {lineCount}\n");
                }
                else
                {
                    Console.WriteLine("Diary file not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error counting lines: {ex.Message}");
            }
        }

        public void SearchEntries()
        {
            try
            {
                Console.WriteLine("Enter the date (YYYY-MM-DD) to retrieve data:");
                string date = Console.ReadLine();

                while (!IsValidDate(date))
                {
                    Console.WriteLine("Invalid date format. Please use YYYY-MM-DD.");
                    date = Console.ReadLine();
                }

                if (File.Exists(DiaryFilePath))
                {
                    var lines = File.ReadAllLines(DiaryFilePath);
                    bool entryFound = false;

                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (lines[i].Equals(date))
                        {
                            Console.WriteLine("Data retrieved: ");
                            entryFound = true;
                            Console.WriteLine(lines[i]); // Print the date
                            if (i + 1 < lines.Length)
                            {
                                Console.WriteLine(lines[i + 1]); // Print the content
                            }
                            Console.WriteLine(); // Add a blank line between entries
                        }
                    }

                    if (!entryFound)
                    {
                        Console.WriteLine("No entries found for the specified date.");
                    }
                }
                else
                {
                    Console.WriteLine("Diary file not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching entries: {ex.Message}");
            }
        }
    }

}