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

namespace Timers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timerDispatchTimer = new DispatcherTimer();
        DispatcherTimer countDispatchTimer = new DispatcherTimer();
        int interval = 100;
        int count = 0;

        public MainWindow()
        {
            InitializeComponent();

            IntervalTextBox.Text = interval.ToString();
 
            timerDispatchTimer.Tick += TimerDispatchTimer_Tick;
            timerDispatchTimer.Interval = new TimeSpan(0, 0, 1);
            timerDispatchTimer.Start();
       }

        private void TimerDispatchTimer_Tick(object sender, EventArgs e) => TimeTextBlock.Text = $"{(DateTime.Now).ToString("H:mm:ss")}";

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void StartTimerButton_Click(object sender, RoutedEventArgs e)
        {
            count = 0;
            CountTextBlock.Text = count.ToString();

            countDispatchTimer.Tick += CountDispatchTimer_Tick;
            countDispatchTimer.Interval = new TimeSpan(0, 0, 0, 0, interval);
            countDispatchTimer.Start();

            StartTimerButton.IsEnabled = false;
            StopTimerButton.IsEnabled = true;
        }

        private void CountDispatchTimer_Tick(object sender, EventArgs e)
        {
            CountTextBlock.Text = (++count).ToString();
        }

        private void StopTimer_Click(object sender, RoutedEventArgs e)
        {
            countDispatchTimer.Stop();

            StartTimerButton.IsEnabled = true;
            StopTimerButton.IsEnabled = false;
        }
    }
}
