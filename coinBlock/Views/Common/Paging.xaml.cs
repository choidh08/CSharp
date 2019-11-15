using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Paging.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Paging : UserControl
    {
        public delegate void PageChangedEventHandler(object sender, PageingDataEventArgs e);
        public event PageChangedEventHandler PageChanged;

        public virtual ObservableCollection<clsPage> PagingData { get; set; }
        public static readonly DependencyProperty myDependency2 = DependencyProperty.Register("_nowNum", typeof(int), typeof(Paging), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnText2ChangePropertyChanged)));
        public static readonly DependencyProperty myDependency = DependencyProperty.Register("LastNum", typeof(int), typeof(Paging), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnTextChangePropertyChanged)));

        public string BaseUrl { get; set; }
        public int _nowNum { get; set; } = 1;
        public int _firstNum { get; set; }
        public int LastNum
        {
            get
            {
                return int.Parse(GetValue(myDependency).ToString());
            }
            set
            {
                SetValue(myDependency, value);
            }
        }
        public int _intervalFirstNum { get; set; }
        public int _intervalLastNum { get; set; }
        public int _pageNum { get; set; } = 1;
        public int _termNum { get; set; } = 10;
        public int _PrevNexNum { get; set; } = 1;        

        public Paging()
        {
            InitializeComponent();
        }

        private static void OnTextChangePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                if (e.OldValue != e.NewValue)
                {
                    Paging p = d as Paging;
                    p.SetPaging(p._firstNum, p._pageNum, p.LastNum);
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        private static void OnText2ChangePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            try
            {               
                Paging p = d as Paging;
                p.CmdNumber(int.Parse(e.NewValue.ToString()));                
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void SetPaging(int first, int page, int last)
        {
            try
            {
                _intervalFirstNum = first;
                _intervalLastNum = last;
                _pageNum = page;

                if (last >= page * _termNum)
                {
                    _intervalLastNum = page * _termNum;
                }
                else
                {
                    _intervalLastNum = LastNum;
                }

                PagingData = new ObservableCollection<clsPage>();

                for (int i = _intervalFirstNum; i <= _intervalLastNum; i++)
                {
                    PagingData.Add(ViewModelSource.Create(() => new clsPage { iNum = i, sBackColor = "#FFF", sForeColor = "#FFF" }));
                }

                itemCon.ItemsSource = PagingData;
                if (!PagingData.Count.Equals(0))
                {
                    if (PagingData.Count.Equals(1))
                    {
                        CmdNumber(1);
                    }
                    else
                    {
                        CmdNumber(_nowNum);
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void CmdNumber(int content)
        {
            try
            {
                if (PagingData != null)
                {
                    foreach (var item in PagingData)
                    {
                        if (item.iNum.Equals(content))
                        {
                            item.sBackColor = "#3e4b5e";
                            item.sForeColor = "#FFF";

                            _nowNum = item.iNum;
                        }
                        else
                        {
                            item.sBackColor = "#e5e5e5";
                            item.sForeColor = "#000";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void PageChange()
        {
            try
            {
                PageChanged(this, new PageingDataEventArgs() { baseUrl = BaseUrl, PageNum = _nowNum });
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        private void Paging_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = sender as Button;

                CmdNumber(int.Parse(btn.Content.ToString()));
                if (PageChanged != null)
                {
                    PageChange();
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        private void GoFirst_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CmdNumber(_intervalFirstNum);
                PageChange();
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        private void GoPrev_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_intervalFirstNum <= _nowNum - _PrevNexNum)
                {
                    CmdNumber(_nowNum - _PrevNexNum);
                    PageChange();
                }
                else if (_firstNum == _nowNum)
                {
                    Alert alert = new Alert(Localization.Resource.Paging_1);
                    alert.ShowDialog();
                }
                else
                {
                    SetPaging(_nowNum - _termNum, _pageNum - _PrevNexNum, LastNum);
                    CmdNumber(_nowNum - _PrevNexNum);
                    PageChange();
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        private void GoNext_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_intervalLastNum >= _nowNum + _PrevNexNum)
                {
                    CmdNumber(_nowNum + _PrevNexNum);
                    PageChange();

                }
                else if (LastNum == _nowNum)
                {
                    Alert alert = new Alert(Localization.Resource.Paging_2);
                    alert.ShowDialog();
                }
                else
                {
                    SetPaging(_nowNum + _PrevNexNum, _pageNum + _PrevNexNum, LastNum);
                    CmdNumber(_nowNum + _PrevNexNum);
                    PageChange();
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        private void GoLast_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CmdNumber(_intervalLastNum);
                PageChange();
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
    }

    public class PageingDataEventArgs : EventArgs
    {
        public string baseUrl { get; set; }
        public int PageNum { get; set; }
    }

    public class clsPage
    {
        public virtual int iNum { get; set; }

        public virtual string sBackColor { get; set; }

        public virtual string sForeColor { get; set; }
    }

    public class PagingInfo
    {
        public virtual string Title { get; set; }
        public virtual int LastIndex { get; set; }
    }
}
