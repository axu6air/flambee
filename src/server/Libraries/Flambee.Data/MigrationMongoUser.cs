﻿using Flambee.Core.Domain.UserDetails;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flambee.Data
{
    internal class MigrationMongoUser : MigrationMongoUser<ObjectId>
    {
        public MigrationMongoUser() : base() { }
    }

    internal class MigrationMongoUser<TKey> : IdentityUser<TKey> where TKey : IEquatable<TKey>
    {
        public MigrationMongoUser()
        {
            Roles = new List<string>();
            Claims = new List<IdentityUserClaim<string>>();
            Logins = new List<IdentityUserLogin<string>>();
            Tokens = new List<IdentityUserToken<string>>();
            RecoveryCodes = new List<TwoFactorRecoveryCode>();
        }

        public string AuthenticatorKey { get; set; }

        public List<string> Roles { get; set; }

        public List<IdentityUserClaim<string>> Claims { get; set; }

        public List<IdentityUserLogin<string>> Logins { get; set; }

        public List<IdentityUserToken<string>> Tokens { get; set; }

        public List<TwoFactorRecoveryCode> RecoveryCodes { get; set; }
    }
}