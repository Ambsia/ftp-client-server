namespace FTPServer.util
{
    public class Cryptography
    {
        public static string HashPassword(string unhashedPassword, string salt)
        {
            return BCrypt.Net.BCrypt.HashPassword(unhashedPassword,salt);
        }

        public static string GenerateSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt();
        }

        public static bool CompareValues(string compared, string actual)
        {
            return BCrypt.Net.BCrypt.Verify(compared, actual);
        }
    }

}
