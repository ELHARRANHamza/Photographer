using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Photographie.Models;
using Photographie.Models.Repository;
using Photographie.ViewModel;

namespace Photographie.Controllers
{
    public class MessageController : Controller
    {
        public Repository<Message_> RepMessage { get; }
        public UserManager<AppUsers> UserManager { get; }

        public MessageController(Repository<Message_> repMessage,
            UserManager<AppUsers> userManager)
        {
            RepMessage = repMessage;
            UserManager = userManager;
        }
        // GET: Message
        public async Task<ActionResult> Index()
        {
            var msg = await RepMessage.List();
            return View(msg);
        }
        // GET: Message/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var find_Message = await RepMessage.Find(id);
            return View(find_Message);
        }

        // POST: Message/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
               await RepMessage.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}