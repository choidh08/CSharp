﻿using System;
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

namespace coinBlock.Views.AdditionalService.Popup
{
    /// <summary>
    /// Interaction logic for ArbitragePopAdditionalService.xaml
    /// </summary>
    public partial class ArbitragePopAdditionalService : Window
    {
        public ArbitragePopAdditionalService()
        {
            InitializeComponent();
        }

        private void uiExit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.DialogResult = false;
                this.Close();
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
    }
}
