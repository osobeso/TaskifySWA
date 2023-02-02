using Taskify.Data.Models;

namespace Taskify.Data.Repositories
{
    public interface IUserProfileRepository
    {
        Task<UserProfileModel?> GetAsync(Guid id);
        Task<UserProfileModel> UpsertAsync(UserProfileModel task);
        Task<bool> DeleteAsync(Guid id);
    }
}
