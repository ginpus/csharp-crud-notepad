using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace csharp_crud_notepad
{
    internal class Notepad
    {
        protected List<Note> _notepad;
        protected string _fileName;
        protected string[] _savedNotes;
        protected Note _newNote;

        public Notepad()
        {
            _notepad = new List<Note> { }; // creating of empty notepad to place note objects (entries)
            _fileName = "notepad.txt";
            if (!File.Exists(_fileName)) // if notepad file does not exit, it gets created
            {
                File.Create(_fileName);
            }
        }

        public void ImportNotes() // adding all notes from file into object
        {
            _savedNotes = File.ReadAllLines("notepad.txt");
            var count = 0;
            foreach (var line in _savedNotes)
            {
                _notepad.Add(new Note(++count, DateTime.Now, $"{++count}", "two"));
            }
        }

        public void PrintNotes() // print all existing notes from file into console
        {
            _savedNotes = File.ReadAllLines("notepad.txt");
            Console.WriteLine("Current notepad entries:");
            foreach (var line in _savedNotes)
            {
                Console.WriteLine(line);
            }
        }

        public void ShowNotes() // print existent notepad objects into console
        {
            foreach (var entry in _notepad)
            {
                Console.WriteLine(entry.ToString());
            }
        }

        public void InsertNote()
        {
            Console.WriteLine("Note title:");
            var title = Console.ReadLine();
            Console.WriteLine("Note text:");
            var name = Console.ReadLine();
            _newNote = new Note(title, name);
            _notepad.Add(_newNote);
            File.AppendAllLines("notepad.txt", new[] { _newNote.ToString() });
        }

        public void PrintAllSelections()
        {
            var count = 0;
            foreach (var name in Enum.GetNames(typeof(Selection)))
            {
                Console.WriteLine($"{++count}: {name}");
            }
        }
    }
}