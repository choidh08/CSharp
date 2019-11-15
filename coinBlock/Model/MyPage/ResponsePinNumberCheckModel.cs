using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.MyPage
{
    public class ResponsePinNumberCheckModel : ResponseBaseModel
    {
        public ResponsePinNumberCheckDataModel data { get; set; }
        public ResponsePinNumberCheckModel()
        {
            data = new ResponsePinNumberCheckDataModel();
        }
    }

    public class ResponsePinNumberCheckDataModel
    {
        public virtual string failCd { get; set; }

        public virtual string pinResetYn { get; set; }
    }
}