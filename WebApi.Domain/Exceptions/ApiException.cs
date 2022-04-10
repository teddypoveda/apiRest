﻿using System;
using System.Net;

namespace WebApi.Domain.Exceptions
{
    public class ApiException : Exception
    {
        public readonly HttpStatusCode StatusCode;

        public ApiException(string message,HttpStatusCode statusCode):base(message)
        {
            StatusCode = statusCode;
        }

        public ApiException(string? message) : base(message)
        {
        }
    }
}