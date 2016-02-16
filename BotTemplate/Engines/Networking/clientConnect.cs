using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Net;
using System.Threading;
using BotTemplate.Interact;

namespace BotTemplate.Engines.Networking
{
    internal static class clientConnect
    {
        private static bool run = false;
        private static Thread connectThr;
        internal static void Start()
        {
            connectThr = new Thread(socketThread);
            connectThr.IsBackground = true;
            run = true;
            curAction = "";
            connectThr.Start();
        }

        internal static Objects.Location requestCoords()
        {
            tmpCoords.x = 0;
            tmpCoords.y = 0;
            tmpCoords.z = 0;
            curAction = "coords";
            buildConn = true;
            while (buildConn) ;
            return tmpCoords;
        }
        private static Objects.Location tmpCoords = new Objects.Location(0, 0, 0);

        internal static void Stop()
        {
            run = false;
        }

        internal static void requestWait()
        {
            curAction = "wait";
            buildConn = true;
        }

        internal static void requestResume()
        {
            curAction = "resume";
            buildConn = true;
        }

        private static string lastResponse = "";
        private static bool buildConn = false;
        private static string curAction = "";
        private static int count = 0;
        private static void socketThread()
        {
            while (run)
            {
                try
                {
                    if (buildConn && curAction != lastResponse)
                    {
                        TcpClient client = new TcpClient();
                        client.Connect(new IPEndPoint(new IPAddress(new byte[] { 127, 0, 0, 1 }), Data.Port));
                        Stream s = client.GetStream();
                        StreamWriter sw = new StreamWriter(s);
                        StreamReader sr = new StreamReader(s);
                        sw.AutoFlush = true;

                        if (curAction == "coords")
                        {
                            sw.WriteLine(curAction);

                        }
                        else
                        {
                            sw.WriteLine(count + "|" + ObjectManager.playerGuid + "|" + curAction);
                            count++;
                        }
                        int timeOut = Environment.TickCount;
                        while (sr.Peek() == 0 && Environment.TickCount - timeOut <= 500) ;
                        lastResponse = sr.ReadLine();

                        if (curAction != "coords")
                        {
                            File.AppendAllText(@".\\socketLog.txt", DateTime.Now.ToString("HH:mm:ss") + " | My state: " + lastResponse + Environment.NewLine);
                        }
                        else
                        {
                            string[] tmp = lastResponse.Split('|');
                            tmpCoords.x = Convert.ToSingle(tmp[0]); 
                            tmpCoords.y = Convert.ToSingle(tmp[1]);
                            tmpCoords.z = Convert.ToSingle(tmp[2]);
                            File.AppendAllText(@".\\socketLog.txt", DateTime.Now.ToString("HH:mm:ss") + " | Leader Coordinates: " + lastResponse + Environment.NewLine);
                        }
                        sw.Close();
                        sw.Dispose();
                        sr.Close();
                        sr.Dispose();
                        s.Close();
                        s.Dispose();
                        client.Close();
                        buildConn = false;
                    }
                }
                catch (Exception e)
                {
                    buildConn = false;
                    File.AppendAllText(@".\\socketLog.txt", DateTime.Now.ToString("HH:mm:ss") + " | Exception: " + e.ToString() + Environment.NewLine);
                    lastResponse = "invalid";
                }

                Thread.Sleep(100);
            }
        }
    }
}
