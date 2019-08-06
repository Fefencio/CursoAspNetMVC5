using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CursoAspNetMVC5.Models
{
    public class Fabricante
    {
        public long FabricanteId { get; set; }
        //[Required]
        public string Nome { get; set; }
    }
}