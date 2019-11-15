using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;

namespace coinBlock.Views
{
    [POCOViewModel]
    public class AlertViewModel
    {
        public static AlertViewModel Create()
        {
            return ViewModelSource.Create(() => new AlertViewModel());
        }

        protected AlertViewModel()
        {
               
        }
    }
}