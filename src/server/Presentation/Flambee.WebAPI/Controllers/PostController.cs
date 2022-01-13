﻿using AutoMapper;
using Flambee.Core.Configuration.Image;
using Flambee.Core.Domain.PostDetails;
using Flambee.Service.AppServiceFactories;
using Flambee.Service.AppServiceProviders;
using Flambee.Service.AppServiceProviders.Image;
using Flambee.Service.AppServiceProviders.PostDetails;
using Flambee.WebAPI.DataTransferModel.Post;
using Flambee.WebAPI.Factories.Image;
using Flambee.WebAPI.Infrastructure.ServiceRegistrar;
using Flambee.WebAPI.Models.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flambee.WebAPI.Controllers
{
    public class PostController : BaseImageController
    {
        private readonly IUserService _userService;
        private readonly IImageService _imageService;
        private readonly IImageFactory _imageFactory;
        private readonly ServiceFactory _serviceFactory;
        private readonly IMapper _mapper;
        public PostController(
                IUserService userService,
                IMapper mapper, 
                IImageService imageService,
                IImageFactory imageFactory,
                ServiceFactory serviceFactory
            )
        {
            _userService = userService;
            _mapper = mapper;
            _imageService = imageService;
            _imageFactory = imageFactory;
            _serviceFactory = serviceFactory;
        }

        [HttpGet]
        [Route("/GetPosts")]
        public async Task<IActionResult> Get(ObjectId userId)
        {
            var postService = _serviceFactory.GetPostService(userId);
            var posts = await postService.GetPosts();
            return Ok(posts);
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("/Post/Create")]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] AvatarUploadModel model)
        {
            //if (model != null && model.PostImages.Count > 0)
            //{
            //    var postImages = new List<PostImage>();

            //    foreach (var postImageModel in model.PostImages)
            //    {
            //        var postImage = _mapper.Map<PostImage>(postImageModel);
            //        postImage.FileName = await _imageFactory.UploadImage(postImageModel.Image.File, ImageType.Post);
            //        postImages.Add(postImage);
            //    }

            //    var post = _mapper.Map<Post>(model);
            //    post.PostImages = postImages;
            //    var user = await GetUser();
            //    user.Posts.Add(post);
            //    await _userService.UpsertUser(user);
            //}


            return Ok();
        }
    }
}
