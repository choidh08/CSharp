using coinBlock.Utility;
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

namespace coinBlock.Views.Common
{
    /// <summary>
    /// CommonPopup_Sub.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CommonPopup_Sub : UserControl
    {
        System.Windows.Forms.WebBrowser web = new System.Windows.Forms.WebBrowser();

        public CommonPopup_Sub(string imagePath)
        {
            try
            {
                InitializeComponent();
                imgView.Source = CommonLib.GetNoticePop(imagePath);
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
    }
}
