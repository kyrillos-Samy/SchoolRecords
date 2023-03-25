namespace DTO
{
    public class Response<T>
    {
        public Response()
        {
            Code = ResponseStatusEnum.Error;
        }
        public Response(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }
        public Response(string message)
        {
            Succeeded = false;
            Message = message;
        }
        public Response(bool Status, string message)
        {
            Succeeded = Status;
            Message = message;
        }
        public ResponseStatusEnum Code { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public bool Succeeded { get; set; }
    }
}
