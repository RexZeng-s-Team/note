using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfVMSendMsg
{
    public class SendMsgControllerViewModel : ViewModelBase
    {
        public ReplayCommand<string> SendMsgCommand
        {
            get;
            set;
        }
        public SendMsgControllerViewModel()
        {
            SendMsgCommand = new ReplayCommand<string>(SendMsg);
        }

        private void SendMsg(string msg)
        {
            //发送消息
            EventAggregatorRepository.GetInstance().eventAggregator.Send<string>("hello");
        }
    }
}
