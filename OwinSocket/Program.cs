using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwinSocket
{
    class Program
    {
        static void Main(string[] args)
        {
            AppStart();

            Task.Run(() =>
            {
                try
                {
                    string url = System.Configuration.ConfigurationManager.AppSettings["OwinServer:URL"];
                    OwinServer server = new OwinServer(url);
                    server.Start();
                }
                catch (Exception ex)
                {
                    Console.Write($"msg:{ex.Message} stack:{ex.StackTrace}");
                    throw ex;
                }
            });

            Task.Run(() =>
            {
                try
                {
                    OwinSocketServer ws = new OwinSocketServer();
                    ws.Start(new SuperSocket.SocketBase.Config.ServerConfig()
                    {
                        Ip = System.Configuration.ConfigurationManager.AppSettings["SuperSocketServer:HostIp"],
                        Port = int.Parse(System.Configuration.ConfigurationManager.AppSettings["SuperSocketServer:Port"]),
                        MaxConnectionNumber = 1000,
                    });
                }
                catch (Exception ex)
                {
                    Console.Write($"msg:{ex.Message} stack:{ex.StackTrace}");
                    throw ex;
                }
            });

            Console.ReadLine();
        }

        static void AppStart()
        {
            // 注册 log4net
            log4net.Config.XmlConfigurator.Configure();
        }
    }
}
