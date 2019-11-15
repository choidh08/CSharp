using System;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using System.Windows;

namespace coinBlock.Model.MyPage
{
    public class ResponseGetTradeHistoryModel : ResponseBaseModel
    {
        public ResponseGetTradeHistoryDataModel data { get; set; }
        public ResponseGetTradeHistoryModel()
        {
            data = new ResponseGetTradeHistoryDataModel();
        }    
    }

    public class ResponseGetTradeHistoryDataModel
    {
        public ObservableCollection<ResponseGetTradeHistoryListModel> list { get; set; }
        //public virtual int pageIndex { get; set; }
        //public virtual int pageSize { get; set; }
        public virtual string nextKey { get; set; }

        public ResponseGetTradeHistoryDataModel()
        {
            list = new ObservableCollection<ResponseGetTradeHistoryListModel>();
        }
    }

    public class ResponseGetTradeHistoryListModel
    {
        public virtual int no { get; set; }
        public virtual string hisCode { get; set; }
        public virtual string exFlag { get; set; }
        public virtual string curcyCd { get; set; }
        public virtual string cnAmt { get; set; }
        public virtual string totPrc { get; set; }
        public virtual string regDate { get; set; }
        public virtual string tradePrc { get; set; }
        public virtual string apntYn { get; set; } //Y지정가 N시장가
        public virtual string orderDesc { get; set; }
        public virtual string payKndNm { get; set; }
        public virtual string cryCode { get; set; }
        //public virtual string exRate { get; set; }
        public virtual string reqAmt { get; set; }
        public virtual string fee { get; set; }
        public virtual string tradeAmt { get; set; }
        public virtual string orderNo { get; set; }
        public virtual string curcyNm { get; set; }
        public virtual string payKndCd { get; set; }
        public virtual string failMsg { get; set; }
        public virtual string status { get; set; }
        public virtual string sendUserEmail { get; set; }
        public virtual string sendUserNm { get; set; }
        public virtual string receiveEmail { get; set; }
        public virtual string receiveNm { get; set; }
        public virtual string currTransEmail { get; set; }
        public virtual string currTransNm { get; set; }
        public virtual Visibility accountVisible { get; set; } = Visibility.Collapsed;
        public virtual Visibility cardVisible { get; set; } = Visibility.Collapsed;
        public virtual Visibility refusalVisible { get; set; } = Visibility.Collapsed;
        public virtual Visibility cancelVisible { get; set; } = Visibility.Collapsed;
        public virtual Visibility buysellVisible { get; set; } = Visibility.Visible;
        public virtual Visibility orderDescVisible { get; set; } = Visibility.Collapsed;
        public virtual Visibility ptmVisible { get; set; } = Visibility.Collapsed;
        public virtual Visibility trustVisible { get; set; } = Visibility.Collapsed;
        public virtual Visibility rockUpVisible { get; set; } = Visibility.Collapsed;
        public virtual Visibility dtVisible { get; set; } = Visibility.Collapsed;
        public virtual Visibility autoTradeVisible { get; set; } = Visibility.Collapsed;
        public virtual Visibility currTransVisible { get; set; } = Visibility.Collapsed;
             
    }
}