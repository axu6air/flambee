using Flambee.Core.Domain.PostDetails;
using Flambee.Core.Domain.UserDetails;
using Flambee.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flambee.Service.AppServiceProviders.PostDetails
{
    public class PostService : IPostService
    {
        private readonly IMongoRepository<User> _userRepository;

        public PostService(IMongoRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<Post>> GetPosts(User user)
        {
            if (user == null)
                return null;

            var userEntity = await _userRepository.Collection.FirstOrDefaultAsync(x => x.Id == user.Id && x.IsActive && !x.IsDeleted);// ..Select(x => x.Posts);
            var posts = userEntity?.Posts;

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

                return posts;
            }

            return null;
        }

        
    }
}
