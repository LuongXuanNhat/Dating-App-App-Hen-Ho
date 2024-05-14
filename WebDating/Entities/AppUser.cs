﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebDating.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public byte[] PasswordSalt { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime LastActive { get; set; } = DateTime.UtcNow;
        public bool IsUpdatedDatingProfile { get; set; } = false;
        public string Gender { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public List<Photo> Photos { get; set; } = new();
        public List<UserLike> LikedByUsers { get; set; }
        public List<UserLike> LikedUsers { get; set; }
        public List<Message> MessagesSent { get; set; }
        public List<Message> MessagesReceived { get; set; }
        public ICollection<AppUserRole> UserRoles { get; set; }

        public DatingProfile DatingProfile { get; set; }
        public ICollection<PostComment> PostComments { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public ICollection<PostLike> PostLikes { get; set; }
        public ICollection<PostReportDetail> PostReportDetails { get; set; }
        public ICollection<PostSubComment> PostSubComments { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
