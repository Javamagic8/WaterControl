using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WaterControl.Model
{
    public class WindowProperty : INotifyPropertyChanged
    {
        private float _heightGate1 = 0;
        private float _heightGate2 = 0;
        private float _heightGate3 = 0;
        private float _heightGate4 = 0;
        private float _heightGate5 = 0;
        private float _heightGate6 = 0;
        private float _heightGate7 = 0;

        private string _tooth1;
        private string _tooth2;
        private string _tooth3;
        private string _tooth4;
        private string _tooth5;
        private string _tooth6;
        private string _tooth7;

        public float HeightGate1
        {
            get { return _heightGate1; }
            set { _heightGate1 = value; NotifyPropertyChanged(); }
        }

        public float HeightGate2
        {
            get { return _heightGate2; }
            set { _heightGate2 = value; NotifyPropertyChanged(); }
        }

        public float HeightGate3
        {
            get { return _heightGate3; }
            set { _heightGate3 = value; NotifyPropertyChanged();}
        }

        public float HeightGate4
        {
            get { return _heightGate4; }
            set { _heightGate4 = value; NotifyPropertyChanged();}
        }

        public float HeightGate5
        {
            get { return _heightGate5; }
            set { _heightGate5 = value; NotifyPropertyChanged(); }
        }

        public float HeightGate6
        {
            get { return _heightGate6; }
            set { _heightGate6 = value; NotifyPropertyChanged(); }
        }

        public float HeightGate7
        {
            get { return _heightGate7; }
            set { _heightGate7 = value; NotifyPropertyChanged(); }
        }



        public string Tooth1
        {
            get { return _tooth1; }
            set { _tooth1 = value; NotifyPropertyChanged(); }
        }

        public string Tooth2
        {
            get { return _tooth2; }
            set { _tooth2 = value; NotifyPropertyChanged(); }
        }
        public string Tooth3
        {
            get { return _tooth3; }
            set { _tooth3 = value; NotifyPropertyChanged(); }
        }
        public string Tooth4
        {
            get { return _tooth4; }
            set { _tooth4 = value; NotifyPropertyChanged(); }
        }
        public string Tooth5
        {
            get { return _tooth5; }
            set { _tooth5 = value; NotifyPropertyChanged(); }
        }
        public string Tooth6
        {
            get { return _tooth6; }
            set { _tooth6 = value; NotifyPropertyChanged(); }
        }

        public string Tooth7
        {
            get { return _tooth7; }
            set { _tooth7 = value; NotifyPropertyChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
