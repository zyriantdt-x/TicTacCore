using System;
using System.IO;
using System.Xml;
using TicTacToeServer;
using TicTacToeServer.Core;

namespace TicTacCore
{
    class Program
    {
        private static bool IsRunning = true;
        static void Main(string[] args)
        {
            XmlDocument log4netConfig = new XmlDocument();
            log4netConfig.Load(File.OpenRead("log4net.config"));
            log4net.Config.XmlConfigurator.Configure(log4netConfig["log4net"]);

            GameEnvironment.Initialise();
            while (IsRunning)
            {
                #region Console Commands
                if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                    Console.Write("TTT > ");
                    ConsoleCommandHandler.Invoke(Console.ReadLine());
                }
                #endregion
            }
        }
    }
}
