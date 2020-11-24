using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PhoneStoreWeb.Communication.ResponseResult
{
    public class ResponseResult<TResult> where TResult : class
    {
        public bool IsSucceed { get; set; }
        public string Message { get; set; }
        public TResult Result { get; set; }
        public List<string> Errors { get; set; }

        public ResponseResult()
        {
            IsSucceed = false;
            Message = "Request failed";
            Errors = new List<string>();
            Result = null;
        }

        public ResponseResult<TResult> Succeed(TResult result)
        {
            IsSucceed = true;
            Message = string.Empty;
            Errors = new List<string>();
            Result = result;
            return this;
        }
        public ResponseResult<TResult> Failed(string content)
        {
            IsSucceed = false;
            Message = content;
            Errors = new List<string>();
            Result = null;
            return this;
        }
        public ResponseResult<TResult> Failed(string content, ICollection<string> errors)
        {
            IsSucceed = false;
            Message = content;
            Errors.AddRange(errors);
            Result = null;
            return this;
        }       
    }
}
