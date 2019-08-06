using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CursoAspNetMVC5.Models;

namespace CursoAspNetMVC5.Contexts
{
    public class EFContext :DbContext
    {

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Fabricante> Fabricantes { get; set; }

        public EFContext() : base("Asp_Net_MVC_CS")
        {
        }
    }
}