using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GestionProfesores.Api.Client
{
    public class GestionProfesoresApiClient
    {
        static readonly string API_URL = "http://localhost:5000/api/";
        static readonly string TEACHER_METHODS_PATH = "teacher";

        async Task<HttpResponseMessage> ExecuteRequest(string userName, string userPassword, Func<HttpClient, Task<HttpResponseMessage>> executeRequest)
        {
            using (var client = new HttpClient())
            {
                InitializeHttpClient(client, userName, userPassword);
                var requestResponse = await executeRequest(client);
                requestResponse.EnsureSuccessStatusCode();
                return requestResponse;
            }
        }

        async Task<T> ExecuteRequestWithCustomResponse<T>(string userName, string userPassword, string methodPath)
        {
            var httpResponse = await ExecuteRequest(userName, userPassword, (httpClient) => httpClient.GetAsync(methodPath));
            return await httpResponse.Content.ReadAsAsync<T>();
        }

        public async Task<IEnumerable<T>> GetTeachers<T>(string userName, string userPassword) => await ExecuteRequestWithCustomResponse<IEnumerable<T>>(userName, userPassword, TEACHER_METHODS_PATH);

        public async Task<T> GetTeacher<T>(string userName, string userPassword, int teacherId) => await ExecuteRequestWithCustomResponse<T>(userName, userPassword, GetTeacherMethodPathForRequest(teacherId));

        public async Task CreateTeacher<T>(string userName, string userPassword, T teacher) => await ExecuteRequest(userName, userPassword, async (httpclient) => await httpclient.PostAsJsonAsync<T>(TEACHER_METHODS_PATH, teacher));

        public async Task UpdateTeacher<T>(string userName, string userPassword, int teacherId, T teacher) => await ExecuteRequest(userName, userPassword, async (httpclient) => await httpclient.PutAsJsonAsync<T>(GetTeacherMethodPathForRequest(teacherId), teacher));

        public async Task DeleteTeacher(string userName, string userPassword, int teacherId) => await ExecuteRequest(userName, userPassword, async (httpclient) => await httpclient.DeleteAsync(GetTeacherMethodPathForRequest(teacherId)));

        private static string GetTeacherMethodPathForRequest(int teacherId) => $"{TEACHER_METHODS_PATH}/{teacherId}";

        void InitializeHttpClient(HttpClient httpClient, string userName, string userPassword)
        {
            httpClient.BaseAddress = new Uri(API_URL);
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", GetRequestHeaderAuthorizationValue(userName, userPassword));
        }

        string GetRequestHeaderAuthorizationValue(string userName, string userPassword) => Convert.ToBase64String(Encoding.Default.GetBytes($"{userName}:{userPassword}"));
    }
}