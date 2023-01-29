using FluentModbus;
using System;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using WaterControl.Model;

namespace WaterControl.Pages
{
    /// <summary>
    /// Interaction logic for SecondWaterDam.xaml
    /// </summary>
    public partial class SecondWaterDam : Page
    {
        WindowProperty windowProperty = new WindowProperty();

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

        public void gateBlocker(int id)
        {

            if (id != 1)
            {

                btnUp1.IsEnabled = false;
                btnStop1.IsEnabled = false;
                btnDown1.IsEnabled = false;
                darvoza[0].gateBlock = false;
                btnInputHeight1.IsEnabled = false;

            }
            if (id != 2)
            {

                btnUp2.IsEnabled = false;
                btnStop2.IsEnabled = false;
                btnDown2.IsEnabled = false;
                darvoza[1].gateBlock = false;
                btnInputHeight2.IsEnabled = false;

            }
        }

        public void gateUnBlocker()
        {
            darvoza[0].gateBlock = true;
            darvoza[1].gateBlock = true;
            darvoza[2].gateBlock = true;
            darvoza[3].gateBlock = true;

            btnInputHeight1.IsEnabled = true;
            btnInputHeight2.IsEnabled = true;
        }

        public void stopCores()
        {
            // Stop loop 
            Thread.Sleep(100);
            loopControl = false;
        }
        public SecondWaterDam()
        {
            InitializeComponent();


            // Set up ViewModel, assign to DataContext etc.
            //FirstCam.Navigate("https://google.com/");
            gateManipuller[0] = new gateActionManipulator();
            gateManipuller[1] = new gateActionManipulator();
            gateManipuller[2] = new gateActionManipulator();
            gateManipuller[3] = new gateActionManipulator();

            gateManipullerEncoder[0] = new gateActionManipulator();
            gateManipullerEncoder[1] = new gateActionManipulator();
            gateManipullerEncoder[2] = new gateActionManipulator();
            gateManipullerEncoder[3] = new gateActionManipulator();

            darvoza[0] = new GateModel();
            darvoza[0].encoderID = 1;
            darvoza[0].Gate.GateControl.UpOnBit = 0;
            darvoza[0].Gate.GateControl.UpCheckBit = 200;
            darvoza[0].Gate.GateControl.UpOffBit = 200;
            darvoza[0].Gate.GateControl.UpStop = 100;
            darvoza[0].Gate.GateControl.DownOnBit = 1;
            darvoza[0].Gate.GateControl.DownCheckBit = 201;
            darvoza[0].Gate.GateControl.DownOffBit = 201;
            darvoza[0].Gate.GateControl.DownStop = 101;

            darvoza[1] = new GateModel();
            darvoza[1].encoderID = 7;
            darvoza[1].Gate.GateControl.UpOnBit = 2;
            darvoza[1].Gate.GateControl.UpCheckBit = 202;
            darvoza[1].Gate.GateControl.UpOffBit = 202;
            darvoza[1].Gate.GateControl.UpStop = 102;
            darvoza[1].Gate.GateControl.DownOnBit = 3;
            darvoza[1].Gate.GateControl.DownCheckBit = 203;
            darvoza[1].Gate.GateControl.DownOffBit = 203;
            darvoza[1].Gate.GateControl.DownStop = 103;

            darvoza[2] = new GateModel();
            darvoza[2].encoderID = 3;
            darvoza[2].Gate.GateControl.UpOnBit = 4;
            darvoza[2].Gate.GateControl.UpCheckBit = 204;
            darvoza[2].Gate.GateControl.UpOffBit = 204;
            darvoza[2].Gate.GateControl.UpStop = 104;
            darvoza[2].Gate.GateControl.DownOnBit = 5;
            darvoza[2].Gate.GateControl.DownCheckBit = 205;
            darvoza[2].Gate.GateControl.DownOffBit = 205;
            darvoza[2].Gate.GateControl.DownStop = 105;

            darvoza[3] = new GateModel();
            darvoza[3].encoderID = 6;
            darvoza[3].Gate.GateControl.UpOnBit = 6;
            darvoza[3].Gate.GateControl.UpCheckBit = 206;
            darvoza[3].Gate.GateControl.UpOffBit = 206;
            darvoza[3].Gate.GateControl.UpStop = 106;
            darvoza[3].Gate.GateControl.DownOnBit = 7;
            darvoza[3].Gate.GateControl.DownCheckBit = 207;
            darvoza[3].Gate.GateControl.DownOffBit = 207;
            darvoza[3].Gate.GateControl.DownStop = 107;


            absoluteEncoder[0] = new encoderModel();
            absoluteEncoder[0].minValu = 110697;
            absoluteEncoder[0].maxValue = 141917;

            absoluteEncoder[1] = new encoderModel();
            absoluteEncoder[1].minValu = 111962;
            absoluteEncoder[1].maxValue = 139590;

            absoluteEncoder[2] = new encoderModel();
            absoluteEncoder[2].minValu = 101232;
            absoluteEncoder[2].maxValue = 116335;

            absoluteEncoder[3] = new encoderModel();
            absoluteEncoder[3].minValu = 109894;
            absoluteEncoder[3].maxValue = 123729;
        }

        public bool IsClicked = false;


        public void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        SerialPort serialPort;
        bool localStatus;
        bool remoteStatus;
        Int64 time1Interval;
        Int64 time1IntervalEncoder;
        byte[] boolData = new byte[2];
        ModbusRtuClient client = new ModbusRtuClient();
        ModbusRtuClient client2 = new ModbusRtuClient();
        int iteration;
        public static object sinxronKontroller = new object();
        public static GateModel[] darvoza = new GateModel[4];
        public static gateActionManipulator[] gateManipuller = new gateActionManipulator[4];
        public static gateActionManipulator[] gateManipullerEncoder = new gateActionManipulator[4];
        public static int[] absolutValueEncoder = new int[4];
        public bool loopControl = true;
        Thread thread;
        Thread thread2;
        Thread thread3;
        Thread thread4;
        public static bool encoderErrorStatus = true;
        public static bool plcErrorStatus = true;
        DispatcherTimer timer1 = new DispatcherTimer();
        DispatcherTimer timer2 = new DispatcherTimer();
        DispatcherTimer timer3 = new DispatcherTimer();

        public encoderModel[] absoluteEncoder = new encoderModel[7];



        /// <summary>
        /// Darvozalardagi zaborlarning balandligini ko'rsatish uchun chaqiriladi
        /// Darvozalarning qiymati 0 da 100 gacha ko'rsatiladi
        /// Funksiyaning vazifasi darvozaning maksimal va minimal qiymatlarini xisobga olgan xolda
        /// berilgan darvoza qiymatiga mos 0 da 100 gacha bo'lgan sonni chiqarib berish
        /// Qiymatlarni to'g'ri ko'rsatish uchun darvozalar tartiblab chiqilgan bo'lishi kerak
        /// Har bir darvozaning nomi va unga qo'yilgan encoder tartib raqamidan qatiiy nazar aniq tartiblangan 
        /// bo'lishi kerak.
        /// </summary>
        /// <param name="state">Encoderdan kelgan absolyut qiymat</param>
        /// <param name="id">Darvozaning tartib raqami</param>
        public void gateStateVisualizer(int state, int id)
        {

            int interval = 0;
            switch (id)
            {
                case 0:
                    interval = (absoluteEncoder[id].maxValue - absoluteEncoder[id].minValu) / 320;
                    if (320 - ((darvoza[id].encoderValue - absoluteEncoder[id].minValu) / interval) < 0) windowProperty.HeightGate1 = 0;
                    else if (320 - ((darvoza[id].encoderValue - absoluteEncoder[id].minValu) / interval) > 320) windowProperty.HeightGate1 = 320;
                    else windowProperty.HeightGate1 = 320 - ((darvoza[id].encoderValue - absoluteEncoder[id].minValu) / interval);
                    break;
                case 1:
                    interval = (absoluteEncoder[id].maxValue - absoluteEncoder[id].minValu) / 320;
                    if (320 - ((darvoza[id].encoderValue - absoluteEncoder[id].minValu) / interval) < 0) windowProperty.HeightGate2 = 0;
                    else if (320 - ((darvoza[id].encoderValue - absoluteEncoder[id].minValu) / interval) > 320) windowProperty.HeightGate2 = 320;
                    else windowProperty.HeightGate2 = 320 - ((darvoza[id].encoderValue - absoluteEncoder[id].minValu) / interval);
                    break;
                case 2:
                    interval = (absoluteEncoder[id].maxValue - absoluteEncoder[id].minValu) / 320;
                    if (320 - ((darvoza[id].encoderValue - absoluteEncoder[id].minValu) / interval) < 0) windowProperty.HeightGate3 = 0;
                    else if (320 - ((darvoza[id].encoderValue - absoluteEncoder[id].minValu) / interval) > 320) windowProperty.HeightGate3 = 320;
                    else windowProperty.HeightGate3 = 320 - ((darvoza[id].encoderValue - absoluteEncoder[id].minValu) / interval);
                    break;
                case 3:
                    interval = (absoluteEncoder[id].maxValue - absoluteEncoder[id].minValu) / 320;
                    if (320 - ((darvoza[id].encoderValue - absoluteEncoder[id].minValu) / interval) < 0) windowProperty.HeightGate4 = 0;
                    else if (320 - ((darvoza[id].encoderValue - absoluteEncoder[id].minValu) / interval) > 320) windowProperty.HeightGate4 = 320;
                    else windowProperty.HeightGate4 = 320 - ((darvoza[id].encoderValue - absoluteEncoder[id].minValu) / interval);
                    break;
            }
        }

        public void controlTooth(int activGateId, bool iterationControl, int gateNextValue)
        {

            int accuracyControl = 50;

            if ((!darvoza[activGateId].isActive) && iterationControl)
            {
                if (darvoza[activGateId].encoderValue > gateNextValue)
                {
                    gateManipuller[activGateId].active2Down = true;
                    gateManipuller[activGateId].deactivate2Stop = false;
                    gateManipullerEncoder[activGateId].active2Down = true;
                    gateManipullerEncoder[activGateId].deactivate2Stop = false;
                    iterationControl = false;
                    this.Dispatcher.Invoke(new Action(delegate
                    {

                        gateBlocker(0);
                    }));
                    while (darvoza[activGateId].encoderValue > gateNextValue)
                    {
                    }
                    gateManipuller[activGateId].active2Down = false;
                    gateManipuller[activGateId].deactivate2Stop = true;
                    gateManipullerEncoder[activGateId].active2Down = false;
                    gateManipullerEncoder[activGateId].deactivate2Stop = true;
                    this.Dispatcher.Invoke(new Action(delegate
                    {

                        gateUnBlocker();
                    }));
                }
                else
                {
                    gateManipuller[activGateId].active2Up = true;
                    gateManipuller[activGateId].deactivate2Stop = false;
                    gateManipullerEncoder[activGateId].active2Up = true;
                    gateManipullerEncoder[activGateId].deactivate2Stop = false;
                    iterationControl = false;
                    this.Dispatcher.Invoke(new Action(delegate
                    {

                        gateBlocker(0);
                    }));
                    while (darvoza[activGateId].encoderValue < gateNextValue)
                    {
                        iteration++;
                    }
                    gateManipuller[activGateId].active2Up = false;
                    gateManipuller[activGateId].deactivate2Stop = true;
                    gateManipullerEncoder[activGateId].active2Up = false;
                    gateManipullerEncoder[activGateId].deactivate2Stop = true;
                    this.Dispatcher.Invoke(new Action(delegate
                    {

                        gateUnBlocker();
                    }));

                }
            }
        }

        public void encoderUpdator(int encoderSum)
        {
            bool encoderDirection = false;
            encoderCoreModel[] encoderGates = new encoderCoreModel[7];

            encoderGates[0] = new encoderCoreModel();
            encoderGates[0].currentTime = time1IntervalEncoder;
            encoderGates[0].nextTime1Up = time1IntervalEncoder + 100;
            encoderGates[0].nextTime1Down = time1IntervalEncoder + 100;
            encoderGates[0].nextTime1Update = time1IntervalEncoder + 5;
            encoderGates[0].enabled = true;
            //repository test
            time1IntervalEncoder += 17;
            encoderGates[1] = new encoderCoreModel();
            encoderGates[1].currentTime = time1IntervalEncoder;
            encoderGates[1].nextTime1Up = time1IntervalEncoder + 100;
            encoderGates[1].nextTime1Down = time1IntervalEncoder + 100;
            encoderGates[1].nextTime1Update = time1IntervalEncoder + 5;
            encoderGates[1].enabled = true;

            time1IntervalEncoder += 17;
            encoderGates[2] = new encoderCoreModel();
            encoderGates[2].currentTime = time1IntervalEncoder;
            encoderGates[2].nextTime1Up = time1IntervalEncoder + 100;
            encoderGates[2].nextTime1Down = time1IntervalEncoder + 100;
            encoderGates[2].nextTime1Update = time1IntervalEncoder + 5;
            encoderGates[2].enabled = true;

            time1IntervalEncoder += 17;
            encoderGates[3] = new encoderCoreModel();
            encoderGates[3].currentTime = time1IntervalEncoder;
            encoderGates[3].nextTime1Up = time1IntervalEncoder + 100;
            encoderGates[3].nextTime1Down = time1IntervalEncoder + 100;
            encoderGates[3].nextTime1Update = time1IntervalEncoder + 5;
            encoderGates[3].enabled = true;

            do
            {
                //if (encoderErrorStatus)
                // {
                // lock (sinxronKontroller)
                /*
                // { 
                this.Dispatcher.Invoke(new Action(delegate
                {
                    ConnectionLost.Visibility = Visibility.Visible;
                    AddShadow();
                }));
                */
                // Thread.Sleep(1);

                /*  if (encoderGates[0].spinActivate)
                  {

                  }
                */
                Thread.Sleep(1);
                if (client2.IsConnected)
                {
                    /*
                    Span<uint> floatData = client2.ReadInputRegisters<uint>(6, 1, 1);
                    uint firstValue = floatData[0];
                    encoderValue.Text = firstValue.ToString();
                    absolutValueEncoder[0] = ((int)firstValue);
                    */
                    for (int i = 0; i < encoderSum; i++)
                    {
                        if (encoderGates[i].enabled)
                        {
                            if (encoderGates[i].timeIntervalChange)
                            {
                                encoderGates[i].nextTime1Update = time1IntervalEncoder;
                            }
                            if (encoderGates[i].updateTime1)
                            {
                                encoderGates[i].updateTimeInterval = 2;
                                encoderGates[i].timeIntervalChange = false;
                                //encoderGates[0].spinActivate = false;

                            }
                            else
                            {
                                encoderGates[i].updateTimeInterval = 200;
                                encoderGates[i].timeIntervalChange = false;
                                //  encoderGates[0].spinActivate = true;
                            }

                            //Tepaga yurish buyrug'i berilgandan
                            if (gateManipullerEncoder[i].active2Up)
                            {
                                if (encoderGates[i].upreporter1)
                                {
                                    encoderGates[i].upreporter1 = false;
                                    encoderGates[i].stopreport1 = true;
                                    encoderGates[i].updateTime1 = true;
                                    encoderGates[i].timeIntervalChange = true;
                                }
                            }
                            //Pastga yurish buyrug'i berilganda
                            if (gateManipullerEncoder[i].active2Down)
                            {
                                if (encoderGates[i].downreporter1)
                                {

                                    encoderGates[i].downreporter1 = false;
                                    encoderGates[i].stopreport1 = true;
                                    encoderGates[i].updateTime1 = true;
                                    encoderGates[i].timeIntervalChange = true;
                                }
                            }
                            //Stop buyrug'i berilganda
                            if (gateManipullerEncoder[i].deactivate2Stop & encoderGates[i].stopreport1)
                            {
                                encoderGates[i].stopreport1 = false;
                                encoderGates[i].upreporter1 = true;
                                encoderGates[i].downreporter1 = true;
                                encoderGates[i].updateTime1 = false;
                                encoderGates[i].timeIntervalChange = true;

                            }
                        }

                        if (encoderGates[0].enabled)
                        {
                            //Tegishli darvozani tekshirish algoritmlari
                            this.Dispatcher.Invoke(new Action(delegate
                            {



                                if (time1IntervalEncoder > encoderGates[0].nextTime1Update)
                                {
                                    Span<uint> floatData = new Span<uint>();
                                    uint firstValue = new uint();
                                    encoderGates[0].nextTime1Update = time1IntervalEncoder + encoderGates[0].updateTimeInterval;
                                    if (encoderErrorStatus)
                                    {
                                        try
                                        {
                                                //Bu yerda darvozaga tegishli Encoder ma'lumotlari tekshiriladi. E'tibor berish kerakki
                                                //Agarda Encoderdan o'qish amalga oshmasa xatolik yuz beradi
                                                //Xatolik yuz bergani sababali pastroqdagi kodlar bajarilmaydi
                                                //Kelasi safar Encoderdan ma'lumot o'qish bajarilganda
                                                //pastdagi kodlar odatiy holda bajariladi
                                            floatData = client2.ReadInputRegisters<uint>(darvoza[0].encoderID, 1, 1);
                                            firstValue = floatData[0];
                                            if (encoderDirection)
                                            {
                                                darvoza[0].encoderValue = ((int)firstValue);
                                            }
                                            else
                                            {
                                                darvoza[0].encoderValue = 262144 - ((int)firstValue);
                                            }

                                                //Bu qism faqatfina test uchun bo'lib asosiy dastur kodida bu qism
                                                //aks ettirilmaydi
                                            absoluteEncoder[0].value = darvoza[0].encoderValue;
                                            absoluteEncoder[0].toothStateUpdate();
                                                //Kodning bu qismi asosiy kodda aks ettiriladi.
                                                //Ushbu qiymatning chiqishi uchun darvoza encoderining maksimal va minimal 
                                                //qiymatlari kirilgan bo'lishi kerak
                                            windowProperty.Tooth1 = absoluteEncoder[0].toothState.ToString();
                                                //Kodning bu qismi trakBarni darvoza qiymatiga mos ravishta sozlaydi

                                            gateStateVisualizer(darvoza[0].encoderValue, 0);
                                        }
                                        catch (Exception ex)
                                        {
                                                /*
                                                encoderErrorStatus = false;
                                                MessageBox.Show(ex.Message+"Encoder");
                                                this.Invoke((MethodInvoker)delegate
                                                {
                                                    waitEncoder.Visible = true;
                                                });
                                                */
                                        }
                                    }
                                }
                            }));
                        }

                        if (encoderGates[1].enabled)
                        {
                            //Tegishli darvozani tekshirish algoritmlari
                            this.Dispatcher.Invoke(new Action(delegate
                            {
                                if (time1IntervalEncoder > encoderGates[1].nextTime1Update)
                                {
                                    Span<uint> floatData = new Span<uint>();
                                    uint firstValue = new uint();
                                    encoderGates[1].nextTime1Update = time1IntervalEncoder + encoderGates[1].updateTimeInterval;
                                    if (encoderErrorStatus)
                                    {
                                        try
                                        {   //Bu yerda darvozaga tegishli Encoder ma'lumotlari tekshiriladi. E'tibor berish kerakki
                                            //Agarda Encoderdan o'qish amalga oshmasa xatolik yuz beradi
                                            //Xatolik yuz bergani sababali pastroqdagi kodlar bajarilmaydi
                                            //Kelasi safar Encoderdan ma'lumot o'qish bajarilganda
                                            //pastdagi kodlar odatiy holda bajariladi
                                            floatData = client2.ReadInputRegisters<uint>(darvoza[1].encoderID, 1, 1);
                                            firstValue = floatData[0];
                                            if (encoderDirection)
                                            {
                                                darvoza[1].encoderValue = ((int)firstValue);
                                            }
                                            else
                                            {
                                                darvoza[1].encoderValue = 262144 - ((int)firstValue);
                                            }
                                            absoluteEncoder[1].value = darvoza[1].encoderValue;
                                            absoluteEncoder[1].toothStateUpdate();
                                            windowProperty.Tooth2 = absoluteEncoder[1].toothState.ToString();
                                            gateStateVisualizer(darvoza[1].encoderValue, 1);
                                        }
                                        catch (Exception ex)
                                        {
                                                /*  encoderErrorStatus = false;
                                                  MessageBox.Show(ex.Message + "Encoder");
                                                  this.Invoke((MethodInvoker)delegate
                                                  {
                                                      waitEncoder.Visible = true;
                                                  });
                                                */
                                        }
                                    }
                                }
                            }));
                        }

                        if (encoderGates[2].enabled)
                        {
                            //Tegishli darvozani tekshirish algoritmlari
                            this.Dispatcher.Invoke(new Action(delegate
                            {
                                if (time1IntervalEncoder > encoderGates[2].nextTime1Update)
                                {
                                    Span<uint> floatData = new Span<uint>();
                                    uint firstValue = new uint();
                                    encoderGates[2].nextTime1Update = time1IntervalEncoder + encoderGates[2].updateTimeInterval;
                                    if (encoderErrorStatus)
                                    {
                                        try
                                        {   //Bu yerda darvozaga tegishli Encoder ma'lumotlari tekshiriladi. E'tibor berish kerakki
                                            //Agarda Encoderdan o'qish amalga oshmasa xatolik yuz beradi
                                            //Xatolik yuz bergani sababali pastroqdagi kodlar bajarilmaydi
                                            //Kelasi safar Encoderdan ma'lumot o'qish bajarilganda
                                            //pastdagi kodlar odatiy holda bajariladi
                                            floatData = client2.ReadInputRegisters<uint>(darvoza[2].encoderID, 1, 1);
                                            firstValue = floatData[0];
                                            if (encoderDirection)
                                            {
                                                darvoza[2].encoderValue = ((int)firstValue);
                                            }
                                            else
                                            {
                                                darvoza[2].encoderValue = 262144 - ((int)firstValue);
                                            }
                                            absoluteEncoder[2].value = darvoza[2].encoderValue;
                                            absoluteEncoder[2].toothStateUpdate();
                                            windowProperty.Tooth3 = absoluteEncoder[2].toothState.ToString();
                                            gateStateVisualizer(darvoza[2].encoderValue, 2);
                                        }
                                        catch (Exception ex)
                                        {
                                                /*  encoderErrorStatus = false;
                                                  MessageBox.Show(ex.Message + "Encoder");
                                                  this.Invoke((MethodInvoker)delegate
                                                  {
                                                      waitEncoder.Visible = true;
                                                  });
                                                */
                                        }
                                    }
                                }
                            }));
                        }

                        if (encoderGates[3].enabled)
                        {
                            //Tegishli darvozani tekshirish algoritmlari
                            this.Dispatcher.Invoke(new Action(delegate
                            {
                                if (time1IntervalEncoder > encoderGates[3].nextTime1Update)
                                {
                                    Span<uint> floatData = new Span<uint>();
                                    uint firstValue = new uint();
                                    encoderGates[3].nextTime1Update = time1IntervalEncoder + encoderGates[3].updateTimeInterval;
                                    if (encoderErrorStatus)
                                    {
                                        try
                                        {   //Bu yerda darvozaga tegishli Encoder ma'lumotlari tekshiriladi. E'tibor berish kerakki
                                            //Agarda Encoderdan o'qish amalga oshmasa xatolik yuz beradi
                                            //Xatolik yuz bergani sababali pastroqdagi kodlar bajarilmaydi
                                            //Kelasi safar Encoderdan ma'lumot o'qish bajarilganda
                                            //pastdagi kodlar odatiy holda bajariladi
                                            floatData = client2.ReadInputRegisters<uint>(darvoza[3].encoderID, 1, 1);
                                            firstValue = floatData[0];
                                            if (encoderDirection)
                                            {
                                                darvoza[3].encoderValue = ((int)firstValue);
                                            }
                                            else
                                            {
                                                darvoza[3].encoderValue = 262144 - ((int)firstValue);
                                            }
                                            absoluteEncoder[3].value = darvoza[3].encoderValue;
                                            absoluteEncoder[3].toothStateUpdate();
                                            windowProperty.Tooth4 = absoluteEncoder[3].toothState.ToString();
                                            gateStateVisualizer(darvoza[3].encoderValue, 3);
                                        }
                                        catch (Exception ex)
                                        {
                                                /*  encoderErrorStatus = false;
                                                  MessageBox.Show(ex.Message + "Encoder");
                                                  this.Invoke((MethodInvoker)delegate
                                                  {
                                                      waitEncoder.Visible = true;
                                                  });
                                                */
                                        }
                                    }
                                }
                            }));
                        }
                    }
                    // MessageBox.Show("Encoder");
                    // Thread.Sleep(1000);
                }
                //}
                // }
            } while (loopControl);
        }

        public void plcCore(int plcGates)
        {

            plcCoreModel[] coreGate = new plcCoreModel[4];


            coreGate[0] = new plcCoreModel();
            coreGate[0].currentTime = time1Interval;
            coreGate[0].nextTime1Up = time1Interval + 100;
            coreGate[0].nextTime1Down = time1Interval + 100;
            coreGate[0].nextTime1Update = time1Interval + 10;
            coreGate[0].enabled = true;

            time1Interval += 15;
            coreGate[1] = new plcCoreModel();
            coreGate[1].currentTime = time1Interval;
            coreGate[1].nextTime1Up = time1Interval + 100;
            coreGate[1].nextTime1Down = time1Interval + 100;
            coreGate[1].nextTime1Update = time1Interval + 10;
            coreGate[1].enabled = true;

            time1Interval += 15;
            coreGate[2] = new plcCoreModel();
            coreGate[2].currentTime = time1Interval;
            coreGate[2].nextTime1Up = time1Interval + 100;
            coreGate[2].nextTime1Down = time1Interval + 100;
            coreGate[2].nextTime1Update = time1Interval + 10;
            coreGate[2].enabled = true;

            time1Interval += 10;
            coreGate[3] = new plcCoreModel();
            coreGate[3].currentTime = time1Interval;
            coreGate[3].nextTime1Up = time1Interval + 100;
            coreGate[3].nextTime1Down = time1Interval + 100;
            coreGate[3].nextTime1Update = time1Interval + 10;
            coreGate[3].enabled = true;

            while (loopControl)
            {
                if (plcErrorStatus)
                {
                    // lock (sinxronKontroller)
                    // {
                    this.Dispatcher.Invoke(new Action(delegate
                    {
                            //PLC bilan aloqa o'rnatilganda plcErrorStatus enable qiymatni oladi
                            //Shu bilan birga plcCore ishga ham tushadi
                            //Bu degani PLC bilan aloqa yo'qligini ko'rsatuvchi animatsiya o'chishi kerak degani
                            //Ushbu kod kutish animatsiyasini o'chiradi va uning o'chganligi plcCore ishlayotganini
                            // va PLC bilan anoqa mavjudligini anglatadi


                    }));
                    Thread.Sleep(1);
                    if (client.IsConnected)
                    {
                        for (int i = 0; i < plcGates; i++)
                        {

                            if (coreGate[i].enabled & plcErrorStatus)
                            {
                                if (coreGate[i].timeIntervalChange)
                                {
                                    coreGate[i].nextTime1Update = time1Interval;
                                }
                                if (coreGate[i].updateTime1)
                                {
                                    coreGate[i].updateTimeInterval = 10;
                                    coreGate[i].timeIntervalChange = false;
                                    //coreGate[0].spinActivate = false;
                                }
                                else
                                {
                                    coreGate[i].updateTimeInterval = 200;
                                    coreGate[i].timeIntervalChange = false;
                                    //coreGate[0].spinActivate = true;
                                }
                                // coreGate[i].nextTime1Update = time1Interval + coreGate[i].updateTimeInterval;

                                //Tepaga yurish buyrug'i berilgandan
                                if (gateManipuller[i].active2Up)
                                {
                                    if (coreGate[i].upreporter1)
                                    {
                                        try
                                        {
                                            client.WriteSingleCoil(darvoza[i].Gate.Id, darvoza[i].Gate.GateControl.UpOnBit, true);
                                            darvoza[i].isActive = true;
                                        }
                                        catch (Exception ex)
                                        {
                                            plcErrorStatus = false;
                                            MessageBox.Show(ex.Message);
                                            this.Dispatcher.Invoke(new Action(delegate
                                            {
                                                    //PLC bilan aloqa yo'qotilganda unga so'rov jo'natganimizda try qismidagi kodda 
                                                    //xatolik yuz beradi. va catch qismiga o'tadi.
                                                    //Aynan shu yerda plcCore ishlashini to'xtatish uchun buyruq beriladi
                                                    //Shu bilan birga kutish animatsiyasi ko'rsatib qo'yiladi
                                                    //Kutish animatsiyasining ko'rinayotgani plcCore ishlamayotgani 
                                                    //va PLC bilan aloqa yo'qligini ko'rsatadi
                                                    // wait.Visible = true;
                                            }));
                                        }

                                        coreGate[i].upreporter1 = false;
                                        coreGate[i].stopreport1 = true;
                                        coreGate[i].updateTime1 = true;
                                        coreGate[i].timeIntervalChange = true;

                                    }

                                    if ((time1Interval > coreGate[i].nextTime1Up) & plcErrorStatus)
                                    {
                                        try
                                        {
                                            coreGate[i].nextTime1Up = time1Interval + 100;
                                            client.WriteSingleCoil(darvoza[i].Gate.Id, darvoza[i].Gate.GateControl.UpOnBit, true);
                                            darvoza[i].isActive = true;
                                        }
                                        catch (Exception ex)
                                        {
                                            plcErrorStatus = false;
                                            MessageBox.Show(ex.Message);
                                            this.Dispatcher.Invoke(new Action(delegate
                                            {
                                                    //PLC bilan aloqa yo'qotilganda unga so'rov jo'natganimizda try qismidagi kodda 
                                                    //xatolik yuz beradi. va catch qismiga o'tadi.
                                                    //Aynan shu yerda plcCore ishlashini to'xtatish uchun buyruq beriladi
                                                    //Shu bilan birga kutish animatsiyasi ko'rsatib qo'yiladi
                                                    //Kutish animatsiyasining ko'rinayotgani plcCore ishlamayotgani 
                                                    //va PLC bilan aloqa yo'qligini ko'rsatadi
                                                    //wait.Visible = true;

                                            }));
                                        }

                                    }
                                }
                                //Pastga yurish buyrug'i berilganda
                                if (gateManipuller[i].active2Down)
                                {
                                    if (coreGate[i].downreporter1 & plcErrorStatus)
                                    {
                                        try
                                        {
                                            client.WriteSingleCoil(darvoza[i].Gate.Id, darvoza[i].Gate.GateControl.DownOnBit, true);
                                            darvoza[i].isActive = true;
                                        }
                                        catch (Exception ex)
                                        {
                                            plcErrorStatus = false;
                                            MessageBox.Show(ex.Message);
                                            this.Dispatcher.Invoke(new Action(delegate
                                            {
                                                    //PLC bilan aloqa yo'qotilganda unga so'rov jo'natganimizda try qismidagi kodda 
                                                    //xatolik yuz beradi. va catch qismiga o'tadi.
                                                    //Aynan shu yerda plcCore ishlashini to'xtatish uchun buyruq beriladi
                                                    //Shu bilan birga kutish animatsiyasi ko'rsatib qo'yiladi
                                                    //Kutish animatsiyasining ko'rinayotgani plcCore ishlamayotgani 
                                                    //va PLC bilan aloqa yo'qligini ko'rsatadi
                                                    //wait.Visible = true;
                                            }));
                                        }
                                        coreGate[i].downreporter1 = false;
                                        coreGate[i].stopreport1 = true;
                                        coreGate[i].updateTime1 = true;
                                        coreGate[i].timeIntervalChange = true;


                                    }
                                    if (time1Interval > coreGate[i].nextTime1Down & plcErrorStatus)
                                    {
                                        try
                                        {
                                            coreGate[i].nextTime1Down = time1Interval + 100;
                                            client.WriteSingleCoil(darvoza[i].Gate.Id, darvoza[i].Gate.GateControl.DownOnBit, true);
                                            darvoza[i].isActive = true;
                                        }
                                        catch (Exception ex)
                                        {
                                            plcErrorStatus = false;
                                            MessageBox.Show(ex.Message);
                                            this.Dispatcher.Invoke(new Action(delegate
                                            {
                                                    //PLC bilan aloqa yo'qotilganda unga so'rov jo'natganimizda try qismidagi kodda 
                                                    //xatolik yuz beradi. va catch qismiga o'tadi.
                                                    //Aynan shu yerda plcCore ishlashini to'xtatish uchun buyruq beriladi
                                                    //Shu bilan birga kutish animatsiyasi ko'rsatib qo'yiladi
                                                    //Kutish animatsiyasining ko'rinayotgani plcCore ishlamayotgani 
                                                    //va PLC bilan aloqa yo'qligini ko'rsatadi
                                                    //wait.Visible = true;
                                            }));
                                        }

                                    }
                                }
                                //Stop buyrug'i berilganda
                                if (gateManipuller[i].deactivate2Stop & coreGate[i].stopreport1 & plcErrorStatus)
                                {

                                    try
                                    {
                                        client.WriteSingleCoil(darvoza[i].Gate.Id, darvoza[i].Gate.GateControl.DownOffBit, false);
                                        client.WriteSingleCoil(darvoza[i].Gate.Id, darvoza[i].Gate.GateControl.UpOffBit, false);
                                        darvoza[i].isActive = false;
                                    }
                                    catch (Exception ex)
                                    {
                                        plcErrorStatus = false;
                                        MessageBox.Show(ex.Message);
                                        this.Dispatcher.Invoke(new Action(delegate
                                        {
                                                //PLC bilan aloqa yo'qotilganda unga so'rov jo'natganimizda try qismidagi kodda 
                                                //xatolik yuz beradi. va catch qismiga o'tadi.
                                                //Aynan shu yerda plcCore ishlashini to'xtatish uchun buyruq beriladi
                                                //Shu bilan birga kutish animatsiyasi ko'rsatib qo'yiladi
                                                //Kutish animatsiyasining ko'rinayotgani plcCore ishlamayotgani 
                                                //va PLC bilan aloqa yo'qligini ko'rsatadi
                                                //wait.Visible = true;
                                        }));
                                    }

                                    coreGate[i].stopreport1 = false;
                                    coreGate[i].upreporter1 = true;
                                    coreGate[i].downreporter1 = true;
                                    coreGate[i].updateTime1 = false;
                                    coreGate[i].timeIntervalChange = true;

                                }
                            }
                        }
                        #region 1-darvoza indekatorlari 
                        if (coreGate[0].enabled & plcErrorStatus)
                        {
                            //Tegishli darvozani tekshirish algoritmlari
                            //Butun kod qismi Invoke ga qo'yilgan
                            //Sabab algoritm ichida bir necha bor interfeysga murojat qilinadi
                            //Shularni bitta qilib bitta invoke ga olingan
                            this.Dispatcher.Invoke(new Action(delegate
                            {
                                if (time1Interval > coreGate[0].nextTime1Update)
                                {
                                    coreGate[0].nextTime1Update = time1Interval + coreGate[0].updateTimeInterval;
                                    Span<byte> boolData = new Span<byte>();
                                    byte position = 0;
                                    if (plcErrorStatus)
                                    {
                                        try
                                        {
                                            boolData = client.ReadCoils(darvoza[0].Gate.Id, darvoza[0].Gate.GateControl.UpStop, 1);

                                            if (((boolData[0] >> position) & 1) > 0)
                                            {
                                                    //darvozalarda darvozaning maksimal balandlikka yoki pastlikga tushganini
                                                    //ko'rsatuvchi kansaveklar bor.
                                                    //Ushbu kansaveklar PLC ga ulangan va ularni tekshirish mumkin.
                                                    //Xuddi shu kansaveklar xolati progressbarlar orqali ko'rsatiladi
                                                    //Progressbar 100 qiymatga ega degani kansavek ishladi degani
                                                    //Progressbar 0 degani kansavek ishlab ketmagan va darvoza 
                                                    //Maksimal yoki minimal qiymatga yetmagan
                                                MaxHeight1.Background = new SolidColorBrush(Color.FromRgb(239, 83, 80));

                                                    //Bu qismda kansavek ishlaganda mator qurulmaviy jixatdan o'chadi
                                                    //Matorni boshqaradigan shit shunday qilinganki kansavek ishlasa
                                                    //Matorga to'k bormaydi.
                                                    //Ammo kansavek ishlasa ham PLC tomonidan ko'tarish buyrug'i o'z kuchida qolaveradi
                                                    //Bunga yo'l qo'ymaslik uchun kansaveklar PLC ga ulangan va kansavek ishlagani 
                                                    //haqida buyruq olingan zaxoti PLC ham ko'tarish yoki tushirish buyrug'ini bekor qiladi
                                                if (gateManipuller[0].active2Up)
                                                {
                                                    gateManipuller[0].active2Down = false;
                                                    gateManipuller[0].active2Up = false;
                                                    gateManipuller[0].deactivate2Stop = true;
                                                    gateManipullerEncoder[0].active2Down = false;
                                                    gateManipullerEncoder[0].active2Up = false;
                                                    gateManipullerEncoder[0].deactivate2Stop = true;

                                                    coreGate[0].StopUnlocker = false;
                                                }

                                            }
                                            else
                                            {
                                                MaxHeight1.Background = new SolidColorBrush(Color.FromRgb(3, 155, 229));
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                                //PLC dan ma'lumot olish uchun buyruq berganda javob qaytaradi
                                                //Shuning bilan birga PLC ga ko'tarish yoki tushirish buyrug'i berilganda ham
                                                //javob qaytaradi. Agar Buyruq berilganda javob qaytmasa demak PLC bilan aloqa 
                                                //yo'qotilgan. Aloqa yo'qotilgandagi senary amalga oshiriladi bu vaziyatda
                                            plcErrorStatus = false;
                                            MessageBox.Show(ex.Message);
                                                ///oConnectionWnd.Show();
                                        }
                                    }


                                    if (plcErrorStatus)
                                    {
                                        try
                                        {
                                            boolData = client.ReadCoils(darvoza[0].Gate.Id, darvoza[0].Gate.GateControl.DownStop, 1);
                                            position = 0;
                                            if (((boolData[0] >> position) & 1) > 0)
                                            {
                                                    //darvozalarda darvozaning maksimal balandlikka yoki pastlikga tushganini
                                                    //ko'rsatuvchi kansaveklar bor.
                                                    //Ushbu kansaveklar PLC ga ulangan va ularni tekshirish mumkin.
                                                    //Xuddi shu kansaveklar xolati progressbarlar orqali ko'rsatiladi
                                                    //Progressbar 100 qiymatga ega degani kansavek ishladi degani
                                                    //Progressbar 0 degani kansavek ishlab ketmagan va darvoza 
                                                    //Maksimal yoki minimal qiymatga yetmagan
                                                MinHeight1.Background = new SolidColorBrush(Color.FromRgb(239, 83, 80));
                                                    //Bu qismda kansavek ishlaganda mator qurulmaviy jixatdan o'chadi
                                                    //Matorni boshqaradigan shit shunday qilinganki kansavek ishlasa
                                                    //Matorga to'k bormaydi.
                                                    //Ammo kansavek ishlasa ham PLC tomonidan ko'tarish buyrug'i o'z kuchida qolaveradi
                                                    //Bunga yo'l qo'ymaslik uchun kansaveklar PLC ga ulangan va kansavek ishlagani 
                                                    //haqida buyruq olingan zaxoti PLC ham ko'tarish yoki tushirish buyrug'ini bekor qiladi
                                                if (gateManipuller[0].active2Down)
                                                {
                                                    gateManipuller[0].active2Down = false;
                                                    gateManipuller[0].active2Up = false;
                                                    gateManipuller[0].deactivate2Stop = true;
                                                    gateManipullerEncoder[0].active2Down = false;
                                                    gateManipullerEncoder[0].active2Up = false;
                                                    gateManipullerEncoder[0].deactivate2Stop = true;

                                                    coreGate[0].StopUnlocker = false;
                                                }
                                            }
                                            else
                                            {
                                                MinHeight1.Background = new SolidColorBrush(Color.FromRgb(3, 155, 229));
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                                //PLC dan ma'lumot olish uchun buyruq berganda javob qaytaradi
                                                //Shuning bilan birga PLC ga ko'tarish yoki tushirish buyrug'i berilganda ham
                                                //javob qaytaradi. Agar Buyruq berilganda javob qaytmasa demak PLC bilan aloqa 
                                                //yo'qotilgan. Aloqa yo'qotilgandagi senary amalga oshiriladi bu vaziyatda
                                            plcErrorStatus = false;
                                            MessageBox.Show(ex.Message);

                                        }
                                    }

                                    if (coreGate[0].statusCheck || !(coreGate[0].stopreport1))
                                    {
                                        if (plcErrorStatus)
                                        {
                                            try
                                            {
                                                    // Bu yerda PLC aktiv xolatda ya'ni ko'tarish yoki tushish xolatida yoki yo'qligi tekshiriladi
                                                    // Interfeys kompyuterdagi buyruq xolatini ko'tsatishga emas balki PLC xolatini ko'rsatishga asoslangan
                                                    // Bu degani buyruq beriladi va buyruqni PLC qabul qilgandagina interfeysda o'zgarish bo'ladi
                                                    // PLC xolati doimiy tekshirib turiladi va natija interfeysda aks etib turadi
                                                boolData = client.ReadCoils(darvoza[0].Gate.Id, darvoza[0].Gate.GateControl.DownCheckBit, 1);
                                                position = 0;
                                                coreGate[0].down = ((boolData[0] >> position) & 1) > 0;

                                                boolData = client.ReadCoils(darvoza[0].Gate.Id, darvoza[0].Gate.GateControl.UpCheckBit, 1);
                                                position = 0;
                                                coreGate[0].up = ((boolData[0] >> position) & 1) > 0;

                                                boolData = client.ReadCoils(28, 26, 1);
                                                position = 0;
                                                localStatus = ((boolData[0] >> position) & 1) > 0;

                                                boolData = client.ReadCoils(28, 27, 1);
                                                remoteStatus = ((boolData[0] >> position) & 1) > 0;

                                                if (localStatus)
                                                {

                                                }

                                                if (remoteStatus)
                                                {

                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                    //PLC dan ma'lumot olish uchun buyruq berganda javob qaytaradi
                                                    //Shuning bilan birga PLC ga ko'tarish yoki tushirish buyrug'i berilganda ham
                                                    //javob qaytaradi. Agar Buyruq berilganda javob qaytmasa demak PLC bilan aloqa 
                                                    //yo'qotilgan. Aloqa yo'qotilgandagi senary amalga oshiriladi bu vaziyatda
                                                plcErrorStatus = false;
                                                MessageBox.Show(ex.Message);

                                            }
                                        }

                                        coreGate[0].stop = !(coreGate[0].up || coreGate[0].down);
                                    }
                                    if (coreGate[0].StopUnlocker)
                                    {


                                        if (coreGate[0].up)
                                        {
                                                //Ushbu kod qismida PLC xolatini ko'tsatuvchi indekatorlar boshqariladi
                                                //Agar PLC ko'tarish xolatida bo'lsa statusPause1 o'chadi va o'rniga statusUp1 yonadi
                                                //Asosiy interfeysda bu o'zgarish tugmalarning enable va disable bo'lishi bilan ro'y berishi kerak
                                                //Agar PLC kotarish xolatida bo'lsa ko'tarish va pastga tushirish tugmalari disable bo'lib
                                                //stop tugmasi enable bo'lishi kerak
                                            btnUp1.IsEnabled = false;
                                            btnStop1.IsEnabled = true;
                                            btnDown1.IsEnabled = false;
                                            gateBlocker(1);
                                        }

                                        if (coreGate[0].down)
                                        {
                                                //Ushbu kod qismida PLC xolatini ko'tsatuvchi indekatorlar boshqariladi
                                                //Agar PLC tushirish xolatida bo'lsa statusPause1 o'chadi va o'rniga statusDown1 yonadi
                                                //Asosiy interfeysda bu o'zgarish tugmalarning enable va disable bo'lishi bilan ro'y berishi kerak
                                                //Agar PLC tushirish xolatida bo'lsa ko'tarish va pastga tushirish tugmalari disable bo'lib
                                                //stop tugmasi enable bo'lishi kerak
                                                // statusPause1.Visible = false;
                                                // statusDown1.Visible = true;
                                            btnUp1.IsEnabled = false;
                                            btnStop1.IsEnabled = true;
                                            btnDown1.IsEnabled = false;
                                            gateBlocker(1);
                                        }
                                    }

                                    if (coreGate[0].stop)
                                    {
                                            //Ushbu kod qismida PLC xolatini ko'tsatuvchi indekatorlar boshqariladi
                                            //Agar PLC stop xolatida bo'lsa statusPause1 yonadi va  statusDown1, statusUp1 lar o'chadi
                                            //Asosiy interfeysda bu o'zgarish tugmalarning enable va disable bo'lishi bilan ro'y berishi kerak
                                            //Agar PLC stop xolatida bo'lsa ko'tarish va pastga tushirish tugmalari enable bo'lib
                                            //stop tugmasi disable bo'lishi kerak
                                        if (darvoza[0].gateBlock)
                                        {
                                            btnUp1.IsEnabled = true;
                                            btnStop1.IsEnabled = false;
                                            btnDown1.IsEnabled = true;
                                            coreGate[0].StopUnlocker = true;
                                        }


                                    }
                                    if (gateManipuller[0].active2Up || gateManipuller[0].active2Down)
                                    {
                                        coreGate[0].statusCheck = false;
                                    }
                                    else
                                    {
                                        coreGate[0].statusCheck = true;
                                    }


                                }
                            }));
                        }
                        #endregion
                        #region 2-darvoza indekatorlari
                        if (coreGate[1].enabled & plcErrorStatus)
                        {
                            //Tegishli darvozani tekshirish algoritmlari
                            this.Dispatcher.Invoke(new Action(delegate
                            {
                                if (time1Interval > coreGate[1].nextTime1Update)
                                {
                                    coreGate[1].nextTime1Update = time1Interval + coreGate[1].updateTimeInterval;

                                    Span<byte> boolData = new Span<byte>();
                                    var position = 0;
                                    if (plcErrorStatus)
                                    {
                                        try
                                        {
                                            boolData = client.ReadCoils(darvoza[1].Gate.Id, darvoza[1].Gate.GateControl.DownStop, 1);
                                            if (((boolData[0] >> position) & 1) > 0)
                                            {
                                                MinHeight2.Background = new SolidColorBrush(Color.FromRgb(239, 83, 80));
                                                if (gateManipuller[1].active2Down)
                                                {
                                                    gateManipuller[1].active2Down = false;
                                                    gateManipuller[1].active2Up = false;
                                                    gateManipuller[1].deactivate2Stop = true;
                                                    gateManipullerEncoder[1].active2Down = false;
                                                    gateManipullerEncoder[1].active2Up = false;
                                                    gateManipullerEncoder[1].deactivate2Stop = true;
                                                    coreGate[1].StopUnlocker = false;
                                                        // goto stop2;
                                                }
                                            }
                                            else
                                            {
                                                MinHeight2.Background = new SolidColorBrush(Color.FromRgb(3, 155, 229));
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            plcErrorStatus = false;
                                            MessageBox.Show(ex.Message);

                                        }
                                    }
                                    if (plcErrorStatus)
                                    {
                                        try
                                        {
                                            boolData = client.ReadCoils(darvoza[1].Gate.Id, darvoza[1].Gate.GateControl.UpStop, 1);
                                            if (((boolData[0] >> position) & 1) > 0)
                                            {
                                                MaxHeight2.Background = new SolidColorBrush(Color.FromRgb(239, 83, 80));
                                                if (gateManipuller[1].active2Up)
                                                {
                                                    gateManipuller[1].active2Down = false;
                                                    gateManipuller[1].active2Up = false;
                                                    gateManipuller[1].deactivate2Stop = true;
                                                    gateManipullerEncoder[1].active2Down = false;
                                                    gateManipullerEncoder[1].active2Up = false;
                                                    gateManipullerEncoder[1].deactivate2Stop = true;
                                                    coreGate[1].StopUnlocker = false;
                                                        //goto stop2;
                                                }
                                            }
                                            else
                                            {
                                                MaxHeight2.Background = new SolidColorBrush(Color.FromRgb(3, 155, 229)); ;
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            plcErrorStatus = false;
                                            MessageBox.Show(ex.Message);

                                        }
                                    }
                                    position = 0;
                                    if (coreGate[1].statusCheck || !(coreGate[1].stopreport1))
                                    {
                                        if (plcErrorStatus)
                                        {
                                            try
                                            {
                                                boolData = client.ReadCoils(darvoza[1].Gate.Id, darvoza[1].Gate.GateControl.DownCheckBit, 1);
                                                position = 0;
                                                coreGate[1].down = ((boolData[0] >> position) & 1) > 0;

                                                boolData = client.ReadCoils(darvoza[1].Gate.Id, darvoza[1].Gate.GateControl.UpCheckBit, 1);
                                                position = 0;
                                                coreGate[1].up = ((boolData[0] >> position) & 1) > 0;
                                            }
                                            catch (Exception ex)
                                            {
                                                plcErrorStatus = false;
                                                MessageBox.Show(ex.Message);

                                            }

                                        }
                                        coreGate[1].stop = !(coreGate[1].up || coreGate[1].down);
                                    }

                                    if (coreGate[1].StopUnlocker)
                                    {


                                        if (coreGate[1].up)
                                        {
                                            btnUp2.IsEnabled = false;
                                            btnStop2.IsEnabled = true;
                                            btnDown2.IsEnabled = false;
                                            gateBlocker(2);
                                        }

                                        if (coreGate[1].down)
                                        {
                                            btnUp2.IsEnabled = false;
                                            btnStop2.IsEnabled = true;
                                            btnDown2.IsEnabled = false;
                                            gateBlocker(2);
                                        }
                                    }
                                        //stop2:
                                    if (coreGate[1].stop)
                                    {
                                        if (darvoza[1].gateBlock)
                                        {
                                            btnUp2.IsEnabled = true;
                                            btnStop2.IsEnabled = false;
                                            btnDown2.IsEnabled = true;
                                            coreGate[1].StopUnlocker = true;
                                        }

                                    }

                                    if (gateManipuller[1].active2Up || gateManipuller[1].active2Down)
                                    {
                                        coreGate[1].statusCheck = false;
                                    }
                                    else
                                    {
                                        coreGate[1].statusCheck = true;
                                    }
                                }

                            }));

                        }
                        #endregion
                    }
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!timer1.IsEnabled)
            {
                timer1.Tick += dispatcherTimer_Tick;
                timer1.Interval = new TimeSpan(0, 0, 0, 0, 1);

                timer2.Interval = new TimeSpan(0, 2, 0);
                timer2.Tick += dispatcherTimer_Tick3;
                timer3.Interval = new TimeSpan(0, 0, 0, 0, 1);
                timer3.Tick += timer1_Tick;
                timer1.Start();
                timer2.Start();
                timer3.Start();

            }

            thread = new Thread(() => { encoderUpdator(7); });
            thread2 = new Thread(() => { plcCore(7); });

            thread.IsBackground = true;
            thread2.IsBackground = true;
            thread.Start();
            thread2.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            time1Interval++;
        }

        private void dispatcherTimer_Tick3(object sender, EventArgs e)
        {
            plcErrorStatus = true;
            encoderErrorStatus = true;
        }

        private bool PLComConnect(string plcComport)
        {
            try
            {
                client.ReadTimeout = 100;
                client.StopBits = StopBits.One;
                client.Parity = Parity.None;
                client.BaudRate = 19200;
                client.Connect(plcComport);
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("PLC xato kiritilgan");
                return false;
            }


        }

        private void check1_Click(object sender, EventArgs e)
        {
            var boolData = client.ReadCoils(28, 26, 1);
            var position = 0;
            localStatus = ((boolData[0] >> position) & 1) > 0;

            boolData = client.ReadCoils(28, 27, 1);
            remoteStatus = ((boolData[0] >> position) & 1) > 0;

            if (localStatus)
            {
            }

            if (remoteStatus)
            {
            }
        }

        private bool EncodercomConnect(string encoderComport)
        {
            try
            {
                client2.ReadTimeout = 100;
                client2.StopBits = StopBits.One;
                client2.Parity = Parity.Even;
                client2.BaudRate = 19200;
                client2.Connect(encoderComport,
                    ModbusEndianness.BigEndian);
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("Encoder comport xato kiritilgan");
                return false;
            }

        }

        private void encoderUpdate_Click(object sender, EventArgs e)
        {
            // Encoder qiymatini oluvchi funksiya
            //Buni Enign yaratib shu orqali qiymat oladigan qilish kerak
            thread = new Thread(() => { encoderUpdator(7); });
            thread2 = new Thread(() => { plcCore(7); });

            thread.IsBackground = true;
            thread2.IsBackground = true;
            // thread.Start();
            // thread2.Start();

            /*
            Span<uint> floatData = client2.ReadInputRegisters<uint>(6, 1, 1);
            uint firstValue = floatData[0];
            encoderValue.Text = firstValue.ToString();
            */

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            thread4 = new Thread(timeUpdator);
            thread4.IsBackground = true;
            thread4.Start();

        }

        public void timeUpdator()
        {
            // time1Interval++;
            time1IntervalEncoder++;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            plcErrorStatus = true;
            encoderErrorStatus = true;
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            //  time1IntervalEncoder++;
        }

        private void btnInputHeight1_Click(object sender, RoutedEventArgs e)
        {
            absoluteEncoder[0].value = darvoza[0].encoderValue;
            int inputHeight = Int32.Parse(InputHeight1.Text);
            thread3 = new Thread(() => { controlTooth(0, true, absoluteEncoder[0].getStateForNextTooth(inputHeight)); });
            thread3.IsBackground = true;
            thread3.Start();
        }

        private void btnUp1_Click(object sender, RoutedEventArgs e)
        {
            gateManipuller[0].active2Up = true;
            gateManipuller[0].deactivate2Stop = false;
            gateManipullerEncoder[0].active2Up = true;
            gateManipullerEncoder[0].deactivate2Stop = false;



        }

        private void btnStop1_Click(object sender, RoutedEventArgs e)
        {
            gateManipuller[0].active2Up = false;
            gateManipuller[0].active2Down = false;
            gateManipuller[0].deactivate2Stop = true;
            gateManipullerEncoder[0].active2Up = false;
            gateManipullerEncoder[0].active2Down = false;
            gateManipullerEncoder[0].deactivate2Stop = true;
            gateUnBlocker();

        }

        private void btnDown1_Click(object sender, RoutedEventArgs e)
        {
            gateManipuller[0].active2Down = true;
            gateManipuller[0].deactivate2Stop = false;
            gateManipullerEncoder[0].active2Down = true;
            gateManipullerEncoder[0].deactivate2Stop = false;



        }

        private void btnInputHeight2_Click(object sender, RoutedEventArgs e)
        {
            absoluteEncoder[1].value = darvoza[1].encoderValue;
            int inputHeight = Int32.Parse(InputHeight2.Text);
            thread3 = new Thread(() => { controlTooth(1, true, absoluteEncoder[1].getStateForNextTooth(inputHeight)); });
            thread3.IsBackground = true;
            thread3.Start();
        }

        private void btnUp2_Click(object sender, RoutedEventArgs e)
        {
            gateManipuller[1].active2Up = true;
            gateManipuller[1].deactivate2Stop = false;
            gateManipullerEncoder[1].active2Up = true;
            gateManipullerEncoder[1].deactivate2Stop = false;


        }

        private void btnStop2_Click(object sender, RoutedEventArgs e)
        {
            gateManipuller[1].active2Up = false;
            gateManipuller[1].active2Down = false;
            gateManipuller[1].deactivate2Stop = true;
            gateManipullerEncoder[1].active2Up = false;
            gateManipullerEncoder[1].active2Down = false;
            gateManipullerEncoder[1].deactivate2Stop = true;
            gateUnBlocker();
        }

        private void btnDown2_Click(object sender, RoutedEventArgs e)
        {
            gateManipuller[1].active2Down = true;
            gateManipuller[1].deactivate2Stop = false;
            gateManipullerEncoder[1].active2Down = true;
            gateManipullerEncoder[1].deactivate2Stop = false;

        }
    }
}
