using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace csharp_crud_notepad
{
    internal class Note
    {
        protected int _id;
        protected int _prevId;
        protected DateTime _date;
        protected string _title;
        protected string _name;

        public Note(string title, string name)
        {
            foreach (var line in File.ReadAllLines("notepad.txt"))
            {
                _prevId++;
            }
            _id = ++_prevId;
            _date = DateTime.Now;
            _title = title;
            _name = name;
        }

        public Note(int id, DateTime date, string title, string name) // extended constructor to be used for imported notes
        {
            _id = id;
            _date = date;
            _title = title;
            _name = name;
        }

        public override string ToString()
        {
            return $"{_id} - {_date.ToString("yyyy-MM-dd")} - {_title} - {_name}";
        }
    }
}