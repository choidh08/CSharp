using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.MyPage
{
    public class ResponsePinNumbrInitModel : ResponseBaseModel
    {
        public ResponsePinNumbrInitDataModel data { get; set; }
        public ResponsePinNumbrInitModel()
        {
            data = new ResponsePinNumbrInitDataModel();
        }
    }

    public class ResponsePinNumbrInitDataModel
    {
        public virtual string failCd { get; set; }
    }
}