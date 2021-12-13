using Microsoft.AspNetCore.Http;

namespace Flambee.WebAPI.DataTransferModel.Post
{
    public class PostImageCreateModel 
    {
        public string ImageBase64 { get; set; }
        public string Title { get; set; }
        public string MimeType { get; set; }
        public string DisplayName { get; set; }
        public IFormFile Image { get; set; }

    }
}
