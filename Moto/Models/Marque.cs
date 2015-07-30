using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Motonet.Models
{
    public class Marque
    {
        public int ID { get; set; }
        public string Nom { get; set; }

        public virtual List<Moto> Motos { get; set; }
    }
}