using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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
using WaterControl.Data;
using WaterControl.Pages;

namespace WaterControl.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool menuOpen = false; private Duration _openCloseDuration = new Duration(TimeSpan.FromSeconds(0.3));
        HttpClient client = new HttpClient();


        public MainWindow()
        {
            InitializeComponent();
            MenuPanel.Width = 0;
            waterdDammName.Text = "Хўжаобод Гидроузели";
            mainWindowMiddleContent.Navigate(new RegionCardPage());
            LoadDataFromSmartWater();

        }

        private async void LoadDataFromSmartWater()
        {
            try
            {
                string url = "https://api.weatherapi.com/v1/current.json?key=9054e78b46684e71a36180430232801&q=Andijan&lat=40.769724&lon=73.061400";
                HttpClient client = new HttpClient();
                var response = await client.GetAsync(url);
                string responseString = await response.Content.ReadAsStringAsync();
                WeatherInfo? waterCondition = JsonSerializer.Deserialize<WeatherInfo>(responseString);
                temp.Text = waterCondition.current.temp_c.ToString();
                wind.Text = waterCondition.current.wind_kph.ToString();
                region.Text = waterCondition.location.region;
                cloud.Text = waterCondition.current.cloud.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            Login loginWindow = new Login();
            loginWindow.Show();
            this.Close();
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

        private void btnOpenInterfaceSettingsDialog_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void polmonWaterDamm_Click(object sender, RoutedEventArgs e)
        {
            RegionCard.Visibility = Visibility.Collapsed;
            mainWindowMiddleContent.Visibility = Visibility.Visible;
            mainWindowMiddleContent.Navigate(new SecondWaterDam());
        }

        private void kampirqalaWaterDamm_Click(object sender, RoutedEventArgs e)
        {
            RegionCard.Visibility = Visibility.Collapsed;
            mainWindowMiddleContent.Visibility = Visibility.Visible;
            mainWindowMiddleContent.Navigate(new FirsWaterDam());
        }
    }
}
