namespace QuizItUp.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using QuizItUp.Data.Models;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {

            if (dbContext.Categories.Any())
            {
                return;
            }

            var categories = new Dictionary<string, string>();
            categories.Add("Movie", "https://menafn.com/updates/pr/2020-11/06/I_ce6442e6-bimage_story.jpg");
            categories.Add("Games", "https://xenocell.com/wp-content/uploads/2019/03/games.jpg");
            categories.Add("Programming", "https://hub.packtpub.com/wp-content/uploads/2018/05/programming.jpg");
            categories.Add("Sport", "https://img.freepik.com/free-vector/sport-equipment-concept_1284-13034.jpg?size=338&ext=jpg");

            foreach (var category in categories)
            {
                var pic = new Picture()
                {
                    Url = category.Value,
                };
                await dbContext.Pictures.AddAsync(pic);

                await dbContext.Categories.AddAsync(new Category()
                {
                    Title = category.Key,
                    PictureId = pic.Id,
                });
            }
        }
    }
}
