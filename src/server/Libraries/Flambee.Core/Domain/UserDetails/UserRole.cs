using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flambee.Core.Domain.UserDetails
{
    public class UserRole : UserRole<ObjectId>
    {
        public UserRole() : base() { }

        public UserRole(string name) : base(name) { }
    }   

    public class UserRole<TKey> : IdentityRole<TKey> where TKey : IEquatable<TKey>
    {
        public UserRole()
        {
            Claims = new List<IdentityRoleClaim<string>>();
        }

        public UserRole(string name) : this()
        {
            Name = name;
            NormalizedName = name.ToUpperInvariant();
        }

        public override string ToString()
        {
            return Name;
        }

        public List<IdentityRoleClaim<string>> Claims { get; set; }
    }
}
