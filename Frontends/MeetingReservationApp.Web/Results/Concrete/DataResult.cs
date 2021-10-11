
using MeetingReservationApp.Web.Results.Abstract;
using MeetingReservationApp.Web.Results.ComplexTypes;
using System;
using System.Text.Json.Serialization;

namespace MeetingReservationApp.Web.Results.Concrete
{
    public class DataResult<T> : IDataResult<T>
    {
        public DataResult()
        {
        }
        public DataResult(ResultStatus resultStatus, T data)
        {
            ResultStatus = resultStatus;
            Data = data;
        }
        public DataResult(ResultStatus resultStatus, string message, T data)
        {
            ResultStatus = resultStatus;
            Message = message;
            Data = data;
        }
        public DataResult(ResultStatus resultStatus, string message, T data, Exception exception)
        {
            ResultStatus = resultStatus;
            Message = message;
            Data = data;
            exception = Exception;
        }

        public T Data { get; set; }
        public ResultStatus ResultStatus { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
    }
}
