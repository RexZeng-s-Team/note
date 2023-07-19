using System.Windows;

namespace WpfCalc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.txtResult.Text = int.Parse(this.txtNumber1.Text) + int.Parse(this.txtNumber2.Text) + "";
        }
    }
}
