using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.DepositWithdraw
{
    public class ResponseLmtWithdrowModel : ResponseBaseModel
    {
        public ResponseLmtWithdrowDataModel data { get; set; }
        public ResponseLmtWithdrowModel()
        {
            data = new ResponseLmtWithdrowDataModel();
        }
    }

    public class ResponseLmtWithdrowDataModel
    {
        public virtual string failCd { get; set; }

        public virtual string withdrowType { get; set; }
    }
}