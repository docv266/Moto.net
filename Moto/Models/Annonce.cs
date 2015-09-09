using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Runtime.CompilerServices;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Text;

namespace Motonet.Models
{
    public class Annonce
    {
        public int ID { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(60, ErrorMessage = "La longueur doit être comprise entre {2} et {1}.", MinimumLength = 3)]
        public string Titre { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(10000, ErrorMessage = "La longueur doit être comprise entre {2} et {1}.", MinimumLength = 3)]
        public string Description { get; set; }
        
        [Required]
        [Range(1900, 3000, ErrorMessage = "Veuillez saisir un nombre compris entre {2} et {1}.")]
        [DisplayAttribute(Name="Année")]
        public int Annee { get; set; }

        [Required]
        [Range(0, 999999, ErrorMessage = "Veuillez saisir un nombre compris entre {2} et {1}.")]
        [DisplayAttribute(Name = "Kilométrage")]
        public int Kilometrage { get; set; }

        [Required]
        [Range(0, 999999, ErrorMessage = "Veuillez saisir un nombre compris entre {2} et {1}.")]
        public int Prix { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "La longueur doit être comprise entre {2} et {1}.", MinimumLength = 3)]
        [DataType(DataType.Text)]
        public string Nom { get; set; }

        [Required]
        [StringLength(68, ErrorMessage = "La longueur doit être comprise entre {2} et {1}.", MinimumLength = 3)]
        [DataType(DataType.Password)]
        [DisplayAttribute(Name = "Mot de passe")]
        public string MotDePasse { get; set; }

        [Required]
        [NotMapped]
        [StringLength(68, ErrorMessage = "La longueur doit être comprise entre {2} et {1}.", MinimumLength = 3)]
        [DataType(DataType.Password)]
        [DisplayAttribute(Name = "Confirmer le mot de passe")]
        [Compare("MotDePasse", ErrorMessage = "Les mots de passe sont différents")]
        public string ConfirmerMotDePasse { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(20, MinimumLength = 20)]
        public string CodeValidation { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "La longueur doit être comprise entre {2} et {1}.", MinimumLength = 5)]
        [EmailAddress(ErrorMessage = "Entrer une adresse mail valide.")]
        public string Mail { get; set; }

        [DataType(DataType.PhoneNumber)]
        [DisplayAttribute(Name = "Téléphone")]
        [DisplayFormat(NullDisplayText = "Non renseigné")]
        public string Telephone { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public Boolean Autorisee { get; set; }

        public Boolean Validee { get; set; }

        [Required]
        [DisplayAttribute(Name = "Modèle")]
        public int MotoProposeeID { get; set; }

        [Required]
        [DisplayAttribute(Name = "Département")]
        public int DepartementID { get; set; }

        [Required]
        [DisplayAttribute(Name = "Modèles")]
        public List<int> MotosAccepteesID { get; set; }

        [DisplayAttribute(Name = "Marques")]
        public List<int> MarquesAccepteesID { get; set; }

        [DisplayAttribute(Name = "Genres")]
        public List<int> GenresAcceptesID { get; set; }

        [DisplayAttribute(Name = "Photos")]
        public List<int> PhotosID { get; set; }

        [InverseProperty("AnnoncesAvecMotoProposee")]
        public virtual Moto MotoProposee { get; set; }

        public virtual Departement Departement { get; set; }

        public virtual List<Photo> Photos { get; set; }

        [InverseProperty("AnnoncesAvecMotosAcceptees")]
        [Display(Name = "Modèles acceptés")]
        public virtual List<Moto> MotosAcceptees { get; set; }

        [Display(Name = "Marques accepteés")]
        public virtual List<Marque> MarquesAcceptees { get; set; }

        [Display(Name = "Genres acceptés")]
        public virtual List<Genre> GenresAcceptes { get; set; }

        public Annonce()
        {
            MotosAccepteesID = new List<int>();
            GenresAcceptesID = new List<int>();
            MarquesAccepteesID = new List<int>();
            PhotosID = new List<int>();

            MotosAcceptees = new List<Moto>();
            MarquesAcceptees = new List<Marque>();
            GenresAcceptes = new List<Genre>();
            Photos = new List<Photo>();

            // On génère le code de validation afin de l'envoyer à l'adresse spécifiée
            CodeValidation = GenerateRandomCode();
        }

        public string KilometrageAvecUnite
        {
            get
            {
                return Kilometrage + "km";
            }
        }

        public string PrixAvecUnite
        {
            get
            {
                return Prix + "€";
            }
        }

        public static string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] buffer4;
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }
            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }
            return ByteArraysEqual(buffer3, buffer4);
        }

        // Compares two byte arrays for equality. The method is specifically written so that the loop is not optimized. 
        [MethodImpl(MethodImplOptions.NoOptimization)]
        private static bool ByteArraysEqual(byte[] a, byte[] b)
        {
            if (Object.ReferenceEquals(a, b))
            {
                return true;
            }

            if (a == null || b == null || a.Length != b.Length)
            {
                return false;
            }

            bool areSame = true;
            for (int i = 0; i < a.Length; i++)
            {
                areSame &= (a[i] == b[i]);
            }
            return areSame;
        } 

        public static String GenerateRandomCode()
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < 20; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }

        
    }
}