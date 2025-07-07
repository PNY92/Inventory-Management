using Inventory_Management2.Enum;
using Inventory_Management2.Interfaces;
using Inventory_Management2.Models;
using Inventory_Management2.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.Text.Json;

namespace Inventory_Management2.Repositories
{
    public class WebClientRepository : IWebClientRepository
    {

        private readonly HttpClient _httpClient;    
        public WebClientRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ItemView>> GetItemsAsync()
        {
            
            return await _httpClient.GetFromJsonAsync<List<ItemView>>("/api/items");
        }

        public async Task<ItemView> GetItemByIdAsync(string itemId)
        {
            return await _httpClient.GetFromJsonAsync<ItemView>("/api/items/" + itemId);
        }

        public async Task<List<IdentityUser>> GetAllUsersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<IdentityUser>>("/api/user");
        }

        public async Task<Status> AddItemAsync(ItemView item)
        {

            var response = await _httpClient.PostAsJsonAsync("/api/items", item);

            if (response.IsSuccessStatusCode)
            {
                return new Status
                {
                    Code = StatusCode.STATUS_SUCCESS_CODE,
                    Description = "Successfully Added"
                };
            }

            return new Status
            {
                Code = StatusCode.STATUS_FAILED_CODE,
                Description = "Failed to insert item"
            };
        }
        public async Task<Status> EditItemAsync(ItemView item)
        {


            var response = await _httpClient.PutAsJsonAsync($"/api/items/{item.Id}", item);
            
            if (response.IsSuccessStatusCode)
            {
                return new Status
                {
                    Code = StatusCode.STATUS_SUCCESS_CODE,
                    Description = "Successfully Added"
                };
            }
            return new Status
            {
                Code = StatusCode.STATUS_FAILED_CODE,
                Description = "Failed to edit item"
            };
        }

        public async Task<Status> DeleteItemAsync(string id)
        {


            var response = await _httpClient.DeleteAsync($"/api/items/{id}");

            if (response.IsSuccessStatusCode)
            {
                return new Status
                {
                    Code = StatusCode.STATUS_SUCCESS_CODE,
                    Description = "Successfully Deleted"
                };
            }
            return new Status
            {
                Code = StatusCode.STATUS_FAILED_CODE,
                Description = "Failed to delete item"
            };
        }

        public async Task<IdentityUser> GetUserByIdAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<IdentityUser>($"/api/user/{id}");
        }

        public async Task<Status> UpdateUserAsync(UserView user)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/user/{user.Id}", user);

            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Status>(content);

        }

        public async Task<Status> DeleteUserAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"/api/user/{id}");

            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Status>(content);

        }

        public async Task<Status> AddUserAsync(UserCreate user)
        {
            var response = await _httpClient.PostAsJsonAsync<UserCreate>("/api/user/AddUser", user);

            if (response.IsSuccessStatusCode)
            {
                return new Status
                {
                    Code = StatusCode.STATUS_SUCCESS_CODE,
                    Description = "Successfully Added"
                };
            }
            return new Status
            {
                Code = StatusCode.STATUS_FAILED_CODE,
                Description = "Failed to add user"
            };
        }
    }
}
