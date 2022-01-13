using Flambee.Core.Domain.PostDetails;
using Flambee.Core.Domain.UserDetails;
using Flambee.Data;
using Flambee.Service.AppServiceFactories;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flambee.Service.AppServiceProviders.PostDetails
{
    public class PostService : UserBaseService, IPostService
    {
         
        public PostService(
                IUserService userService,
                ObjectId userId
            ) : base(userService, userId)
        {
        }

        public Task<List<Post>> GetPosts()
        {
            if(user is null)
            {
                return null;
            }
            var posts = user.Posts;
            if (posts.Count > 0)
            {
                posts = posts.Where(p => !p.IsDeleted).OrderByDescending(p => p.UploadTime).Select(p =>
                    new Post {
                        Id = p.Id,
                        PostImages = p.PostImages.Where(pi => !pi.IsDeleted).OrderBy(pi => pi.UploadTime).ToList(),
                        Title = p.Title,
                        UploadTime = p.UploadTime,
                        ModifiedTime = p.ModifiedTime,
                    }).ToList();
            }

            return Task.FromResult(posts);
        }

        
    }
}
