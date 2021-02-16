using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace SyukatuInfomationManegementApp {
    /// <summary>
    /// Login.xaml の相互作用ロジック
    /// </summary>
    /// 
    public partial class Login : Window {
        SyukatuInfomationManegementApp.RecruitManagementDataBaseDataSet recruitManagDBDataSet;

        public int ID { get; private set; }
        public string stdName { get; private set; }

        public Login() {
            InitializeComponent();

        }

        private void Roguinn_Click(object sender, RoutedEventArgs e) {
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

            if (tbID.Text != "" || tbPassWord.Password != "") {
                ID = int.Parse(tbID.Text);

                var password = tbPassWord.Password;

                //学生情報の抽出
                var data = recruitManagDBDataSet.StudentTable.Where
                    (d => d.StudentNumber.ToString().Contains(tbID.Text)).ToList();

                //コースのパスワードを抽出
                var pass = recruitManagDBDataSet.CourseTable.Where
                    (d => d.PassWord.ToString().Contains(password)).ToList();

                if (data.Exists(d => d.StudentNumber.ToString().StartsWith(tbID.Text))) {
                    if (tbID.Text == "") MessageBox.Show("学籍番号を正しく入力してください");
                    else {
                        if (tbPassWord.Password == "") MessageBox.Show("パスワードを正しく入力してください");
                        else {
                            if (pass.Exists(s => s.PassWord.ToString().StartsWith(password))) {
                                MainWindow main = new MainWindow(this);                      
                                stdName = data[0].StudentName.ToString();
                                main.ID = tbID.Text;
                                main.GetID(ID, stdName);                    
                                this.Visibility = Visibility.Hidden; //非表示
                                main.ShowDialog();
                            }
                            else MessageBox.Show("パスワードを正しく入力してください");
                        }
                    }
                }
                else MessageBox.Show("学籍番号を正しく入力してください");
            }
            else {
                MessageBox.Show("学籍番号とパスワードを入力してください");
            }

        }
    }
}
