using Microsoft.AspNetCore.DataProtection;
using System.Security.Cryptography;
using System.Text;

namespace BlazorApp1.Codes;

public class ASymetriskEncryptHandler
{
    private string _privateKey;
    private string _publicKey;
    private string path = @"C:\Users\Tec\Desktop\xmlKey.txt";
    public ASymetriskEncryptHandler()
    {
        if (File.Exists(path))
        {
            var keys = File.ReadAllText(path);
        }
        else
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                _privateKey = rsa.ToXmlString(true);
                _publicKey = rsa.ToXmlString(false);
                File.WriteAllText(path, _privateKey);
                string[] keys = [_privateKey, _publicKey];
                File.WriteAllLines(path, keys);
            }
        }
    }

    public string EncryptAsymetrisk(string textToEncrypt) => Encrypter.Encrypt(textToEncrypt, _publicKey);

    public string DecryptAsymetrisk(string textToDecrypt)
    {
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        {
            rsa.FromXmlString(_privateKey);
            byte[] byteArrayTextToDecrypt = Convert.FromBase64String(textToDecrypt);
            byte[] decryptedValue = rsa.Decrypt(byteArrayTextToDecrypt, true);

            return Encoding.UTF8.GetString(decryptedValue);
        }
    }
}
