using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMVVMCalc
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private int _number1 = 12;

        public int Number1
        {
            get { return _number1; }
            set { _number1 = value; }
        }

        private int _number2 = 11;

        public int Number2
        {
            get { return _number2; }
            set { _number2 = value; }
        }

        private int _result = 23;

        /// <summary>
        /// 实现INotifyPropertyChanged接口，当绑定属性值发生变化时通知界面
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public int Result
        {
            get { return _result; }
            set { _result = value; OnPropertyChanged(nameof(Result)); }
        }

        public ReplayCommand GetResult
        {
            get => new()
            {
                DoExecute = new Action<object>(CalcSum)
            };
        }

        private void CalcSum(object obj)
        {
            this.Result = _number1 + _number2;
        }
    }
}
