using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.AdditionalService
{
    public class ResponseArbitrageSetSignUpModel : ResponseBaseModel
    {
       public ResponseArbitrageSetSignUpDataModel data { get; set; }
       public ResponseArbitrageSetSignUpModel()
        {
            data = new ResponseArbitrageSetSignUpDataModel();
        }
    }

    public class ResponseArbitrageSetSignUpDataModel
    {
        public virtual string failCd { get; set; }
        public ResponseArbitrageSetSignUpDataModel()
        {

        }
    }
}