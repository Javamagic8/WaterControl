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
                temp.Text = waterCondition.current.temp_c.ToString() + " C°";
                wind.Text = waterCondition.current.wind_kph.ToString() + " m/s";
                region.Text = waterCondition.location.region;
                cloud.Text = waterCondition.current.cloud.ToString() + "%";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            backtocard.Visibility = Visibility.Visible;
            waterdDammName.Text = "Полвон 1- Гидропост";

        }

        private void kampirqalaWaterDamm_Click(object sender, RoutedEventArgs e)
        {
            RegionCard.Visibility = Visibility.Collapsed;
            mainWindowMiddleContent.Visibility = Visibility.Visible;
            mainWindowMiddleContent.Navigate(new FirsWaterDam());
            backtocard.Visibility = Visibility.Visible;
            waterdDammName.Text = "Бош магистраль 1- Гидропост";

        }

        private void backtocard_Click(object sender, RoutedEventArgs e)
        {
            RegionCard.Visibility = Visibility.Visible;
            mainWindowMiddleContent.Visibility = Visibility.Collapsed;
            backtocard.Visibility = Visibility.Hidden;
            waterdDammName.Text = "Андижон Вилояти";

            controlbtncolor.Fill = new SolidColorBrush(Color.FromRgb(14, 77, 164));
            viewbtncolor.Fill = new SolidColorBrush(Color.FromRgb(130, 137, 140));
            monitorbtncolor.Fill = new SolidColorBrush(Color.FromRgb(130, 137, 140));
            commandbtncolor.Fill = new SolidColorBrush(Color.FromRgb(130, 137, 140));

            textControlBtn.Foreground = new SolidColorBrush(Color.FromRgb(14, 77, 164));
            textViewBtn.Foreground = new SolidColorBrush(Color.FromRgb(130, 137, 140));
            textMonitoringBtn.Foreground = new SolidColorBrush(Color.FromRgb(130, 137, 140));
            textCommandBtn.Foreground = new SolidColorBrush(Color.FromRgb(130, 137, 140));
            ipCamContent.Visibility = Visibility.Hidden;
            mainWindowMiddleContent.Visibility = Visibility.Visible;
            RegionCard.Visibility = Visibility.Visible;

            mainWindowMiddleContent.Visibility = Visibility.Hidden;

        }

        private void btnControlPanel_Click(object sender, RoutedEventArgs e)
        {
            controlbtncolor.Fill = new SolidColorBrush(Color.FromRgb(14, 77, 164));
            viewbtncolor.Fill = new SolidColorBrush(Color.FromRgb(130, 137, 140));
            monitorbtncolor.Fill = new SolidColorBrush(Color.FromRgb(130, 137, 140));
            commandbtncolor.Fill = new SolidColorBrush(Color.FromRgb(130, 137, 140));

            textControlBtn.Foreground = new SolidColorBrush(Color.FromRgb(14, 77, 164));
            textViewBtn.Foreground = new SolidColorBrush(Color.FromRgb(130, 137, 140));
            textMonitoringBtn.Foreground = new SolidColorBrush(Color.FromRgb(130, 137, 140));
            textCommandBtn.Foreground = new SolidColorBrush(Color.FromRgb(130, 137, 140));
            ipCamContent.Visibility = Visibility.Hidden;
            mainWindowMiddleContent.Visibility = Visibility.Hidden;
            RegionCard.Visibility = Visibility.Visible;

            mainWindowMiddleContent.Visibility = Visibility.Hidden;
        }

        private void btnViewsPanel_Click(object sender, RoutedEventArgs e)
        {
            controlbtncolor.Fill = new SolidColorBrush(Color.FromRgb(130, 137, 140));
            viewbtncolor.Fill = new SolidColorBrush(Color.FromRgb(14, 77, 164));
            monitorbtncolor.Fill = new SolidColorBrush(Color.FromRgb(130, 137, 140));
            commandbtncolor.Fill = new SolidColorBrush(Color.FromRgb(130, 137, 140));

            textControlBtn.Foreground = new SolidColorBrush(Color.FromRgb(130, 137, 140));
            textViewBtn.Foreground = new SolidColorBrush(Color.FromRgb(14, 77, 164));
            textMonitoringBtn.Foreground = new SolidColorBrush(Color.FromRgb(130, 137, 140));
            textCommandBtn.Foreground = new SolidColorBrush(Color.FromRgb(130, 137, 140));
            RegionCard.Visibility = Visibility.Hidden;
            mainWindowMiddleContent.Navigate( new ViewPage());

        }

        private void btnMonitoringPanel_Click(object sender, RoutedEventArgs e)
        {
            controlbtncolor.Fill = new SolidColorBrush(Color.FromRgb(130, 137, 140));
            viewbtncolor.Fill = new SolidColorBrush(Color.FromRgb(130, 137, 140));
            monitorbtncolor.Fill = new SolidColorBrush(Color.FromRgb(14, 77, 164));
            commandbtncolor.Fill = new SolidColorBrush(Color.FromRgb(130, 137, 140));

            textControlBtn.Foreground = new SolidColorBrush(Color.FromRgb(130, 137, 140));
            textViewBtn.Foreground = new SolidColorBrush(Color.FromRgb(130, 137, 140));
            textMonitoringBtn.Foreground = new SolidColorBrush(Color.FromRgb(14, 77, 164));
            textCommandBtn.Foreground = new SolidColorBrush(Color.FromRgb(130, 137, 140));
            ipCamContent.Visibility = Visibility.Hidden;
            mainWindowMiddleContent.Visibility = Visibility.Visible;
            RegionCard.Visibility = Visibility.Hidden;

            // mainWindowMiddleContent.Navigate(new MonitoringPage());
        }

        private void btnInstruction_Click(object sender, RoutedEventArgs e)
        {
            controlbtncolor.Fill = new SolidColorBrush(Color.FromRgb(130, 137, 140));
            viewbtncolor.Fill = new SolidColorBrush(Color.FromRgb(130, 137, 140));
            monitorbtncolor.Fill = new SolidColorBrush(Color.FromRgb(130, 137, 140));
            commandbtncolor.Fill = new SolidColorBrush(Color.FromRgb(14, 77, 164));

            textControlBtn.Foreground = new SolidColorBrush(Color.FromRgb(130, 137, 140));
            textViewBtn.Foreground = new SolidColorBrush(Color.FromRgb(130, 137, 140));
            textMonitoringBtn.Foreground = new SolidColorBrush(Color.FromRgb(130, 137, 140));
            textCommandBtn.Foreground = new SolidColorBrush(Color.FromRgb(14, 77, 164));
            ipCamContent.Visibility = Visibility.Hidden;
            mainWindowMiddleContent.Visibility = Visibility.Visible;
            mainWindowMiddleContent.Navigate(new InstructionPage());
            RegionCard.Visibility = Visibility.Hidden;
        }
    }
}
