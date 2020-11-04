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
using Photographie.Models.Repository;
using Photographie.ViewModel;

namespace Photographie.Controllers
{
    public class MyProtfolioController : Controller
    {
        public Repository<Photo_> Rep_Photo { get; }
        public IHostingEnvironment Hosting { get; }
        public UserManager<AppUsers> UserManager { get; }
        public SignInManager<AppUsers> SignIn { get; }

        public MyProtfolioController(Repository<Photo_> rep_Photo,
            IHostingEnvironment hosting,
             UserManager<AppUsers> userManager,
              SignInManager<AppUsers> signIn)
        {
            Rep_Photo = rep_Photo;
            Hosting = hosting;
            UserManager = userManager;
            SignIn = signIn;
        }
        // GET: MyProtfolio
        public async Task<ActionResult> Index()
        {
            if (SignIn.IsSignedIn(User))
            {
                var liste_Image = await Rep_Photo.List();
                return View(liste_Image);
            }
            return NotFound();
        }

        // GET: MyProtfolio/Details/5
        public async Task<ActionResult> Details(int id)
        {
            if (SignIn.IsSignedIn(User))
            {
                var find_Image = await Rep_Photo.Find(id);
                return View(find_Image);
            }
            return NotFound();
        }

        // GET: MyProtfolio/Create
        public async Task<ActionResult> Create()
        {
            if (SignIn.IsSignedIn(User))
            {
                return View();
            }
            return NotFound();
        }

        // POST: MyProtfolio/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PhotoViewModel model)
        {
            if (SignIn.IsSignedIn(User))
            {
                try
                {
                    string fileName = "";
                    if (model.file.FileName != null)
                    {
                        string chemain = Path.Combine(Hosting.WebRootPath, "MyProtfolio");
                        fileName = model.file.FileName;
                        string path = Path.Combine(chemain, fileName);
                        await model.file.CopyToAsync(new FileStream(path, FileMode.Create));
                        var photo_ = new Photo_
                        {
                            Image_ = fileName
                        };
                        await Rep_Photo.Add(photo_);
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            return NotFound();
        }

        // GET: MyProtfolio/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            if (SignIn.IsSignedIn(User))
            {
                var find = await Rep_Photo.Find(id);
                var model = new PhotoViewModel()
                {
                    Id = id,
                    Image_ = find.Image_
                };
                return View(model);
            }
            return NotFound();
        }

        // POST: MyProtfolio/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, PhotoViewModel model)
        {
            if (SignIn.IsSignedIn(User))
            {
                try
                {
                    string fileName = "";
                    var findImage = await Rep_Photo.Find(id);
                    string image = findImage.Image_;
                    if (model.file.FileName != null)
                    {

                        string chemain = Path.Combine(Hosting.WebRootPath, "MyProtfolio");
                        fileName = model.file.FileName;
                        string path = Path.Combine(chemain, fileName);
                        string oldPath = Path.Combine(chemain, image);
                        if (path != oldPath)
                        {
                            System.IO.File.Delete(oldPath);
                            await model.file.CopyToAsync(new FileStream(path, FileMode.Create));
                        }
                    }
                    else
                    {
                        fileName = image;
                    }
                    findImage.Image_ = fileName;
                    await Rep_Photo.Update(id, findImage);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            return NotFound();
        }

        // GET: MyProtfolio/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            if (SignIn.IsSignedIn(User))
            {
                var find_Image = await Rep_Photo.Find(id);
                return View(find_Image);
            }
            return NotFound();
        }

        // POST: MyProtfolio/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            if (SignIn.IsSignedIn(User))
            {
                try
                {
                    await Rep_Photo.Delete(id);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            return NotFound();
        }
    }
}