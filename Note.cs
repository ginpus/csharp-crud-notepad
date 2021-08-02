using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace csharp_crud_notepad
{
    internal class Note
    {
        public int _id { get; set; }  // only with public and {get;set;} defined Json methods work
        public DateTime _date { get; set; }
        public string _title { get; set; }
        public string _name { get; set; }

        public Note(int id, string title, string name)
        {
            _id = id;
            _date = DateTime.Now;
            _title = title;
            _name = name;
        }

        public Note(int id, DateTime date, string title, string name) // extended constructor to be used for imported notes. NO LONGER USED with JSON methods applied
        {
            _id = id;
            _date = date;
            _title = title;
            _name = name;
        }

        public Note() // an empty constructor must be explicitly defined for json metods to work
        {
        }

        public int GetId() // returns ID of a note
        {
            return (int)_id;
        }

        public string SetTitle(string newTitle)
        {
            return _title = newTitle;
        }

        public string SetName(string newName)
        {
            return _name = newName;
        }

        public override string ToString()
        {
            return $"{_id} - {_date.ToString("yyyy/MM/dd")} - {_title} - {_name}";
        }
    }
}