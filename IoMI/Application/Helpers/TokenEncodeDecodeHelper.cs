using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace IoMI.Application.Helpers;

public static class TokenEncodeDecodeHelper
{
    public static string EncodeForUrl(this string value)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(value);
        return WebEncoders.Base64UrlEncode(bytes);
    }

    public static string DecodeFromUrl(this string value)
    {
        byte [] bytes = WebEncoders.Base64UrlDecode(value);
        return Encoding.UTF8.GetString(bytes);
    }
}