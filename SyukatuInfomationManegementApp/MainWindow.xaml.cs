using System;
using System.Collections.Generic;
using System.Data;
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

        SyukatuInfomationManegementApp.RecruitManagementDataBaseDataSet recruitManagDBDataSet;
        System.Windows.Data.CollectionViewSource recruitViewSource;

        public MainWindow()
        {
            InitializeComponent();
            Window_Lorded();
        }

        //新規追加ボタンイベントハンドラ
        private void Sinkituika_Click(object sender, RoutedEventArgs e)
        {
            NewAddShow();
        }

        private static void NewAddShow()
        {
            NewAdd newAdd = new NewAdd(); //新規追加画面インスタンス作成
            newAdd.ShowDialog(); //表示
        }

        private void Rogout_Click(object sender, RoutedEventArgs e)
        {
            Loginshow();
            this.Close();
        }

        private static void Loginshow()
        {
            Login login = new Login();
            login.Show();
        }

        private void Window_Lorded() {
            recruitManagDBDataSet = ((SyukatuInfomationManegementApp.RecruitManagementDataBaseDataSet)
                (this.FindResource("recruitManagDBDataSet")));

            // テーブル 商品 にデータを読み込みます。必要に応じてこのコードを変更できます。
            SyukatuInfomationManegementApp.RecruitManagementDataBaseDataSetTableAdapters.RecruitTableTableAdapter recruitManagDataSetRecruitTableTableAdapter
                = new SyukatuInfomationManegementApp.RecruitManagementDataBaseDataSetTableAdapters.RecruitTableTableAdapter();
            recruitManagDataSetRecruitTableTableAdapter.Fill(recruitManagDBDataSet.RecruitTable);

            System.Windows.Data.CollectionViewSource RecruitViewSource
                = ((System.Windows.Data.CollectionViewSource)(this.FindResource("RecruitViewSource")));
            RecruitViewSource.View.MoveCurrentToFirst();

        }

        private void Itiran_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }

        private void Edit_Click(object sender, RoutedEventArgs e) {
            DataRowView drv = (DataRowView)recruitViewSource.View.CurrentItem;
        }
    }
}
