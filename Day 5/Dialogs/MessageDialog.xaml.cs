using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace Dialogs
{
    /// <summary>
    /// Interaction logic for MessageDialog.xaml
    /// </summary>
    public partial class MessageDialog : Window
    {
        private int optionButtonChecked;
        public int OptionButtonChecked { get => optionButtonChecked; }

        public MessageDialog()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void OptionRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton is null)
                throw new ArgumentException("Argument must be RadioButton", nameof(sender));

            Debug.Assert(radioButton.Tag is string);
            if (!int.TryParse(radioButton.Tag as string, out optionButtonChecked))
            {
                Debug.Fail($"{nameof(radioButton.Tag)} must be an integer");
            }
        }
    }
}
