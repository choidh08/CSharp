using System;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;

namespace coinBlock.Model.AdditionalService
{
    public class ResponseTrustInterestListModel : ResponseBaseModel
    {
        public ResponseTrustInterestListDataModel data { get; set; }
        public ResponseTrustInterestListModel()
        {
            data = new ResponseTrustInterestListDataModel();
        }
    }

    public class ResponseTrustInterestListDataModel
    {
        public virtual string failCd { get; set; }
        public ObservableCollection<ResponseTrustInterestListListModel> list { get; set; }
        public ResponseTrustInterestListDataModel()
        {
            list = new ObservableCollection<ResponseTrustInterestListListModel>();
        }
    }

    public class ResponseTrustInterestListListModel
    {
        public virtual string no { get; set; }

        public virtual string list_gb { get; set; }

        public virtual string cnDptCode { get; set; }

        public virtual string curcyCd { get; set; }

        public virtual string cmmNm { get; set; }

        public virtual string reqDt { get; set; }

        public virtual decimal?  cnAmt { get; set; }
    }
}