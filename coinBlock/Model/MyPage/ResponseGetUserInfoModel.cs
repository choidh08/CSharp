using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.MyPage
{
    public class ResponseGetUserInfoModel : ResponseBaseModel
    {
        public ResponseGetUserInfoDataModel data { get; set; }
        public ResponseGetUserInfoModel()
        {
            data = new ResponseGetUserInfoDataModel();
        }
    }

    public class ResponseGetUserInfoDataModel
    {
        public virtual string userEmail { get; set; }
        public virtual string userNm { get; set; }
        public virtual string userMobile { get; set; }
        public virtual string rcmdCode { get; set; }
        public virtual string countryCd { get; set; }
        public virtual string countryNm { get; set; }
        public virtual string rcmdNm { get; set; }
        public virtual string rcmdUserCode { get; set; }
        public virtual string rcmdUserCodeEn { get; set; }
        public virtual string subUserEmail { get; set; }
        public virtual string birthDay { get; set; } = string.Empty;
        public virtual string gender { get; set; } = "1";
        public virtual string postCd { get; set; }
        public virtual string adrs { get; set; }
        public virtual string dtlAdrs { get; set; }
        public virtual string brhCode { get; set; }
        public virtual string natnCode { get; set; }
        public virtual string langCd { get; set; }
        public virtual string birthType { get; set; }
    }
}