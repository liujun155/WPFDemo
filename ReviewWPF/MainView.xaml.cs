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
    public partial class MainView : Window
    {
        public MainView()
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

        /// <summary>
        /// 删除行后序号变更
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridRowEventArgs"/> instance containing the event data.</param>
        /// 创建人:刘俊  创建时间:2019/8/23 10:12
        private void HumanDG_UnloadingRow(object sender, DataGridRowEventArgs e)
        {
            DataGrid_LoadingRow(sender, e);
            if (humanDG.Items != null)
            {
                for (int i = 0; i < humanDG.Items.Count; i++)
                {
                    try
                    {
                        DataGridRow row = humanDG.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                        if (row != null)
                        {
                            row.Header = (i + 1).ToString();
                        }
                    }
                    catch { }
                }
            }
        }
    }
}
