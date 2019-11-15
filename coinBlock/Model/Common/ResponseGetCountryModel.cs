using System;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;

namespace coinBlock.Model.Common
{
    public class ResponseGetCountryModel : ResponseBaseModel
    {
        public ResponseGetNatnBankLisDatatModel data { get; set; }
        public ResponseGetCountryModel()
        {
            data = new ResponseGetNatnBankLisDatatModel();
        }
    }

    public class ResponseGetNatnBankLisDatatModel
    {
        public ObservableCollection<ResponseGetCountryListModel> list { get; set; }
        public  ResponseGetNatnBankLisDatatModel()
        {
            list = new ObservableCollection<ResponseGetCountryListModel>();
        }
    }

    public class ResponseGetCountryListModel
    {
        public virtual string countryCd { get; set; }
        public virtual string countryNm { get; set; }
    }
}