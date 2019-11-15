using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace coinBlock.Views
{
    /// <summary>
    /// Interaction logic for Alert.xaml
    /// </summary>
    public partial class Alert : Window
    {
        int _width = 280;
        int _height = 140;
        ButtonType _bt;

        public enum ButtonType
        {
            Ok, YesNo
        }

        public Alert()
        {
            InitializeComponent();
        }
        public Alert(string Message)
        {
            InitializeComponent();
            Alert_Sub(Message, ButtonType.Ok, _width, _height);
        }
        public Alert(string Message, ButtonType bt)
        {
            InitializeComponent();
            Alert_Sub(Message, bt, _width, _height);
        }

        public Alert(string Message, int width)
        {
            InitializeComponent();
            Alert_Sub(Message, ButtonType.Ok, width, _height);
        }

        public Alert(string Message, ButtonType bt, int width, int height)
        {
            InitializeComponent();
            Alert_Sub(Message, bt, width, height);
        }

        public Alert(string Message, int width, int height)
        {
            InitializeComponent();
            Alert_Sub(Message, ButtonType.Ok, width, height);
        }

        public Alert(string Message, int width, int height, string text)
        {
            InitializeComponent();
            Alert_Sub(Message, ButtonType.Ok, width, height, text);
        }

        public Alert(string Message, ButtonType bt, int width)
        {
            InitializeComponent();
            Alert_Sub(Message, bt, width, _height);
        }

        public void Alert_Sub(string Message, ButtonType bt, int width, int height)
        {
            Alert_Sub(Message, bt, width, height, string.Empty);
        }

        public void Alert_Sub(string Message, ButtonType bt, int width, int height, string text)
        {
            txtMessage.TextWrapping = TextWrapping.Wrap;
            txtMessage.Text = Message;
            _bt = bt;
            this.Width = width;
            this.Height = height;
            if(string.IsNullOrWhiteSpace(text))
            {
                Ok.Content = Localization.Resource.Alert_2;
            }
            else
            {
                Ok.Content = text;
            }

            if (bt == ButtonType.Ok)
            {
                Ok.Visibility = Visibility.Visible;
                Yes.Visibility = Visibility.Hidden;
                No.Visibility = Visibility.Hidden;
            }
            else
            {
                Ok.Visibility = Visibility.Hidden;
                Yes.Visibility = Visibility.Visible;
                No.Visibility = Visibility.Visible;
            }

            this.KeyDown += Alert_KeyDown;
        }

        private void Alert_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Space)
            {
                this.DialogResult = true;
                this.Close();
            }
            else if (e.Key == Key.Escape)
            {
                if (_bt == ButtonType.Ok)
                {
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    this.DialogResult = false;
                    this.Close();
                }
            }
        }

        private void uiExit_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
