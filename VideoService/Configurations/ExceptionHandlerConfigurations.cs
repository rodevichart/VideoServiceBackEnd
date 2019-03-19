using System.Diagnostics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Logging;
using VideoService.Extensions;
using VideoService.Factories;
using VideoService.Services.Interfaces;
using VideoServiceBL.Exceptions;

namespace VideoService.Configurations
{
    public class ExceptionHandlerConfigurations
    {
        public static void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory,
            IWriteToFileText writeToFileText)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var exception = errorFeature.Error;
                    var isConnectionTrusted = context.Request.IsTrusted();

                    var problemDetails = ProblemDetailsFactory.Build(exception, isConnectionTrusted);

                    var logger = LoggerConfigurations.Configure(loggerFactory, writeToFileText);

                    var logString =
                        $"Processing request {context.Request.Path},\n Status Code: {problemDetails.Status.Value},\n Details:{exception.Demystify().ToString() ?? problemDetails.Detail}\n";

                    if (!(exception is BusinessLogicException))
                    {
                        logger.LogError(logString);
                    }

                    context.Response.StatusCode = problemDetails.Status.Value;
                    context.Response.WriteJson(problemDetails, "application/problem+json");
                });
            });
        }
    }
}