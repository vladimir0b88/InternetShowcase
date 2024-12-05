using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace API
{
    public static class ControllerExtension
    {
        public static ActionResult<T> SendResponse<T>(this ControllerBase controller, ErrorOr<T> result)
        {
            if (!result.IsError)
            {
                return controller.Ok(result);
            }

            return result.FirstError.Type switch
            {
                ErrorType.Failure => controller.BadRequest(result.Errors),
                ErrorType.NotFound => controller.NotFound(result.Errors),
                ErrorType.Validation => controller.StatusCode(422, result.Errors),
                _ => controller.StatusCode(500, result.Errors),
            };
        }
    }
}
