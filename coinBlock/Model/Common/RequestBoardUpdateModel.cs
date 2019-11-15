using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.Common
{
    public class RequestBoardUpdateModel : RequestBaseModel
    {
        public virtual string boardId { get; set; }
        public virtual string contentId { get; set; }
        public virtual string boardTitle { get; set; }
        public virtual string contentMsg { get; set; }
        public virtual string userPhone { get; set; }
        public virtual string regUser { get; set; }
        public virtual string noticeYn { get; set; }
        public virtual string catId { get; set; }
        public virtual string regIp { get; set; }
        public virtual string lang { get; set; }
        public virtual string useYn { get; set; }

        public RequestBoardUpdateModel() : base("bt.board.insUptBoardOne.dp/proc.go") { }
    }
}