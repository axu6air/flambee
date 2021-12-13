using Flambee.Core.Domain.PostDetails;
using Flambee.Core.Domain.UserDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flambee.Service.AppServiceProviders.PostDetails
{
    public interface IPostService
    {
        Task<List<Post>> GetPosts(User user);
    }
}
