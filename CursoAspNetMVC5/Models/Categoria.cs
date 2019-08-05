using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CursoAspNetMVC5.Models
{
    [Serializable]
    public class Categoria : IEnumerable<object>
    {
        public long CategoriaId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        private IEnumerable<object> Events()
        {
            yield return this.CategoriaId;
            yield return this.Nome;
        }

        public IEnumerator<object> GetEnumerator()
        {
            return Events().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}