using System;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;

namespace coinBlock.Model.DepositWithdraw
{
    public class ResponseGetInfinityUserModel : ResponseBaseModel
    {
        public ResponseGetInfinityUserDataModel data { get; set; }
        public ResponseGetInfinityUserModel()
        {
            data = new ResponseGetInfinityUserDataModel();
        }
    }

    public class ResponseGetInfinityUserDataModel
    {
        public virtual ObservableCollection<ResponseGetInfinityUserListModel> list { get; set; }
        public virtual string failCd { get; set; }
        public ResponseGetInfinityUserDataModel()
        {
            list = new ObservableCollection<ResponseGetInfinityUserListModel>();
        }
    }

    public class ResponseGetInfinityUserListModel
    {
        public virtual string cnKndCd { get; set; }

        public virtual string cnKndNm { get; set; }

        public virtual string stdDt { get; set; }

        public virtual string endDt { get; set; }             
    }
}