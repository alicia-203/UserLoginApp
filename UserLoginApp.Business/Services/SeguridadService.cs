using System.Text.RegularExpressions;

namespace UserLoginApp.Business.Services
{
    public static class SeguridadService
    {
        public static bool PasswordValida(string password)
        {
            var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{9,}$");
            return regex.IsMatch(password);
        }
    }
}
