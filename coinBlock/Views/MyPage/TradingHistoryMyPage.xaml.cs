using System.Windows.Controls;
using coinBlock.Model;
using coinBlock.Utility;
using System;
using coinBlock.ViewModels;
using System.Windows.Markup;

namespace coinBlock.Views.MyPage
{
    /// <summary>
    /// Interaction logic for TradingHistoryMyPage.xaml
    /// </summary>
    public partial class TradingHistoryMyPage : UserControl
    {
        public TradingHistoryMyPage()
        {
            InitializeComponent();

            dpFrom.Language = XmlLanguage.GetLanguage(System.Threading.Thread.CurrentThread.CurrentUICulture.TextInfo.CultureName);
            dpTo.Language = XmlLanguage.GetLanguage(System.Threading.Thread.CurrentThread.CurrentUICulture.TextInfo.CultureName);
        }        
    }
}
