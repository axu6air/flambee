using Microsoft.AspNetCore.Http;

namespace Flambee.WebAPI.DataTransferModel.Post
{
    public class PostImageCreateModel 
    {
        
        public IFormFileWrapper Image { get; set; }


       
    }

    public class IFormFileWrapper
    {
        public IFormFile File { get; set; }
    }


}
