
namespace UWPCore
{
    using System;
    using System.Runtime.InteropServices.WindowsRuntime;
    using System.Threading.Tasks;
    using Windows.Storage.Streams;
    using Windows.UI.Xaml.Media.Imaging;

    public static class ImageTools
    {
        #region  Public Static Methods

        public static async Task<BitmapImage> Base64ToBitmapImage(string base64String)
        {
            var fileBytes = Convert.FromBase64String(base64String);

            BitmapImage image;

            using (var memoryRandom = new InMemoryRandomAccessStream())
            {
                using (var dataWriter = new DataWriter(memoryRandom))
                {
                    dataWriter.WriteBytes(fileBytes);
                    await dataWriter.StoreAsync();
                    image = new BitmapImage();
                    memoryRandom.Seek(0);
                    image.SetSource(memoryRandom);
                }
            }
            return image;
        }

        public static string BufferToStringBase64(IBuffer buffer)
        {
            var string64 = Convert.ToBase64String(buffer.ToArray());
            return string64;
        }

        public static string BytesToStringBase64(byte[] array)
        {
            var string64 = Convert.ToBase64String(array);
            return string64;
        }

        #endregion
    }
}
