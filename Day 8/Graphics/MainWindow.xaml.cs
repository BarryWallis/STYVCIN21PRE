using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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
using Microsoft.Win32;

namespace Graphics
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PaintWindow paintWindow = new PaintWindow();
        private SolidColorBrush brush = blackBrush;
        private System.Windows.Controls.Image image = null;
        private RadioButton currentToolRadioButton = null;

        private static SolidColorBrush blackBrush = new SolidColorBrush(Colors.Black);
        private static SolidColorBrush blueBrush = new SolidColorBrush(Colors.Blue);
        private static SolidColorBrush greenBrush = new SolidColorBrush(Colors.Green);
        private static SolidColorBrush cyanBrush = new SolidColorBrush(Colors.Cyan);
        private static SolidColorBrush redBrush = new SolidColorBrush(Colors.Red);
        private static SolidColorBrush magentaBrush = new SolidColorBrush(Colors.Magenta);
        private static SolidColorBrush yellowBrush = new SolidColorBrush(Colors.Yellow);
        private static SolidColorBrush whiteBrush = new SolidColorBrush(Colors.White);

        public MainWindow()
        {
            InitializeComponent();

            Activated += OnActivated;
            paintWindow.Show();
            currentToolRadioButton = PenRadioButton;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OnActivated(object sender, EventArgs eventArgs)
        {
            paintWindow.Owner = this;
            Activated -= OnActivated;
        }

        private void ColorChanged(object sender, RoutedEventArgs e)
        {
            if (sender is null)
                throw new ArgumentNullException(nameof(sender));

            RadioButton radioButton = sender as RadioButton;
            Debug.Assert(sender != null);

            switch (radioButton.Name)
            {
                case "BlackRadioButton":
                    brush = blackBrush;
                    break;
                case "BlueRadioButton":
                    brush = blueBrush;
                    break;
                case "GreenRadioButton":
                    brush = greenBrush;
                    break;
                case "CyanRadioButton":
                    brush = cyanBrush;
                    break;
                case "RedRadioButton":
                    brush = redBrush;
                    break;
                case "MagentaRadioButton":
                    brush = magentaBrush;
                    break;
                case "YellowRadioButton":
                    brush = yellowBrush;
                    break;
                case "WhiteRadioButton":
                    brush = whiteBrush;
                    break;
                default:
                    Debug.Fail("Unexpected brush found");
                    break;
            }

            Draw();
        }

        private void ShapeChanged(object sender, RoutedEventArgs e)
        {
            Draw();
        }

        private void ToolChanged(object sender, RoutedEventArgs e)
        {
            if (sender is null)
                throw new ArgumentNullException(nameof(sender));

            Draw();
            currentToolRadioButton = sender as RadioButton;
            Debug.Assert(currentToolRadioButton != null);
        }

        private void Draw()
        {
            if (LineRadioButton is null)
                return;

            if (BitmapRadioButton.IsChecked == true)
            {
                if (image is null)
                {
                    MessageBox.Show("No image file selected");
                    currentToolRadioButton.IsChecked = true;
                }
                else
                    paintWindow.DrawImage(image);
            }
            else if (LineRadioButton.IsChecked == true)
                paintWindow.DrawLines(brush);
            else if (SquareRadioButton.IsChecked == true)
                paintWindow.DrawShapes(brush, TheShape.Square, BrushRadioButton.IsChecked == true);
            else if (CircleRadioButton.IsChecked == true)
                paintWindow.DrawShapes(brush, TheShape.Circle, BrushRadioButton.IsChecked == true);
            else
                Debug.Fail("Shape radio button not found");
        }

        private void BitmapButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                DefaultExt = ".bmp",
                Filter = "Image files|*.bmp;*.gif;*.ico;*.jpg;*.png;*.wdp;*tiff",
                InitialDirectory = "Pictures",
                CheckPathExists = true,
            };
            if (openFileDialog.ShowDialog() == true)
            {
                image = new System.Windows.Controls.Image();
                BitmapImage sourceImage = new BitmapImage();
                sourceImage.BeginInit();
                sourceImage.UriSource = new Uri(openFileDialog.FileName);
                sourceImage.EndInit();
                image.Source = sourceImage;
                image.Stretch = Stretch.Uniform;
                Draw();
            }
        }
    }
}
