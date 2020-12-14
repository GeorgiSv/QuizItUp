namespace QuizItUp.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using QuizItUp.Data;
    using QuizItUp.Data.Common.Repositories;
    using QuizItUp.Data.Models;
    using QuizItUp.Services;

    public class BadgesController : AdministrationController
    {
        private readonly IDeletableEntityRepository<Badge> badgesRepository;
        private readonly Cloudinary cloudinary;

        public BadgesController(IDeletableEntityRepository<Badge> badgesRepository, Cloudinary cloudinary)
        {
            this.badgesRepository = badgesRepository;
            this.cloudinary = cloudinary;
        }

        // GET: Administration/Badges
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = this.badgesRepository.AllAsNoTracking().Include(b => b.Picture);
            return this.View(await applicationDbContext.OrderByDescending(x => x.CreatedOn).ToListAsync());
        }

        // GET: Administration/Badges/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var badge = await this.badgesRepository.All()
                .Include(b => b.Picture)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (badge == null)
            {
                return this.NotFound();
            }

            return this.View(badge);
        }

        // GET: Administration/Badges/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Administration/Badges/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuizId,PictureId,Title,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Badge badge, IFormFile picture)
        {
            if (picture == null)
            {
                this.ModelState.AddModelError(nameof(badge.Picture), "Picture is required!");
            }

            if (this.ModelState.IsValid)
            {
                if (!(picture.FileName.EndsWith(".png")
                || picture.FileName.EndsWith(".jpeg")
                || picture.FileName.EndsWith(".jpg")))
                {
                    this.ModelState.AddModelError("Picture", "Picture must be file with extension jpeg, jpg png");
                }

                if (picture.Length > 10 * 102 * 1024)
                {
                    this.ModelState.AddModelError("Picture", "Picture is too large - Max 10Mb");
                }

                var path = await CloudinaryService.UploadPicture(this.cloudinary, picture, "testPicName", "categories");
                if (path != null)
                {
                    badge.Picture = new Picture() { Url = path };
                }

                await this.badgesRepository.AddAsync(badge);
                await this.badgesRepository.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            //this.ViewData["PictureId"] = new SelectList(this.badgesRepository.Pictures, "Id", "Id", badge.PictureId);
            //this.ViewData["Id"] = new SelectList(this.badgesRepository.Quizes, "Id", "Id", badge.Id);
            return this.View(badge);
        }

        // GET: Administration/Badges/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var badge = await this.badgesRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            if (badge == null)
            {
                return this.NotFound();
            }

           // this.ViewData["PictureId"] = new SelectList(this.badgesRepository.Pictures, "Id", "Id", badge.PictureId);
            //this.ViewData["Id"] = new SelectList(this.badgesRepository.Quizes, "Id", "Id", badge.Id);
            return this.View(badge);
        }

        // POST: Administration/Badges/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("QuizId,PictureId,Title,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Badge badge)
        {
            if (id != badge.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.badgesRepository.Update(badge);
                    await this.badgesRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.BadgeExists(badge.Id))
                    {
                        return this.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return this.RedirectToAction(nameof(this.Index));
            }

            //this.ViewData["PictureId"] = new SelectList(this.badgesRepository.Pictures, "Id", "Id", badge.PictureId);
            //this.ViewData["Id"] = new SelectList(this.badgesRepository.Quizes, "Id", "Id", badge.Id);
            return this.View(badge);
        }

        // GET: Administration/Badges/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var badge = await this.badgesRepository.All()
                .Include(b => b.Picture)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (badge == null)
            {
                return this.NotFound();
            }

            return this.View(badge);
        }

        // POST: Administration/Badges/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var badge = await this.badgesRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            this.badgesRepository.Delete(badge);
            await this.badgesRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool BadgeExists(string id)
        {
            return this.badgesRepository.All().Any(e => e.Id == id);
        }
    }
}
