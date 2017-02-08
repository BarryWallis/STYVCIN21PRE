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

namespace Mouse
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double previousX = 0;
        double previousY = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e is null)
                throw new ArgumentNullException(nameof(e));

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                double x = e.MouseDevice.GetPosition(this).X;
                double y = e.MouseDevice.GetPosition(this).Y;
                Line line = new Line()
                {
                    X1 = previousX,
                    Y1 = previousY,
                    X2 = x,
                    Y2 = y,
                    SnapsToDevicePixels = true,
                    Stroke = Brushes.Black,
                    StrokeThickness = 1,
                };
                Canvas.Children.Add(line);
                previousX = x;
                previousY = y;
            }
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            previousX = e.MouseDevice.GetPosition(this).X;
            previousY = e.MouseDevice.GetPosition(this).Y;
        }
    }
}
