using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace WaterControl.Pages
{
    /// <summary>
    /// Interaction logic for SecondWaterDam.xaml
    /// </summary>
    public partial class SecondWaterDam : Page
    {
        public SecondWaterDam()
        {
            InitializeComponent();
        }

        #region ClickEvents
        private void btnUp_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {


        }

        private void btnDown_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {

        }


        #endregion

        #region Helper
        public void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        #endregion
    }
}
