using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using MvcZoolandia.Models;
using System.Collections;
using MvcZoolandia.ViewModels;
using System;
using Microsoft.AspNet.Http;
using Microsoft.Net.Http.Headers;
using System.IO;

namespace MvcZoolandia.Controllers
{
    public class AnimalController : Controller
    {
        private ApplicationDbContext _context;

        public AnimalController(ApplicationDbContext context)
        {
            _context = context;    
        }

        #region Index

        // GET: Animals
        public IActionResult Index(string searchString)
        {
            var  ListOfAnimals =
                (from animal in _context.Animal
                 join habitat in _context.Habitat
                 on animal.IdHabitat equals habitat.ID

                 join species in _context.Species
                 on animal.IdSpecies equals species.ID

                 
                 //basically means 'create new' based on search results
                 select new AnimalViewModel
                 {
                     ID = animal.ID,
                     Animal = animal.Name,
                     Habitat = habitat.Name,
                     Species = species.commonName

                 }).ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                var filteredAnimalInfo = ListOfAnimals.Where(item => item.Animal == searchString);
                return View(filteredAnimalInfo);
            }

            return View(ListOfAnimals);
        }
        #endregion

        #region Detail Section

        // GET: Animals/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Animal animal = _context.Animal.Single(a => a.ID == id);
            if (animal == null)
            {
                return HttpNotFound();
            }
            //return View(animal);

            var docs = (from d in _context.Document
                            select new Document
                            {

                            }).ToList();

            DocumentDetailsViewModel vm = new DocumentDetailsViewModel()
            {
                Documents = docs,
                ID = animal.ID,
                Animal = animal.Name,
                HabitatId = animal.IdHabitat,
                SpeciesId = animal.IdSpecies
            };
 
            return View(vm);
        }

        //POST
        [HttpPost]
        public IActionResult Details(IFormFile file, DocumentDetailsViewModel docViewModel)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                if (file != null && file.Length > 0)
                    try
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        using (var reader = new StreamReader(file.OpenReadStream()))
                        {
                            string contentAsString = reader.ReadToEnd();
                            byte[] bytes = new byte[contentAsString.Length * sizeof(char)];
                            System.Buffer.BlockCopy(contentAsString.ToCharArray(), 0, bytes, 0, bytes.Length);
                            var fileType = file.ContentType;
                            Document doc = new Document()
                            {
                                Title = docViewModel.Title,
                                FileName = fileName,
                                Contents = bytes,
                                ContentType = fileType,
                                UploadDate = DateTime.Now,
                                UploadUserId = "JeremyLandi@gmail.com"
                            };

                            dbContext.Document.Add(doc);
                            dbContext.SaveChanges();
                        }

                        ViewBag.Message = "File uploaded successfully";

                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "ERROR:" + ex.Message.ToString();
                    }
                //go back to index and the new doc should show

                var docs = from d in dbContext.Document
                           select d;

                DocumentDetailsViewModel vm = new DocumentDetailsViewModel()
                {
                    Documents = docs.ToList()

                };
                return View(vm);
            }
        }

        #endregion

        #region Create Section

        // GET: Animals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Animals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Animal animal)
        {
            if (ModelState.IsValid)
            {
                _context.Animal.Add(animal);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(animal);
        }

        #endregion

        #region Edit Section

        // GET: Animals/Edit/5
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Animal animal = _context.Animal.Single(m => m.ID == id);
            if (animal == null)
            {
                return HttpNotFound();
            }
            return View(animal);
        }

        // POST: Animals/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Animal animal)
        {
            if (ModelState.IsValid)
            {
                _context.Update(animal);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(animal);
        }

        #endregion

        #region Delete Section

        // GET: Animals/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Animal animal = _context.Animal.Single(m => m.ID == id);
            if (animal == null)
            {
                return HttpNotFound();
            }

            return View(animal);
        }       

        // POST: Animals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Animal animal = _context.Animal.Single(m => m.ID == id);
            _context.Animal.Remove(animal);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion
    }
}
