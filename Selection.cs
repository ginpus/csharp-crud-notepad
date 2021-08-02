using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_crud_notepad
{
    internal enum Selection
    {
        List_all_saved_notes = 1,
        Add_new_note,
        Show_current_note_objects,
        Edit_note,
        Delete_note,
        Delete_all_notes,
        Create_backup,
        Restore_from_backup,
        List_all_backed_notes,
        Exit
    }
}