using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace MvcZoolandia.ViewModels
{
    public class DocumentDetailsViewModel
    {
        //animal
        public int ID { get; set; }
        public string Animal { get; set; }
        public int HabitatId { get; set; }
        public int SpeciesId { get; set; }

        //document
        public int DocumentId { get; set; }
        public string Title { get; set; }
        public string FileName { get; set; }
        public byte[] Contents { get; set; }
        public DateTime UploadDate { get; set; }
        public string UploadUserId { get; set; }
        public List<Models.Document> Documents { get; set; }
    }
}
