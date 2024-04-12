using Microsoft.AspNetCore.DataProtection;
using System.Security.Cryptography;
using System.Text;

namespace BlazorApp1.Codes;

public class ASymetriskEncryptHandler
{
    private string _privateKey;
    private string _publicKey;
    private string privatePath;
    private string publicPath;
    public ASymetriskEncryptHandler()
    {
        string rootPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        rootPath = Path.Combine(rootPath, ".aspnet");

        privatePath = Path.Combine(rootPath, "xmlPrivateKey.pem");
        publicPath = Path.Combine(rootPath, "xmlPublicKey.pem");

        if (File.Exists(privatePath)) _privateKey = File.ReadAllText(privatePath);
        if (File.Exists(publicPath)) _publicKey = File.ReadAllText(publicPath);
        else
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                _privateKey = rsa.ToXmlString(true);
                _publicKey = rsa.ToXmlString(false);
                File.WriteAllText(privatePath, _privateKey);
                File.WriteAllText(publicPath, _publicKey);
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
