using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfVMSendMsg
{
    /// <summary>
    /// 事件聚合器
    /// </summary>
    public class EventAggregator
    {
        //添加一个线程安全字典
        public static ConcurrentDictionary<Type, List<Action<object>>> _handles = new ConcurrentDictionary<Type, List<Action<object>>>();
        /// <summary>
        /// 添加注册方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        public void Register<T>(Action<object> action)
        {
            if (!_handles.ContainsKey(typeof(T)))
            {
                _handles[typeof(T)] = new List<Action<object>>();
            }
            _handles[typeof(T)].Add(action);
        }
        /// <summary>
        /// 添加一个发送消息的方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public void Send<T>(object obj)
        {
            if (_handles.ContainsKey(typeof(T)))
            {
                foreach (var action in _handles[typeof(T)])
                {
                    action!.Invoke(obj);
                }
            }
        }
    }
}
