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
        private readonly ObjectId _userId;
        private PostService _postService = null;
        public ServiceFactory(
                IUserService userService,
                ObjectId userId
            )
        {
            _userService = userService;
            _userId = userId;
        }
        public  PostService GetPostService() // caching will not work currently need to use singelton or key pair caching for the service creation.
        {
            _postService = _postService ?? new PostService(_userService, _userId);
            return _postService;
        }
    }
}
