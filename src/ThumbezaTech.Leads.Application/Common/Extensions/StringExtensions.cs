using System.Text;

namespace ThumbezaTech.Leads.Application.Common.Extensions;

public static class StringExtensions
{
    public static string CreateMD5(this string input)
    {
        if (string.IsNullOrEmpty(input))
            return null;

        using var md5 = System.Security.Cryptography.MD5.Create();
        var inputBytes = Encoding.ASCII.GetBytes(input);
        var hashBytes = md5.ComputeHash(inputBytes);

        var sb = new StringBuilder();
        for (var i = 0; i < hashBytes.Length; i++)
        {
            sb.Append(hashBytes[i].ToString("X2"));
        }
        return sb.ToString();
    }
}
