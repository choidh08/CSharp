using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.Common
{
    public class ResponseBoardUpdateModel : ResponseBaseModel
    {
        public ResponseBoardUpdateDataModel data { get; set; }
        public ResponseBoardUpdateModel()
        {
            data = new ResponseBoardUpdateDataModel();
        }
    }

    public class ResponseBoardUpdateDataModel
    {
        public virtual string contentId { get; set; }
    }
}