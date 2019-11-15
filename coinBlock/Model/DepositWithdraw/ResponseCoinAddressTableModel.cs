using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;

namespace coinBlock.Model.DepositWithdraw
{
    [POCOViewModel]
    public class ResponseCoinAddressTableModel : ResponseBaseModel
    {
        public ResponseCoinAddressTableDataModel data { get; set; }
        public ResponseCoinAddressTableModel()
        {
            data = new ResponseCoinAddressTableDataModel();
        }
    }

    public class ResponseCoinAddressTableDataModel
    {
        public ObservableCollection<ResponseCoinAddressTableListModel> list { get; set; }
        public ResponseCoinAddressTableDataModel()
        {
            list = new ObservableCollection<ResponseCoinAddressTableListModel>();
        }
    }

    public class ResponseCoinAddressTableListModel
    {
        /// <summary>
        /// 전자지갑주소
        /// </summary>
        public virtual string accNo { get; set; }
        /// <summary>
        /// 코인코드
        /// </summary>
        public virtual string curcyCd { get; set; }
        /// <summary>
        /// 코인이름
        /// </summary>
        public virtual string curcyNm { get; set; }
        /// <summary>
        /// 데스티네이션코드
        /// </summary>
        public virtual string destiTag { get; set; }
    }
}