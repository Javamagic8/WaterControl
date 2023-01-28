using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace WaterControl.Windows
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window, INotifyPropertyChanged
    {
        public Login()
        {
            InitializeComponent();
        }
        private bool isJustStarted = true;
        private bool isLoggedIn = false;

        public event PropertyChangedEventHandler? PropertyChanged;

        public bool IsLoggedIn
        {
            get => isLoggedIn;
            set
            {
                isLoggedIn = value;
                NotifyPropertyChanged();
            }
        }

        public bool IsJustStarted
        {
            get => isJustStarted;
            set
            {
                isJustStarted = value;
                NotifyPropertyChanged();
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private async Task<bool> validateCreds()
        {
            if(UserName.Text == "Admin" && UserPassword.Password  == "123456")
            {
                return true;
            }
            return false;
        }

        private async void openCB(object sender, MaterialDesignThemes.Wpf.DialogOpenedEventArgs eventArgs)
        {
            try
            {
                if (await validateCreds())
                {
                    eventArgs.Session.Close(true);
                }
                else
                {
                    eventArgs.Session.Close(false);
                }
            }
            catch (Exception)
            {
                //throw
            }
        }

        private async void closeCB(object sender, MaterialDesignThemes.Wpf.DialogClosingEventArgs eventArgs)
        {
            if (eventArgs.Parameter != null)
            {
                if ((bool)eventArgs.Parameter)
                {
                    //Login Success
                    IsLoggedIn = true;

                    IsJustStarted = false;

                    MainWindow main = new MainWindow();
                    main.Show();
                    this.Close();
                }
                else
                {
                    //Login Fail
                    IsLoggedIn = false;

                    IsJustStarted = false;
                }
            }
        }
    }
}

