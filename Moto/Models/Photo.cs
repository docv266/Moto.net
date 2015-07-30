using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Motonet.Models
{
    public class Photo
    {
        public int ID { get; set; }
        public string Url { get; set; }

        public virtual Annonce Annonce { get; set; }
    }
}