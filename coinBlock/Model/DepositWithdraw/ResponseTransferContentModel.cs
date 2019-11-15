using System;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;

namespace coinBlock.Model.DepositWithdraw
{
    public class ResponseTransferContentModel : ResponseBaseModel
    {
        public ResponseTransferContentDataModel data { get; set; }
        public ResponseTransferContentModel()
        {
            data = new ResponseTransferContentDataModel();
        }
    }

    public class ResponseTransferContentDataModel
    {
        public ObservableCollection<ResponseTransferContentListModel> list { get; set; }
        public ResponseTransferContentDataModel()
        {
            list = new ObservableCollection<ResponseTransferContentListModel>();
        }
    }

    public class ResponseTransferContentListModel
    {
        public virtual string wdrReqCode { get; set; }
        public virtual string cnKndCd { get; set; }
        public virtual decimal? wdrReqAmt { get; set; }
        public virtual decimal? feeAmt { get; set; }
        public virtual string status { get; set; }
        public virtual string regDt { get; set; }
        public virtual string dealNo { get; set; }
        public virtual string exFlag { get; set; }
        public virtual string orderDesc { get; set; }
    }
}