using Inventory_Management2.Data;
using Inventory_Management2.Interfaces;
using Inventory_Management2.Models;
using Inventory_Management2.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using System.Data;

namespace Inventory_Management2.Repositories
{
    public class UserRepository : IUserRepository
    {

        protected readonly AuthDbContext _authContext;
        protected readonly UserManager<IdentityUser> _userManager;

        public UserRepository(AuthDbContext authContext, UserManager<IdentityUser> userManager)
        {
            _authContext = authContext;
            _userManager = userManager;
        }

        public async Task<Status> SetRoleAsync(IdentityUser user, string role)
        {
            try
            {



                if (!string.IsNullOrEmpty(role))
                {
                    var currentRoles = await _userManager.GetRolesAsync(user);

                    if (currentRoles.Any())
                    {
                        var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                        if (!removeResult.Succeeded)
                        {
                            return new Status()
                            {
                                Code = Enum.StatusCode.STATUS_FAILED_CODE,
                                Description = "Failed to remove existing roles"
                            };
                        }
                    }

                    var addResult = await _userManager.AddToRoleAsync(user, role);
                    if (!addResult.Succeeded)
                    {
                        return new Status()
                        {
                            Code = Enum.StatusCode.STATUS_FAILED_CODE,
                            Description = "Failed to assign new role"
                        };
                    }
                }

                return new Status()
                {
                    Code = Enum.StatusCode.STATUS_SUCCESS_CODE,
                    Description = "Successfully updated user and role"
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Status()
                {
                    Code = Enum.StatusCode.STATUS_FAILED_CODE,
                    Description = $"An error has occurred: {ex.Message}"
                };
            }
        }
        public async Task<Status> AddUserAsync(UserCreate user)
        {
            try
            {

                await _userManager.CreateAsync(user.User);

                await SetRoleAsync(user.User, user.Role);
                return new Status()
                {
                    Code = Enum.StatusCode.STATUS_SUCCESS_CODE,
                    Description = "Successfully created a user"
                };
            }
            catch (Exception ex)
            {
                return new Status()
                {
                    Code = Enum.StatusCode.STATUS_FAILED_CODE,
                    Description = $"An error has occurred: {ex.Message}"
                };
            }

        }

        public async Task<List<IdentityUser>> GetAllUsersAsync()
        {
            return await _authContext.Users.ToListAsync();
        }

        public async Task<IdentityUser> GetUserByIdAsync(string id)
        {
            return await _authContext.Users
                .FirstOrDefaultAsync((u) => u.Id == id);
        }

        public async Task<Status> UpdateUserAsync(IdentityUser user, string role)
        {
            if (user != null)
            {

                try
                {
                    var existingUser = await _userManager.FindByIdAsync(user.Id);

                    if (existingUser == null)
                        return new Status()
                        {
                            Code = Enum.StatusCode.STATUS_FAILED_CODE,
                            Description = "User not found"
                        };

                    existingUser.Email = user.Email;
                    existingUser.UserName = user.UserName;
                    


                    // Save changes
                    var result = await _userManager.UpdateAsync(existingUser);

                    await SetRoleAsync(existingUser, role);

                    return new Status()
                    {
                        Code = Enum.StatusCode.STATUS_SUCCESS_CODE,
                        Description = "Done"
                    };
                }
                catch (Exception ex) {
                    Console.WriteLine(ex);
                }
                
            }
            
            return new Status()
            {
                Code = Enum.StatusCode.STATUS_FAILED_CODE,
                Description = "User not found"
            };
        }
        public async Task<Status> DeleteUserAsync(string id)
        {
            try
            {
                var existingUser = await _userManager.FindByIdAsync(id);

                if (existingUser == null)
                    return new Status()
                    {
                        Code = Enum.StatusCode.STATUS_FAILED_CODE,
                        Description = "User not found"
                    };

                await _userManager.DeleteAsync(existingUser);

                return new Status()
                {
                    Code = Enum.StatusCode.STATUS_SUCCESS_CODE,
                    Description = "Success"
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return new Status()
            {
                Code = Enum.StatusCode.STATUS_FAILED_CODE,
                Description = "User not found"
            };
        }    
    }
}
