using System;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;

namespace coinBlock.Model.HelpDesk
{
    public class ResponseCertContentModel : ResponseBaseModel
    {
        public ResponseCertContentDataModel data { get; set; }
        public ResponseCertContentModel()
        {
            data = new ResponseCertContentDataModel();
        }
    }

    public class ResponseCertContentDataModel
    {
        public ObservableCollection<ResponseCertContentListModel> list { get; set; }
        public ResponseCertContentDataModel()
        {
            list = new ObservableCollection<ResponseCertContentListModel>();
        }
    }

    public class ResponseCertContentListModel
    {
        public virtual string no { get; set; }
        public virtual string regDt { get; set; }
        public virtual string certGrade { get; set; }
        public virtual string certSubGrade { get; set; }
        public virtual string certYn { get; set; }
        public virtual string certFailMsg { get; set; }
        public virtual string uptDt { get; set; }
    }
}