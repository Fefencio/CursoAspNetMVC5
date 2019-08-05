using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursoAspNetMVC5.Models.Interfaces
{
    interface ICRUDJsonXml<T> : ICRUD<T> where T : class
    {
        void Create();
    }
}
