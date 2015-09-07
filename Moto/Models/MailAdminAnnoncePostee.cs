using Postal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Motonet.Models
{
    public class MailAdminAnnoncePostee : Email
    {
        public string Lien { get; set; }
    }
}