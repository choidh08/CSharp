using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.Common
{
    public class RequestBoardDeleteModel : RequestBaseModel
    {
        public virtual string boardId { get; set; }
        public virtual string contentId { get; set; }

        public RequestBoardDeleteModel() : base("bt.board.delBoardOne.dp/proc.go") { }
    }
}