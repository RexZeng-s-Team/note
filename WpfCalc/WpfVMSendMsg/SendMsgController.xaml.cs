using System.Windows.Controls;

namespace WpfVMSendMsg
{
    /// <summary>
    /// SendMsgController.xaml 的交互逻辑
    /// </summary>
    public partial class SendMsgController : UserControl
    {
        public SendMsgController()
        {
            InitializeComponent();
            this.DataContext = new SendMsgControllerViewModel();
        }
    }
}
