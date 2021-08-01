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
        protected int _maxId;

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
            foreach (var line in _savedNotes)
            {
                string[] temp = line.Split('-'); // each line in text gets separated
                int tempId = Int32.Parse(temp[0].Trim());
                DateTime tempDate = DateTime.Parse(temp[1].Trim());
                Note entry = new Note(tempId, tempDate, temp[2].Trim(), temp[3].Trim()); //values gets assigned to Note constructor
                _notepad.Add(entry);
            }
        }

        public int GetMaxId()
        {
            _savedNotes = File.ReadAllLines("notepad.txt");
            _maxId = 0;
            foreach (var line in _savedNotes)
            {
                string[] temp = line.Split('-'); // each line in text gets separated
                int tempId = Int32.Parse(temp[0].Trim());
                if (tempId > _maxId)
                {
                    _maxId = tempId;
                }
            }
            return _maxId;
        }

        public void PrintNotes() // print all existing notes FROM FILE into console
        {
            _savedNotes = File.ReadAllLines("notepad.txt");
            Console.WriteLine("Current notepad entries:");
            foreach (var line in _savedNotes)
            {
                Console.WriteLine(line);
            }
            Console.WriteLine("------End of notes------");
        }

        public void ShowNotes() // print existent notepad objects FROM NOTEPAD OBJECT LIST into console
        {
            foreach (var entry in _notepad)
            {
                Console.WriteLine(entry.ToString());
            }
            Console.WriteLine("------End of notes------");
        }

        public void InsertNote() // ID will always be greater than current greatest one. Date will always be creation or modification date
        {
            Console.Write("Note title: ");
            var title = Console.ReadLine();
            Console.Write("Note text: ");
            var name = Console.ReadLine();
            _newNote = new Note(GetMaxId() + 1, title, name); // default ID gets assigned as defined in Note constructor
            _notepad.Add(_newNote);
            File.AppendAllLines("notepad.txt", new[] { _newNote.ToString() });
        }

        public Note GetNoteById(int id) // method to return Note object
        {
            foreach (Note note in _notepad)
            {
                if (note.GetId() == id)
                {
                    return note;
                }
            }
            return null;
        }

        public void DeleteNote(Note note) // returns the edited list without specific Note (which gets deleted)
        {
            _notepad.Remove(note);
            File.WriteAllText(_fileName, ""); // deletes current contents from the file
            foreach (var entry in _notepad)
            {
                File.AppendAllLines("notepad.txt", new[] { entry.ToString() }); // rewrites all entries to the file
            }
        }

        public void EditNote(Note note)
        {
            //issaugoti indexa, is kur paimtas irasas
            _notepad.Remove(note);
            File.WriteAllText(_fileName, ""); // deletes current contents from the file
            foreach (var entry in _notepad)
            {
                File.AppendAllLines("notepad.txt", new[] { entry.ToString() }); // rewrites all entries to the file
            }
        }

        public void DeleteAllNotes()
        {
            _notepad = new List<Note> { }; // deletes all entries from the notepad list
            File.WriteAllText(_fileName, ""); // deletes the file contents
        }

        public void CreateBackup() // putting all the notes into backup
        {
            if (!File.Exists("notepad_backup.txt"))
            {
                File.Create("notepad_backup.txt");
            }
            File.WriteAllText("notepad_backup.txt", "");
            foreach (var entry in _notepad)
            {
                File.AppendAllLines("notepad_backup.txt", new[] { entry.ToString() });
            }
        }

        public void RestoreFromBackup() // adding all notes from file into object
        {
            _notepad = new List<Note> { }; // deletes all entries from the notepad list
            File.WriteAllText(_fileName, ""); // deletes the file contents
            var restoredNotes = File.ReadAllLines("notepad_backup.txt");
            foreach (var line in restoredNotes)
            {
                string[] temp = line.Split('-'); // each line in text gets separated
                int tempId = Int32.Parse(temp[0].Trim());
                DateTime tempDate = DateTime.Parse(temp[1].Trim());
                Note entry = new Note(tempId, tempDate, temp[2].Trim(), temp[3].Trim()); //values gets assigned to Note constructor
                _notepad.Add(entry);
                File.AppendAllLines("notepad.txt", new[] { entry.ToString() });
            }
        }

        public void PrintAllSelections()
        {
            var count = 0;
            foreach (var name in Enum.GetNames(typeof(Selection)))
            {
                Console.WriteLine($"{++count} - {name}");
            }
        }
    }
}