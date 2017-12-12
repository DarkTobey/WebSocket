using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.WebSocket;
using SuperSocket.SocketBase.Config;

namespace OwinSocket
{
    public class OwinSocketServer
    {
        private const string _HeartBeat = "HEART_BEAT";
        private const string _Close = "SOCKET_CLOSE";

        public void Start(ServerConfig config)
        {
            WebSocketServer ws = new WebSocketServer();
            ws.NewSessionConnected += Ws_SessionConnected;  //当有用户连入时
            ws.NewMessageReceived += Ws_MessageReceived;    //当有信息传入时
            ws.NewDataReceived += Ws_DataReceived;          //当有数据传入时
            ws.SessionClosed += Ws_SessionClosed;           //当有用户退出时
            if (ws.Setup(config)) ws.Start();               //绑定端口并启动服务

            Console.WriteLine($"\n WebSocket 已启动 正在监听 {config.Ip}:{config.Port}  输入quit退出\n");
            while (true)
            {
                if (Console.ReadLine().Equals("quit")) break;
            }
        }

        private void Ws_SessionConnected(WebSocketSession session)
        {
            session.Send($"{{\"msg\":\"welcome\"}}");
        }

        private void Ws_MessageReceived(WebSocketSession session, string value)
        {
            if (value.Equals(_HeartBeat))
            {
                session.Send($"{{\"{_HeartBeat}\":\".\"}}");
            }
            else if (value.Equals(_Close))
            {
                Utils.MessageBus.UnSubscribe(session);
            }
            else
            {
                Utils.MessageBus.Subscribe($"{value}", session);
            }
        }

        private void Ws_DataReceived(WebSocketSession session, byte[] value)
        {
        }

        private void Ws_SessionClosed(WebSocketSession session, SuperSocket.SocketBase.CloseReason value)
        {
            Utils.MessageBus.UnSubscribe(session);
        }

        private string RouterAnalyse(WebSocketSession session)
        {
            return $"{session.Origin}{session.Path}";
        }
    }
}
