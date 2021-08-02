using System;
using System.IO;
using System.Text.Json;

namespace csharp_crud_notepad
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("----------Notepad 3000----------");

            var notepad = new Notepad();
            notepad.ImportNotes(); // taking all existen notes from the file and putting them into the object list

            bool desire = true;

            while (desire)
            {
                Console.WriteLine("\nPress any key to continue. ");
                Console.ReadKey();
                notepad.PrintAllSelections();
                Console.WriteLine("\nSelect action: ");
                var input = Console.ReadLine();
                int choice = Convert.ToInt32(input);

                switch (choice)
                {
                    case 1:
                        notepad.PrintNotes();
                        break;

                    case 2:
                        notepad.InsertNote();
                        break;

                    case 3:
                        notepad.ShowNotes();
                        break;

                    case 4:
                        Console.WriteLine("Current list of notes: ");
                        notepad.ShowNotes();
                        Console.WriteLine("Select ID of note to edit: ");
                        var input2 = Console.ReadLine();
                        int idEdit = Convert.ToInt32(input2);
                        notepad.EditNote(idEdit);
                        break;

                    case 5:
                        Console.WriteLine("Current list of notes: ");
                        notepad.ShowNotes();
                        Console.WriteLine("Select ID of note to delete: ");
                        var input3 = Console.ReadLine();
                        int idDelete = Convert.ToInt32(input3);
                        notepad.DeleteNote(notepad.GetNoteById(idDelete));
                        break;

                    case 6:
                        notepad.DeleteAllNotes();
                        break;

                    case 7:
                        notepad.CreateBackup();
                        break;

                    case 8:
                        notepad.RestoreFromBackup();
                        break;

                    case 9:
                        notepad.PrintBackupNotes();
                        break;

                    case 10:
                        Console.WriteLine("Closing notepad. Goodnight.");
                        desire = false;
                        break;
                }
            }
        }
    }
}