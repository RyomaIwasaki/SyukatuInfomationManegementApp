using System;
using System.Collections.Generic;
using System.Data.Common;
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
    /// Login.xaml の相互作用ロジック
    /// </summary>
    public partial class Login : Window
    {
        SyukatuInfomationManegementApp.RecruitManagementDataBaseDataSet recruitManagDBDataSet;

        public static int gakuban;
        private int propatygakuban {
            set { gakuban = int.Parse(tbID.Text); }
            get { return gakuban; }
        }

        public Login()
        {
            InitializeComponent();

        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Roguinn_Click(object sender, RoutedEventArgs e)
        {
            recruitManagDBDataSet = ((SyukatuInfomationManegementApp.RecruitManagementDataBaseDataSet)
                (this.FindResource("recruitManagDBDataSet")));

            //学生テーブル読込
            SyukatuInfomationManegementApp.RecruitManagementDataBaseDataSetTableAdapters.StudentTableTableAdapter RecruitManagDBDataSetStudentTableTableAdapter
                = new SyukatuInfomationManegementApp.RecruitManagementDataBaseDataSetTableAdapters.StudentTableTableAdapter();
            RecruitManagDBDataSetStudentTableTableAdapter.Fill(recruitManagDBDataSet.StudentTable);

            //コーステーブル
            SyukatuInfomationManegementApp.RecruitManagementDataBaseDataSetTableAdapters.CourseTableTableAdapter RecruitManagDBDataSetCourseTableTableAdapter
                = new SyukatuInfomationManegementApp.RecruitManagementDataBaseDataSetTableAdapters.CourseTableTableAdapter();
            RecruitManagDBDataSetCourseTableTableAdapter.Fill(recruitManagDBDataSet.CourseTable);

            System.Windows.Data.CollectionViewSource RecruitViewSource
                = ((System.Windows.Data.CollectionViewSource)(this.FindResource("RecruitViewSource")));
            RecruitViewSource.View.MoveCurrentToFirst();

            int GakusekiNum = int.Parse(tbID.Text);

            var data = recruitManagDBDataSet.StudentTable.Where
                (d => d.StudentNumber.ToString().Contains(tbID.Text)).ToList();

            var pass = recruitManagDBDataSet.CourseTable.Where
                (d => d.PassWord.ToString().Contains(tbPassWord.Password)).ToList();

            if (data.Exists(d => d.StudentNumber.ToString().StartsWith(tbID.Text))) {
                if (tbID.Text == "") MessageBox.Show("学籍番号を正しく入力してください");
                else {
                    if (tbPassWord.Password == "") MessageBox.Show("パスワードを正しく入力してください");
                    else {
                        if (pass.Exists(s => s.PassWord.ToString().StartsWith(tbPassWord.Password))) {
                            MainWindowShow();
                            this.Close();
                        }
                        else MessageBox.Show("パスワードを正しく入力してください");
                    }                   
                }
            }
            else MessageBox.Show("学籍番号を正しく入力してください");

            int gakuban = int.Parse(tbID.Text);
        }

        private static void MainWindowShow()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        
    }
}
