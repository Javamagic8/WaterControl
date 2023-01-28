using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WaterControl.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool menuOpen = false; private Duration _openCloseDuration = new Duration(TimeSpan.FromSeconds(0.3));


        public MainWindow()
        {
            InitializeComponent();
            MenuPanel.Width = 0;
        }

        private void btnOpenInterfaceSettingsDialog_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnControlPanel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnViewsPanel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnMonitoringPanel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnInstructionPanel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnExitAccount_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (!menuOpen)
            {
                dropdownInnerContent.Measure(new Size(MenuPanel.MaxWidth, MenuPanel.MaxHeight));
                DoubleAnimation widthhAnimation = new DoubleAnimation(0, 382, _openCloseDuration);
                MenuPanel.BeginAnimation(WidthProperty, widthhAnimation);
                menuOpen = true;
            }
            else
            {
                DoubleAnimation widthhAnimation = new DoubleAnimation(0, _openCloseDuration);
                MenuPanel.BeginAnimation(WidthProperty, widthhAnimation);
                menuOpen = false;
            }
        }
    }
}
