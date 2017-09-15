using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using Windows.Storage.Streams;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

class AvatarHelper
{
    public static BitmapImage GenerateAvatar()
    {
        var em = Windows.Storage.ApplicationData.Current.LocalSettings.Values["email"].ToString();
        var hash = CalculateMD5Hash(em);
        return new BitmapImage(new Uri($"https://www.gravatar.com/avatar/{hash}", UriKind.RelativeOrAbsolute));
    }
    private static string CalculateMD5Hash(string input)

    {

        // step 1, calculate MD5 hash from input

        MD5 md5 = System.Security.Cryptography.MD5.Create();

        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);

        byte[] hash = md5.ComputeHash(inputBytes);


        // step 2, convert byte array to hex string

        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < hash.Length; i++)

        {

            sb.Append(hash[i].ToString(“X2”));

        }

        return sb.ToString();

    }
}

