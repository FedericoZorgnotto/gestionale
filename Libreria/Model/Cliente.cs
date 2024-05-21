using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Model
{
    public class Cliente : Elemento
    {
        private string nome;
        private string cognome;
        private float sconto; 


        public string Nome { get => nome; set => nome = value; }
        public string Cognome { get => cognome; set => cognome = value; }
        public float Sconto { get => sconto; set => sconto = value; }
        
        public Cliente() : base()
        {
        }

        public Cliente(int id, string nome, string cognome, float sconto): base(id)
        {
            this.Nome = nome;
            this.Cognome = cognome;
            this.Sconto = sconto;
        }
    }
}
