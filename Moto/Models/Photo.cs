﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Motonet.Models
{
    public class Photo
    {
        public int ID { get; set; }

        [StringLength(255)]
        public string FileName { get; set; }

        [StringLength(100)]
        public string ContentType { get; set; }

        public byte[] Content { get; set; }

        public int AnnonceID { get; set; }

        public virtual Annonce Annonce { get; set; }
    }
}