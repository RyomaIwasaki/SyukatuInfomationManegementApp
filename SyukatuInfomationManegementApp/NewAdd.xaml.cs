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

        public int ID { get; set; }

        //就活情報取得
        SyukatuInfomationManegementApp.RecruitManagementDataBaseDataSetTableAdapters.RecruitTableTableAdapter
            RecruitTableAdapter
                = new SyukatuInfomationManegementApp.RecruitManagementDataBaseDataSetTableAdapters.RecruitTableTableAdapter();

        public NewAdd() {
            InitializeComponent();
        }

        private void Return_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void Register_Click(object sender, RoutedEventArgs e) {  
            try {
                var rctDay = DateTime.Parse(DaySelect.SelectedDates[0].ToString());
                var limit = rctDay.AddDays(7);

                var newRct = RecruitManagDBDataSet.RecruitTable.NewRow();            

                newRct[1] = MakerName.Text;
                newRct[2] = Place.Text;
                
                newRct[5] = this.ID;
                newRct[6] = "未編集";
                newRct[7] = "未編集";
                newRct[8] = "未編集";
                newRct[9] = "未編集";
                newRct[10] = "未提出";

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
                else if (Naitei.IsChecked == true) {
                    newRct[3] = "内定後";
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
                RecruitTableAdapter.Update(RecruitManagDBDataSet.RecruitTable);

                MessageBox.Show("登録が完了しました。");
                TextClear();

            }
            catch (Exception ex) {
                MessageBox.Show("登録に失敗しました。" + "\n" + ex.Message);
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            RecruitManagDBDataSet = ((SyukatuInfomationManegementApp.RecruitManagementDataBaseDataSet)
                (this.FindResource("recruitManagDBDataSet")));


            RecruitTableAdapter.Fill(RecruitManagDBDataSet.RecruitTable);
        }

        private void TextClear() {
            MakerName.Text = "";
            Place.Text = "";
            DaySelect = null;
            if (Session.IsChecked == true) {
                Session.IsChecked = false;
            }
            else if (PaperTest.IsChecked == true) {
                PaperTest.IsChecked = false;
            }
            else if (Mensetu.IsChecked == true) {
                Mensetu.IsChecked = false;
            }
            else if (Goudou.IsChecked == true) {
                Goudou.IsChecked = false;
            }
            else if (Naitei.IsChecked == true) {
                Naitei.IsChecked = false;
            }

            if (Throughout.IsChecked == true) {
                Throughout.IsChecked = false;
            }
            else if (Am.IsChecked == true) {
                Am.IsChecked = false;
            }
            else if (Pm.IsChecked == true) {
                Pm.IsChecked = false;
            }
        }
    }
}
