using System;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;

namespace coinBlock.Model.DepositWithdraw
{   
    public class ResponseWithdrawContentModel : ResponseBaseModel
    {
        public ResponseWithdrawContentDataModel data { get; set; }
        public ResponseWithdrawContentModel()
        {
            data = new ResponseWithdrawContentDataModel();
        }
    }

    public class ResponseWithdrawContentDataModel
    {
        public ObservableCollection<ResponseWithdrawContentListModel> list { get; set; }
        public ResponseWithdrawContentDataModel()
        {
            list = new ObservableCollection<ResponseWithdrawContentListModel>();
        }
    }

    public class ResponseWithdrawContentListModel
    {
        public virtual string WdrReqCode { get; set; }

        public virtual decimal? wdrPrc { get; set; }

        public virtual string userEmail { get; set; }

        public virtual string regDt { get; set; }

        public virtual string wdrCmplDt { get; set; }

        public virtual string compDt { get; set; }

        public virtual string status { get; set; }

        public virtual string inCryCode { get; set; }

        public virtual string exCryCode { get; set; }

        //public virtual decimal exRate { get; set; }

        public virtual decimal outPrc { get; set; }    
        
        public virtual decimal feePrc { get; set; }  
        
        public virtual string payMthCd { get; set; }  

        public virtual string cardNo { get; set; }

        public virtual string receiveEmail { get; set; }

        public virtual string receiveNm { get; set; }

        public virtual string receiveMobile { get; set; }        
    }
}