using System;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;

namespace coinBlock.Model.HelpDesk
{
    public class ResponseGetCoinMaxMinModel : ResponseBaseModel
    {
        public ResponseGetCoinMaxMinDataModel data { get; set; }
        public ResponseGetCoinMaxMinModel()
        {
            data = new ResponseGetCoinMaxMinDataModel();
        }
    }

    public class ResponseGetCoinMaxMinDataModel
    {
        public ObservableCollection<ResponseGetCoinMaxMinListModel> list { get; set; }
        public virtual decimal mkKRW { get; set; }
        public virtual decimal mkETH { get; set; }
        public virtual decimal mkUSDT { get; set; }
        public virtual decimal mkBTC { get; set; }
        public ResponseGetCoinMaxMinDataModel()
        {
            list = new ObservableCollection<ResponseGetCoinMaxMinListModel>();
        }
    }

    public class ResponseGetCoinMaxMinListModel
    {
        public virtual string cnkndcd { get; set; }
        public virtual string cnkndnm { get; set; }
        public virtual decimal tradeMinAmt { get; set; }
        public virtual decimal tradeMaxAmt { get; set; }
        public virtual decimal tradeMinPrc { get; set; }
        public virtual decimal minAmt2 { get; set; }
        public virtual decimal maxAmt2 { get; set; }
        public virtual decimal maxAmtDay2 { get; set; }
        public virtual decimal minAmt3 { get; set; }
        public virtual decimal maxAmt3 { get; set; }
        public virtual decimal maxAmtDay3 { get; set; }
        public virtual decimal cnSndFee { get; set; }
        public virtual decimal coinDecimal { get; set; }
        public virtual decimal cashDecimal { get; set; }
        public virtual decimal asking { get; set; }
        public virtual string isTag { get; set; }
    }
}