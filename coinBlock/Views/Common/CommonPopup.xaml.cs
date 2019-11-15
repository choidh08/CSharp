using DevExpress.Mvvm;
using coinBlock.Model;
using coinBlock.Model.Common;
using coinBlock.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace coinBlock.Views.Common
{
    /// <summary>
    /// Interaction logic for CommonPopup.xaml
    /// </summary>
    public partial class CommonPopup : Window
    {
        System.Windows.Forms.WebBrowser web = new System.Windows.Forms.WebBrowser();

        public CommonPopup(ObservableCollection<ResponseGetPopupListListModel> list)
        {
            try
            {
                InitializeComponent();

                List<CommonPopup_Sub> temp = new List<CommonPopup_Sub>();

                foreach (ResponseGetPopupListListModel item in list)
                {
                    temp.Add(new CommonPopup_Sub(item.popUrl));
                }

                uiFlipView.ItemsSource = temp;
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public CommonPopup(string imagePath, int width, int height)
        {
            try
            {
                InitializeComponent();

                string sPath = imagePath;
                Uri imgUrl = new Uri(sPath, UriKind.Relative);
                BitmapImage imgBitmap = new BitmapImage(imgUrl);
                Image img = new Image();
                img.Source = imgBitmap;

                //imgView.Source = img.Source;
                //this.Width = width;
                //this.Height = height;
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void uiExit_Click(object sender, RoutedEventArgs e)
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

        private void uiFlipView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Messenger.Default.Send(new MenuModel() { Name = LocalizationLib.GetLocalizaionString("Main_Menu_5_1"), Id = "NoticeHelpDesk", IconPath = "/Images/ico_nav_notice.png" });
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
    }
}
