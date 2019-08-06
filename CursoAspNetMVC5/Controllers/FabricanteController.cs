using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CursoAspNetMVC5.Contexts;
using CursoAspNetMVC5.Models;

namespace CursoAspNetMVC5.Controllers
{
    public class FabricanteController : Controller
    {
        EFContext MyContext = new EFContext();
        // GET: Fabricante
        public ActionResult Index()
        {
            return View(MyContext.Fabricantes.OrderBy(i => i.Nome));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Fabricante fabricante)
        {
            MyContext.Fabricantes.Add(fabricante);
            MyContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fabricante fabricante = MyContext.Fabricantes.Find(id);
            if (fabricante==null)
            {
                return HttpNotFound();
            }
            return View(fabricante);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Fabricante fabricante)
        {
            if (ModelState.IsValid)
            {
                MyContext.Entry(fabricante).State = System.Data.Entity.EntityState.Modified;
                MyContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fabricante);
        }

        public ActionResult Details(long? id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Fabricante fabricante = MyContext.Fabricantes.Find(id);
            if (fabricante==null)
            {
                return HttpNotFound();
            }
            return View(fabricante);
        }

        public ActionResult Delete(long? id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Fabricante fabricante = MyContext.Fabricantes.Find(id);
            if (fabricante==null)
            {
                return HttpNotFound();
            }
            return View(fabricante);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            Fabricante fabricante = MyContext.Fabricantes.Find(id);
            MyContext.Fabricantes.Remove(fabricante);
            MyContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}