using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace csharp_crud_notepad
{
    internal class Notepad
    {
        public List<Note> _notepad { get; set; }

        //protected string _fileName = "notepad.txt";
        protected string _fileNameJ = "notepad_json.txt";

        //protected string _backupFileName = "notepad_backup.txt";
        protected string _backupFileNameJ = "notepad_json_backup.txt";

        public string[] _savedNotes { get; set; }
        public string[] _backedNotes { get; set; }
        public Note _newNote { get; set; }
        public int _maxId { get; set; }

        public Notepad()
        {
            _notepad = new List<Note> { }; // creating of empty notepad to place note objects (entries)
            if (!File.Exists(_fileNameJ)) // if notepad file does not exit, it gets created
            {
                File.Create(_fileNameJ);
            }
        }

        public void ImportNotes() // adding all notes from file into object
        {
            _savedNotes = File.ReadAllLines(_fileNameJ);
            foreach (var line in _savedNotes)
            {
                var entry = JsonSerializer.Deserialize<Note>(line);
                _notepad.Add(entry);
            }
            Console.Write("-> Notes loaded from the file");
        }

        public int GetMaxId()
        {
            _savedNotes = File.ReadAllLines(_fileNameJ); // this is based on the fact that what is saved is identical what is in memory. REMAKE TO USE OBJECT LIST instead
            _maxId = 0;
            foreach (var line in _savedNotes)
            {
                var entry = JsonSerializer.Deserialize<Note>(line);
                if (entry._id > _maxId)
                {
                    _maxId = entry._id;
                }
            }
            return _maxId;
        }

        public void PrintNotes() // print all existing notes FROM FILE into console
        {
            _savedNotes = File.ReadAllLines(_fileNameJ);
            Console.WriteLine("Current saved notepad entries:");
            foreach (var line in _savedNotes)
            {
                var entry = JsonSerializer.Deserialize<Note>(line);
                Console.WriteLine(entry.ToString());
            }
            Console.WriteLine("------End of notes------\n");
        }

        public void ShowNotes() // print existent notepad objects FROM NOTEPAD OBJECT LIST into console
        {
            Console.WriteLine("Current active (in-memory) notepad entries:");
            foreach (var entry in _notepad)
            {
                Console.WriteLine(entry.ToString());
            }
            Console.WriteLine("------End of notes------\n");
        }

        public void InsertNote() // ID will always be greater than current greatest one. Date will always be creation or modification date
        {
            Console.Write("Note title: ");
            string title = Console.ReadLine();
            Console.Write("Note text: ");
            string name = Console.ReadLine();
            _newNote = new Note(GetMaxId() + 1, title, name); // disregards, if there are any gaps in numeration. NENAUDOTI GERIAU
            _notepad.Add(_newNote); // inserting into object list
            string newJsonNote = JsonSerializer.Serialize<Note>(_newNote); //inserting into JSON file
            File.AppendAllLines(_fileNameJ, new[] { newJsonNote });
            Console.Write($"-> New note inserted: \n{_newNote}");
            //Console.Write($"-> New note inserted: \n{_newNote.ToString()}\n{newJsonNote}");
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
            File.WriteAllText(_fileNameJ, ""); // deletes current contents from the file
            foreach (var entry in _notepad)
            {
                string newJsonNote = JsonSerializer.Serialize<Note>(entry);
                File.AppendAllLines(_fileNameJ, new[] { newJsonNote });
            }
            Console.Write("-> Note deleted");
        }

        public void EditNote(int noteId)
        {
            //issaugoti indexa, is kur paimtas irasas
            var note = _notepad.FirstOrDefault(x => x.GetId() == noteId);
            if (note != null)
            {
                Console.Write("Insert new title: ");
                var title = Console.ReadLine();
                note.SetTitle(title);
                Console.Write("Insert new name: ");
                var name = Console.ReadLine();
                note.SetName(name);
            }
            File.WriteAllText(_fileNameJ, ""); // deletes current contents from the file
            foreach (var entry in _notepad)
            {
                string newJsonNote = JsonSerializer.Serialize<Note>(entry);
                File.AppendAllLines(_fileNameJ, new[] { newJsonNote });
            }
            Console.Write("-> Note modified");
        }

        public void DeleteAllNotes()
        {
            _notepad = new List<Note> { }; // deletes all entries from the notepad list
            File.WriteAllText(_fileNameJ, ""); // empties file contents
            Console.Write("-> All notes deleted");
        }

        public void CreateBackup() // putting all the notes into backup
        {
            if (!File.Exists(_backupFileNameJ)) // jei failas neegzistuoja, tai jis sukuriamas, bet programa luzta, nes nera multithreadingo
            {
                File.Create(_backupFileNameJ);
            }
            File.WriteAllText(_backupFileNameJ, "");
            foreach (var entry in _notepad)
            {
                string newJsonNote = JsonSerializer.Serialize<Note>(entry);
                File.AppendAllLines(_backupFileNameJ, new[] { newJsonNote });
            }
            Console.Write("-> Notepad entries saved in backup");
        }

        public void RestoreFromBackup() // adding all notes from file into object
        {
            _notepad = new List<Note> { }; // deletes all entries from the notepad list
            File.WriteAllText(_fileNameJ, ""); // deletes the file contents
            var restoredNotes = File.ReadAllLines(_backupFileNameJ);
            foreach (var line in restoredNotes)
            {
                var entry = JsonSerializer.Deserialize<Note>(line);
                _notepad.Add(entry);
                File.AppendAllLines(_fileNameJ, new[] { line });
            }
            Console.Write("-> Notepad entries restored from backup");
        }

        public void PrintBackupNotes() // print all existing notes FROM FILE into console
        {
            _backedNotes = File.ReadAllLines(_backupFileNameJ);
            Console.WriteLine("Current backed-up notepad entries:");
            foreach (var line in _backedNotes)
            {
                var entry = JsonSerializer.Deserialize<Note>(line);
                Console.WriteLine(entry.ToString());
            }
            Console.WriteLine("------End of notes------\n");
        }

        public void PrintAllSelections()
        {
            var count = 0;
            foreach (var name in Enum.GetNames(typeof(Selection)))
            {
                Console.WriteLine($"{++count} - {name}");
            }
        }

        // FLAT FILE USAGE METHODS BACKUP:

        /*        public void ImportNotes() // adding all notes from file into object
                {
                    _savedNotes = File.ReadAllLines(_fileName);
                    foreach (var line in _savedNotes)
                    {
                        string[] temp = line.Split('-'); // each line in text gets separated
                        int tempId = Int32.Parse(temp[0].Trim());
                        DateTime tempDate = DateTime.Parse(temp[1].Trim());
                        Note entry = new Note(tempId, tempDate, temp[2].Trim(), temp[3].Trim()); //values gets assigned to Note constructor
                        _notepad.Add(entry); //evry created Note gets added to master notepad
                    }
                    Console.Write("-> Notes loaded from the file");
                }*/

        /*        public int GetMaxId()
                {
                    _savedNotes = File.ReadAllLines(_fileName); // this is based on the fact that what is saved is identical what is in memory
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
                }*/

        /*        public void PrintNotes() // print all existing notes FROM FILE into console
                {
                    _savedNotes = File.ReadAllLines(_fileName);
                    Console.WriteLine("Current saved notepad entries:");
                    foreach (var line in _savedNotes)
                    {
                        Console.WriteLine(line);
                    }
                    Console.WriteLine("------End of notes------\n");
                }*/

        /*        public void InsertNote() // ID will always be greater than current greatest one. Date will always be creation or modification date
        {
            Console.Write("Note title: ");
            string title = Console.ReadLine();
            Console.Write("Note text: ");
            string name = Console.ReadLine();
            _newNote = new Note(GetMaxId() + 1, title, name); // disregards, if there are any gaps in numeration
            // inserting into object list
            _notepad.Add(_newNote);
            //inserting into file as plain text
            File.AppendAllLines(_fileName, new[] { _newNote.ToString() });
        }*/

        /*        public void DeleteNote(Note note) // returns the edited list without specific Note (which gets deleted)
        {
            _notepad.Remove(note);
            File.WriteAllText(_fileName, ""); // deletes current contents from the file
            foreach (var entry in _notepad)
            {
                File.AppendAllLines(_fileName, new[] { entry.ToString() }); // rewrites all entries to the file
            }
            Console.Write("-> Note deleted");
        }*/

        /*        public void EditNote(int noteId)
                {
                    //issaugoti indexa, is kur paimtas irasas
                    var note = _notepad.FirstOrDefault(x => x.GetId() == noteId);
                    if (note != null)
                    {
                        Console.Write("Insert new title: ");
                        var title = Console.ReadLine();
                        note.SetTitle(title);
                        Console.Write("Insert new name: ");
                        var name = Console.ReadLine();
                        note.SetName(name);
                    }
                    File.WriteAllText(_fileName, ""); // deletes current contents from the file
                    foreach (var entry in _notepad)
                    {
                        File.AppendAllLines(_fileName, new[] { entry.ToString() }); // rewrites all entries to the file
                    }
                    Console.Write("-> Note modified");
                }*/

        /*        public void DeleteAllNotes()
        {
            _notepad = new List<Note> { }; // deletes all entries from the notepad list
            File.WriteAllText(_fileName, ""); // empties file contents
            Console.Write("-> All notes deleted");
        }*/

        /*        public void CreateBackup() // putting all the notes into backup
                {
                    if (!File.Exists(_backupFileName))
                    {
                        File.Create(_backupFileName);
                    }
                    File.WriteAllText(_backupFileName, "");
                    foreach (var entry in _notepad)
                    {
                        File.AppendAllLines(_backupFileName, new[] { entry.ToString() });
                    }
                    Console.Write("-> Notepad emtries saved in backup");
                }*/

        /*        public void RestoreFromBackup() // adding all notes from file into object
                {
                    _notepad = new List<Note> { }; // deletes all entries from the notepad list
                    File.WriteAllText(_fileName, ""); // deletes the file contents
                    var restoredNotes = File.ReadAllLines(_backupFileName);
                    foreach (var line in restoredNotes)
                    {
                        string[] temp = line.Split('-'); // each line in text gets separated
                        int tempId = Int32.Parse(temp[0].Trim());
                        DateTime tempDate = DateTime.Parse(temp[1].Trim());
                        Note entry = new Note(tempId, tempDate, temp[2].Trim(), temp[3].Trim()); //values gets assigned to Note constructor
                        _notepad.Add(entry);
                        File.AppendAllLines(_fileName, new[] { entry.ToString() });
                    }
                    Console.Write("-> Notepad emtries restored from backup");
                }*/
    }
}