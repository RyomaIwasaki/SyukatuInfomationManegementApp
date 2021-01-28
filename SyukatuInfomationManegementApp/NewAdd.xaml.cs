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

namespace SyukatuInfomationManegementApp
{
    /// <summary>
    /// NewAdd.xaml の相互作用ロジック
    /// </summary>
    public partial class NewAdd : Window
    {
        SyukatuInfomationManegementApp.RecruitManagementDataBaseDataSet RecruitManagementDataBaseDataSet;
        public NewAdd()
        {
            InitializeComponent();
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Register_Click(object sender, RoutedEventArgs e) {
            Login login = new Login();

            DataRow newRct = (DataRow)RecruitManagementDataBaseDataSet.RecruitTable.NewRow();
            newRct[1] = MakerName.Text;
            newRct[2] = Place.Text;
            newRct[4] = Calender.SelectedDates;
            newRct[5] = login.;
            
            if (Session.IsChecked == true) {
                newRct[3] = "説明会";
            }else if (PaperTest.IsChecked == true) {
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
            }else if (Am.IsChecked == true) {
                newRct[12] = "午前";
            }
            else if (Pm.IsChecked == true) {
                newRct[12] = "午後";
            }

        }

        private void Session_Checked(object sender, RoutedEventArgs e) {
            
        }

        private void Throughout_Checked(object sender, RoutedEventArgs e) {
            
        }

        private void Am_Checked(object sender, RoutedEventArgs e) {

        }

        private void Pm_Checked(object sender, RoutedEventArgs e) {

        }

        private void PaperTest_Checked(object sender, RoutedEventArgs e) {

        }

        private void Goudou_Checked(object sender, RoutedEventArgs e) {

        }

        private void Mensetu_Checked(object sender, RoutedEventArgs e) {

        }
    }
}
