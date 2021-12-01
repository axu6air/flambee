using Flambee.Core.Domain.Image;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using Flambee.Core.Domain.PostDetails;

namespace Flambee.Core.Domain.UserDetails
{
    public class User : User<ObjectId>, IBaseEntity
    {
        public User() : base() { }

        public User(string userName) : base(userName) { }

        public DateTime UploadTime { get; set; }
        public DateTime ModifiedTime { get; set; }

    }

    public class User<TKey> : IdentityUser<TKey> where TKey : IEquatable<TKey>
    {
        public User()
        {
            UserInfo = new();
            TwoFactorRecoveryCode = new();
            Roles = new();
            Claims = new();
            Logins = new();
            Tokens = new();
            Avatars = new();
            Posts = new();
            IsActive = true;
        }

        public User(string userName) : this()
        {
            UserName = userName;
            NormalizedUserName = userName.ToUpperInvariant();
        }
        public UserInfo UserInfo { get; set; }

        public List<string> Roles { get; set; }

        public List<IdentityUserClaim<string>> Claims { get; set; }

        public List<IdentityUserLogin<string>> Logins { get; set; }

        public List<IdentityUserToken<string>> Tokens { get; set; }

        public TwoFactorRecoveryCode TwoFactorRecoveryCode { get; set; }

        public List<Avatar> Avatars { get; set; }
        public List<Post> Posts { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

    }
}