using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiBotica.SolPedido.Entidades.Core
{
    public class Opcion
    {
        public int IdOpcion { get; set; }
        public string NombreOpcion { get; set; }
        public string UrlOpcion { get; set; }
        public string RutaImagen { get; set; }
        public int NroOrden { get; set; }
        public int IdOpcionRef { get; set; }
    }
}