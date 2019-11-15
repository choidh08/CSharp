using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.DepositWithdraw
{
    public class ResponseCardCheckModel : ResponseBaseModel
    {
        public ResponseCardCheckDataModel data { get; set; }
        public ResponseCardCheckModel()
        {
            data = new ResponseCardCheckDataModel();
        }
    }

    public class ResponseCardCheckDataModel
    {
        public virtual string failCd { get; set; }
    }
}