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
        string gakunum;
        public void getGakunum(string gakuseki) {
            gakunum = gakuseki;
        }
        int ID = 0;
        public MainWindow(int id)
        {
            InitializeComponent();
            Window_Lorded();
            ID = id;
        }

        //新規追加ボタンイベントハンドラ
        private void Sinkituika_Click(object sender, RoutedEventArgs e)
        {
            NewAdd na = new NewAdd(ID);
            na.Getgakusekinum(gakunum);
            na.ShowDialog();
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

            //生徒テーブル読込
            SyukatuInfomationManegementApp.RecruitManagementDataBaseDataSetTableAdapters.StudentTableTableAdapter StudentTableAdapter
                = new SyukatuInfomationManegementApp.RecruitManagementDataBaseDataSetTableAdapters.StudentTableTableAdapter();
            StudentTableAdapter.Fill(recruitManagDBDataSet.StudentTable);

            var StudNum = recruitManagDBDataSet.StudentTable.Where
                (d => d.StudentNumber.ToString().Contains(gakunum)).ToList();

            var StudName = recruitManagDBDataSet.StudentTable.Where
                (d => d.StudentName.ToString().Contains(gakunum)).ToList();

            

        }

        private void Itiran_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }

        private void Edit_Click(object sender, RoutedEventArgs e) {
            DataRowView drv = (DataRowView)recruitViewSource.View.CurrentItem;
        }
    }
}
