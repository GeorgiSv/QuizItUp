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
    using QuizItUp.Common;
    using QuizItUp.Data;
    using QuizItUp.Data.Common.Repositories;
    using QuizItUp.Data.Models;
    using QuizItUp.Services;

    public class CategoriesController : AdministrationController
    {
        private readonly IDeletableEntityRepository<Category> cateogriesRepository;
        private readonly Cloudinary cloudinary;

        public CategoriesController(IDeletableEntityRepository<Category> cateogriesRepository, Cloudinary cloudinary)
        {
            this.cateogriesRepository = cateogriesRepository;
            this.cloudinary = cloudinary;
        }

        // GET: Administration/Categories
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = this.cateogriesRepository.All().Include(c => c.Picture);
            return this.View(await applicationDbContext.ToListAsync());
        }

        // GET: Administration/Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var category = await this.cateogriesRepository.All()
                .Include(c => c.Picture)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return this.NotFound();
            }

            return this.View(category);
        }

        // GET: Administration/Categories/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Administration/Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,PictureId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Category category, IFormFile picture)
        {
            if (picture == null)
            {
                this.ModelState.AddModelError(nameof(category.Picture), "Picture is required!");
                return this.View();
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
                    category.Picture = new Picture() { Url = path };
                }

                await this.cateogriesRepository.AddAsync(category);
                await this.cateogriesRepository.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            // this.ViewData["PictureId"] = new SelectList(this.cateogriesRepository.Pictures, "Id", "Id", category.PictureId);
            return this.View(category);
        }

        // GET: Administration/Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var category = await this.cateogriesRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
            {
                return this.NotFound();
            }

            return this.View(category);
        }

        // POST: Administration/Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,PictureId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Category category)
        {
            if (id != category.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.cateogriesRepository.Update(category);
                    await this.cateogriesRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.CategoryExists(category.Id))
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

            return this.View(category);
        }

        // GET: Administration/Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var category = await this.cateogriesRepository.All()
                .Include(c => c.Picture)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return this.NotFound();
            }

            return this.View(category);
        }

        // POST: Administration/Categories/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await this.cateogriesRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            this.cateogriesRepository.Delete(category);
            await this.cateogriesRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool CategoryExists(int id)
        {
            return this.cateogriesRepository.All().Any(e => e.Id == id);
        }
    }
}
