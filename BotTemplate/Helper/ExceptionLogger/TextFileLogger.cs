using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Utilities
{
    /// <summary>
    /// This logger will log to a text file called 'BugReport.txt'
    /// </summary>
    public class TextFileLogger : LoggerImplementation
    {
        /// <summary>Logs the specified error.</summary>
        /// <param name="error">The error to log.</param>
        public override void LogError(string error)
        {
            string filename = Path.GetDirectoryName(Application.ExecutablePath);
            filename += "\\BugReport.txt";

            List<string> data = new List<string>();

            lock (this)
            {
                if (File.Exists(filename))
                {
                    using (StreamReader reader = new StreamReader(filename))
                    {
                        string line = null;
                        do
                        {
                            line = reader.ReadLine();
                            data.Add(line);
                        }
                        while (line != null);
                    }
                }

                // truncate the file if it's too long
                int writeStart = 0;
                if (data.Count > 500)
                    writeStart = data.Count - 500;

                using (StreamWriter stream = new StreamWriter(filename, false))
                {
                    for (int i = writeStart; i < data.Count; i++)
                    {
                        stream.WriteLine(data[i]);
                    }

                    stream.Write(error);
                }
            }
        }
    }
}
