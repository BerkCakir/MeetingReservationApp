using MeetingReservationApp.Shared.Utilities.Results.Abstract;
using MeetingReservationApp.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MeetingReservationApp.Shared.ControllerBases
{
    public class CustomControllerBase : ControllerBase
    {
        public IActionResult CreateResultWithData<T>(IDataResult<T> response)
        {
            int statusCode = 0;
            switch (response.ResultStatus)
            {
                case ResultStatus.Success:
                    statusCode = (int)HttpStatusCode.OK;
                    break;
                case ResultStatus.Error:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    break;
                default:
                    statusCode = (int)HttpStatusCode.NotFound;
                    break;
            }
            return new ObjectResult(response)
            {
                StatusCode = statusCode
            };
        }
        public IActionResult CreateResult(IResult response)
        {
            int statusCode;
            switch (response.ResultStatus)
            {
                case ResultStatus.Success:
                    statusCode = (int)HttpStatusCode.OK;
                    break;
                case ResultStatus.Error:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    break;
                default:
                    statusCode = (int)HttpStatusCode.NotFound;
                    break;
            }
            return new ObjectResult(response)
            {
                StatusCode = statusCode
            };
        }
    }
}
