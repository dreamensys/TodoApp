using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using ToDo.API.Models;
using ToDo.Domain.DTO;
using Xunit;

namespace ToDo.API.IntegrationTests
{
    public class TodoApiTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private readonly TestHelper _helper;

        public TodoApiTest(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
            _helper = new TestHelper();
        }


        [Fact]
        public async Task Get_ShouldReturnAllTodoItems()
        { 
           // Act
           var response = await _client.GetAsync(Constants.TodoApiUrl);
           var data = await response.Content.ReadAsStringAsync();
            
           //Assert
           response.EnsureSuccessStatusCode();
        }
        
        [Fact]
        public async Task Get_WithID_ShouldReturnTodoItem()
        {
            // Arrange
            var postRequest = _helper.GetNewTodoItemDto();
            var postResponse = await _client.PostAsync(Constants.TodoApiUrl, RequestContentHelper.GetStringContent(postRequest));
            var json = await postResponse.Content.ReadAsStringAsync();
            var newItem = JsonConvert.DeserializeObject<TodoItemDto>(json);

            // Act
            var response = await _client.GetAsync($"{Constants.TodoApiUrl}/{newItem.Id}");
            json = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<TodoItemDto>(json);


            // Assert
            Assert.Equal(newItem.Id, data.Id);
        }

        [Fact]
        public async Task Post_ShouldReturnCreatedTodoItem()
        {
            // Arrange
            var request = _helper.GetNewTodoItemDto();
            var body = RequestContentHelper.GetStringContent(request);

            // Act
            var response = await _client.PostAsync(Constants.TodoApiUrl, body);
            var data = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Put_ShouldUpdateTodoItem()
        {
            // Arrange
            var postRequest = _helper.GetNewTodoItemDto();
            var postResponse = await _client.PostAsync(Constants.TodoApiUrl,  RequestContentHelper.GetStringContent(postRequest));
            var json = await postResponse.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<TodoItemDto>(json);
            var updateRequest = new UpdateTodoitem()
            {
                Title = "Task Updated",
                Completed = true
            };
            var body = RequestContentHelper.GetStringContent(updateRequest);

            // Act
            var updateResponse = await _client.PutAsync($"{Constants.TodoApiUrl}/{data.Id}", RequestContentHelper.GetStringContent(body));

            // Assert
            updateResponse.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Delete_ShouldDeleteExistingTask()
        {
            // Arrange
            var postRequest = _helper.GetNewTodoItemDto();
            var postResponse = await _client.PostAsync(Constants.TodoApiUrl, RequestContentHelper.GetStringContent(postRequest));
            var json = await postResponse.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<TodoItemDto>(json);

            // Act
            var response = await _client.DeleteAsync($"{Constants.TodoApiUrl}/{data.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
        }


    }
}
