using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace OwinSocket.Controller
{
    [RoutePrefix("api/demo")]
    public class ValuesController : ApiController
    {
        [HttpGet]
        [Route("login")]
        public string Login(string sign = "http://localhost?LoginSign=1234")
        {
            Utils.MessageBus.Publish(sign, x =>
            {
                (x as SuperSocket.WebSocket.WebSocketSession).Send("ok,login now!");
            });
            return "ok";
        }
    }
}
