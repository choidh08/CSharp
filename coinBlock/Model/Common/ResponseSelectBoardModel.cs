using System;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;

namespace coinBlock.Model.Common
{
    public class ResponseSelectBoardModel : ResponseBaseModel
    {
        public ResponseSelectBoardDataModel data { get; set; }
        public ResponseSelectBoardModel()
        {
            data = new ResponseSelectBoardDataModel();
        }
    }

    public class ResponseSelectBoardDataModel
    {
        public virtual int listCnt { get; set; }
        public virtual int pageIndex { get; set; }
        public virtual int pageSize { get; set; }
        public virtual int pageUnit { get; set; }

        public virtual ObservableCollection<ResponseSelectBoardListModel> list { get; set; }
        public ResponseSelectBoardDataModel()
        {
            list = new ObservableCollection<ResponseSelectBoardListModel>();
        }
    }

    public class ResponseSelectBoardListModel
    {
        public virtual int no { get; set; }
        public virtual string boardId { get; set; }
        public virtual string contentId { get; set; }
        public virtual string boardTitle { get; set; }
        public virtual string contentMsg { get; set; }
        public virtual string regUser { get; set; }
        public virtual string regDt { get; set; }
        public virtual string readCnt { get; set; }
        public virtual string secretYn { get; set; }
        public virtual string boardPwd { get; set; }
        public virtual string noticeYn { get; set; }
        public virtual string noticeToDt { get; set; }
        public virtual string noticeFromDt { get; set; }
        public virtual string replyYn { get; set; }
        public virtual string replyTitle { get; set; }
        public virtual string replyMsg { get; set; }
        public virtual string replyId { get; set; }
        public virtual string catId { get; set; }
        public virtual string lang { get; set; }
        public virtual string catName { get; set; }
    }
}