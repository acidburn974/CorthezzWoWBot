using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace BotTemplate.Helper
{
    internal static class Tools
    {
        [DllImport("user32.dll")]
        internal static extern bool SetForegroundWindow(IntPtr hWnd);

        static public string EncodeTo64(string str)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(str));
        }

        static public string DecodeFrom64(string str)
        {
            return System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(str));
        }
        internal static byte[] ReadFile(string filePath)
        {
            byte[] buffer;
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            try
            {
                int length = (int)fileStream.Length;  // get file length
                buffer = new byte[length];            // create buffer
                int count;                            // actual number of bytes read
                int sum = 0;                          // total number of bytes read

                // read until Read method returns 0 (end of the stream has been reached)
                while ((count = fileStream.Read(buffer, sum, length - sum)) > 0)
                    sum += count;  // sum is a buffer offset for next reading
            }
            finally
            {
                fileStream.Close();
            }
            return buffer;
        }
    }

    public class ComboboxItem
    {
        public string Text { get; set; }
        public string mapId { get; set; }
        public string x { get; set; }
        public string y { get; set; }
        public string z { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
