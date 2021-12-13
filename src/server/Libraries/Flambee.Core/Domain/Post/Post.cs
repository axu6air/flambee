using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flambee.Core.Domain.PostDetails
{
    public class Post : BaseEntity
    {
        public Post()
        {
            PostImages = new();
            SharedFrom = ObjectId.Empty;
        }

        public string Title { get; set; }
        public ObjectId SharedFrom { get; set; }
        public List<PostImage> PostImages { get; set; }
    }
}
