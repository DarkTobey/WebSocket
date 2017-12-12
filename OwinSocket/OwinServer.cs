using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Owin;
using Microsoft.Owin.Hosting;
using OwinSocket.Config;

namespace OwinSocket
{
    public class OwinServer
    {
        private string BaseAddress;

        public OwinServer(string url)
        {
            this.BaseAddress = url;
        }

        public void Start()
        {
            var startOpts = new StartOptions(this.BaseAddress)
            {
                ServerFactory = "Microsoft.Owin.Host.HttpListener"
            };
            WebApp.Start<OwinServer>(startOpts);


            Console.WriteLine($"\n OwinServer 已经启动 正在监听 {this.BaseAddress} 输入quit退出 \n");
            while (true)
            {
                if (Console.ReadLine().Equals("quit")) break;
            }
        }

        public void Configuration(IAppBuilder app)
        {
            // 配置WebApi
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);

            // 注册 log4net
            log4net.Config.XmlConfigurator.Configure();

            // 注册管道事件对应的处理程序
            OwinPipelineRegister.Register(app);

            //JSON格式化
            config.Formatters.JsonFormatter.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            config.Formatters.JsonFormatter.SerializerSettings = new Newtonsoft.Json.JsonSerializerSettings()
            {
                DateFormatString = "yyyy-MM-dd HH:mm:ss",
            };

            // 注册WebApi 一定要最后才注册
            app.UseWebApi(config);
        }
    }
}
