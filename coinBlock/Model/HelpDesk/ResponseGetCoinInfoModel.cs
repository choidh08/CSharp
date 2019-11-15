using System;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;

namespace coinBlock.Model.HelpDesk
{
    public class ResponseGetCoinInfoModel : ResponseBaseModel
    {
        public ResponseGetCoinInfoDataModel data { get; set; }
        public ResponseGetCoinInfoModel()
        {
            data = new ResponseGetCoinInfoDataModel();
        }
    }

    public class ResponseGetCoinInfoDataModel
    {
        public virtual string coinInfoCd { get; set; }
        public virtual string cnKndCd { get; set; }
        public virtual string title { get; set; }
        public virtual string showYn { get; set; }
        public virtual string firstPushAmt { get; set; }
        public virtual string pushMth { get; set; }
        public virtual string mktTotPrc { get; set; }
        public virtual string curTotAmt { get; set; }
        public virtual string maxAmt { get; set; }
        public virtual string pubUrl { get; set; }
        public virtual string atchFileId { get; set; }
        public virtual string langCd { get; set; }
        public virtual string etc1 { get; set; }
        public virtual string etc2 { get; set; }
        public virtual string abb { get; set; }

        public ResponseGetCoinInfoDataModel()
        {
        }
       
    }
}