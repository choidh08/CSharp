using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using coinBlock.Model;
using coinBlock.Utility;

namespace coinBlock.ViewModels.AdditionalService
{
    [POCOViewModel]
    public class CouponAdditionalServiceViewModel
    {
        public static CouponAdditionalServiceViewModel Create()
        {
            return ViewModelSource.Create(() => new CouponAdditionalServiceViewModel());
        }

        public CouponAdditionalServiceViewModel()
        {
            
        }
    }
}