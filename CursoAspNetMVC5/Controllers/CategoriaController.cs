using CursoAspNetMVC5.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using CursoAspNetMVC5.Models.Repository;

namespace CursoAspNetMVC5.Controllers
{
    public class CategoriaController : Controller
    {
        CategoriaRepository repositor = new CategoriaRepository();

        // GET: Categoria
        public ActionResult Index()
        {
            var categorias = repositor.Select();
            return View(categorias.OrderBy(t => t.Nome));
        }

        //GET: Create Categoria
        //[ValidateAntiForgeryToken]//Impede que a action seja executado a partir de outro site (Hacker)
        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Categoria categoria)
        {
            if (repositor.Select().Count > 0)
            {
                categoria.CategoriaId = repositor.Select().Select(i => i.CategoriaId).Max() + 1;
            }
            else
            {
                categoria.CategoriaId = 1;
            }
            repositor.Insert(categoria);
            
            return RedirectToAction("Create");
        }

        //GET: Edit Categoria
        public ActionResult Edit(long id)
        {
            return View(repositor.Select().Where(i => i.CategoriaId==id).First());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Categoria categoria)
        {
            repositor.Update(categoria);
            return RedirectToAction("Index");
        }

        //GET: Details
        public ActionResult Details(long id)
        {
            return View(repositor.Select().Where(i => i.CategoriaId==id).First());
        }

        //Get: Delete
        public ActionResult Delete(long id)
        {
            return View(repositor.Select().Where(i => i.CategoriaId == id).First());
        }

        //Post: Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Categoria categoria)
        {
            repositor.Delete(categoria);
            return RedirectToAction("Index");
        }
    }
}