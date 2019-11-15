using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyHtml.Wpf;

namespace coinBlock.Utility
{
    public class TinyHtml : WpfHtmlControl
    {
        protected override Stream OnLoadResource(string url)
        {
            try
            {
                return typeof(WpfHtmlControl).Assembly.GetManifestResourceStream(typeof(WpfHtmlControl), url);
            }
            catch (Exception)
            {
                return null;
            }
        }

        protected override void OnAnchorClicked(string url)
        {
            Process.Start(url);
        }
    }
}
