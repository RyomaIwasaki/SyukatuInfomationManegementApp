﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
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

        public Window login;

        public void GetID(int id, string stdname) {
            //学籍番号表示
            tbStudentNumber.Text = id.ToString();
            //生徒名表示
            tbStudentName.Text = stdname;
        }

        

        public MainWindow(Window login) {
            InitializeComponent();
            this.login = login;

            recruitManagDBDataSet = ((SyukatuInfomationManegementApp.RecruitManagementDataBaseDataSet)
                (this.FindResource("recruitManagDBDataSet")));

            //生徒テーブル読込
            SyukatuInfomationManegementApp.RecruitManagementDataBaseDataSetTableAdapters.StudentTableTableAdapter StudentTableAdapter
                = new SyukatuInfomationManegementApp.RecruitManagementDataBaseDataSetTableAdapters.StudentTableTableAdapter();
            StudentTableAdapter.Fill(recruitManagDBDataSet.StudentTable);

            //リクルートテーブル読込
            SyukatuInfomationManegementApp.RecruitManagementDataBaseDataSetTableAdapters.RecruitTableTableAdapter RecruitTableAdapter
                = new SyukatuInfomationManegementApp.RecruitManagementDataBaseDataSetTableAdapters.RecruitTableTableAdapter();
            RecruitTableAdapter.Fill(recruitManagDBDataSet.RecruitTable);

            
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

        private void Loginshow() {

            login.Visibility = Visibility.Visible; //再表示
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

        

        //絞り込み
        private void dgSort() {
            
            var StdSort = recruitManagDBDataSet.RecruitTable.Where(
                d => d.StudenNumber.ToString().Contains(tbStudentNumber.Text)).ToArray();

            if (rbBriefing.IsChecked == true) {
                var datacont = StdSort.Where(
                d => d.Type.ToString().Contains("説明会")).ToArray();
                dgItiran.DataContext = datacont;
            }
            else if (rbGousetu.IsChecked == true) {
                var datacont = StdSort.Where(
                d => d.Type.ToString().Contains("合同企業説明会")).ToArray();
                dgItiran.DataContext = datacont;
            }
            else if (rbMensetu.IsChecked == true) {
                var datacont = StdSort.Where(
                d => d.Type.ToString().Contains("面接")).ToArray();
                dgItiran.DataContext = datacont;
            }
            else if (rbTest.IsChecked == true) {
                var datacont = StdSort.Where(
                d => d.Type.ToString().Contains("筆記")).ToArray();
                dgItiran.DataContext = datacont;
            }
            else if (rbOther.IsChecked == true) {
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

            if (rbBriefing.IsChecked == true) {
                rbBriefing.IsChecked = false;
            }
            else if (rbGousetu.IsChecked == true) {
                rbGousetu.IsChecked = false;
            }
            else if (rbMensetu.IsChecked == true) {
                rbMensetu.IsChecked = false;
            }
            else if (rbTest.IsChecked == true) {
                rbTest.IsChecked = false;
            }
            else if (rbOther.IsChecked == true) {
                rbOther.IsChecked = false;
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            Cbupdate();

            
            var datacb = recruitManagDBDataSet.RecruitTable.Where(
                d => d.StudenNumber.ToString().Contains(tbStudentNumber.Text)).Select(s=>s.EmployeeName).Distinct().ToArray();



            foreach (var cmitem in datacb) {
                cbSort.Items.Add(cmitem.ToString());
            }

            cbSort.SelectedItem = "絞り込みなし";
        }

        private void Cbupdate() {
            var datacont = recruitManagDBDataSet.RecruitTable.Where(
                d => d.StudenNumber.ToString().Contains(tbStudentNumber.Text)).ToArray();

            dgItiran.DataContext = datacont;
        }
    }
}
