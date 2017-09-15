using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace Sepidrah.UWP.Classes
{
    class AvatarHelper
    {
        public static BitmapImage GenerateAvatar(string email = null)
        {
            if (email == null)
            {
                try
                {
                    var em = Windows.Storage.ApplicationData.Current.LocalSettings.Values["email"].ToString();
                    var hash = CalculateMD5Hash(em);
                    return new BitmapImage(new Uri($"https://www.gravatar.com/avatar/{hash}", UriKind.RelativeOrAbsolute));
                }
                catch (Exception ex)
                {
                    return new BitmapImage(new Uri($"ms-appx:///Assets/LockScreenLogo.scale-200.png", UriKind.RelativeOrAbsolute));
                }
            }
            else
            {

                var em = email;
                var hash = CalculateMD5Hash(em);
                return new BitmapImage(new Uri($"https://www.gravatar.com/avatar/{hash}", UriKind.RelativeOrAbsolute));
            }
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

                sb.Append(hash[i].ToString("X2"));

            }

            return sb.ToString();

        }
    }
}
