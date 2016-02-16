using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace BotTemplate.Engines.Networking
{
    class socketAcceptClass
    {
        static object conLock = new object();
        static int connections
        {
            get
            {
                lock (conLock)
                {
                    return c;
                }
            }
            set
            {
                lock (conLock)
                {
                    c = value;
                }
            }
        }
        static int c = 1;

        TcpClient client;
        Stream s;
        StreamReader sr;
        StreamWriter sw;
        internal void start(TcpListener listen, int count)
        {
            try
            {
                File.AppendAllText(@".\\socketLog.txt", DateTime.Now.ToString("HH:mm:ss") + " | Listener: Waiting for a connection" + Environment.NewLine);
                client = listen.AcceptTcpClient();
                File.AppendAllText(@".\\socketLog.txt", DateTime.Now.ToString("HH:mm:ss") + " | Listener: Got a connection" + Environment.NewLine);
                s = client.GetStream();
                sr = new StreamReader(s);
                sw = new StreamWriter(s);
                sw.AutoFlush = true;

                string res = slaveStates.process(sr.ReadLine());
                sw.WriteLine(res);
                socketAcceptClass.connections++;

                sr.Close();
                sr.Dispose();
                s.Close();
                s.Dispose();
                client.Close();
            }
            catch (Exception e) 
            {
                File.AppendAllText(@".\\socketLog.txt", DateTime.Now.ToString("HH:mm:ss") + " | Listener Exception: " + e.ToString() + Environment.NewLine);
            }
        }

        internal void stop()
        {
            try
            {
                sr.Close();
                sr.Dispose();
                s.Close();
                s.Dispose();
                client.Close();
            }
            catch { }
        }
    }
}
