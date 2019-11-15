using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using HTMLConverter;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;

namespace coinBlock.Model
{
    public class BrowserBehavior
    {
        public static readonly DependencyProperty HtmlProperty = DependencyProperty.RegisterAttached(
                "Html",
                typeof(string),
                typeof(BrowserBehavior),
                new FrameworkPropertyMetadata(OnHtmlChanged));

        [AttachedPropertyBrowsableForType(typeof(WebBrowser))]
        public static string GetHtml(WebBrowser d)
        {
            try
            {
                return (string)d.GetValue(HtmlProperty);
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
                return string.Empty;
            }
        }

        public static void SetHtml(WebBrowser d, string value)
        {
            try
            {
                d.SetValue(HtmlProperty, value);
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        static void OnHtmlChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                WebBrowser webBrowser = dependencyObject as WebBrowser;
                if (webBrowser != null)
                    webBrowser.NavigateToString(e.NewValue as string ?? "&nbsp;");
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
    }

    public class HtmlRichTextBoxBehavior : DependencyObject
    {
        public static readonly DependencyProperty TextProperty =
          DependencyProperty.RegisterAttached("Text", typeof(string),
          typeof(HtmlRichTextBoxBehavior), new UIPropertyMetadata(null, OnValueChanged));

        public static string GetText(RichTextBox o) { return (string)o.GetValue(TextProperty); }

        public static void SetText(RichTextBox o, string value) { o.SetValue(TextProperty, value); }

        private static void OnValueChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                var richTextBox = (RichTextBox)dependencyObject;
                var text = (e.NewValue ?? string.Empty).ToString();
                var xaml = HtmlToXamlConverter.ConvertHtmlToXaml(text, true);
                var flowDocument = XamlReader.Parse(xaml) as FlowDocument;
                HyperlinksSubscriptions(flowDocument);
                richTextBox.Document = flowDocument;
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        private static void HyperlinksSubscriptions(FlowDocument flowDocument)
        {
            try
            {
                if (flowDocument == null) return;
                GetVisualChildren(flowDocument).OfType<Hyperlink>().ToList().ForEach(i => i.RequestNavigate += HyperlinkNavigate);
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        private static IEnumerable<DependencyObject> GetVisualChildren(DependencyObject root)
        {
            foreach (var child in LogicalTreeHelper.GetChildren(root).OfType<DependencyObject>())
            {
                yield return child;
                foreach (var descendants in GetVisualChildren(child))
                    yield return descendants;
            }
        }

        private static void HyperlinkNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
                e.Handled = true;
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
    }
}
