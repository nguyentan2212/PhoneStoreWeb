using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhoneStoreWeb.Communication.ResponseResult
{
    public class ResponseResult<TResult> where TResult : class
    {
        public bool IsSucceed { get; set; }
        public string Message { get; set; }
        public TResult Result { get; set; }
        public IEnumerable<string> Errors { get; set; }

        public ResponseResult<TResult> Succeed(TResult result)
        {
            IsSucceed = true;
            Message = string.Empty;
            Errors = Enumerable.Empty<string>();
            Result = result;
            return this;
        }
        public ResponseResult<TResult> Failed(string content)
        {
            IsSucceed = false;
            Message = content;
            Errors = Enumerable.Empty<string>();
            Result = null;
            return this;
        }
        public ResponseResult<TResult> Failed(string content, IEnumerable<string> errors)
        {
            IsSucceed = false;
            Message = content;
            Errors = (Errors ?? Enumerable.Empty<string>()).Concat(errors ?? Enumerable.Empty<string>());
            Result = null;
            return this;
        }       
    }
}
