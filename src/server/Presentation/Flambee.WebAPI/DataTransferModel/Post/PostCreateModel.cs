
using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace Flambee.WebAPI.DataTransferModel.Post
{
    public class PostCreateModel
    {
        public PostCreateModel()
        {
            PostImages = new();
            SharedFrom = ObjectId.Empty;
        }

        public string Title { get; set; }
        public ObjectId? SharedFrom { get; set; }
        public List<PostImageCreateModel> PostImages { get; set; }
        public DateTime UploadTime { get; set; }
        public DateTime ModifiedTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
