using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcZoolandia.Models
{
    public class Animal
    {
        public int ID { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$"), Required, StringLength(30)]
        public string Name { get; set; }
        [Required]
        public int IdHabitat { get; set; }
        [Required]
        public int IdSpecies { get; set; }
    }
}
