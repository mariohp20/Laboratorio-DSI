using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MiBotica.SolPedido.Entidades.Core
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string CodUsuario { get; set; }
        public byte[] Clave { get; set; }
        public string Nombres { get; set; }



        public string ClaveTexto { get; set; }
    }
}