using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcZoolandia.Models
{
    public class Genus
    {
        public int ID { get; set; }
        public string scientificName { get; set; }
        public string commonName { get; set; }
        public string url { get; set; }
    }
}
