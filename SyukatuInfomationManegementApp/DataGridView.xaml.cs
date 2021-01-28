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
using System.Windows.Shapes;

namespace SyukatuInfomationManegementApp
{
    /// <summary>
    /// DataGridView.xaml の相互作用ロジック
    /// </summary>
    public partial class DataGridView : Window
    {
        public DataGridView()
        {
            InitializeComponent();
        }

        private void Return_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e) {
            
        }




        private void EmployeeName_TextChanged(object sender, TextChangedEventArgs e) {
            
        }
    }
}
