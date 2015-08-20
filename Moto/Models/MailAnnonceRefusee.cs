using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Postal;

namespace Motonet.Models
{
    public class MailAnnonceRefusee : Email
    {
        public string Destinataire { get; set; }
        public string Nom { get; set; }
        public string Raison { get; set; }
    }
}