namespace QuizItUp.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using QuizItUp.Data.Common.Repositories;
    using QuizItUp.Data.Models;
    using QuizItUp.Services.Data.Contracts;

    public class TagsService : ITagsService
    {
        private readonly IDeletableEntityRepository<QuizTag> quizTagRepo;
        private readonly IDeletableEntityRepository<Tag> tagsRepo;

        public TagsService(IDeletableEntityRepository<QuizTag> quizTagRepo, IDeletableEntityRepository<Tag> tagsRepo)
        {
            this.quizTagRepo = quizTagRepo;
            this.tagsRepo = tagsRepo;
        }

        public async Task<ICollection<QuizTag>> CreateTags(string tags, string quizId, int categoryId, Quiz quiz)
        {
            var tagsTitles = this.SplitTags(tags.ToLower());
            var listOfQuizTags = new List<QuizTag>();

            foreach (var title in tagsTitles)
            {
                var tag = await this.TagExist(title);

                if (tag == null)
                {
                    //TODO: think for optimization 
                    tag = new Tag()
                    {
                        Title = title,
                        CategoryId = categoryId,
                    };
                }

                listOfQuizTags.Add(new QuizTag
                {
                    QuizId = quizId,
                    Quiz = quiz,
                    TagId = tag.Id,
                    Tag = tag,
                });
            }

            return listOfQuizTags;
        }

        public async Task<IList<QuizTag>> GetAllQuizTags()
            => await this.quizTagRepo.All().ToListAsync();

        public async Task<IList<Tag>> GetAllTags()
            => await this.tagsRepo
            .All()
            .ToListAsync();

        public async Task<IList<QuizTag>> GetAllWithTitle(string input)
            => await this.quizTagRepo
            .All()
            .Where(x => x.Tag.Title.Contains(input.ToLower()))
            .ToListAsync();

        private string[] SplitTags(string tags)
        {
            var splitedTags = tags
                .Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrEmpty(x) && !string.IsNullOrWhiteSpace(x))
                .ToArray();

            return splitedTags;
        }

        private async Task<Tag> TagExist(string title)
        {
            var allTags = await this.tagsRepo.All().ToListAsync();
            var tag = await this.tagsRepo.All().Where(x => x.Title == title).FirstOrDefaultAsync();

            return tag;
        }
    }
}
