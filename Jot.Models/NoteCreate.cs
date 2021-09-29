using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jot.Models
{
    public class NoteCreate
    {
        [Required]
        [MinLength(2, ErrorMessage ="Title must be at least 2 characters")]
        [MaxLength(100, ErrorMessage ="Title can't be longer than 100 characters")]
        public string Title { get; set; }
        [MaxLength(5000, ErrorMessage ="We don't want to stifle your thoughts, but you've exceeded the character limit!")]
        public string Content { get; set; }
    }
}
