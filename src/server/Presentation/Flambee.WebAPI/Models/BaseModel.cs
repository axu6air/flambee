using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flambee.WebAPI.Models
{
    public class BaseModel
    {
        public ObjectId Id { get; set; }
    }
}
