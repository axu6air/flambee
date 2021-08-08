using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.WebAPI.DataTransferModel
{
    public class BaseErrorModel : BaseResponseModel
    {
        public BaseErrorModel()
        {
            Errors = new List<string>();
        }


        public IList<string> Errors { get; set; }
        public string Error { get; set; }
    }
}
