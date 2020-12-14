namespace QuizItUp.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using QuizItUp.Data;
    using QuizItUp.Data.Common.Repositories;
    using QuizItUp.Data.Models;

    public class PicturesController : AdministrationController
    {
        private readonly IDeletableEntityRepository<Picture> pictureRepository;

        public PicturesController(IDeletableEntityRepository<Picture> pictureRepository)
        {
            this.pictureRepository = pictureRepository;
        }

        // GET: Administration/Pictures
        public async Task<IActionResult> Index()
        {
            return this.View(await this.pictureRepository.AllAsNoTracking().ToListAsync());
        }

        // GET: Administration/Pictures/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var picture = await this.pictureRepository.All()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (picture == null)
            {
                return this.NotFound();
            }

            return this.View(picture);
        }

        // GET: Administration/Pictures/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Administration/Pictures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Url,Path,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Picture picture)
        {
            if (this.ModelState.IsValid)
            {
                await this.pictureRepository.AddAsync(picture);
                await this.pictureRepository.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(picture);
        }

        // GET: Administration/Pictures/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var picture = await this.pictureRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            if (picture == null)
            {
                return this.NotFound();
            }

            return this.View(picture);
        }

        // POST: Administration/Pictures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Url,Path,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Picture picture)
        {
            if (id != picture.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.pictureRepository.Update(picture);
                    await this.pictureRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.PictureExists(picture.Id))
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

            return this.View(picture);
        }

        // GET: Administration/Pictures/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var picture = await this.pictureRepository.All()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (picture == null)
            {
                return this.NotFound();
            }

            return this.View(picture);
        }

        // POST: Administration/Pictures/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var picture = await this.pictureRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            this.pictureRepository.Delete(picture);
            await this.pictureRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool PictureExists(string id)
        {
            return this.pictureRepository.All().Any(e => e.Id == id);
        }
    }
}
