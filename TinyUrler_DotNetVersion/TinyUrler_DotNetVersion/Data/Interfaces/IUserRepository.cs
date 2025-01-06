using TinyUrler_DotNetVersion.Models;
using TinyUrler_DotNetVersion.Models.Context;

namespace TinyUrler_DotNetVersion.Data;

public interface IUserRepository
{
    public IEnumerable<AppUser> GetUsers();
    public AppUser? GetUser(string username);
    public AppUser GetUserById(string id);
    public bool AddLinkToUser(AppUser user, Link link);
    public bool AddUser(AppUser user);
    public bool UpdateUser(AppUser user);
    public bool DeleteUser(string username);
    public bool Save();
}