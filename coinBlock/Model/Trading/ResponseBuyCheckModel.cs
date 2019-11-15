using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.Trading
{
    public class ResponseBuyCheckModel : ResponseBaseModel
    {
        public virtual ResponseBuyCheckDataModel data { get; set; }

        public ResponseBuyCheckModel()
        {
            data = new ResponseBuyCheckDataModel();
        }
    }

    public class ResponseBuyCheckDataModel
    {
        //구매 제한 결과('Y':구매가능, 'N':구매제한)
        public virtual string buyCheck { get; set; }

        public virtual decimal askingPrc { get; set; }

        public virtual string failCd { get; set; }
    }
}