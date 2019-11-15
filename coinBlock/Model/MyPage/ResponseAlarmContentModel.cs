using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.MyPage
{
    public class ResponseAlarmContentModel : ResponseBaseModel
    {
        public ResponseAlarmContentDataModel data { get; set; }
        public ResponseAlarmContentModel()
        {
            data = new ResponseAlarmContentDataModel();
        }
    }

    public class ResponseAlarmContentDataModel
    {
        /// <summary>
        /// 기본알림 실행여부
        /// </summary>
        public virtual string alrmBsYn { get; set; }
        /// <summary>
        /// 로그인
        /// </summary>
        public virtual string bsBs01 { get; set; }
        /// <summary>
        /// 공지사항
        /// </summary>
        public virtual string bsBs02 { get; set; }
        /// <summary>
        /// 부가서비스
        /// </summary>
        public virtual string bsBs03 { get; set; }
        /// <summary>
        /// 1:1 문의
        /// </summary>
        public virtual string bsBs04 { get; set; }
        /// <summary>
        /// 구매주문
        /// </summary>
        public virtual string bsSb01 { get; set; }
        /// <summary>
        /// 구매체결
        /// </summary>
        public virtual string bsSb02 { get; set; }
        /// <summary>
        /// 판매주문
        /// </summary>
        public virtual string bsSb03 { get; set; }
        /// <summary>
        /// 판매체결
        /// </summary>
        public virtual string bsSb04 { get; set; }
        /// <summary>
        /// 코인입금
        /// </summary>
        public virtual string bsIo01 { get; set; }
        /// <summary>
        /// 코인송금
        /// </summary>
        public virtual string bsIo02 { get; set; }
        /// <summary>
        /// 원화충전
        /// </summary>
        public virtual string bsIo03 { get; set; }
        /// <summary>
        /// 원화출금
        /// </summary>
        public virtual string bsIo04 { get; set; }
        /// <summary>
        /// 시간별알림 실행여부
        /// </summary>
        public virtual string alrmTmYn { get; set; }
        /// <summary>
        /// BTC
        /// </summary>
        public virtual string tmCoin01 { get; set; }
        /// <summary>
        /// ETH
        /// </summary>
        public virtual string tmCoin02 { get; set; }
        /// <summary>
        /// BCH
        /// </summary>
        public virtual string tmCoin03 { get; set; }
        /// <summary>
        /// XRP
        /// </summary>
        public virtual string tmCoin04 { get; set; }
        /// <summary>
        /// SGC
        /// </summary>
        public virtual string tmCoin05 { get; set; }
        /// <summary>
        /// BTG
        /// </summary>
        public virtual string tmCoin06 { get; set; }
        /// <summary>
        /// DASH
        /// </summary>
        public virtual string tmCoin07 { get; set; }
        /// <summary>
        /// LTC
        /// </summary>
        public virtual string tmCoin08 { get; set; }
        /// <summary>
        /// QTUM
        /// </summary>
        public virtual string tmCoin09 { get; set; }
        /// <summary>
        /// BMC
        /// </summary>
        public virtual string tmCoin10 { get; set; }
        /// <summary>
        /// BMC
        /// </summary>
        public virtual string tmCoin11 { get; set; }
        /// <summary>
        /// VSTC
        /// </summary>
        public virtual string tmCoin12 { get; set; }
        /// <summary>
        /// ADM
        /// </summary>
        public virtual string tmCoin13 { get; set; }
        /// <summary>
        /// BSV
        /// </summary>
        public virtual string tmCoin14 { get; set; }
        /// <summary>
        /// TRX
        /// </summary>
        public virtual string tmCoin15 { get; set; }
        /// <summary>
        /// XLM
        /// </summary>
        public virtual string tmCoin16 { get; set; }
        /// <summary>
        /// WAVES
        /// </summary>
        public virtual string tmCoin17 { get; set; }
        /// <summary>
        /// POLY
        /// </summary>
        public virtual string tmCoin18 { get; set; }
        /// <summary>
        /// OMG
        /// </summary>
        public virtual string tmCoin19 { get; set; }
        /// <summary>
        /// MITH
        /// </summary>
        public virtual string tmCoin20 { get; set; }
        /// <summary>
        /// ICX
        /// </summary>
        public virtual string tmCoin21 { get; set; }
        /// <summary>
        /// XEM
        /// </summary>
        public virtual string tmCoin22 { get; set; }
        /// <summary>
        /// PAN
        /// </summary>
        public virtual string tmCoin23 { get; set; }
        /// <summary>
        /// XCEL
        /// </summary>
        public virtual string tmCoin24 { get; set; }
        /// <summary>
        /// HEC
        /// </summary>
        public virtual string tmCoin25 { get; set; }
        /// <summary>
        /// GIFO
        /// </summary>
        public virtual string tmCoin26 { get; set; }
        /// <summary>
        /// HAMA
        /// </summary>
        public virtual string tmCoin27 { get; set; }
        /// <summary>
        /// ERB
        /// </summary>
        public virtual string tmCoin28 { get; set; }
        /// <summary>
        /// 시작시간
        /// </summary>
        public virtual int sttHh { get; set; }
        /// <summary>
        /// 종료시간
        /// </summary>
        public virtual int endHh { get; set; }
        /// <summary>
        /// 알림간격
        /// </summary>
        public virtual int termHh { get; set; }
    }
}