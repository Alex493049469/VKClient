using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKAPI.Model.ErrorModel
{
    public class RequestParam
    {
        public string key { get; set; }
        public string value { get; set; }
    }

    public class Error
    {
        public int error_code { get; set; }
        public string error_msg { get; set; }
        public List<RequestParam> request_params { get; set; }
    }
    
    public class ErrorModel
    {
        public Error error { get; set; }
    }

}
