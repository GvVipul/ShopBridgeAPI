namespace ShowBridge.Services.ProductService
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = null;
        public int ResponseCode { get; set; }
    }
}