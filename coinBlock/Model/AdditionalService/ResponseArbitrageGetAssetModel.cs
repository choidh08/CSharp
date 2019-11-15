using System;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;

namespace coinBlock.Model.AdditionalService
{
    public class ResponseArbitrageGetAssetModel : ResponseBaseModel
    {
        public ResponseArbitrageGetAssetDataModel data { get; set; }
        public ResponseArbitrageGetAssetModel()
        {
            data = new ResponseArbitrageGetAssetDataModel();
        }
    }

    public class ResponseArbitrageGetAssetDataModel
    {
        public virtual decimal? gap { get; set; }

        public ObservableCollection<ResponseArbitrageGetAssetListModel> list { get; set; }
        public ResponseArbitrageGetAssetDataModel()
        {
            list = new ObservableCollection<ResponseArbitrageGetAssetListModel>();
        }
    }


    public class ResponseArbitrageGetAssetListModel
    {
        public virtual string type { get; set; }
        public virtual string exchgeId { get; set; }
        public virtual string userEmail { get; set; }
        public virtual string cnKndCd { get; set; }
        public virtual string cnKndNm { get; set; }
        public virtual decimal? totAmt { get; set; }
        public virtual string sTotAmt { get; set; }
        public virtual decimal? bsAmt { get; set; }
        public virtual string uptDt { get; set; }
        public virtual string uptEmail { get; set; }
        public virtual string exchgeNm { get; set; }
        public virtual decimal? cnPtcPrc { get; set; }
        public virtual decimal? cnExPrc { get; set; }
        public virtual decimal? ptcRate { get; set; }
        public virtual decimal? hasPtc { get; set; }
        public virtual string sHasPtc { get; set; }
        public virtual decimal? prcGap { get; set; }
        public virtual string sBackColor { get; set; }
        public virtual string sImage { get; set; }
        public virtual string sFavicon { get; set; }
        public virtual string sCoinImage { get; set; }
    }
}