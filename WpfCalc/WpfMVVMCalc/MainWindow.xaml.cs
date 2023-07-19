using System.Windows;

namespace WpfMVVMCalc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //数据上下文，供数据绑定使用
            this.DataContext = new MainWindowViewModel();
        }
    }
}
