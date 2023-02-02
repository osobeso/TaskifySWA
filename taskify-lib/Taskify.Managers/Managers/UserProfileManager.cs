using Taskify.Data.Repositories;

namespace Taskify.Managers.Managers
{
    public class UserProfileManager : IUserProfileManager
    {
        private readonly IUserProfileRepository UserProfiles;
        public UserProfileManager(IUserProfileRepository userProfiles)
        {
            UserProfiles = userProfiles;
        }
    }
}
