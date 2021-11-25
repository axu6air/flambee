using MongoDB.Bson;

namespace Flambee.WebAPI.Models.User
{
    public class UserLoginModel
    {
        public ObjectId UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
