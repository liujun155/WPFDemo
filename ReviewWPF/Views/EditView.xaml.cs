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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ReviewWPF
{
    /// <summary>
    /// EditView.xaml 的交互逻辑
    /// </summary>
    public partial class EditView : Window
    {
        public EditView()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.Title == "添加")
            {
                ageText.Text = "";
            }
        }

        private void AgeText_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tx = sender as TextBox;
            if (!string.IsNullOrEmpty(tx.Text))
            {
                int num;
                if (!int.TryParse(tx.Text, out num))
                {
                    MessageBox.Show("输入错误", "提示");
                    tx.Text = "";
                }
            }
            else
                tx.Text = null;
        }
    }
}
