namespace QuizItUp.Data.Models
{
    public class ApplicationUserBadge
    {
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public string BadgeId { get; set; }

        public Badge Badges { get; set; }
    }
}
