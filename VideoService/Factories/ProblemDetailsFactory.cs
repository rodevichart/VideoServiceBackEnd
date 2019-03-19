using System;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using VideoServiceBL.Exceptions;

namespace VideoService.Factories
{
    public static class ProblemDetailsFactory
    {
        public static ProblemDetails Build(Exception exception, bool isConnectionTrusted)
        {
            var problemDetails = new ProblemDetails
            {
                Instance = $"urn:VideoRentalService:error:{Guid.NewGuid()}"
            };

            if (!isConnectionTrusted)
            {
                problemDetails.Detail =
                    "The instance value should be used to identify the problem when calling customer support";
            }

            switch (exception)
            {
                case BadHttpRequestException badHttpRequestException:
                    problemDetails.Title = "Invalid request";
                    problemDetails.Status = (int)typeof(BadHttpRequestException).GetProperty("StatusCode",
                        BindingFlags.NonPublic | BindingFlags.Instance).GetValue(badHttpRequestException);
                    problemDetails.Detail = problemDetails.Detail ?? badHttpRequestException.Message;
                    break;

                case UserServiceException userServiceException:
                    problemDetails.Title = "Invalid request";
                    problemDetails.Status = (int)HttpStatusCode.BadRequest;
                    problemDetails.Detail = userServiceException.Message;
                    break;

                case BusinessLogicException businessLogicException:
                    problemDetails.Title = "Error";
                    problemDetails.Status = (int)HttpStatusCode.BadRequest;
                    problemDetails.Detail = businessLogicException.Message;
                    break;

                default:
                    problemDetails.Title = "An unexpected error occurred!";
                    problemDetails.Status = (int)HttpStatusCode.InternalServerError;
                    problemDetails.Detail = problemDetails.Detail ?? exception.Demystify().ToString();
                    break;
            }

            return problemDetails;
        }
    }
}