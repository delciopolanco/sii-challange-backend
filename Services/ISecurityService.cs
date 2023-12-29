using System;
namespace addCard_backend.Services
{
    public interface ISecurityService
    {
        string Encrypt(string value);
        string Decrypt(string value);
    }
}

