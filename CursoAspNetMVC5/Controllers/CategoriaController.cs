﻿using CursoAspNetMVC5.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using CursoAspNetMVC5.Contexts;
using System.Net;

namespace CursoAspNetMVC5.Controllers
{
    public class CategoriaController : Controller
    {
        EFContext MyContext = new EFContext();

        // GET: Categoria
        public ActionResult Index()
        {
            return View(MyContext.Categorias.OrderBy(t => t.Nome));
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
            MyContext.Categorias.Add(categoria);
            MyContext.SaveChanges();
            return RedirectToAction("Create");
        }

        //GET: Edit Categoria
        public ActionResult Edit(long? id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var categoria = MyContext.Categorias.Find(id);
            if (categoria==null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                MyContext.Entry(categoria).State = System.Data.Entity.EntityState.Modified;
                MyContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoria);
        }

        //GET: Details
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var categoria = MyContext.Categorias.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        //Get: Delete
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var categoria = MyContext.Categorias.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        //Post: Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            Categoria categoria = MyContext.Categorias.Find(id);
            MyContext.Categorias.Remove(categoria);
            MyContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}