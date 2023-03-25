namespace Common.ServiceConnector.Contract
{
    public interface IServiceConnector
    {
        public Task<bool> TryGet<T>(string host, string apiUrl, out T returnObj, out string errorMessage) where T : new();
        public Task<bool> TryGet<T>(string host, string apiUrl, out IList<T> list, out string errorMessage) where T : class;
        public bool TryPost<T>(string host, string mediaType, string apiUrl, T serviceModel, out string errorMessage);
        public T TryPost<T>(string host, string mediaType, string apiUrl, object serviceModel, out string errorMessage);
        public Task<T> TryPost<T>(string host, string apiUrl, T serviceModel, out string errorMessage);
    }
}
