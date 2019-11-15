using System;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;

namespace coinBlock.Model.DepositWithdraw
{
    public class ResponseFeeModel : ResponseBaseModel
    {
        public ResponseFeeDataModel data { get; set; }
        public ResponseFeeModel()
        {
            data = new ResponseFeeDataModel();
        }
    }

    public class ResponseFeeDataModel
    {
        public ObservableCollection<ResponseFeeListModel> list { get; set; }
        public ResponseFeeDataModel()
        {
            list = new ObservableCollection<ResponseFeeListModel>();
        }
    }

    public class ResponseFeeListModel
    {
        public string curcyCd { get; set; }
        public decimal? fee { get; set; }
    }
}