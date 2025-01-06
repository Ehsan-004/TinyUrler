using TinyUrler_DotNetVersion.Models;
using TinyUrler_DotNetVersion.Models.Context;

namespace TinyUrler_DotNetVersion.Data;

public class UserRepository : IUserRepository
{
    private readonly TContext _context;

    public UserRepository(TContext context)
    {
        _context = context;
    }

    public IEnumerable<AppUser> GetUsers()
    {
        return _context.Users.ToList();
    }

    public AppUser? GetUser(string username)
    {
        return _context.Users.FirstOrDefault(u => u.UserName == username);
    }

    public AppUser GetUserById(string id)
    {
        return _context.Users.FirstOrDefault(u => u.Id == id);
    }

    public bool AddLinkToUser(AppUser user, Link link)
    {
        user.Links.Add(link);
        return Save();
    }

    public bool AddUser(AppUser user)
    {
        _context.Users.Add(user);
        return Save();
    }

    public bool UpdateUser(AppUser user)
    {
        _context.Users.Update(user);
        return Save();
    }

    public bool DeleteUser(string username)
    {
        var user = GetUser(username);
        if (user != null)
        {
            _context.Users.Remove(user);
            return Save();
        }
        return false;
    }

    public bool Save()
    {
        return _context.SaveChanges() >= 0;
    }
}