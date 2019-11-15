using System;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;

namespace coinBlock.Model.HelpDesk
{
    public class ResponseCertSelectCodeModel : ResponseBaseModel
    {
        public ResponseCertSelectCodeDataModel data { get; set; }
        public ResponseCertSelectCodeModel()
        {
            data = new ResponseCertSelectCodeDataModel();                
        }
    }

    public class ResponseCertSelectCodeDataModel
    {
        public ObservableCollection<ResponseCertSelectCodeListModel> list { get; set; }
        public ResponseCertSelectCodeDataModel()
        {
            list = new ObservableCollection<ResponseCertSelectCodeListModel>();
        }
    }

    public class ResponseCertSelectCodeListModel
    {
        public virtual string cmmCd { get; set; }
        public virtual string cmmDesc { get; set; }
        public virtual string cmmUpperCd { get; set; }
        public virtual string cmmNm { get; set; }
    }
}