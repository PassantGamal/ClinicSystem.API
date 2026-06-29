using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicSystem.Shared.CommonResult
{
    public class Error
    {
        public string Code { get; }
        public string Description { get; }
        public ErrorType Type { get; }

        private Error(string code, string description, ErrorType type)
        {
            Code = code;
            Description = description;
            Type = type;
        }

        //6 static Factory Methods To Create Errors
        public static Error Failure(string code="General.Failure", string description="A General Failure Has occurred")
        {
            return new Error(code, description, ErrorType.Failure);
        }
        public static Error Validation(string code = "General.Validation", string description = "A General Validation Error Has occurred")
        {
            return new Error(code, description, ErrorType.Validation);
        }
        public static Error NotFound(string code = "General.NotFound", string description = "A General Not Found Error Has occurred")
        {
            return new Error(code, description, ErrorType.NotFound);
        }
        public static Error Unauthorized(string code = "General.Unauthorized", string description = "A General Unauthorized Error Has occurred")
        {
            return new Error(code, description, ErrorType.Unauthorized);
        }
        public static Error Forbidden(string code = "General.Forbidden", string description = "A General Forbidden Error Has occurred")
        {
            return new Error(code, description, ErrorType.Forbidden);
        }
        public static Error InvalidCredentials(string code = "General.InvalidCredentials", string description = "A General Invalid Credentials Error Has occurred")
        {
            return new Error(code, description, ErrorType.InvalidCredentials);
        }
    }
}
