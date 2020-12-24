namespace UserManagement.API.IntegrationTests
{
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    /// <summary>
    /// Generic methods for JSON serialization.
    /// </summary>
    public static class IntegrationTestHelper
    {
        /// <summary>
        /// Serialize the JSON in API response.
        /// </summary>
        /// <param name="obj">The input object to be serialized.</param>
        /// <returns>Return the string conent the JSON.</returns>
        public static StringContent GetRequestContent(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }

        /// <summary>
        /// Deserialize the JSON.
        /// </summary>
        /// <typeparam name="T">Input genric class object.</typeparam>
        /// <param name="response">The HttpResponseMessage object.</param>
        /// <returns>The result.</returns>
        public static async Task<T> GetResponseContent<T>(HttpResponseMessage response)
        {
            var stringResponse = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<T>(stringResponse);

            return result;
        }
    }
}
