using Microsoft.Owin;
using Microsoft.Owin.Extensions;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwinSocket.Config
{
    public class OwinPipelineRegister
    {
        public static void DoSomethingWhenPipelineStageChange(IOwinContext context, string stage)
        {
#if DEBUG
            ////测试时固定用户
            //if (stage.Equals("Authenticate"))
            //{
            //    var userManager = context.GetUserManager<AppUserManager>();
            //    var user = userManager.FindByName("1");
            //    var identity = userManager.GenerateUserIdentityAsync(user, OAuthDefaults.AuthenticationType).Result;
            //    context.Authentication.User = new ClaimsPrincipal(identity);
            //}
#endif
        }

        #region 注册管道事件

        public static void Register(IAppBuilder app)
        {
            app.Use((context, next) =>
            {
                DoSomethingWhenPipelineStageChange(context, "Start");
                return next.Invoke();
            });


            app.Use((context, next) =>
            {
                DoSomethingWhenPipelineStageChange(context, "Authenticate");
                return next.Invoke();
            });
            app.UseStageMarker(PipelineStage.Authenticate);


            app.Use((context, next) =>
            {
                DoSomethingWhenPipelineStageChange(context, "PostAuthenticate");
                return next.Invoke();
            });
            app.UseStageMarker(PipelineStage.PostAuthenticate);


            app.Use((context, next) =>
            {
                DoSomethingWhenPipelineStageChange(context, "Authorize");
                return next.Invoke();
            });
            app.UseStageMarker(PipelineStage.Authorize);


            app.Use((context, next) =>
            {
                DoSomethingWhenPipelineStageChange(context, "PostAuthorize");
                return next.Invoke();
            });
            app.UseStageMarker(PipelineStage.PostAuthorize);


            app.Use((context, next) =>
            {
                DoSomethingWhenPipelineStageChange(context, "ResolveCache");
                return next.Invoke();
            });
            app.UseStageMarker(PipelineStage.ResolveCache);


            app.Use((context, next) =>
            {
                DoSomethingWhenPipelineStageChange(context, "PostResolveCache");
                return next.Invoke();
            });
            app.UseStageMarker(PipelineStage.PostResolveCache);


            app.Use((context, next) =>
            {
                DoSomethingWhenPipelineStageChange(context, "MapHandler");
                return next.Invoke();
            });
            app.UseStageMarker(PipelineStage.MapHandler);


            app.Use((context, next) =>
            {
                DoSomethingWhenPipelineStageChange(context, "PostMapHandler");
                return next.Invoke();
            });
            app.UseStageMarker(PipelineStage.PostMapHandler);


            app.Use((context, next) =>
            {
                DoSomethingWhenPipelineStageChange(context, "AcquireState");
                return next.Invoke();
            });
            app.UseStageMarker(PipelineStage.AcquireState);


            app.Use((context, next) =>
            {
                DoSomethingWhenPipelineStageChange(context, "PostAcquireState");
                return next.Invoke();
            });
            app.UseStageMarker(PipelineStage.PostAcquireState);


            app.Use((context, next) =>
            {
                DoSomethingWhenPipelineStageChange(context, "PreHandlerExecute");
                return next.Invoke();
            });
            app.UseStageMarker(PipelineStage.PreHandlerExecute);


            app.Use((context, next) =>
            {
                DoSomethingWhenPipelineStageChange(context, "End");
                return next.Invoke();
            });
        }

        #endregion


    }
}
