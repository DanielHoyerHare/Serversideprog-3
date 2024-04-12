using Microsoft.AspNetCore.DataProtection;

namespace BlazorApp1.Codes;

public class SymetriskEncryptHandler
{
    private readonly IDataProtector _protector;
    public SymetriskEncryptHandler(IDataProtectionProvider protector)
    { 
        _protector = protector.CreateProtector(key);
    }
    private string key = "test";
    public string EncryptData(string textToEncrypt) => _protector.Protect(textToEncrypt);
    public string DecryptData(string textToEncrypt) => _protector.Unprotect(textToEncrypt);
}
