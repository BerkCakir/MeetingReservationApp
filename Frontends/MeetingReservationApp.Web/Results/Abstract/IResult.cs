
using MeetingReservationApp.Web.Results.ComplexTypes;
using System;

namespace MeetingReservationApp.Web.Results.Abstract
{
    public interface IResult
    {
        public ResultStatus ResultStatus { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
    }
}
