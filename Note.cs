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
        protected DateTime _date;
        protected string _title;
        protected string _name;

        public Note(int id, string title, string name)
        {
            _id = id;
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

        public int GetId() // returns ID of a note
        {
            return (int)_id;
        }

        public override string ToString()
        {
            return $"{_id} - {_date.ToString("yyyy/MM/dd")} - {_title} - {_name}";
        }
    }
}