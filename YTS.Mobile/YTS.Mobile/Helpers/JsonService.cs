using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

// ReSharper disable ArrangeThisQualifier

namespace YTS.Mobile.Helpers
{
   
    public class JsonService 
    {
        #region Public Methods

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">The URL.</param>
        /// <param name="accessToken">The accessToken.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>Task&lt;T&gt;.</returns>
        public Task<T> GetAsync<T>(string url, string accessToken, JsonSerializerSettings settings) where T : new()
        {
            return this.GetAsync<T>(url, CancellationToken.None, accessToken, settings);
        }

        /// <summary>
        /// get as an asynchronous operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">The URL.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="accessToken">The access token.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>Task&lt;T&gt;.</returns>
        /// <exception cref="HttpRequestException"></exception>
        public async Task<T> GetAsync<T>(string url, CancellationToken cancellationToken, string accessToken,
            JsonSerializerSettings settings) where T : new()
        {
            var client = new HttpClient();
            if (!string.IsNullOrEmpty(accessToken))
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            }
            var response = await client.GetAsync(url, cancellationToken);
            var responseString = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await this.ToDataObject<T>(cancellationToken, settings, responseString);
            }
            throw new HttpRequestException(responseString);
        }

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="accessToken">The accessToken.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>Task&lt;T&gt;.</returns>
        public Task<T> GetAsync<T>(string baseUrl, Dictionary<string, object> parameters, string accessToken,
            JsonSerializerSettings settings) where T : new()
        {
            return this.GetAsync<T>(AddUrlParams(baseUrl, parameters), CancellationToken.None, accessToken, settings);
        }

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="accessToken">The accessToken.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>Task&lt;T&gt;.</returns>
        public Task<T> GetAsync<T>(string baseUrl, Dictionary<string, object> parameters,
            CancellationToken cancellationToken, string accessToken, JsonSerializerSettings settings) where T : new()
        {
            return this.GetAsync<T>(AddUrlParams(baseUrl, parameters), cancellationToken, accessToken, settings);
        }

        /// <summary>
        /// Gets the json data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        public string GetJsonData<T>(T value)
        {
            return JsonConvert.SerializeObject(value);
        }

        /// <summary>
        /// Posts the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">The URL.</param>
        /// <param name="postContent">Content of the post.</param>
        /// <param name="accessToken">The accessToken.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>Task&lt;T&gt;.</returns>
        public Task<T> PostAsync<T>(string url, object postContent, string accessToken, JsonSerializerSettings settings)
            where T : new()
        {
            return this.PostAsync<T>(url, postContent, CancellationToken.None, accessToken, settings);
        }

        /// <summary>
        /// post as an asynchronous operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">The URL.</param>
        /// <param name="postContent">Content of the post.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="accessToken">The accessToken.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>Task&lt;T&gt;.</returns>
        /// <exception cref="HttpRequestException"></exception>
        public async Task<T> PostAsync<T>(string url, object postContent, CancellationToken cancellationToken,
            string accessToken, JsonSerializerSettings settings)
            where T : new()
        {
            var client = new HttpClient();
            var serializedContent = JsonConvert.SerializeObject(postContent);
            HttpContent httpContent = new StringContent(serializedContent, Encoding.UTF8, "application/json");
            if (!string.IsNullOrEmpty(accessToken))
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            }
            var response = await client.PostAsync(url, httpContent, cancellationToken);
            var responseString = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await this.ToDataObject<T>(cancellationToken, settings, responseString);
            }
            throw new HttpRequestException(responseString);
        }

        /// <summary>
        /// Posts the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="postContent">Content of the post.</param>
        /// <param name="accessToken">The accessToken.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>Task&lt;T&gt;.</returns>
        public Task<T> PostAsync<T>(string baseUrl, Dictionary<string, object> parameters, object postContent,
            string accessToken, JsonSerializerSettings settings)
            where T : new()
        {
            return this.PostAsync<T>(AddUrlParams(baseUrl, parameters), postContent, accessToken, settings);
        }

        /// <summary>
        /// Posts the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="postContent">Content of the post.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="accessToken">The accessToken.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>Task&lt;T&gt;.</returns>
        public Task<T> PostAsync<T>(string baseUrl, Dictionary<string, object> parameters, object postContent,
            CancellationToken cancellationToken, string accessToken, JsonSerializerSettings settings) where T : new()
        {
            return this.PostAsync<T>(AddUrlParams(baseUrl, parameters), postContent, cancellationToken, accessToken,
                settings);
        }

        /// <summary>
        /// put as an asynchronous operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="postContent">Content of the post.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="accessToken">The accessToken.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>Task&lt;T&gt;.</returns>
        /// <exception cref="HttpRequestException"></exception>
        public async Task<T> PutAsync<T>(string baseUrl, object postContent,
            CancellationToken cancellationToken, string accessToken, JsonSerializerSettings settings) where T : new()
        {
            var client = new HttpClient();
            if (!string.IsNullOrEmpty(accessToken))
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            }
            var jsonString = JsonConvert.SerializeObject(postContent);
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var response = await client.PutAsync(baseUrl, httpContent, cancellationToken);

            var responseString = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await this.ToDataObject<T>(cancellationToken, settings, responseString);
            }

            throw new HttpRequestException(responseString);
        }

        /// <summary>
        /// Puts the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">The URL.</param>
        /// <param name="postContent">Content of the post.</param>
        /// <param name="accessToken">The accessToken.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>Task&lt;T&gt;.</returns>
        public Task<T> PutAsync<T>(string url, object postContent, string accessToken, JsonSerializerSettings settings)
            where T : new()
        {
            return this.PutAsync<T>(url, postContent, CancellationToken.None, accessToken, settings);
        }

        /// <summary>
        /// Puts the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="putContent">Content of the put.</param>
        /// <param name="accessToken">The accessToken.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>Task&lt;T&gt;.</returns>
        public Task<T> PutAsync<T>(string baseUrl, Dictionary<string, object> parameters, object putContent,
            string accessToken, JsonSerializerSettings settings)
            where T : new()
        {
            return this.PutAsync<T>(AddUrlParams(baseUrl, parameters), putContent, accessToken, settings);
        }

        /// <summary>
        /// Puts the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="putContent">Content of the put.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="accessToken">The accessToken.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>Task&lt;T&gt;.</returns>
        public Task<T> PutAsync<T>(string baseUrl, Dictionary<string, object> parameters, object putContent,
            CancellationToken cancellationToken, string accessToken, JsonSerializerSettings settings) where T : new()
        {
            return this.PutAsync<T>(AddUrlParams(baseUrl, parameters), putContent, cancellationToken, accessToken,
                settings);
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Adds the URL parameters.
        /// </summary>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>System.String.</returns>
        private static string AddUrlParams(string baseUrl, Dictionary<string, object> parameters)
        {
            var stringBuilder = new StringBuilder(baseUrl);
            var hasFirstParam = baseUrl.Contains("?");

            foreach (var parameter in parameters)
            {
                var format = hasFirstParam ? "&{0}={1}" : "?{0}={1}";
                stringBuilder.AppendFormat(format, Uri.EscapeDataString(parameter.Key),
                    Uri.EscapeDataString(parameter.Value.ToString()));
                hasFirstParam = true;
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Deserlizes the object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="responseString">The response string.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>T.</returns>
        private T DeserlizeObject<T>(string responseString, JsonSerializerSettings settings = null)
        {
            return settings == null
                ? JsonConvert.DeserializeObject<T>(responseString)
                : JsonConvert.DeserializeObject<T>(responseString, settings);
        }

        /// <summary>
        /// To the data object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <param name="settings">The settings.</param>
        /// <param name="responseString">The response string.</param>
        /// <returns>Task&lt;T&gt;.</returns>
        private async Task<T> ToDataObject<T>(CancellationToken cancellationToken, JsonSerializerSettings settings,
            string responseString) where T : new()
        {
            return await
                Task<T>.Factory.StartNew(() => this.DeserlizeObject<T>(responseString, settings), cancellationToken);
        }

        #endregion Private Methods
    }
}