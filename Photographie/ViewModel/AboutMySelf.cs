using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Photographie.ViewModel
{
    public class AboutMySelf
    {
        [Required]
        [MinLength(6)]
        public string Description { get; set; }
        public string Image_ { get; set; }
        [Required]
        [StringLength(16,MinimumLength =4)]
        public string nom { get; set; }
        [Required]
        [StringLength(16, MinimumLength = 4)]
        public string prenom { get; set; }
        public IFormFile file { get; set; }
        public string UserName { get; set; }
    }
}
