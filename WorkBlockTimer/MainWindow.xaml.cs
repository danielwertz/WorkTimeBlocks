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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WorkBlockTimer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer dispatcherTimer;
        private DispatcherTimer breakDispatcherTimer;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void WorkStartAndPauseButton_Click(object sender, RoutedEventArgs e)
        {
            ClearWarning();
            if (CheckFormat())
            {
                return;
            }

            var content = WorkStartAndPauseButton.Content.ToString();

            if(content == "Start")
            {
                Timer();
                WorkStartAndPauseButton.Content = "Pause";
            }
            else if(content == "Pause")
            {
                dispatcherTimer.Stop();
                dispatcherTimer = null;
                WorkStartAndPauseButton.Content = "Start";
            }
        }

        private void BreakStartAndPauseButton_Click(object sender, RoutedEventArgs e)
        {
            var content = BreakStartAndPauseButton.Content.ToString();

            if (content == "Start")
            {
                BreakTimer();
                BreakStartAndPauseButton.Content = "Pause";
            }
            else if (content == "Pause")
            {
                breakDispatcherTimer.Stop();
                breakDispatcherTimer = null;
                BreakStartAndPauseButton.Content = "Start";
            }
            
        }

        private void WorkClearButton_Click(object sender, RoutedEventArgs e)
        {
            if (dispatcherTimer != null)
                dispatcherTimer.Stop();
            WorkTextBox.Text = "0:00";
            WorkStartAndPauseButton.Content = "Start";
        }

        private void BreakClearButton_Click(object sender, RoutedEventArgs e)
        {
            if (breakDispatcherTimer != null)
                breakDispatcherTimer.Stop();
            BreakTextBox.Text = "0:00";
            BreakStartAndPauseButton.Content = "Start";
        }

        private void Timer()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();


        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            var time = WorkTextBox.Text;
            string[] timeSplit = time.Split(':');
            if (time.Contains(":"))
            {
                if (timeSplit[1] != "00")
                {
                    var timeInt = Convert.ToInt16(timeSplit[1]);
                    timeInt--;
                    if (timeInt < 10)
                    {
                        timeSplit[1] = $"0{timeInt.ToString()}";
                    }
                    else
                    {
                        timeSplit[1] = timeInt.ToString();
                    }

                    WorkTextBox.Text = $"{timeSplit[0]}:{timeSplit[1]}";
                }
                else if (timeSplit[0] != "0")
                {
                    var timeHour = Convert.ToInt16(timeSplit[0]);
                    timeHour--;
                    timeSplit[1] = "59";
                    timeSplit[0] = timeHour.ToString();
                    WorkTextBox.Text = $"{timeSplit[0]}:{timeSplit[1]}";
                }
                else
                {
                    if (WorkTextBox.Text == "0:00")
                    {
                        dispatcherTimer.Stop();
                        dispatcherTimer = null;
                    }
                    if (!WorkTextBox.Text.Contains(":"))
                        return;
                }
            }
        }


        private void BreakTimer()
        {
            breakDispatcherTimer = new DispatcherTimer();
            breakDispatcherTimer.Tick += dispatcherTimerBreak_Tick;
            breakDispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            breakDispatcherTimer.Start();
        }

        private void dispatcherTimerBreak_Tick(object sender, EventArgs e)
        {
            var time = BreakTextBox.Text;
            string[] timeSplit = time.Split(':');
            if (time.Contains(":"))
            {
                if (timeSplit[1] != "00")
                {
                    var timeInt = Convert.ToInt16(timeSplit[1]);
                    timeInt--;
                    if (timeInt < 10)
                    {
                        timeSplit[1] = $"0{timeInt.ToString()}";
                    }
                    else
                    {
                        timeSplit[1] = timeInt.ToString();
                    }

                    BreakTextBox.Text = $"{timeSplit[0]}:{timeSplit[1]}";
                }
                else if (timeSplit[0] != "0")
                {
                    var timeHour = Convert.ToInt16(timeSplit[0]);
                    timeHour--;
                    timeSplit[1] = "59";
                    timeSplit[0] = timeHour.ToString();
                    BreakTextBox.Text = $"{timeSplit[0]}:{timeSplit[1]}";
                }
                else
                {
                    if (BreakTextBox.Text == "0:00")
                    {
                        breakDispatcherTimer.Stop();
                        breakDispatcherTimer = null;
                    }
                    if (!BreakTextBox.Text.Contains(":"))
                        return;
                }
            }
        }

        private bool CheckFormat()
        {
            if (!BreakTextBox.Text.Contains(":"))
            {
                WarningGrid.Visibility = Visibility.Visible;
                WarningTextBox.Text = "Incorrect formate. Example of correct format = mm:ss or 23:32.";
                return true;
            }
            if (!WorkTextBox.Text.Contains(":"))
            {
                WarningGrid.Visibility = Visibility.Visible;
                WarningTextBox.Text = "Incorrect formate. Example of correct format = mm:ss or 23:32.";
                return true;
            }
            return false;
        }
        private void ClearWarning()
        {
            WarningGrid.Visibility = Visibility.Hidden;
        }
    }
}
