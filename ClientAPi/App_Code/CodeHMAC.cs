using System;
using System.Security.Cryptography;
using System.Text;

namespace ClientAPi.App_Code
{
    public class CodeHMAC
    {
        public string getHMAC256(string message, String key = "3st03l@C1@v3")
        {
            byte[] keyByte = new ASCIIEncoding().GetBytes(key);
            byte[] messageBytes = new ASCIIEncoding().GetBytes(message);
            byte[] hashmessage = new HMACSHA256(keyByte).ComputeHash(messageBytes);
            String.Concat(Array.ConvertAll(hashmessage, x => x.ToString("x2")));
            return Convert.ToBase64String(hashmessage);
        }
    }
}