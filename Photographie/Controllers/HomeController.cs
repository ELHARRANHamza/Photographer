using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Photographie.Models;
using Photographie.Models.Repository;
using Photographie.ViewModel;

namespace Photographie.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(Repository<Photo_> rep_Protfolio,
            Repository<Message_> repMessage,
            UserManager<AppUsers> userManager)
        {
            Rep_Protfolio = rep_Protfolio;
            RepMessage = repMessage;
            UserManager = userManager;
        }

        public Repository<Photo_> Rep_Protfolio { get; }
        public Repository<Message_> RepMessage { get; }
        public UserManager<AppUsers> UserManager { get; }

        public async Task<ActionResult> Index()
        {
            var liste_Protfolio = await Rep_Protfolio.List();
            var users = await UserManager.FindByIdAsync("c1661d14-1885-4a72-a4a5-929952f0cb67");
            var home = new HomeViewModel()
            {
                GetPhotos = liste_Protfolio,
                users=users
            };
            return View(home);
        }
        
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Create(HomeViewModel message_)
        {
            try
            {
                var users = await UserManager.FindByIdAsync("c1661d14-1885-4a72-a4a5-929952f0cb67");
                var message = new Message_
                {
                    dateEnvoyer = DateTime.Now,
                    Email = message_.Email,
                    Nom = message_.Nom,
                    Prenom = message_.Prenom,
                    Subject = message_.Subject,
                    Message = message_.Message,
                    users = users
                };
                await RepMessage.Add(message);
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

    }
}