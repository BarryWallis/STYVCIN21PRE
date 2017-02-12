using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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

namespace Dialogs
{
    /// <summary>
    /// Interaction logic for MessageDialog.xaml
    /// </summary>
    public partial class MessageDialog : Window, INotifyPropertyChanged
    {
        private bool isStartupMessageShown = true;

        private MessageBoxResult _messageBoxResult = MessageBoxResult.None;
        public MessageBoxResult MessageBoxResult { get => _messageBoxResult; }

        private int _optionButtonChecked;
        public int OptionButtonChecked { get => _optionButtonChecked; }

        private string _startupMessage = "Enter message here...";
        public string StartupMessage
        {
            get { return _startupMessage; }
            set
            {
                _startupMessage = value;
                NotifyPropertyChanged();
            }
        }

        public MessageDialog()
        {
            InitializeComponent();

            DataContext = this;
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
            if (!int.TryParse(radioButton.Tag as string, out _optionButtonChecked))
            {
                Debug.Fail($"{nameof(radioButton.Tag)} must be an integer");
            }
        }

        private void MessageTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (isStartupMessageShown)
            {
                StartupMessage = "";
                isStartupMessageShown = false;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property. 
        // The CallerMemberName attribute that is applied to the optional propertyName 
        // parameter causes the property name of the caller to be substituted as an argument. 
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void YesNoCancelButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Press Yes, No or Cancel", "Yes, No, Cancel", MessageBoxButton.YesNoCancel);
            switch (messageBoxResult)
            {
                case MessageBoxResult.None:
                    Debug.Fail($"Unexpected {nameof(_messageBoxResult)} of None");
                    break;
                case MessageBoxResult.OK:
                    Debug.Fail($"Unexpected {nameof(_messageBoxResult)} of OK");
                    break;
                default:
                    _messageBoxResult = messageBoxResult;
                    break;
            }
        }
    }
}
