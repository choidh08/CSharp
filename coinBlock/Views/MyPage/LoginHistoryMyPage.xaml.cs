using coinBlock.Model;
using coinBlock.Utility;
using coinBlock.ViewModels;
using System;
using System.Windows.Controls;
using System.Windows.Markup;

namespace coinBlock.Views.MyPage
{
    /// <summary>
    /// Interaction logic for LoginHistoryMyPage.xaml
    /// </summary>
    public partial class LoginHistoryMyPage : UserControl
    {
        public LoginHistoryMyPage()
        {
            InitializeComponent();

            dpFrom.Language = XmlLanguage.GetLanguage(System.Threading.Thread.CurrentThread.CurrentUICulture.TextInfo.CultureName);
            dpTo.Language = XmlLanguage.GetLanguage(System.Threading.Thread.CurrentThread.CurrentUICulture.TextInfo.CultureName);
        }
    }
}
