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
    /// Interaction logic for NoticePopup.xaml
    /// </summary>
    public partial class NoticePopup : Window
    {
        public enum KindNotice
        {
            Notice,
            ReCharge,
            HTS_IP_CHECK_1,
            HTS_IP_CHECK_2,
            HTS_IP_NOT_CHECK_1
        }

        public NoticePopup()
        {
            InitializeComponent();
        }

        public NoticePopup(KindNotice kn)
        {
           InitializeComponent();
            if (kn.Equals(KindNotice.Notice))
            {
                this.Width = 605;
                this.Height = 380;
                Notice.Visibility = Visibility.Visible;
            }
            else if (kn.Equals(KindNotice.ReCharge))
            {
                this.Width = 620;
                this.Height = 362;
                ReCharge.Visibility = Visibility.Visible;
            }
            else if (kn.Equals(KindNotice.HTS_IP_CHECK_1))
            {
                this.Width = 428;
                this.Height = 200;
                HTS_IP_CHECK_1.Visibility = Visibility.Visible;
            }
            else if (kn.Equals(KindNotice.HTS_IP_CHECK_2))
            {
                this.Width = 428;
                this.Height = 200;
                HTS_IP_CHECK_2.Visibility = Visibility.Visible;
            }
            else if (kn.Equals(KindNotice.HTS_IP_NOT_CHECK_1))
            {
                this.Width = 428;
                this.Height = 200;
                HTS_IP_NOT_CHECK_1.Visibility = Visibility.Visible;
            }
        }

        private void HyperlinkEdit_RequestNavigation(object sender, DevExpress.Xpf.Editors.HyperlinkEditRequestNavigationEventArgs e)
        {
            string emailUri = "http://devbitkrx.noryweb.com/download/bitkrx_gide_v1.0.pdf";
            e.NavigationUrl = emailUri;
            e.Handled = true;
        }

        private void uiConfirm_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void uiExit_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
