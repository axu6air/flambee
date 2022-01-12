
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace Flambee.WebAPI.DataTransferModel.Post
{
    public class PostCreateModel
    {
        public PostCreateModel()
        {
            //PostImages = new();
            //SharedFrom = ObjectId.Empty;
        }

        public string Title { get; set; }
        //public ObjectId? SharedFrom { get; set; }
        //public IFormFile Image { get; set; }
        public IFormFileWrapper Prop1 { get; set; }
        //public List<PostImageCreateModel> PostImages { get; set; }
    }
}
