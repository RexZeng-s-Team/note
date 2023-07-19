using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfVMSendMsg
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _recieveMsg = string.Empty;

        public string RecieveMsg
        {
            get { return _recieveMsg; }
            set { _recieveMsg = value; RasiePropertyChanged(() => RecieveMsg); }
        }

        public MainWindowViewModel()
        {
            //注册消息事件，接收消息
            EventAggregatorRepository.GetInstance().eventAggregator.Register<string>(ShowData);
        }

        private void ShowData(object obj)
        {
            string msg = obj.ToString();
            RecieveMsg = msg;
            MessageBox.Show(obj.ToString());
        }
    }
}
