using System;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;

namespace coinBlock.Model.DepositWithdraw
{
    public class ResponseGetCoinAndNodeModel : ResponseBaseModel
    {
        public ResponseGetCoinAndNodeDataModel data { get; set; }
        public ResponseGetCoinAndNodeModel()
        {
            data = new ResponseGetCoinAndNodeDataModel();
        }
    }

    public class ResponseGetCoinAndNodeDataModel
    {
        public virtual string note { get; set; }
        public virtual decimal amt { get; set; }        
    }
}