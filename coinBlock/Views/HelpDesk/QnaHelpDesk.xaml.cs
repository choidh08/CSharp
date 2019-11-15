using System.Windows.Controls;
using coinBlock.Model;
using coinBlock.Utility;
using System;
using coinBlock.ViewModels;
using System.Windows.Markup;

namespace coinBlock.Views.HelpDesk
{
    /// <summary>
    /// Interaction logic for QnaHelpDesk.xaml
    /// </summary>
    public partial class QnaHelpDesk : UserControl
    {
        public QnaHelpDesk()
        {
            InitializeComponent();

            dpFrom.Language = XmlLanguage.GetLanguage(System.Threading.Thread.CurrentThread.CurrentUICulture.TextInfo.CultureName);
            dpTo.Language = XmlLanguage.GetLanguage(System.Threading.Thread.CurrentThread.CurrentUICulture.TextInfo.CultureName);
        }      
    }
}
