using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
    /// NewAdd.xaml の相互作用ロジック
    /// </summary>
    public partial class NewAdd : Window {
        SyukatuInfomationManegementApp.RecruitManagementDataBaseDataSet RecruitManagDBDataSet;
        SyukatuInfomationManegementApp.RecruitManagementDataBaseDataSetTableAdapters.RecruitTableTableAdapter RecruitTableTableAdapter;
        SyukatuInfomationManegementApp.RecruitManagementDataBaseDataSetTableAdapters.RecruitTableTableAdapter RecruitManagDBDataSetStudentTableTableAdapter;
        string gakusekinum = "";

        public void Getgakusekinum(string gakuban) {
            gakusekinum = gakuban;
        }

        int ID = 0;

        public NewAdd(int id) {
            InitializeComponent();
            ID = id;
        }

        private void Return_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void Register_Click(object sender, RoutedEventArgs e) {
            
            var rctDay =  DateTime.Parse(Calender.SelectedDates[0].ToString());
            var limit = rctDay.AddDays(7);

            try {
                DataRow newRct = (DataRow)RecruitManagDBDataSet.RecruitTable.NewRow();               

                newRct[1] = MakerName.Text;
                newRct[2] = Place.Text;
                
                newRct[5] = ID;
                newRct[6] = "未編集";
                newRct[7] = "未編集";
                newRct[8] = "未編集";
                newRct[9] = "未編集";
                newRct[10] = "未確認";

                newRct[4] = rctDay;
                newRct[11] = limit;

                if (Session.IsChecked == true) {
                    newRct[3] = "説明会";
                }
                else if (PaperTest.IsChecked == true) {
                    newRct[3] = "筆記試験";
                }
                else if (Mensetu.IsChecked == true) {
                    newRct[3] = "面接";
                }
                else if (Goudou.IsChecked == true) {
                    newRct[3] = "合同企業説明会";
                }

                if (Throughout.IsChecked == true) {
                    newRct[12] = "終日";
                }
                else if (Am.IsChecked == true) {
                    newRct[12] = "午前";
                }
                else if (Pm.IsChecked == true) {
                    newRct[12] = "午後";
                }

                RecruitManagDBDataSet.RecruitTable.Rows.Add(newRct);

                //データベース更新
                //RecruitTableTableAdapter.Update(RecruitManagDBDataSet.RecruitTable);
                RecruitManagDBDataSetStudentTableTableAdapter.Update(RecruitManagDBDataSet.RecruitTable);
            }
            catch (Exception ex) {
                MessageBox.Show("登録に失敗しました。" + "\n" + ex.Message);
            }

        }

    }
}
