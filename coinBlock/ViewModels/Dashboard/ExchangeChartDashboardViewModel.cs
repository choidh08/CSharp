using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System.Collections.ObjectModel;
using coinBlock.Model.Dashboard;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Threading;
using coinBlock.Utility;
using coinBlock.ViewModels.Common;
using System.Windows;

namespace coinBlock.ViewModels.Dashboard
{
    [POCOViewModel]
    public class ExchangeChartDashboardViewModel
    {
        public static ExchangeChartDashboardViewModel Create()
        {
            return ViewModelSource.Create(() => new ExchangeChartDashboardViewModel());
        }

        public ExchangeChartDashboardViewModel()
        {
            try
            {
            
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
     
        ~ExchangeChartDashboardViewModel()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void Loaded()
        {
            try
            {
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void UnLoaded()
        {
            try
            {

            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
    }
}