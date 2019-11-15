using System;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;

namespace coinBlock.Model.DepositWithdraw
{
    public class ResponseGetAccountInfoModel : ResponseBaseModel
    {
        public ResponseGetAccountInfoDataModel data { get; set; }
        public ResponseGetAccountInfoModel()
        {
            data = new ResponseGetAccountInfoDataModel();
        }
    }

    public class ResponseGetAccountInfoDataModel
    {
        public ObservableCollection<ResponseGetAccountInfoListModel> list { get; set; }
        public ResponseGetAccountInfoDataModel()
        {
            list = new ObservableCollection<ResponseGetAccountInfoListModel>();
        }

        public virtual string userAccNm { get; set; }
    }

    public class ResponseGetAccountInfoListModel
    {
        /// <summary>
        /// 은행명
        /// </summary>
        public virtual string bankNm { get; set; }
        /// <summary>
        /// 은행코드
        /// </summary>
        public virtual string bankCd { get; set; }
        /// <summary>
        /// 계좌번호
        /// </summary>
        public virtual string accNo { get; set; }
        /// <summary>
        /// 계좌명
        /// </summary>
        public virtual string accNm { get; set; }

        
    }
}