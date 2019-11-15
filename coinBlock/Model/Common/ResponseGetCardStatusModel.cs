using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.Common
{
    public class ResponseGetCardStatusModel : ResponseBaseModel
    {
        public ResponseGetCardStatusDataModel data { get; set; }
        public ResponseGetCardStatusModel()
        {
            data = new ResponseGetCardStatusDataModel();
        }
    }

    public class ResponseGetCardStatusDataModel
    {
        public virtual string status { get; set; }
    }
}