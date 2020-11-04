using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Photographie.Models;
using Photographie.ViewModel;

namespace Photographie.Controllers
{
    public class AboutMySelfController : Controller
    {
        public UserManager<AppUsers> UserManager { get; }
        public SignInManager<AppUsers> SignIn { get; }
        public IHostingEnvironment Hosting { get; }

        public AboutMySelfController(UserManager<AppUsers> userManager,
            SignInManager<AppUsers> signIn,
            IHostingEnvironment hosting)
        {
            UserManager = userManager;
            SignIn = signIn;
            Hosting = hosting;
        }
        // GET: AboutMySelf
        public async Task<IActionResult> Index()
        {
            if (SignIn.IsSignedIn(User))
            {
                var user = await UserManager.FindByIdAsync("c1661d14-1885-4a72-a4a5-929952f0cb67");
                var model = new AboutMySelf()
                {
                    UserName = user.UserName,
                    Image_ = user.Image_,
                    Description = user.AboutMySelf,
                    nom = user.nom,
                    prenom = user.prenom
                };
                return View(model);
            }
            return NotFound();
        }

        // GET: AboutMySelf/Details/5
        public async Task<IActionResult> Details()
        {
            if (SignIn.IsSignedIn(User))
            {
                var user = await UserManager.FindByIdAsync("c1661d14-1885-4a72-a4a5-929952f0cb67");
                var model = new AboutMySelf()
                {
                    UserName = user.UserName,
                    Image_ = user.Image_,
                    Description = user.AboutMySelf,
                    nom = user.nom,
                    prenom = user.prenom
                };
                return View(model);
            }
            return NotFound();
        }

        // GET: AboutMySelf/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AboutMySelf/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AboutMySelf/Edit/5
        public async Task<IActionResult> Edit()
        {
            if (SignIn.IsSignedIn(User))
            {
                var user = await UserManager.FindByIdAsync("c1661d14-1885-4a72-a4a5-929952f0cb67");
                var model = new AboutMySelf()
                {
                    UserName = user.UserName,
                    Image_ = user.Image_,
                    Description = user.AboutMySelf,
                    nom = user.nom,
                    prenom = user.prenom
                };
                return View(model);
            }
            return NotFound();
        }

        // POST: AboutMySelf/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AboutMySelf mySelf)
        {
            if (SignIn.IsSignedIn(User))
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        string fileName = "";
                        if (mySelf.file.FileName != null)
                        {
                            if (mySelf.Image_ == null)
                            {
                                string chemain = Path.Combine(Hosting.WebRootPath, "MyImage");
                                fileName = mySelf.file.FileName;
                                string path = Path.Combine(chemain, fileName);
                                await mySelf.file.CopyToAsync(new FileStream(path
                                     , FileMode.Create));
                            }
                            else
                            {
                                string chemain = Path.Combine(Hosting.WebRootPath, "MyImage");
                                fileName = mySelf.file.FileName;
                                string path = Path.Combine(chemain, fileName);
                                string oldPath = Path.Combine(chemain, mySelf.Image_);
                                if (path != oldPath)
                                {
                                    System.IO.File.Delete(oldPath);
                                    await mySelf.file.CopyToAsync(new FileStream(path
                                         , FileMode.Create));
                                }
                            }
                        }
                        else
                        {
                            fileName = mySelf.Image_;
                        }
                        var user = await UserManager.FindByIdAsync("c1661d14-1885-4a72-a4a5-929952f0cb67");
                        user.AboutMySelf = mySelf.Description;
                        user.Image_ = fileName;
                        user.prenom = mySelf.prenom;
                        user.nom = mySelf.nom;
                        await UserManager.UpdateAsync(user);
                        return RedirectToAction(nameof(Index));
                    }
                    catch
                    {
                        return View();
                    }
                }
                return View();
            }
            return NotFound();
        }

        // GET: AboutMySelf/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AboutMySelf/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}