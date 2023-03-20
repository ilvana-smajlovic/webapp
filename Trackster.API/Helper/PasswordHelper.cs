using System.Security.Cryptography;
using System.Text;

namespace Trackster.API.Helper
{
    public static class PasswordHelper
    {

        public static string Hash(string password)
        {
            //DateTime Now = DateTime.Now;
            //var newstring = Now.ToString();
            string newstring = "NovaSifra123";

            var algorithm = SHA256.Create();

            var HashedPasswordByte = algorithm.ComputeHash(Encoding.UTF8.GetBytes(password + newstring));

            return BitConverter.ToString(HashedPasswordByte).Replace("-", string.Empty);
        }

    }
}
