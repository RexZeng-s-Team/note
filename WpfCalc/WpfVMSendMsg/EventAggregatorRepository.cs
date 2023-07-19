using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfVMSendMsg
{
    /// <summary>
    /// 事件聚合器的仓库，用于生成聚合器
    /// </summary>
    public class EventAggregatorRepository
    {
        /// <summary>
        /// 聚合器实例
        /// </summary>
        public EventAggregator eventAggregator;
        /// <summary>
        /// 唯一实例
        /// </summary>
        private static EventAggregatorRepository? eventAggregatorRepository;
        /// <summary>
        /// 线程锁
        /// </summary>
        private static object _lock = new object();
        /// <summary>
        /// 构造方法
        /// </summary>
        public EventAggregatorRepository()
        {
            eventAggregator = new EventAggregator();
        }
        /// <summary>
        /// 单例获取对象实例
        /// </summary>
        /// <returns></returns>
        public static EventAggregatorRepository GetInstance()
        {
            if (eventAggregatorRepository == null)
            {
                lock(_lock)
                {
                    if (eventAggregatorRepository == null)
                    {
                        eventAggregatorRepository = new EventAggregatorRepository();
                    }
                }
            }
            return eventAggregatorRepository;
        }
    }
}
