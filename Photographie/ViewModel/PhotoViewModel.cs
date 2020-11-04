using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Photographie.ViewModel
{
    public class PhotoViewModel
    {
        public int Id { get; set; }
        public string Image_ { get; set; }
        public IFormFile file { get; set; }
    }
}
