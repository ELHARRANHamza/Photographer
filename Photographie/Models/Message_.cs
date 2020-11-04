using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Photographie.Models
{
    public class Message_
    {
        public int Id { get; set; }
        [Required]
       [StringLength(16,MinimumLength =3)]
        public string Nom { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 3)]
        public string Prenom { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Subject { get; set; }
        [Required]
        [MinLength(6)]
        public string Message { get; set; }
        public AppUsers users { get; set; }
        public DateTime dateEnvoyer { get; set; }
    }
}
