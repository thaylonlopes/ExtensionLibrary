using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HttpClientExtensionsLibrary
{
    public static partial class HttpClientExtensions
    {
        /// <summary>
        /// Logs details of the HTTP request and response to Console.
        /// </summary>
        public static async Task<HttpResponseMessage> LogRequestDetails(this HttpClient client, HttpRequestMessage request)
        {
            var stopwatch = Stopwatch.StartNew();
            var response = await client.SendAsync(request);
            stopwatch.Stop();

            Console.WriteLine($"Request to {request.RequestUri} took {stopwatch.ElapsedMilliseconds} ms.");
            Console.WriteLine($"Response status code: {response.StatusCode}");
            Console.WriteLine($"Response headers: {FormatHeaders(response.Headers)}");

            return response;
        }

        /// <summary>
        /// Logs full request and response details to Console.
        /// </summary>
        public static async Task<HttpResponseMessage> LogRequestAndResponse(this HttpClient client, HttpRequestMessage request)
        {
            var stopwatch = Stopwatch.StartNew();
            var response = await client.SendAsync(request);
            stopwatch.Stop();

            await LogRequestToConsoleAsync(request);
            await LogResponseToConsoleAsync(response, stopwatch.ElapsedMilliseconds);

            return response;
        }

        /// <summary>
        /// Logs errors during the HTTP request.
        /// </summary>
        public static async Task<HttpResponseMessage> LogError(this HttpClient client, HttpRequestMessage request)
        {
            try
            {
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Request to {request.RequestUri} failed with exception: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Logs the status of the HTTP response to Console.
        /// </summary>
        public static async Task<HttpResponseMessage> LogResponseStatus(this HttpClient client, HttpRequestMessage request)
        {
            var response = await client.SendAsync(request);
            Console.WriteLine($"Response Status Code: {response.StatusCode}");
            return response;
        }

        /// <summary>
        /// Logs request and response details to a file.
        /// </summary>
        public static async Task<HttpResponseMessage> LogToFile(this HttpClient client, HttpRequestMessage request, string filePath)
        {
            var stopwatch = Stopwatch.StartNew();
            var response = await client.SendAsync(request);
            stopwatch.Stop();

            using (var writer = new StreamWriter(filePath, append: true))
            {
                await LogRequestToFileAsync(writer, request);
                await LogResponseToFileAsync(writer, response, stopwatch.ElapsedMilliseconds);
            }

            return response;
        }

        private static async Task LogRequestToConsoleAsync(HttpRequestMessage request)
        {
            Console.WriteLine($"Request URI: {request.RequestUri}");
            Console.WriteLine($"Request Method: {request.Method}");
            Console.WriteLine($"Request Headers: {FormatHeaders(request.Headers)}");

            if (request.Content != null)
            {
                var requestBody = await request.Content.ReadAsStringAsync();
                Console.WriteLine($"Request Body: {requestBody}");
            }
        }

        private static async Task LogResponseToConsoleAsync(HttpResponseMessage response, long elapsedMilliseconds)
        {
            Console.WriteLine($"Response Status Code: {response.StatusCode}");
            Console.WriteLine($"Response Headers: {FormatHeaders(response.Headers)}");

            if (response.Content != null)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response Body: {responseBody}");
            }

            Console.WriteLine($"Elapsed Time: {elapsedMilliseconds} ms");
        }

        private static async Task LogRequestToFileAsync(StreamWriter writer, HttpRequestMessage request)
        {
            await writer.WriteLineAsync($"Request URI: {request.RequestUri}");
            await writer.WriteLineAsync($"Request Method: {request.Method}");
            await writer.WriteLineAsync($"Request Headers: {FormatHeaders(request.Headers)}");

            if (request.Content != null)
            {
                var requestBody = await request.Content.ReadAsStringAsync();
                await writer.WriteLineAsync($"Request Body: {requestBody}");
            }
        }

        private static async Task LogResponseToFileAsync(StreamWriter writer, HttpResponseMessage response, long elapsedMilliseconds)
        {
            await writer.WriteLineAsync($"Response Status Code: {response.StatusCode}");
            await writer.WriteLineAsync($"Response Headers: {FormatHeaders(response.Headers)}");

            if (response.Content != null)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                await writer.WriteLineAsync($"Response Body: {responseBody}");
            }

            await writer.WriteLineAsync($"Elapsed Time: {elapsedMilliseconds} ms");
        }

        private static string FormatHeaders(HttpHeaders headers)
        {
            return string.Join(", ", headers.Select(h => $"{h.Key}: {string.Join(";", h.Value)}"));
        }
    }
}