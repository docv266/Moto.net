using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Postal;

namespace Motonet.Models
{
    public class MailEnvoyer : Email
    {
        public string Destinataire { get; set; }
        public string TitreAnnonce { get; set; }
        public string Expediteur { get; set; }
        public string Message { get; set; }
    }
}