using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Photographie.Models
{
    public class AppUsers:IdentityUser
    {
        public string AboutMySelf { get; set; }
        public string Image_ { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public List<Message_> messages { get; set; }
        public List<Photo_> GetPhotos { get; set; }
    }
}
