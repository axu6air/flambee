using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flambee.WebAPI.DataTransferModel
{
    public class BaseResponseModel 
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public bool HandleLocally { get; set; }
    }

    public interface IBaseResponseModel
    {
        public BaseResponseModel BaseResponseModel { get; set; }
    }
}
