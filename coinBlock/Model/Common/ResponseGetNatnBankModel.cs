using System;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;

namespace coinBlock.Model.Common
{
    public class ResponseGetNatnBankModel : ResponseBaseModel
    {
        public ResponseGetNatnBankDataModel data { get; set; }
        public ResponseGetNatnBankModel()
        {
            data = new ResponseGetNatnBankDataModel();
        }
    }

    public class ResponseGetNatnBankDataModel
    {
        public ObservableCollection<ResponseGetNatnBankListModel> list { get; set; }
        public ResponseGetNatnBankDataModel()
        {
            list = new ObservableCollection<ResponseGetNatnBankListModel>();
        }
    }

    public class ResponseGetNatnBankListModel
    {
        public virtual string natnBankCode { get; set; }
        public virtual string natnCode { get; set; }
        public virtual string bankNm { get; set; }
    }
}