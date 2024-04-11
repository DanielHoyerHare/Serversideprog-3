using BCrypt.Net;
using System.Security.Cryptography;
using System.Text;

namespace BlazorApp1.Codes;

public static class HashingHandler
{
    public static string MD5Hashing(string textToHash)
    {
        MD5 md5 = MD5.Create();

        byte[] inputBytes = Encoding.ASCII.GetBytes(textToHash);
        byte[] hashedValueBytes = md5.ComputeHash(inputBytes);

        return Convert.ToBase64String(hashedValueBytes);
    }

    public static string SHAHashing(string textToHash)
    {
        SHA256 sha256 = SHA256.Create();

        byte[] inputBytes = Encoding.ASCII.GetBytes(textToHash);
        byte[] hashedValueBytes = sha256.ComputeHash(inputBytes);

        return Convert.ToBase64String(hashedValueBytes);
    }

    public static string HMACHashing(string textToHash)
    {
        byte[] myKey = Encoding.ASCII.GetBytes("MyNiceKey");
        byte[] inputBytes = Encoding.ASCII.GetBytes(textToHash);

        HMACSHA256 hmac = new HMACSHA256();
        hmac.Key = myKey;

        byte[] hashedValueBytes = hmac.ComputeHash(inputBytes);

        return Convert.ToBase64String(hashedValueBytes);
    }

    public static string PBKDF2Hashing(string textToHash)
    {
        byte[] inputBytes = Encoding.ASCII.GetBytes(textToHash);
        byte[] salt = Encoding.ASCII.GetBytes("MyNiceKey");

        var hashAlgorythm = new HashAlgorithmName("SHA256");
        int itirations = 10;
        int outputLength = 32;

        byte[] hashedValueBytes = Rfc2898DeriveBytes.Pbkdf2(inputBytes, salt, itirations, hashAlgorythm, outputLength);
        return Convert.ToBase64String(hashedValueBytes);
    }

    public static string BCryptHashing(string textToHash)
    {
        //return BCrypt.Net.BCrypt.HashPassword(textToHash);

        //int workFactor = 10;
        //bool enhanceEntropy = true;
        //return BCrypt.Net.BCrypt.HashPassword(textToHash, workFactor, enhanceEntropy);

        string salt = BCrypt.Net.BCrypt.GenerateSalt();
        bool enhanceEntropy = true;
        HashType hashType = HashType.SHA256;
        return BCrypt.Net.BCrypt.HashPassword(textToHash, salt, enhanceEntropy, hashType);
    }

    public static bool BCryptVerifyHashing(string textToHash, string hashedValueFromDB)
    {
        //return BCrypt.Net.BCrypt.Verify(textToHash, hashedValueFromDB);

        //bool enhanceEntropy = true;
        //return BCrypt.Net.BCrypt.Verify(textToHash, hashedValueFromDB, enhanceEntropy);

        bool enhanceEntropy = true;
        HashType hashType = HashType.SHA256;
        return BCrypt.Net.BCrypt.Verify(textToHash, hashedValueFromDB, enhanceEntropy, hashType);
    }
}
