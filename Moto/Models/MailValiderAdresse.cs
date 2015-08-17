using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Postal;

namespace Motonet.Models
{
    public class MailValiderAdresse : Email
    {
        public string Destinataire { get; set; }
        public string Nom { get; set; }
        public string Lien { get; set; }
    }
}