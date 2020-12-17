namespace QuizItUp.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using QuizItUp.Data.Common.Repositories;
    using QuizItUp.Data.Models;
    using QuizItUp.Services.Data.Contracts;
    using QuizItUp.Services.Mapping;
    using QuizItUp.Web.ViewModels.Ranks;

    public class RanksService : IRanksService
    {
        private readonly IDeletableEntityRepository<Rank> ranksRepo;

        public RanksService(IDeletableEntityRepository<Rank> ranksRepo)
        {
            this.ranksRepo = ranksRepo;
        }

        //public async Task AddAsync(RankInputModel input)
        //{
        //    var rank = new Rank()
        //    {
        //        Name = input.Name,
        //        TrophiesNeeded = input.TrophiesNeeded,
        //        Color = input.Color,
        //    };

        //    await this.ranksRepo.AddAsync(rank);
        //    await this.ranksRepo.SaveChangesAsync();
        //}

        public async Task<IList<RankViewModel>> GetAllRanksAsync()
            => await this.ranksRepo
            .All()
            .Where(x => x.IsPublished)
            .To<RankViewModel>()
            .OrderByDescending(x => x.TrophiesNeeded)
            .ToListAsync();

        //public async Task<string> RemoveAsync(string id)
        //{
        //    var rank = await this.ranksRepo
        //        .All()
        //        .FirstOrDefaultAsync(x => x.Id == id);

        //    this.ranksRepo.Delete(rank);
        //    await this.ranksRepo.SaveChangesAsync();

        //    return rank.Id;
        //}

        //public async Task<string> Publish(string id)
        //{
        //    var rank = await this.ranksRepo
        //        .All()
        //        .FirstOrDefaultAsync(x => x.Id == id);

        //    rank.IsPublished = true;

        //    this.ranksRepo.Update(rank);
        //    await this.ranksRepo.SaveChangesAsync();

        //    return rank.Id;
        //}

        //public async Task<string> UnPublish(string id)
        //{
        //    var rank = await this.ranksRepo
        //        .All()
        //        .FirstOrDefaultAsync(x => x.Id == id);

        //    rank.IsPublished = false;

        //    this.ranksRepo.Update(rank);
        //    await this.ranksRepo.SaveChangesAsync();

        //    return rank.Id;
        //}
    }
}
