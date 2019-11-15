using System;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;


namespace coinBlock.Model.Common
{
    public class ResponseFindUserIdModel : ResponseBaseModel
    {
        public ResponseFindUserIdDataModel data { get; set; }
        public ResponseFindUserIdModel()
        {
            data = new ResponseFindUserIdDataModel();
        }
    }

    public class ResponseFindUserIdDataModel
    {
        public virtual string failCd { get; set; }
        public virtual string findYn { get; set; }
        public virtual string userEmail { get; set; }
        public virtual string userName { get; set; }

        public ResponseFindUserIdDataModel()
        {

        }
    }
}