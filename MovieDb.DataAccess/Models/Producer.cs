using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDb.DataAccess
{
    public class Producer
    {
        [Key]
        public int RowId { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Sex { get; set; }

        public ICollection<Movie> Movies { get; set; }
    }
}
