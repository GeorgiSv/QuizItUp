// ReSharper disable VirtualMemberCallInConstructor
namespace QuizItUp.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;
    using QuizItUp.Data.Common.Models;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Quizes = new HashSet<Quiz>();
            this.Badges = new HashSet<ApplicationUserBadge>();
        }

        //[Required]
        //[MaxLength(80)]
        public string FirstName { get; set; }

        //[Required]
        //[MaxLength(80)]
        public string LastName { get; set; }

        public int Trophies { get; set; }

        //[Required]
        public DateTime DateOfBirth { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public string PictureId { get; set; }

        public virtual Picture Picture { get; set; }

        public string RankId { get; set; }

        public virtual Rank Rank { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<Quiz> Quizes { get; set; }

        public virtual ICollection<ApplicationUserBadge> Badges { get; set; }
    }
}
