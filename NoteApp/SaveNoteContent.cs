using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp
{
    public class NoteContent
    {
        public int Id { get; set; }

        public string GetNoteTitle { get; set; }

        public string GetNoteContent { get; set; }

        
        public string FullNote
        {
            get
            {
                return $"{GetNoteTitle} {GetNoteContent}";
            }
        }


    }
}
