using Microsoft.AspNetCore.Identity;

namespace UserLoginApp.Business.Services;

public static class HashService
{
    public static string Hash(string password)
    {
        var hasher = new PasswordHasher<object>();
        return hasher.HashPassword(null, password);
    }
}
