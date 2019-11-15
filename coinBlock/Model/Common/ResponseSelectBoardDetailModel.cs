using System;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;

namespace coinBlock.Model.Common
{
    public class ResponseSelectBoardDetailModel : ResponseBaseModel
    {
        public ResponseSelectBoardDetailDataModel data { get; set; }
        public ResponseSelectBoardDetailModel()
        {
            data = new ResponseSelectBoardDetailDataModel();
        }
    }

    public class ResponseSelectBoardDetailDataModel
    {
        public virtual string boardId { get; set; }
        public virtual string contentId { get; set; }
        public virtual string boardTitle { get; set; }
        public virtual string contentMsg { get; set; }
        public virtual string userPhone { get; set; }
        public virtual string regUser { get; set; }
        public virtual string regDt { get; set; }
        public virtual string replyYn { get; set; }
        public virtual string noticeYn { get; set; }
        public virtual string boardPwd { get; set; }
        public virtual string catId { get; set; }
        public virtual string catName { get; set; }
        public virtual string lang { get; set; }

        //public ObservableCollection<ResponseSelectBoardDetailListModel> list { get; set; }

        public ResponseSelectBoardDetailDataModel()
        {
            //list = new ObservableCollection<ResponseSelectBoardDetailListModel>();
        }
    }

    //public class ResponseSelectBoardDetailListModel
    //{
    //    public virtual string boardId { get; set; }
    //    public virtual string contentId { get; set; }
    //    public virtual string boardTitle { get; set; }
    //    public virtual string contentMsg { get; set; }
    //    public virtual string regUser { get; set; }
    //    public virtual string regDt { get; set; }
    //    public virtual string replyYn { get; set; }
    //    public virtual string noticeYn { get; set; }
    //    public virtual string boardPwd { get; set; }     
    //    public virtual string catId { get; set; }
    //    public virtual string catName { get; set; }
    //    public virtual string lang { get; set; }

    //}
}