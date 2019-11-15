using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.Common
{
    public class ResponsePhoneNumberCheckModel : ResponseBaseModel
    {
        public ResponsePhoneNumberCheckDataModel data { get; set; }
        public ResponsePhoneNumberCheckModel()
        {
            data = new ResponsePhoneNumberCheckDataModel();
        }
    }

    public class ResponsePhoneNumberCheckDataModel
    {
        public virtual int result { get; set; }
    }
}