using Flambee.Service.AppServiceProviders;
using Flambee.Service.AppServiceProviders.PostDetails;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flambee.Service.AppServiceFactories
{
    public class ServiceFactory
    {
        private readonly IUserService _userService;
        private PostService _postService = null;
        private ObjectId _userId = new ObjectId();
        public ServiceFactory(
                IUserService userService
            )
        {
            _userService = userService;
        }
        public PostService GetPostService(ObjectId userId)
        {
            if(userId == _userId)
            {
                _postService = _postService ?? new PostService(_userService, userId);
            }
            else
            {
                _postService = new PostService(_userService, userId);
                _userId = userId;
            }
            return _postService;
        }
    }
}
