﻿using API.ENUMS;

namespace API.Exceptions
{
    public class AppException : System.Exception
    {

        public ReturnCode ReturnCode { get; set; }

        public AppException(ReturnCode returnCode) : base(returnCode.Message)
        {
            ReturnCode = returnCode;
        }
    }
}
