using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.Common
{
    public class RequestSelectBoardModel : RequestBaseModel
    {
        /// <summary>
        /// BDMT_000000000003 1:1문의 / BDMT_000000000001 공지사항 / BDMT_000000000002 FAQ / BDMT_000000000021 코인동향
        /// </summary>
        public virtual string boardId { get; set; }
        /// <summary>
        /// ko', 'en'..
        /// </summary>
        public virtual string lang { get; set; }
        /// <summary>
        /// 01:구매/판매 02:입금/출금/송금 03:회원안내 04: 기타 05:인증자료제출
        /// </summary>
        public virtual string catId { get; set; }

        public virtual string regUser { get; set; }

        public virtual string stdDate { get; set; }

        public virtual string endDate { get; set; }

        public virtual string searchWrd { get; set; }

        public virtual int pageIndex { get; set; }

        public RequestSelectBoardModel() : base("bt.board.selectBoardList.dp/proc.go") { }
    }
}