using FluentResults;
using Microsoft.AspNetCore.Http;

namespace VerticalSliceArchitecture.Core.Common.Errors;

public class NotFoundError : Error
{
    public NotFoundError()
        : base("The Entity was not found.")
    {
    }

    public NotFoundError(string entityName)
        : base($"The Entity {entityName} was not found.")
    {
        Metadata.Add("StatusCode", StatusCodes.Status404NotFound);
    }


    public static NotFoundError Create(string entityName) =>
        new(entityName);
}