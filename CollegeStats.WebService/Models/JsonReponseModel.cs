using System.Collections.Generic;

namespace CollegeStats.WebService.Models
{
    public enum Status
    {
        Ok,
        Error,
        ModelStateError
    }

    public class JsonResponseModel
    {
        public Status Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}