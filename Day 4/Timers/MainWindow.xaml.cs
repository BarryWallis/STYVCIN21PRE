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
        int count;

        public MainWindow()
        {
            InitializeComponent();

            timerDispatchTimer.Tick += TimerDispatchTimer_Tick;
            timerDispatchTimer.Interval = new TimeSpan(0, 0, 1);
            timerDispatchTimer.Start();

            countDispatchTimer.Tick += CountDispatchTimer_Tick;
        }

        private void TimerDispatchTimer_Tick(object sender, EventArgs e) => TimeTextBlock.Text = $"{(DateTime.Now).ToString("H:mm:ss")}";

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void StartTimerButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(IntervalTextBox.Text, out int interval) && interval > 0)
            {
                count = 0;
                CountTextBlock.Text = count.ToString();
                StartTimer(interval);
            }
            else
                MessageBox.Show("Interval must be a positive integer");

        }

        /// <summary>
        /// Start the count timer.
        /// </summary>
        /// <param name="interval">The interval to update the timer in millliseconds</param>
        private void StartTimer(int interval)
        {
            countDispatchTimer.Interval = new TimeSpan(0, 0, 0, 0, interval);
            countDispatchTimer.Start();
            timerDispatchTimer.Interval = new TimeSpan(0, 0, 0, 0, interval);
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
            timerDispatchTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000);

            StartTimerButton.IsEnabled = true;
            StopTimerButton.IsEnabled = false;
        }
    }
}
