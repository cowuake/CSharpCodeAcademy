using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.Entities
{
    public class Result
    {
        public Result (bool success, string message, Exception ex)
        {
            Success = success;
            Message = message;
            InnerException = ex;
        }

        public Result() : this(true, null, null) { }

        public Result(bool success, string message) : this(success, message, null) { }

        public bool Success { get; set; }

        public string Message { get; set; }

        public Exception InnerException { get; set; }
    }
}