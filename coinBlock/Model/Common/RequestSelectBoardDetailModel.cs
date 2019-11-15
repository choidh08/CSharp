using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.Common
{
    public class RequestSelectBoardDetailModel : RequestBaseModel
    {
        public virtual string boardId { get; set; }
 
        public virtual string contentId { get; set; }

        public RequestSelectBoardDetailModel() : base("bt.board.selectBoardDetail.dp/proc.go") { }
    }
}