using System;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;

namespace coinBlock.Model.HelpDesk
{
    public class ResponseWithdrawFeeModel : ResponseBaseModel
    {
        public ResponseWithdrawFeeDataModel data { get; set; }
        public ResponseWithdrawFeeModel()
        {
            data = new ResponseWithdrawFeeDataModel();
        }   
    }

    public class ResponseWithdrawFeeDataModel
    {
        public ObservableCollection<ResponseWithdrawFeeListModel> list { get; set; }
        public ResponseWithdrawFeeDataModel()
        {
            list = new ObservableCollection<ResponseWithdrawFeeListModel>();
        }
    }

    public class ResponseWithdrawFeeListModel
    {
        public virtual string curcyCd { get; set; }

        public virtual decimal fee { get; set; }
    }
}