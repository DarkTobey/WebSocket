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
        private string _BaseAddress;

        public OwinServer(string url)
        {
            _BaseAddress = url;
        }

        public void Start()
        {
            var startOpts = new StartOptions(_BaseAddress)
            {
                ServerFactory = "Microsoft.Owin.Host.HttpListener",
            };
            WebApp.Start<OwinServer>(startOpts);


            Console.WriteLine($"\n OwinServer 已启动 正在监听 {_BaseAddress} \n");
            while (true)
            {
                if (Console.ReadLine().Equals("quit")) break;
            }
        }

        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            // 配置WebApi
            WebApiConfig.Register(config);

            // 注册一些自定义的管道事件
            OwinPipelineRegister.Register(app);

            // 路由，这里可以引用MVC,利用MVC的路由解析模块和动态页面渲染模块生成返回流
            app.Run(context =>
            {
                context.Response.ContentType = "text/html; charset=utf-8";
                return context.Response.WriteAsync("服务已经启动 <br/> 当前路由是：" + context.Request.Uri);
            });

            // 注册WebApi 一定要最后才注册
            app.UseWebApi(config);
        }
    }
}
