using System.Security.Cryptography;
using System.Text;

namespace ZombieDAO; 

public static class Tools {
    public static string GenerateRandomString(int size) {            
        var chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890-_".ToCharArray(); 
        var data = new byte[4 * size];
        
        using (var crypto = RandomNumberGenerator.Create()) 
            crypto.GetBytes(data);
        
        var result = new StringBuilder(size);
        
        for (var i = 0; i < size; i++) {
            var rnd = BitConverter.ToUInt32(data, i * 4);
            var idx = rnd % chars.Length;
            result.Append(chars[idx]);
        }

        return result.ToString();
    }
}
