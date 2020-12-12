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

    public class RanksController : AdministrationController
    {
        private readonly IDeletableEntityRepository<Rank> ranksRepository;

        public RanksController(IDeletableEntityRepository<Rank> ranksRepository)
        {
            this.ranksRepository = ranksRepository;
        }

        // GET: Administration/Ranks
        public async Task<IActionResult> Index()
        {
            return this.View(await this.ranksRepository.AllAsNoTracking().OrderByDescending(x => x.TrophiesNeeded).ToListAsync());
        }

        // GET: Administration/Ranks/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var rank = await this.ranksRepository.AllAsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rank == null)
            {
                return this.NotFound();
            }

            return this.View(rank);
        }

        // GET: Administration/Ranks/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Administration/Ranks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for.
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,TrophiesNeeded,Color,IsPublished,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Rank rank)
        {
            if (this.ModelState.IsValid)
            {
                await this.ranksRepository.AddAsync(rank);
                await this.ranksRepository.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(rank);
        }

        // GET: Administration/Ranks/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var rank = await this.ranksRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            if (rank == null)
            {
                return this.NotFound();
            }

            return this.View(rank);
        }

        // POST: Administration/Ranks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for .
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,TrophiesNeeded,Color,IsPublished,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Rank rank)
        {
            if (id != rank.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.ranksRepository.Update(rank);
                    await this.ranksRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.RankExists(rank.Id))
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

            return this.View(rank);
        }

        // GET: Administration/Ranks/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var rank = await this.ranksRepository.All()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rank == null)
            {
                return this.NotFound();
            }

            return this.View(rank);
        }

        // POST: Administration/Ranks/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var rank = await this.ranksRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            this.ranksRepository.Delete(rank);
            await this.ranksRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool RankExists(string id)
        {
            return this.ranksRepository.AllAsNoTracking().Any(e => e.Id == id);
        }
    }
}
