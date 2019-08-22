using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ReviewWPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
        }

        private void TreeNode_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.DataContext != null)
                (this.DataContext as MainViewModel).ClickNode(sender, e);
        }

        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        private void Storyboard_CurrentTimeInvalidated(object sender, EventArgs e)
        {
            Clock storyboardClock = (Clock)sender;
            if(storyboardClock.CurrentProgress == null)
            {
                lblTime.Content = "";
                progressBar1.Value = 0;
            }
            else
            {
                lblTime.Content = storyboardClock.CurrentTime.ToString();
                progressBar1.Value = (double)storyboardClock.CurrentProgress * 100;
            }
        }
    }
}
