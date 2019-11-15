using System;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;

namespace coinBlock.Model.DepositWithdraw
{
    public class ResponseBankModel : ResponseBaseModel
    {
        public ResponseBankDataModel data { get; set; }
        public ResponseBankModel()
        {
            data = new ResponseBankDataModel();
        }
    }

    public class ResponseBankDataModel
    {
        public ObservableCollection<ResponseBankListModel> list { get; set; }
        public ResponseBankDataModel()
        {
            list = new ObservableCollection<ResponseBankListModel>();
        }
    }

    public class ResponseBankListModel
    {
        public virtual string bankCd { get; set; }

        public virtual string bankNm { get; set; }        
    }
}