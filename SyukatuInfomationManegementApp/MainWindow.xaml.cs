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

namespace SyukatuInfomationManegementApp
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //新規追加ボタンイベントハンドラ
        private void Sinkituika_Click(object sender, RoutedEventArgs e)
        {
            NewAddShow();
        }

        private static void NewAddShow()
        {
            NewAdd newAdd = new NewAdd(); //新規追加画面インスタンス作成
            newAdd.Show(); //表示
        }

        private void Rogout_Click(object sender, RoutedEventArgs e)
        {
            Loginshow();
        }

        private static void Loginshow()
        {
            Login login = new Login();
            login.Show();
        }
    }
}
