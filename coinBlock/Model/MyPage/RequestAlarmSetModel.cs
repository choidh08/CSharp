using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.MyPage
{
    public class RequestAlarmSetModel : RequestBaseModel
    {
        public string userEmail { get; set; }
        /// <summary>
        /// 기본알림 실행여부
        /// </summary>
        public string alrmBsYn { get; set; }
        /// <summary>
        /// 로그인
        /// </summary>
        public string bsBs01 { get; set; }
        /// <summary>
        /// 공지사항
        /// </summary>
        public string bsBs02 { get; set; }
        /// <summary>
        /// 부가서비스
        /// </summary>
        public string bsBs03 { get; set; }
        /// <summary>
        /// 1:1 문의
        /// </summary>
        public string bsBs04 { get; set; }
        /// <summary>
        /// 구매주문
        /// </summary>
        public string bsSb01 { get; set; }
        /// <summary>
        /// 구매체결
        /// </summary>
        public string bsSb02 { get; set; }
        /// <summary>
        /// 판매주문
        /// </summary>
        public string bsSb03 { get; set; }
        /// <summary>
        /// 판매체결
        /// </summary>
        public string bsSb04 { get; set; }
        /// <summary>
        /// 코인입금
        /// </summary>
        public string bsIo01 { get; set; }
        /// <summary>
        /// 코인송금
        /// </summary>
        public string bsIo02 { get; set; }
        /// <summary>
        /// 원화충전
        /// </summary>
        public string bsIo03 { get; set; }
        /// <summary>
        /// 원화출금
        /// </summary>
        public string bsIo04 { get; set; }
        /// <summary>
        /// 시간별알림 실행여부
        /// </summary>
        public string alrmTmYn { get; set; }
        /// <summary>
        /// BTC
        /// </summary>
        public string tmCoin01 { get; set; }
        /// <summary>
        /// ETH
        /// </summary>
        public string tmCoin02 { get; set; }
        /// <summary>
        /// BCH
        /// </summary>
        public string tmCoin03 { get; set; }
        /// <summary>
        /// XRP
        /// </summary>
        public string tmCoin04 { get; set; }
        /// <summary>
        /// SGC
        /// </summary>
        public string tmCoin05 { get; set; }
        /// <summary>
        /// BTG
        /// </summary>
        public string tmCoin06 { get; set; }
        /// <summary>
        /// DASH
        /// </summary>
        public string tmCoin07 { get; set; }
        /// <summary>
        /// LTC
        /// </summary>
        public string tmCoin08 { get; set; }
        /// <summary>
        /// QTUM
        /// </summary>
        public string tmCoin09 { get; set; }
        /// <summary>
        /// BMC
        /// </summary>
        public string tmCoin10 { get; set; }
        /// <summary>
        /// SGC
        /// </summary>
        public string tmCoin11 { get; set; }
        /// <summary>
        /// VSTC
        /// </summary>
        public string tmCoin12 { get; set; }
        /// <summary>
        /// ADM
        /// </summary>
        public string tmCoin13 { get; set; }
        /// <summary>
        /// BSV
        /// </summary>
        public string tmCoin14 { get; set; }
        /// <summary>
        /// TRX
        /// </summary>
        public string tmCoin15 { get; set; }
        /// <summary>
        /// XLM
        /// </summary>
        public string tmCoin16 { get; set; }
        /// <summary>
        /// WAVES
        /// </summary>
        public string tmCoin17 { get; set; }
        /// <summary>
        /// POLY
        /// </summary>
        public string tmCoin18 { get; set; }
        /// <summary>
        /// OMG
        /// </summary>
        public string tmCoin19 { get; set; }
        /// <summary>
        /// MITH
        /// </summary>
        public string tmCoin20 { get; set; }
        /// <summary>
        /// ICX
        /// </summary>
        public string tmCoin21 { get; set; }
        /// <summary>
        /// XEM
        /// </summary>
        public string tmCoin22 { get; set; }
        /// <summary>
        /// PAN
        /// </summary>
        public string tmCoin23 { get; set; }
        /// <summary>
        /// XCEL
        /// </summary>
        public string tmCoin24 { get; set; }
        /// <summary>
        /// HEC
        /// </summary>
        public string tmCoin25 { get; set; }
        /// <summary>
        /// GIFO
        /// </summary>
        public string tmCoin26 { get; set; }
        /// <summary>
        /// HAMA
        /// </summary>
        public string tmCoin27 { get; set; }
        /// <summary>
        /// ERB
        /// </summary>
        public string tmCoin28 { get; set; }
        /// <summary>
        /// 시작시간
        /// </summary>
        public int sttHh { get; set; }
        /// <summary>
        /// 종료시간
        /// </summary>
        public int endHh { get; set; }
        /// <summary>
        /// 알림간격
        /// </summary>
        public int termHh { get; set; }
        /// <summary>
        /// 아이피
        /// </summry>
        public string regIp { get; set; }

        public RequestAlarmSetModel() : base("bt.setPushYn.dp/proc.go") { }
    }
}