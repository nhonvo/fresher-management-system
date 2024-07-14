namespace Application.Utils
{
    public class ServiceResponse
    {
        public bool IsSuccess { get; private set; }
        public string Message { get; private set; }
        public object Result { get; private set; }

        public ServiceResponse Success(object result = null, string message = null)
        {
            Result = result;
            IsSuccess = true;
            Message = message;
            return this;
        }

        public ServiceResponse Fail(string message = null)
        {
            Result = null;
            IsSuccess = false;
            Message = message;
            return this;
        }
    }
}
