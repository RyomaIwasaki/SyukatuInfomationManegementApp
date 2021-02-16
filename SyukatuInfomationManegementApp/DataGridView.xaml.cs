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
using System.Windows.Shapes;

namespace SyukatuInfomationManegementApp {
    /// <summary>
    /// DataGridView.xaml の相互作用ロジック
    /// </summary>
    public partial class DataGridView : Window {
        public string RctID { get; set; }
        public string RctDate { get; set; }
        public string StdNum { get; set; }
        public string LimitDate { get; set; }
        public string EValu { get; set; }

        SyukatuInfomationManegementApp.RecruitManagementDataBaseDataSet recruitManagDBDataSet;

        //リクルートテーブル読込
        SyukatuInfomationManegementApp.RecruitManagementDataBaseDataSetTableAdapters.RecruitTableTableAdapter
            RecruitTableAdapter
            = new SyukatuInfomationManegementApp.RecruitManagementDataBaseDataSetTableAdapters.RecruitTableTableAdapter();
        

        public DataGridView() {
            InitializeComponent();
        }

        private void Return_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        //保存
        private void Save_Click(object sender, RoutedEventArgs e) {
            try {
                var edidata = recruitManagDBDataSet.RecruitTable.Where(
                d => d.RecruitID.ToString().Contains(RctID)).ToArray();

                

                edidata[0].dateContent = tbContext.Text;
                edidata[0].Others = tbOthers.Text;
                edidata[0].Evaluation = "未確認";

                //DB更新
                RecruitTableAdapter.Update(recruitManagDBDataSet.RecruitTable);
                MessageBox.Show("編集しました。");

            }
            catch (Exception ex) {
                MessageBox.Show("編集に失敗しました。" + "\n" + ex.Message);
            }

            
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            recruitManagDBDataSet = ((SyukatuInfomationManegementApp.RecruitManagementDataBaseDataSet)
                (this.FindResource("recruitManagDBDataSet")));

            RecruitTableAdapter.Fill(recruitManagDBDataSet.RecruitTable);

            //当日の内容にデータベースの内容を表示
            var content = recruitManagDBDataSet.RecruitTable.Where(
                d => d.RecruitID.ToString().Contains(RctID)).Select(s=>s.dateContent.ToString()).ToArray();

            if (content[0] != "未編集") {
                tbContext.Text = content[0];
            }else {
                tbContext.Text = "";
            }
            

            //その他にデータベースの内容を表示
            var other = recruitManagDBDataSet.RecruitTable.Where(
                d => d.RecruitID.ToString().Contains(RctID)).Select(s => s.Others.ToString()).ToArray();

            if (other[0] != "未編集") {
                tbOthers.Text = other[0];
            }
            else {
                tbOthers.Text = "";
            }

            //教師メモにデータベースの内容を表示
            var memo = recruitManagDBDataSet.RecruitTable.Where(
                d => d.RecruitID.ToString().Contains(RctID)).Select(s => s.TeachersMemo.ToString()).ToArray();

            if (memo[0] != "未編集") {
                tbTeachersMemo.Text = memo[0];
            }
            else {
                tbTeachersMemo.Text = "";
            }

            //教師が確認してOKだったら保存ボタンをマスク処理する

            if (EValu == "確認済み") {
                Save.IsEnabled = false;
            }

        }
    }
}
