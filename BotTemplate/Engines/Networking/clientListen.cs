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
    internal static class clientListen
    {
        private static Thread listenerThr;
        internal static void start()
        {
            run = true;
            listenerThr = new Thread(listenerThread);
            listenerThr.IsBackground = true;
            listenerThr.Start();
        }

        internal static void stop()
        {
            run = false;
        }

        private static TcpListener listen;

        private static List<socketAcceptClass> t = new List<socketAcceptClass>();
        private static List<socketAcceptClass> threads
        {
            get
            {
                lock (threadsLock)
                {
                    return t;
                }
            }
            set
            {
                lock (threadsLock)
                {
                    t = value;
                }
            }
        }
        private static object threadsLock = new object();

        private static int lastTimeStamp = 0;
        private static int g;
        internal static int groupCount
        {
            get
            {
                if (Environment.TickCount - lastTimeStamp > 5000)
                {
                    int tmp = 0;
                    if (ObjectManager.party1Guid != 0) tmp++;
                    if (ObjectManager.party2Guid != 0) tmp++;
                    if (ObjectManager.party3Guid != 0) tmp++;
                    if (ObjectManager.party4Guid != 0) tmp++;
                    lastTimeStamp = Environment.TickCount;
                    g = tmp;
                    return tmp;
                }
                else
                {
                    return g;
                }
            }
        }

        private static bool run = false;
        private static void listenerThread()
        {
            File.AppendAllText(@".\\socketLog.txt", DateTime.Now.ToString("HH:mm:ss") + " | Listening now" + Environment.NewLine);
            listen = new TcpListener(new IPAddress(new byte[] { 127, 0, 0, 1 }), Data.Port);
            listen.Start();
            while (run)
            {
                while (threads.Count < 1 && run)
                {
                    socketAcceptClass sc = new socketAcceptClass();
                    int count = 0;
                    Thread tmp = new Thread(() => acceptSocketThread(ref sc, count));
                    tmp.IsBackground = true;
                    threads.Add(sc);
                    count = threads.Count;
                    tmp.Start();
                }
                Thread.Sleep(50);
            }
            listen.Stop();
        }
    
        private static void acceptSocketThread(ref socketAcceptClass sc, int count)
        {
            try
            {
                sc.start(listen, count);
                threads.Remove(sc);
                sc = null;
            }
            catch { }

        }
    }
}
