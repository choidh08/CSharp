using System;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using System.Windows;

namespace coinBlock.Model.DepositWithdraw
{
    public class ResponseCardReqContentModel : ResponseBaseModel
    {
        public ResponseCardReqContentDataModel data { get; set; }
        public ResponseCardReqContentModel()
        {
            data = new ResponseCardReqContentDataModel();
        }
    }

    public class ResponseCardReqContentDataModel
    {
        public ObservableCollection<ResponseCardReqContentListModel> list { get; set; }
        public ResponseCardReqContentDataModel()
        {
            list = new ObservableCollection<ResponseCardReqContentListModel>();
        }
    }

    public class ResponseCardReqContentListModel
    {
        public virtual int no { get; set; }
        public virtual string status { get; set; }
        public virtual string cardNum { get; set; }
        public virtual string reqDt { get; set; }
        public virtual string sendDt { get; set; }
        public virtual string cretDt { get; set; }
        public virtual string regDt { get; set; }
        public virtual string inDt { get; set; }
        public virtual string sendMthCd { get; set; }
        public virtual string brhCode { get; set; }
        public virtual string closeYn { get; set; }
        public virtual string postCd { get; set; }
        public virtual string adrs { get; set; }
        public virtual string dtlAdrs { get; set; }
        public virtual string orderId { get; set; }
        public virtual Visibility cancelVisible { get; set; } = Visibility.Collapsed;
    }
}
