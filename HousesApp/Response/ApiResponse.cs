using System.Net;
using System.Reflection.PortableExecutable;

namespace HousesApp.Response
{
    public class ApiResponse
    {
        public HttpStatusCode  StatusCode{ get; set; }
        public bool isSuccess { get; set; }
        public List<string> ErrorMessages { get; set; } = new List<string>();

        public object Result { get; set; }
    }
}
