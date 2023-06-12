using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web.Http;

namespace NinjectPractice.Services
{
    public class JsonPlaceholderService : IJsonPlaceholderService
    {
        private readonly HttpClient _httpClient;
        public JsonPlaceholderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Post>> GetAllPostAsync()
        {
            var response = await _httpClient.GetAsync("posts");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStreamAsync();
            var posts = await JsonSerializer.DeserializeAsync<IEnumerable<Post>>(content);
            return posts;

        }

        public async Task<Post> GetPostById(int id)
        {
            var response = await _httpClient.GetAsync($"posts/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStreamAsync();
            var posts = await JsonSerializer.DeserializeAsync<Post>(content);
            return posts;
        }
    }
}