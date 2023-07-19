using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfMVVMCalc
{
    /// <summary>
    /// 命令的基础类
    /// </summary>
    public class ReplayCommand : ICommand
    {
        /// <summary>
        /// 事件
        /// </summary>
        public event EventHandler? CanExecuteChanged;

        /// <summary>
        /// 是否可以执行的属性
        /// </summary>
        public Func<object, bool>? CanExecution
        {
            set; 
            get;
        }

        /// <summary>
        /// 事件触发执行的委托
        /// </summary>
        public Action<object>? DoExecute
        {
            set;
            get;
        }

        /// <summary>
        /// 检查当前的事件是否执行
        /// 如果绑定事件对应的界面控件IsEnable=false时，事件将不执行
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool CanExecute(object? parameter)
        {
            if (CanExecution != null)
            {
                CanExecute(parameter);
            }

            return true;
        }

        /// <summary>
        /// 具体执行的事件
        /// </summary>
        /// <param name="parameter"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Execute(object? parameter)
        {
            DoExecute!.Invoke(parameter!);
        }
    }
}
