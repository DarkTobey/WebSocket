using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwinSocket.Utils
{
    public class MessageBus
    {
        private static Dictionary<string, List<object>> dic = new Dictionary<string, List<object>>();

        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="topic">主题</param>
        /// <param name="obj">对象</param>
        public static void Subscribe(string topic, object obj)
        {
            if (string.IsNullOrEmpty(topic)) return;
            lock (dic)
            {
                if (dic == null)
                    dic = new Dictionary<string, List<object>>();

                if (!dic.ContainsKey(topic))
                    dic[topic] = new List<object>();

                dic[topic].Add(obj);
            }
        }

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="obj">对象</param>
        public static void UnSubscribe(object obj)
        {
            lock (dic)
            {
                if (dic == null) return;
                List<string> removeTopic = new List<string>();
                foreach (var item in dic)
                {
                    item.Value.Remove(obj);
                    if (item.Value.Count == 0)
                        removeTopic.Add(item.Key);
                }
                foreach (var topic in removeTopic)
                {
                    dic.Remove(topic);
                }
            }
        }

        /// <summary>
        /// 推送数据
        /// </summary>
        /// <param name="topic">主题</param>
        /// <param name="act">动作</param>
        public static void Publish(string topic, Action<object> act)
        {
            if (string.IsNullOrEmpty(topic)) return;
            lock (dic)
            {
                if (dic == null) return;
                if (!dic.ContainsKey(topic)) return;
                var list = dic[topic];
                foreach (var i in list)
                {
                    act(i);
                }
            }
        }
    }
}
