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

namespace SyukatuInfomationManegementApp {
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window {

        SyukatuInfomationManegementApp.RecruitManagementDataBaseDataSet recruitManagDBDataSet;
        System.Windows.Data.CollectionViewSource recruitViewSource;

        //Login login = new Login();

        public string ID { get; set; }
        public string stdname { get; set; }

        public void GetID(int id, string stdname) {
            //学籍番号表示
            tbStudentNumber.Text = id.ToString();
            //生徒名表示
            tbStudentName.Text = stdname;
        }

        public MainWindow() {
            InitializeComponent();

            recruitManagDBDataSet = ((SyukatuInfomationManegementApp.RecruitManagementDataBaseDataSet)
                (this.FindResource("recruitManagDBDataSet")));

            //生徒テーブル読込
            SyukatuInfomationManegementApp.RecruitManagementDataBaseDataSetTableAdapters.StudentTableTableAdapter StudentTableAdapter
                = new SyukatuInfomationManegementApp.RecruitManagementDataBaseDataSetTableAdapters.StudentTableTableAdapter();
            StudentTableAdapter.Fill(recruitManagDBDataSet.StudentTable);

            Window_Lorded();
        }

        //新規追加ボタンイベントハンドラ
        private void Sinkituika_Click(object sender, RoutedEventArgs e) {
            NewAdd newAdd = new NewAdd();

            newAdd.ID = int.Parse(tbStudentNumber.Text);

            newAdd.ShowDialog();
        }

        private void Rogout_Click(object sender, RoutedEventArgs e) {
            Loginshow();
            this.Close();
        }

        private static void Loginshow() {
            Login login = new Login();
            login.Show();
        }

        private void Window_Lorded() {

            //recruitManagDBDataSet = ((SyukatuInfomationManegementApp.RecruitManagementDataBaseDataSet)
            //    (this.FindResource("recruitManagDBDataSet")));

            ////リクルートテーブル読込
            //SyukatuInfomationManegementApp.RecruitManagementDataBaseDataSetTableAdapters.RecruitTableTableAdapter RecruitTableAdapter
            //    = new SyukatuInfomationManegementApp.RecruitManagementDataBaseDataSetTableAdapters.RecruitTableTableAdapter();

            //RecruitTableAdapter.Fill(recruitManagDBDataSet.RecruitTable);


        }


        //編集ボタン
        private void Edit_Click(object sender, RoutedEventArgs e) {
            DataGridView view = new DataGridView();
            try {
                //企業情報の取得
                var drv = (DataRow)dgItiran.SelectedItem;

                //企業情報の受け渡し
                view.tbEmployeeName.Text = drv[1].ToString();
                view.tbPlace.Text = drv[2].ToString();
                view.tbType.Text = drv[3].ToString();

                //データの格納
                view.RctID = drv[0].ToString();
                view.RctDate = drv[4].ToString();
                view.StdNum = tbStudentNumber.Text;
                view.LimitDate = drv[11].ToString();
                view.EValu = drv[10].ToString();

                view.tbContext.Text = drv[6].ToString();

                view.ShowDialog();
            }
            catch (Exception ex) {
                MessageBox.Show("表示に失敗しました。" + "\n" + ex.Message);
            }


        }

        //接続ボタン
        private void Connect_Click(object sender, RoutedEventArgs e) {
            recruitManagDBDataSet = ((SyukatuInfomationManegementApp.RecruitManagementDataBaseDataSet)
                (this.FindResource("recruitManagDBDataSet")));

            //リクルートテーブル読込
            SyukatuInfomationManegementApp.RecruitManagementDataBaseDataSetTableAdapters.RecruitTableTableAdapter RecruitTableAdapter
                = new SyukatuInfomationManegementApp.RecruitManagementDataBaseDataSetTableAdapters.RecruitTableTableAdapter();
            RecruitTableAdapter.Fill(recruitManagDBDataSet.RecruitTable);

            var datacont = recruitManagDBDataSet.RecruitTable.Where(
                d => d.StudenNumber.ToString().Contains(tbStudentNumber.Text)).ToArray();

            var datacm = recruitManagDBDataSet.RecruitTable.Where(
                d => d.StudenNumber.ToString().Contains(tbStudentNumber.Text)).Select(s=>s.EmployeeName).Distinct().ToArray();

            foreach (var cmitem in datacm) {
                cbSort.Items.Add(cmitem.ToString());
            }

            dgItiran.DataContext = datacont;

            Connect.IsEnabled = false;
            Search.IsEnabled = true;

        }

        //絞り込み
        private void dgSort() {
            //リクルートテーブル読込
            SyukatuInfomationManegementApp.RecruitManagementDataBaseDataSetTableAdapters.RecruitTableTableAdapter RecruitTableAdapter
                = new SyukatuInfomationManegementApp.RecruitManagementDataBaseDataSetTableAdapters.RecruitTableTableAdapter();
            RecruitTableAdapter.Fill(recruitManagDBDataSet.RecruitTable);

            var StdSort = recruitManagDBDataSet.RecruitTable.Where(
                d => d.StudenNumber.ToString().Contains(tbStudentNumber.Text)).ToArray();

            if (cbBriefing.IsChecked == true) {
                var datacont = StdSort.Where(
                d => d.Type.ToString().Contains("説明会")).ToArray();
                dgItiran.DataContext = datacont;
            }
            else if (cbGousetu.IsChecked == true) {
                var datacont = StdSort.Where(
                d => d.Type.ToString().Contains("合同企業説明会")).ToArray();
                dgItiran.DataContext = datacont;
            }
            else if (cbMensetu.IsChecked == true) {
                var datacont = StdSort.Where(
                d => d.Type.ToString().Contains("面接")).ToArray();
                dgItiran.DataContext = datacont;
            }
            else if (cbTest.IsChecked == true) {
                var datacont = StdSort.Where(
                d => d.Type.ToString().Contains("筆記")).ToArray();
                dgItiran.DataContext = datacont;
            }
            else if (cbOther.IsChecked == true) {
                var datacont = StdSort.Where(
                d => d.Type.ToString().Contains("内定後")).ToArray();
                dgItiran.DataContext = datacont;
            }


        }

        //説明会チェックボックスがチェックされている
        private void cbBriefing_Checked(object sender, RoutedEventArgs e) {
            dgSort();
        }

        //合同企業説明会チェックボックスがチェックされている
        private void cbGousetu_Checked(object sender, RoutedEventArgs e) {
            dgSort();
        }

        //面接チェックボックスがチェックされている
        private void cbMensetu_Checked(object sender, RoutedEventArgs e) {
            dgSort();
        }

        //筆記チェックボックスがチェックされている
        private void cbTest_Checked(object sender, RoutedEventArgs e) {
            dgSort();
        }

        //内定後チェックボックスがチェックされている
        private void cbOther_Checked(object sender, RoutedEventArgs e) {
            dgSort();
        }

        //検索ボタン
        private void Search_Click(object sender, RoutedEventArgs e) {
            recruitManagDBDataSet = ((SyukatuInfomationManegementApp.RecruitManagementDataBaseDataSet)
                (this.FindResource("recruitManagDBDataSet")));

            //リクルートテーブル読込
            SyukatuInfomationManegementApp.RecruitManagementDataBaseDataSetTableAdapters.RecruitTableTableAdapter RecruitTableAdapter
                = new SyukatuInfomationManegementApp.RecruitManagementDataBaseDataSetTableAdapters.RecruitTableTableAdapter();
            RecruitTableAdapter.Fill(recruitManagDBDataSet.RecruitTable);

            if (cbSort.Text != "絞り込みなし") {
                var cbEmpdata = recruitManagDBDataSet.RecruitTable.Where(
                d => d.EmployeeName.ToString().Contains(cbSort.Text)).ToArray();

                dgItiran.DataContext = cbEmpdata.Where(
                    d => d.StudenNumber.ToString().Contains(tbStudentNumber.Text));
            }
            else {
                var sortWord = "";

                var cbEmpdata = recruitManagDBDataSet.RecruitTable.Where(
                d => d.EmployeeName.ToString().Contains(sortWord)).ToArray();

                dgItiran.DataContext = cbEmpdata.Where(
                    d => d.StudenNumber.ToString().Contains(tbStudentNumber.Text));
            }



        }
    }
}
