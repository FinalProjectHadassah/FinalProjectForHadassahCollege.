using System;
using System.Security.Cryptography;
using System.Text;

namespace NewForumProject.Models
{
    /// <summary>
    /// Class for generating and hashing passwords
    /// </summary>
    public class Password : Entity
    {
        private string _password;
        private int _salt;

        public Password(string strPassword, int nSalt)
        {
            _password = strPassword;
            _salt = nSalt;
        }

        public static string CreateRandomPassword(int passwordLength)
        {
            String _allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ23456789";
            Byte[] randomBytes = new Byte[passwordLength];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);
            char[] chars = new char[passwordLength];
            int allowedCharCount = _allowedChars.Length;

            for (int i = 0; i < passwordLength; i++)
            {
                chars[i] = _allowedChars[(int)randomBytes[i] % allowedCharCount];
            }

            return new string(chars);
        }

        public static int CreateRandomSalt()
        {
            Byte[] _saltBytes = new Byte[4];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(_saltBytes);

            return ((((int)_saltBytes[0]) << 24) + (((int)_saltBytes[1]) << 16) + (((int)_saltBytes[2]) << 8) + ((int)_saltBytes[3]));
        }

        public string ComputeSaltedHash()
        {
            // Create Byte array of password string
            ASCIIEncoding encoder = new ASCIIEncoding();
            Byte[] _secretBytes = encoder.GetBytes(_password);

            // Create a new salt
            Byte[] _saltBytes = new Byte[4];
            _saltBytes[0] = (byte)(_salt >> 24);
            _saltBytes[1] = (byte)(_salt >> 16);
            _saltBytes[2] = (byte)(_salt >> 8);
            _saltBytes[3] = (byte)(_salt);

            // append the two arrays
            Byte[] toHash = new Byte[_secretBytes.Length + _saltBytes.Length];
            Array.Copy(_secretBytes, 0, toHash, 0, _secretBytes.Length);
            Array.Copy(_saltBytes, 0, toHash, _secretBytes.Length, _saltBytes.Length);

            SHA1 sha1 = SHA1.Create();
            Byte[] computedHash = sha1.ComputeHash(toHash);

            return encoder.GetString(computedHash);
        }
    }
}
//namespace HashPassword
//{
//    class TestApplication
//    {
//        [STAThread]
//        static void Main(string[] args)
//        {
//            // Generate a new random password string
//            string myPassword = Password.CreateRandomPassword(8);

//            // Debug output
//            Console.WriteLine(myPassword);

//            // Generate a new random salt
//            int mySalt = Password.CreateRandomSalt();

//            // Initialize the Password class with the password and salt
//            Password pwd = new Password(myPassword, mySalt);

//            // Compute the salted hash
//            // NOTE: you store the salt and the salted hash in the datbase
//            string strHashedPassword = pwd.ComputeSaltedHash();

//            // Debug output
//            Console.WriteLine(strHashedPassword);
//        }
//    }
//}
