namespace ABCStore.Common.Wrappers
{
    public class ResponseWrapper<T> 
    {
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }

        public ResponseWrapper<T> Success(T data, string message = null)
        {
            IsSuccess = true;
            message = message ?? string.Empty;
            Data = data;
            return this;
        }

        public ResponseWrapper<T> Failed(string message = null)
        {
            IsSuccess = false;
            message = message ?? string.Empty;
            return this;
        }
    }
}
