using System;
using System.IO;

namespace csharp_crud_notepad
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("----------Notepad 3000----------");

            var notepad = new Notepad();
            notepad.ImportNotes(); // taking all existen notes from the file and putting them into the object list

            //-----------------------------------------------

            bool desire = true;

            while (desire)
            {
                notepad.PrintAllSelections();
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
                        //notepad.GetNoteById(idEdit);
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

                    /*                    case 7:
                                            notepad.CreateBackup();
                                            break;

                                        case 8:
                                            Console.WriteLine("Restoring");
                                            break;*/

                    case 7:
                        Console.WriteLine("Closing notepad. Goodnight.");
                        desire = false;
                        break;
                }

                //----------------------------------
                /*
                                var user = new User
                                {
                                    UserName = "ginpus",
                                    FullName = "Ponas Pusinskas",
                                    Age = 18
                                };

                                File.AppendAllLines("users.txt", new[] { user.ToString() });

                                //perrasyti Totring metoda, kad tam tikru formatu isaugotu

                                var users = File.ReadAllLines("users.txt"); // stringa konvertuoti atgal i user modeli

                                foreach (var usr in users)
                                {
                                    new User
                                    {
                                        UserName = "jadajada",
                                        FullName = "Vardenis",
                                        Age = 8
                                    };
                                }*/
            }
        }
    }
}