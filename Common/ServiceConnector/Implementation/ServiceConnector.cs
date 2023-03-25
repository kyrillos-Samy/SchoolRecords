using Common.Logger.Contract;
using Common.ServiceConnector.Contract;
using DTO;
using Newtonsoft.Json;
using System.Text;

namespace Common.ServiceConnector.Implementation
{
    public class ServiceConnector : IServiceConnector
    {
        protected readonly HttpClient HttpClient = new HttpClient();
        protected const string MediaType = "application/json";
        private readonly ILoggerBase _logger;
        public ServiceConnector(ILoggerBase loggerBase)
        {
            _logger = loggerBase;
        }

        public Task<bool> TryGet<T>(string host, string apiUrl, out T returnObj, out string errorMessage) where T : new()
        {
            var completeUri = string.Empty;
            errorMessage = string.Empty;
            returnObj = default(T);
            try
            {
                completeUri = host + apiUrl;
                _logger.LogInfo($"Start calling api {completeUri}");
                var response = HttpClient.GetAsync(completeUri).Result;
                _logger.LogInfo($"Calling api {completeUri} returned, {response.StatusCode}");
                if (response.IsSuccessStatusCode)
                {
                    returnObj = JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
                    return Task.FromResult(true);
                }
                errorMessage = response.Content.ReadAsStringAsync().Result;
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogInfo($"Calling api {completeUri} has timed out");
                errorMessage = ex.GetBaseException().Message;
            }
            catch (AggregateException ex)
            {
                _logger.LogInfo(ex.Message);
                _logger.LogInfo($"API {completeUri} didn't respond");
                errorMessage = "An error has happened, please contact administrator.";
            }
            catch (Exception ex)
            {
                _logger.LogInfo(ex.Message);
                _logger.LogInfo($"Calling api {host + apiUrl} faild, {ex.GetBaseException().Message}");
                errorMessage = ex.GetBaseException().Message;
            }

            return Task.FromResult(false);
        }


        public Task<bool> TryGet<T>(string host, string apiUrl, out IList<T> list, out string errorMessage) where T : class
        {
            errorMessage = string.Empty;
            list = new List<T>();

            try
            {
                _logger.LogInfo($"Start calling api {host + apiUrl}");

                var uri = host + apiUrl;
                var response = HttpClient.GetAsync(uri).Result;

                _logger.LogInfo($"Calling api {host + apiUrl} returned, {response.StatusCode}");
                if (response.IsSuccessStatusCode)
                {
                    list = JsonConvert.DeserializeObject<List<T>>(response.Content.ReadAsStringAsync().Result);
                    return Task.FromResult(true);
                }
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    errorMessage = System.Net.HttpStatusCode.NotFound.ToString();
                }
                else
                    errorMessage = response.Content.ReadAsStringAsync().Result;
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogInfo("Calling api has timed out");
                errorMessage = ex.GetBaseException().Message;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Calling api {host + apiUrl} faild, {ex.GetBaseException().Message}");
                errorMessage = ex.GetBaseException().Message;
            }

            return Task.FromResult(false);
        }

        public bool TryPost<T>(string host, string mediaType, string apiUrl, T serviceModel, out string errorMessage)
        {
            try
            {
                errorMessage = string.Empty;
                _logger.LogInfo("MediaType = " + MediaType);
                mediaType = "application/json";
                var json = JsonConvert.SerializeObject(serviceModel);

                _logger.LogInfo($"Start calling api {host + apiUrl} with data {json}");


                var paramString = new StringContent(json, Encoding.UTF8, mediaType);
                var requestUrl = host + apiUrl;
                var response = HttpClient.PostAsync(requestUrl, paramString).Result;

                _logger.LogInfo($"Calling api {host + apiUrl} returned, {response.StatusCode}");
                _logger.LogInfo($"Calling api {host + apiUrl} response, {JsonConvert.SerializeObject(response)}");

                if (response.IsSuccessStatusCode)
                    return true;

                errorMessage = response.Content.ReadAsStringAsync().Result;
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogInfo("Calling api has timed out");
                errorMessage = ex.GetBaseException().Message;
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Calling api {host + apiUrl} faild, {ex.GetBaseException().Message}");

                errorMessage = ex.GetBaseException().Message;
            }
            return false;
        }

        public T TryPost<T>(string host, string mediaType, string apiUrl, object serviceModel, out string errorMessage)
        {
            var result = default(T);
            mediaType = "application/json";
            try
            {
                errorMessage = string.Empty;
                var json = JsonConvert.SerializeObject(serviceModel);

                _logger.LogInfo($"Start calling api {host + apiUrl} with data {json}");


                var paramString = new StringContent(json, Encoding.UTF8, mediaType);
                var requestUrl = host + apiUrl;
                var response = HttpClient.PostAsync(requestUrl, paramString).Result;

                _logger.LogInfo($"Calling api {host + apiUrl} returned, {response.StatusCode}");
                _logger.LogInfo($"Calling api {host + apiUrl} response, {JsonConvert.SerializeObject(response)}");

                if (response.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    errorMessage = response.Content.ReadAsStringAsync().Result;
                }
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogInfo("Calling api has timed out");
                errorMessage = ex.GetBaseException().Message;
            }
            catch (Exception ex)
            {
                _logger.LogInfo(ex.Message);
                _logger.LogInfo($"Calling api {host + apiUrl} failed, {ex.GetBaseException().Message}");
                errorMessage = ex.GetBaseException().Message;
            }
            return result;
        }

        public Task<T> TryPost<T>(string host, string apiUrl, T serviceModel, out string errorMessage)
        {
            var result = default(T);
            string mediaType = "application/json";
            try
            {
                errorMessage = string.Empty;
                var json = JsonConvert.SerializeObject(serviceModel);

                _logger.LogInfo($"Start calling api {host + apiUrl} with data {json}");


                var paramString = new StringContent(json, Encoding.UTF8, mediaType);
                var requestUrl = host + apiUrl;
                var response = HttpClient.PostAsync(requestUrl, paramString).Result;

                _logger.LogInfo($"Calling api {host + apiUrl} returned, {response.StatusCode}");
                _logger.LogInfo($"Calling api {host + apiUrl} response, {JsonConvert.SerializeObject(response)}");

                if (response.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<Response<T>>(response.Content.ReadAsStringAsync().Result).Data;
                }
                else
                {
                    errorMessage = response.Content.ReadAsStringAsync().Result;
                }
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogInfo("Calling api has timed out");
                errorMessage = ex.GetBaseException().Message;
            }
            catch (Exception ex)
            {
                _logger.LogInfo(ex.Message);
                _logger.LogInfo($"Calling api {host + apiUrl} failed, {ex.GetBaseException().Message}");
                errorMessage = ex.Message;
            }
            return Task.FromResult(result);
        }
        }
    }
