// ***********************************************************************
// Assembly         : NALOrder.Model
// Author           : tranthiencdsp@gmail.com
// Created          : 04-6-2016
//
// Last Modified By : tranthiencdsp@gmail.com
// Last Modified On : 04-6-2016
// ***********************************************************************

using System.Threading.Tasks;
namespace NALOrder.Model
{
    public interface IUsersRepository :ISingle<UserDto>, IGet<UserDto>, IAdd<UserDto>, IUpdate<UserDto>, IDelete<UserDto>
    {
        UserDto Login(string email, string password);
        Task<UserDto> LoginAsync(string email, string password);

        SaveResult Unlocked(int id);
        Task<SaveResult> UnlockedAsync(int id);

        SaveResult Locked(int id);
        Task<SaveResult> LockedAsync(int id);

        SaveResult SetRole(int id, RoleType roleType);
        Task<SaveResult> SetRoleAsync(int id, RoleType roleType);
    }
}
