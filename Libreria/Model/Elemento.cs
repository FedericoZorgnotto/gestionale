using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Model
{
    public class Elemento
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public Elemento() { }

        public Elemento(int id)
        {
            this.id = id;
        }
    }
}
