using coinBlock.Model;
using coinBlock.Utility;
using coinBlock.ViewModels;
using DevExpress.Mvvm;
using System;
using System.Windows.Controls;
using System.Windows.Markup;

namespace coinBlock.Views.MyPage
{
    /// <summary>
    /// Interaction logic for AssetsMyPage.xaml
    /// </summary>

    public partial class AssetsMyPage : UserControl
    {
        public AssetsMyPage()
        {
            InitializeComponent();

            dpFrom.Language = XmlLanguage.GetLanguage(System.Threading.Thread.CurrentThread.CurrentUICulture.TextInfo.CultureName);
            dpTo.Language = XmlLanguage.GetLanguage(System.Threading.Thread.CurrentThread.CurrentUICulture.TextInfo.CultureName);
        }
    }
}

