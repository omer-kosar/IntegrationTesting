﻿using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Customers.Api.Validation
{
    public class ValidationExceptionMiddleware
    {
        private readonly RequestDelegate _request;

        public ValidationExceptionMiddleware(RequestDelegate request)
        {
            _request = request;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _request(context);
            }
            catch (ValidationException exception)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                var error = new ValidationProblemDetails
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    Status = (int)HttpStatusCode.BadRequest,
                    Extensions = { ["traceId"] = context.TraceIdentifier },
                };
                foreach (var validationFailure in exception.Errors)
                {
                    error.Errors.Add(new KeyValuePair<string, string[]>(validationFailure.PropertyName, new[] { validationFailure.ErrorMessage }));
                }
                await context.Response.WriteAsJsonAsync(error);
            }
        }
    }
}
