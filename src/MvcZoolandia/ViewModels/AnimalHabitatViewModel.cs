using Microsoft.AspNet.Mvc.Rendering;
using MvcZoolandia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcZoolandia.ViewModels
{
    public class AnimalHabitatViewModel
    {
        public List<Animal> animals;
        public SelectList habitats;
        public string animalHabitat { get; set; }
    }
}
