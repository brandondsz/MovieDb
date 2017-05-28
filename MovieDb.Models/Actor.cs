using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.d


namespace MovieDb.Models
{
    public class Actor
    {
        [Key]
        public int RowId { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public DateTime DateOfBirth { get; set; }
        public char Sex { get; set; }
    }
}
