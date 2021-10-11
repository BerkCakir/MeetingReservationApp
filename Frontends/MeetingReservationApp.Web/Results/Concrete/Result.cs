
using MeetingReservationApp.Web.Results.Abstract;
using MeetingReservationApp.Web.Results.ComplexTypes;
using System;

namespace MeetingReservationApp.Web.Results.Concrete
{
    public class Result : IResult
    {
        public Result()
        {
        }
        public Result(ResultStatus resultStatus)
        {
            ResultStatus = resultStatus;
        }

        public Result(ResultStatus resultStatus, string message)
        {
            ResultStatus = resultStatus;
            Message = message;
        }
        public Result(ResultStatus resultStatus, string message, Exception exception)
        {
            ResultStatus = resultStatus;
            Message = message;
            Exception = exception;
        }
        public ResultStatus ResultStatus { get; set; }

        public string Message { get; set; }

        public Exception Exception { get; set; }
    }
}
