using Microsoft.AspNetCore.DataProtection;
using System.Security.Cryptography;
using System.Text;

namespace BlazorApp1.Codes;

public class ASymetriskEncryptHandler
{
    private string _privateKey;
    private string _publicKey;
    public ASymetriskEncryptHandler()
    { 
        using(RSACryptoServiceProvider rsa = new RSACryptoServiceProvider()) 
        { 
            _privateKey = rsa.ToXmlString(true);
            _publicKey = rsa.ToXmlString(false);
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
