using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace coinBlock.Utility
{
    public static class EnumLib
    {
        /// <summary>
        /// 언어코드
        /// </summary>
        public enum Language
        {
            /// <summary>
            /// 한국어
            /// </summary>
            [StringValue("CMMC00000000018")]
            KOR,
            /// <summary>
            /// 영어
            /// </summary>
            [StringValue("CMMC00000000019")]
            ENG,
            /// <summary>
            /// 중국어
            /// </summary>
            [StringValue("CMMC00000000020")]
            CHN
        }
        /// <summary>
        /// 결제수단
        /// </summary>
        public enum PaymentWay
        {
            /// <summary>
            /// 가상계좌입금
            /// </summary>
            [StringValue("CMMC00000000011")]
            virtualAccountDeposit,
            /// <summary>
            /// 계좌이체
            /// </summary>
            [StringValue("CMMC00000000039")]
            accountTransfer,
            /// <summary>
            /// 전자상품권
            /// </summary>
            [StringValue("CMMC00000000040")]
            electroGifrCard,
            /// <summary>
            /// 선불카드
            /// </summary>
            [StringValue("CMMC00000000805")]
            prepaidCard

        }

        /// <summary>
        /// 전자지갑종류
        /// </summary>
        public enum electroWallet
        {
            /// <summary>
            /// ETH(이더리움) 전자지갑
            /// </summary>
            [StringValue("CMMC00000000012")]
            ethElectroWallet,
            /// <summary>
            /// BTC(비트코인) 전자지갑
            /// </summary>
            [StringValue("CMMC00000000013")]
            btcElectroWallet
        }



        /// <summary>
        /// 코인입금형태
        /// </summary>
        public enum CoinDeposit
        {
            /// <summary>
            /// 타 거래소 -> 당 거래소
            /// </summary>
            [StringValue("CMMC00000000014")]
            otherToThis,
            /// <summary>
            /// 당 거래서 -> 타 거래소
            /// </summary>
            [StringValue("CMMC00000000015")]
            thisToOther
        }


        /// <summary>
        /// 국가코드
        /// </summary>
        public enum NationalCode
        {
            /// <summary>
            /// 한국
            /// </summary>
            [StringValue("CMMC00000000021")]
            KOR,
            /// <summary>
            /// 미국
            /// </summary>
            [StringValue("CMMC00000000022")]
            USA,
            /// <summary>
            /// 중국
            /// </summary>
            [StringValue("CMMC00000000023")]
            CHN
        }

        /// <summary>
        /// 국가 통화코드
        /// </summary>
        public enum NationalCurrencyCode
        {
            /// <summary>
            /// 한국
            /// </summary>
            [StringValue("CMMC00000000024")]
            KOR,
            /// <summary>
            /// 미국
            /// </summary>
            [StringValue("CMMC00000000025")]
            USA,
            /// <summary>
            /// 중국
            /// </summary>
            [StringValue("CMMC00000000026")]
            CHN
        }


        /// <summary>
        /// 은행 코드
        /// </summary>
        public enum BankCode
        {
            /// <summary>
            /// KB국민은행
            /// </summary>
            [StringValue("CMMC00000000045")]
            BankKB,
            /// <summary>
            /// IBK기업은행
            /// </summary>
            [StringValue("CMMC00000000046")]
            BankIBK,
            /// <summary>
            /// 신한은행
            /// </summary>
            [StringValue("CMMC00000000047")]
            BankShinhan,
            /// <summary>
            /// 농협은행
            /// </summary>
            [StringValue("CMMC00000000048")]
            BankNH
        }

        /// <summary>
        /// 국적코드
        /// </summary>
        public enum NationPersonCode
        {
            /// <summary>
            /// 내국인
            /// </summary>
            [StringValue("CMMC00000000061")]
            localPerson,
            /// <summary>
            /// 외국인
            /// </summary>
            [StringValue("CMMC00000000062")]
            foreignerPerson
        }

        /// <summary>
        /// 성별코드
        /// </summary>
        public enum GenderCode
        {
            /// <summary>
            /// 남성
            /// </summary>
            [StringValue("CMMC00000000063")]
            Male,
            /// <summary>
            /// 여성
            /// </summary>
            [StringValue("CMMC00000000064")]
            Female
        }


        /// <summary>
        /// 승인내용코드
        /// </summary>
        public enum ApprovalCode
        {
            /// <summary>
            /// 회원출금
            /// </summary>
            [StringValue("CMMC00000000065")]
            userWithdraw,
            /// <summary>
            /// 회원송금
            /// </summary>
            [StringValue("CMMC00000000066")]
            userTransfer,
            /// <summary>
            /// 거래소출금
            /// </summary>
            [StringValue("CMMC00000000067")]
            exchangeWithdraw
        }

        /// <summary>
        /// 자동예약거래상태코드
        /// </summary>
        public enum AutoReservationTradingStateCode
        {
            /// <summary>
            /// 사용중
            /// </summary>
            [StringValue("CMMC00000000078")]
            Using,
            /// <summary>
            /// 만료
            /// </summary>
            [StringValue("CMMC00000000079")]
            End,
            /// <summary>
            /// 해지
            /// </summary>
            [StringValue("CMMC00000000080")]
            Cancel
        }

        /// <summary>
        /// 결제구분코드
        /// </summary>
        public enum PaymentDivisionCode
        {
            /// <summary>
            /// 자동결제
            /// </summary>
            [StringValue("CMMC00000000081")]
            AutoPayment,
            /// <summary>
            /// 1개월
            /// </summary>
            [StringValue("CMMC00000000082")]
            MonthPayment
        }

        /// <summary>
        /// 시세알림상태코드
        /// </summary>
        public enum AlarmAdditionalStateCode
        {
            /// <summary>
            /// 사용중
            /// </summary>
            [StringValue("CMMC00000000083")]
            Using,
            /// <summary>
            /// 만료
            /// </summary>
            [StringValue("CMMC00000000084")]
            End,
            /// <summary>
            /// 해지
            /// </summary>
            [StringValue("CMMC00000000085")]
            Cancel,
            /// <summary>
            /// 당일해지
            /// </summary>
            [StringValue("CMMC00000000086")]
            TodayCancel
        }

        /// <summary>
        /// 수수료쿠폰상태코드
        /// </summary>
        public enum CommissionCouponStaeCode
        {
            /// <summary>
            /// 사용대기
            /// </summary>
            [StringValue("CMMC00000000087")]
            UseWait,
            /// <summary>
            /// 사용중
            /// </summary>
            [StringValue("CMMC00000000088")]
            Using,
            /// <summary>
            /// 사용완료
            /// </summary>
            [StringValue("CMMC00000000089")]
            UseComplete,
            /// <summary>
            /// 당일해지
            /// </summary>
            [StringValue("CMMC00000000090")]
            Cacnel
        }

        /// <summary>
        /// 거래소출금상태코드
        /// </summary>
        public enum ExchangeWithdrawStateCode
        {
            /// <summary>
            /// 승인대기
            /// </summary>
            [StringValue("CMMC00000000091")]
            approvalWait,
            /// <summary>
            /// 승인중
            /// </summary>
            [StringValue("CMMC00000000092")]
            approvaling,
            /// <summary>
            /// 승인완료
            /// </summary>
            [StringValue("CMMC00000000093")]
            approvalComplete,
            /// <summary>
            /// 승인취소
            /// </summary>
            [StringValue("CMMC00000000094")]
            approvalCacnel,
            /// <summary>
            /// 출금완료
            /// </summary>
            [StringValue("CMMC00000000095")]
            WithdrawComplete
        }

        /// <summary>
        /// 송금상태코드
        /// </summary>
        public enum TransferStateCode
        {
            /// <summary>
            /// 승인대기
            /// </summary>
            [StringValue("CMMC00000000096")]
            approvalWait,
            /// <summary>
            /// 승인중
            /// </summary>
            [StringValue("CMMC00000000097")]
            approvaling,
            /// <summary>
            /// 승인완료
            /// </summary>
            [StringValue("CMMC00000000098")]
            approvalComplete,
            /// <summary>
            /// 송금완료
            /// </summary>
            [StringValue("CMMC00000000099")]
            transferComplete,
            /// <summary>
            /// 취소
            /// </summary>
            [StringValue("CMMC00000000100")]
            cancel,
            /// <summary>
            /// 승인취소
            /// </summary>
            [StringValue("CMMC00000000101")]
            approvalCacnel,
            /// <summary>
            /// 송금실패
            /// </summary>
            [StringValue("CMMC00000000446")]
            transferFail
        }

        /// <summary>
        /// 출금상태코드
        /// </summary>
        public enum WithdrawStateCode
        {
            /// <summary>
            /// 승인대기
            /// </summary>
            [StringValue("CMMC00000000102")]
            approvalWait,
            /// <summary>
            /// 승인중
            /// </summary>
            [StringValue("CMMC00000000103")]
            approvaling,
            /// <summary>
            /// 승인완료
            /// </summary>
            [StringValue("CMMC00000000104")]
            approvalComplete,
            /// <summary>
            /// 송금완료
            /// </summary>
            [StringValue("CMMC00000000105")]
            withdrawComplete,
            /// <summary>
            /// 취소
            /// </summary>
            [StringValue("CMMC00000000106")]
            cancel,
            /// <summary>
            /// 승인취소
            /// </summary>
            [StringValue("CMMC00000000107")]
            approvalCacnel
        }

        /// <summary>
        /// 거래내용코드
        /// </summary>
        public enum TradeContentCode
        {
            /// <summary>
            /// 원화충전(계좌)
            /// </summary>
            [StringValue("CMMC00000000174")]
            krwRechargeAccount,
            /// <summary>
            /// 원화충전(토스)
            /// </summary>
            [StringValue("CMMC00000000175")]
            krwRechargeToss,
            /// <summary>
            /// 원화충전(상품권)
            /// </summary>
            [StringValue("CMMC00000000176")]
            krwRechargeGifrCard,
            /// <summary>
            /// 원화충전(이벤트)
            /// </summary>
            [StringValue("CMMC00000000177")]
            krwRechargeEvent,
            /// <summary>
            /// 구매(지정가)
            /// </summary>
            [StringValue("CMMC00000000178")]
            buyLimitPrice,
            /// <summary>
            /// 구매(시장가)
            /// </summary>
            [StringValue("CMMC00000000179")]
            buyMarketPrice,
            /// <summary>
            /// 구매(자동거래)
            /// </summary>
            [StringValue("CMMC00000000180")]
            buyAutoTradePrice,
            /// <summary>
            /// 구매(코인교환)
            /// </summary>
            [StringValue("CMMC00000000181")]
            buyCoinChangePrice,
            /// <summary>
            /// 판매(지정가)
            /// </summary>
            [StringValue("CMMC00000000182")]
            sellLimitPrice,
            /// <summary>
            /// 판매(시장가)
            /// </summary>
            [StringValue("CMMC00000000183")]
            sellMarketPrice,
            /// <summary>
            /// 판매(자동거래)
            /// </summary>
            [StringValue("CMMC00000000184")]
            sellAutoTradePrice,
            /// <summary>
            /// 판매(코인교환)
            /// </summary>
            [StringValue("CMMC00000000185")]
            sellCoinChangePrice,
            /// <summary>
            /// 원화출금
            /// </summary>
            [StringValue("CMMC00000000186")]
            krwWithdraw,
            /// <summary>
            /// 입금
            /// </summary>
            [StringValue("CMMC00000000187")]
            Deposit,
            /// <summary>
            /// 송금
            /// </summary>
            [StringValue("CMMC00000000188")]
            Transfer,
            /// <summary>
            /// 부가(시세알림)
            /// </summary>
            [StringValue("CMMC00000000189")]
            additionAlarm,
            /// <summary>
            /// 부가(수수료쿠폰)
            /// </summary>
            [StringValue("CMMC00000000190")]
            additionCoupon,
            /// <summary>
            /// 부가(자동거래)
            /// </summary>
            [StringValue("CMMC00000000191")]
            additionAutoTrade,
            /// <summary>
            /// 블록체인수수료
            /// </summary>
            [StringValue("CMMC00000000192")]
            blockchainCommission,
            /// <summary>
            /// 거래소정산판매
            /// </summary>
            [StringValue("CMMC00000000193")]
            exchangeCaculateSell,
            /// <summary>
            /// 거래소정산출금
            /// </summary>
            [StringValue("CMMC00000000194")]
            exchangeCaculateWithdraw,
            /// <summary>
            /// 은행이체수수료
            /// </summary>
            [StringValue("CMMC00000000195")]
            bankTransferCommission
        }

        /// <summary>
        /// 거래구분코드
        /// </summary>
        public enum TradeDivisionCode
        {
            /// <summary>
            /// 충전
            /// </summary>
            [StringValue("CMMC00000000196")]
            Recharge,
            /// <summary>
            /// 구매
            /// </summary>
            [StringValue("CMMC00000000197")]
            Buy,
            /// <summary>
            /// 판매
            /// </summary>
            [StringValue("CMMC00000000198")]
            Sell,
            /// <summary>
            /// 출금
            /// </summary>
            [StringValue("CMMC00000000199")]
            Withdraw,
            /// <summary>
            /// 송금
            /// </summary>
            [StringValue("CMMC00000000200")]
            Transfer,
            /// <summary>
            /// 입금
            /// </summary>
            [StringValue("CMMC00000000201")]
            Deposit,
            /// <summary>
            /// 기타
            /// </summary>
            [StringValue("CMMC00000000202")]
            Etc,
            /// <summary>
            /// 거래소내부
            /// </summary>
            [StringValue("CMMC00000000203")]
            exchangeInner
        }

        /// <summary>
        /// 통화코드
        /// </summary>
        public enum ExchangeCurrencyCode
        {
            /// <summary>
            /// KRW(원화)
            /// </summary>
            [StringValue("CMMC00000000204")]
            KRW,
            /// <summary>
            /// BTC(비트코인)
            /// </summary>
            [StringValue("CMMC00000000205")]
            BTC,
            /// <summary>
            /// ETH(이더리움)
            /// </summary>
            [StringValue("CMMC00000000206")]
            ETH,
            /// <summary>
            /// BCH(비트코인캐시)
            /// </summary>
            [StringValue("CMMC00000000207")]
            BCH,
            /// <summary>
            /// XRP(리플)
            /// </summary>
            [StringValue("CMMC00000000208")]
            XRP,
            /// <summary>
            /// ADA(에이다)
            /// </summary>
            [StringValue("CMMC00000000447")]
            ADA,
            /// <summary>
            /// ETC(이더리움클래식)
            /// </summary>
            [StringValue("CMMC00000000448")]
            ETC,
            /// <summary>
            /// IOTA(아이오타)
            /// </summary>
            [StringValue("CMMC00000000449")]
            IOTA,
            /// <summary>
            /// OMG(오미세고)
            /// </summary>
            [StringValue("CMMC00000000450")]
            OMG,
            /// <summary>
            /// QTUM(퀀텀)
            /// </summary>
            [StringValue("CMMC00000000451")]
            QTUM,
            /// <summary>
            /// SNT(스테이터스 네크워크토큰)
            /// </summary>
            [StringValue("CMMC00000000452")]
            SNT,
            /// <summary>
            /// SGC(스타그램코인)
            /// </summary>
            [StringValue("CMMC00000000685")]
            SGC,
            /// <summary>
            /// EOS(이오스)
            /// </summary>
            [StringValue("CMMC00000000911")]
            EOS,
            /// <summary>
            /// BTG(비트코인골드)
            /// </summary>
            [StringValue("CMMC00000000926")]
            BTG,
            /// <summary>
            /// DASH(대시)
            /// </summary>
            [StringValue("CMMC00000000927")]
            DASH,
            /// <summary>
            /// LTC(라이트코인)
            /// </summary>
            [StringValue("CMMC00000000928")]
            LTC,
            /// <summary>
            /// BMC(빙고뮤직코인)
            /// </summary>
            [StringValue("CMMC00000001005")]
            BMC,
            /// <summary>
            /// SECRET(시크릿코인)
            /// </summary>
            [StringValue("CMMC00000001066")]
            SECRET,
            /// <summary>
            /// USDT(테더코인)
            /// </summary>
            [StringValue("CMMC00000001086")]
            USDT,
            /// <summary>
            /// HDAC(에이치닷 코인)
            /// </summary>
            [StringValue("CMMC00000001126")]
            HDAC,
            /// <summary>
            /// VSTC(비스타 코인)
            /// </summary>
            [StringValue("CMMC00000001206")]
            VSTC,
            /// <summary>
            /// YOA(요아 코인)
            /// </summary>
            [StringValue("CMMC00000001307")]
            YOA,
            /// <summary>
            /// ADM(에이디엠)
            /// </summary>
            [StringValue("CMMC00000001467")]
            ADM,
            /// <summary>
            /// BSV(비트코인에스브이)
            /// </summary>
            [StringValue("CMMC00000001506")]
            BSV,
            /// <summary>
            /// TRX(트론)
            /// </summary>
            [StringValue("CMMC00000001507")]
            TRX,
            /// <summary>
            /// XLM(스텔라루멘)
            /// </summary>
            [StringValue("CMMC00000001527")]
            XLM,
            /// <summary>
            /// POLY(폴리매쓰)
            /// </summary>
            [StringValue("CMMC00000001547")]
            POLY,
            /// <summary>
            /// WAVES(웨이브)
            /// </summary>
            [StringValue("CMMC00000001548")]
            WAVES,
            /// <summary>
            /// MITH(미쓰릴)
            /// </summary>
            [StringValue("CMMC00000001586")]
            MITH,           
            /// <summary>
            /// ICX(아이콘)
            /// </summary>
            [StringValue("CMMC00000001587")]
            ICX,
            /// <summary>
            /// XEM(넴코인)
            /// </summary>
            [StringValue("CMMC00000001606")]
            XEM,
            /// <summary>
            /// PAN(판코인)
            /// </summary>
            [StringValue("CMMC00000001650")]
            PAN,
            /// <summary>
            /// XLAB(엑셀코인)
            /// </summary>
            [StringValue("CMMC00000001665")]
            XLAB,
            /// <summary>
            /// HEC(헤코코인)
            /// </summary>
            [StringValue("CMMC10000000001")]
            HEC,
            /// <summary>
            /// GIFO(기포 토큰)
            /// </summary>
            [StringValue("CMMC10000000003")]
            GIFO,
            /// <summary>
            /// HAMA(하마 토큰)
            /// </summary>
            [StringValue("CMMC10000000004")]
            HAMA,
            /// <summary>
            /// ERB(ERB 토큰)
            /// </summary>
            [StringValue("CMMC10000000008")]
            ERB,
            /// <summary>
            /// 포인트
            /// </summary>
            [StringValue("CMMC00000000248")]
            DestinationTag,         
        }

        /// <summary>
        /// 인증상태코드
        /// </summary>
        public enum CertificationStateCode
        {
            /// <summary>
            /// 대기
            /// </summary>
            [StringValue("CMMC00000000209")]
            Wait,
            /// <summary>
            /// 진행중
            /// </summary>
            [StringValue("CMMC00000000210")]
            Progressing,
            /// <summary>
            /// 완료
            /// </summary>
            [StringValue("CMMC00000000211")]
            Complete
        }


        /// <summary>
        /// 거래상태코드
        /// </summary>
        public enum TradeStateCode
        {
            /// <summary>
            /// 완료
            /// </summary>
            [StringValue("CMMC00000000213")]
            Complete,
            /// <summary>
            /// 처리중
            /// </summary>
            [StringValue("CMMC00000000214")]
            Waiting
        }


        /// <summary>
        /// 로그인실패코드
        /// </summary>
        public enum LoginFailCode
        {
            /// <summary>
            /// 사용자 이메일누락
            /// </summary>
            [StringValue("CMMC00000000215")]
            userEmailFail,
            /// <summary>
            /// 사용자 패스워드 누락
            /// </summary>
            [StringValue("CMMC00000000216")]
            userPasswordFail,
            /// <summary>
            /// 이메일 미존재
            /// </summary>
            [StringValue("CMMC00000000217")]
            notExistEmail,
            /// <summary>
            /// 비밀번호 미일치
            /// </summary>
            [StringValue("CMMC00000000218")]
            notExistPassword,
            /// <summary>
            /// 블록처리된 ID
            /// </summary>
            [StringValue("CMMC00000000219")]
            blockID,
            /// <summary>
            /// 로그인 실패 기타코드
            /// </summary>
            [StringValue("CMMC00000000220")]
            loginFailCode,
            /// <summary>
            /// 등록되지 않은 IP
            /// </summary>
            [StringValue("CMMC00000000385")]
            NotRegIP,
            /// <summary>
            /// 사용불가처리된 아이디
            /// </summary>
            [StringValue("CMMC00000000386")]
            NotUseID,
            /// <summary>
            /// 비밀번호 재설정 아이디
            /// </summary>
            [StringValue("CMMC00000000790")]
            ReSettingPW
        }


        /// <summary>
        /// 클라이언트코드
        /// </summary>
        public enum ClientCd
        {
            /// <summary>
            /// HTS
            /// </summary>
            [StringValue("CMMC00000000221")]
            HTS,
            /// <summary>
            /// MTS
            /// </summary>
            [StringValue("CMMC00000000222")]
            MTS,
            /// <summary>
            /// WEB
            /// </summary>
            [StringValue("CMMC00000000223")]
            WEB
        }

        /// <summary>
        /// 실시간코인 시세시간
        /// </summary>
        public enum RealTimeCoinCode
        {
            /// <summary>
            /// 24시간
            /// </summary>
            [StringValue("CMMC00000000224")]
            Hour24,
            /// <summary>
            /// 12시간
            /// </summary>
            [StringValue("CMMC00000000225")]
            Hour12,
            /// <summary>
            /// 1시간
            /// </summary>
            [StringValue("CMMC00000000226")]
            Hour1
        }

        /// <summary>
        /// 그래프시간간격
        /// </summary>
        public enum GraphTime
        {
            /// <summary>
            /// 1분
            /// </summary>
            [StringValue("CMMC00000000241")]
            min1,
            /// <summary>
            /// 5분
            /// </summary>
            [StringValue("CMMC00000000242")]
            min5,
            /// <summary>
            /// 10분
            /// </summary>
            [StringValue("CMMC00000000243")]
            min10,
            /// <summary>
            /// 30분
            /// </summary>
            [StringValue("CMMC00000000244")]
            min00,
            /// <summary>
            /// 60분
            /// </summary>
            [StringValue("CMMC00000000245")]
            min60,
            /// <summary>
            /// 1일
            /// </summary>
            [StringValue("CMMC00000000246")]
            day1,
            /// <summary>
            /// 1주
            /// </summary>
            [StringValue("CMMC00000000247")]
            week1
        }

        /// <summary>
        /// 주문구분
        /// </summary>
        public enum OrderCode
        {
            /// <summary>
            /// 구매
            /// </summary>
            [StringValue("CMMC00000000249")]
            LimitPrice,
            /// <summary>
            /// 판매
            /// </summary>
            [StringValue("CMMC00000000250")]
            MarketPrice
        }


        /// <summary>
        /// 미결제구분코드
        /// </summary>
        public enum UnsettledDivisionCode
        {
            /// <summary>
            /// 구매대기
            /// </summary>
            [StringValue("CMMC00000000168")]
            buyWait,
            /// <summary>
            /// 판매대기
            /// </summary>
            [StringValue("CMMC00000000169")]
            sellWait
        }

        public enum PushCode
        {
            /// <summary>
            /// 로그인
            /// </summary>
            [StringValue("CMMC00000000284")]
            Login,
            /// <summary>
            /// 공지사항
            /// </summary>
            [StringValue("CMMC00000000285")]
            Notice,
            /// <summary>
            /// 구매/판매
            /// </summary>
            [StringValue("CMMC00000000286")]
            BuySell,
            /// <summary>
            /// 입출금
            /// </summary>
            [StringValue("CMMC00000000287")]
            DepositDrawWith,
            /// <summary>
            /// 부가서비스
            /// </summary>
            [StringValue("CMMC00000000288")]
            Addition,
            /// <summary>
            /// 1:1문의
            /// </summary>
            [StringValue("CMMC00000000289")]
            Question,
            /// <summary>
            /// 시간대별
            /// </summary>
            [StringValue("CMMC00000000290")]
            Time,
            /// <summary>
            /// 금액별
            /// </summary>
            [StringValue("CMMC00000000291")]
            Price,
            /// <summary>
            /// 자동거래등록
            /// </summary>
            [StringValue("CMMC00000000405")]
            AutoTrade,
            /// <summary>
            /// IP등록정보
            /// </summary>
            [StringValue("CMMC00000000427")]
            IPreg,
            /// <summary>
            /// 계정잠금
            /// </summary>
            [StringValue("CMMC00000000428")]
            Lock,
            /// <summary>
            /// 코인교환자동거래 실행
            /// </summary>
            [StringValue("CMMC00000000605")]
            CoinExchange
        }

        public enum MenuCode
        {
            /// <summary>
            /// 거래소현황
            /// </summary>
            [StringValue("CMMC00000000341")]
            ExchangeDashBoard,
            /// <summary>
            /// 비트코인 구매/판매
            /// </summary>
            [StringValue("CMMC00000000342")]
            BTC,
            /// <summary>
            /// 이더리움 구매/판매
            /// </summary>
            [StringValue("CMMC00000000343")]
            ETH,
            /// <summary>
            /// 비트코인캐쉬 구매/판매
            /// </summary>
            [StringValue("CMMC00000000344")]
            BCH,
            /// <summary>
            /// 리플 구매/판매
            /// </summary>
            [StringValue("CMMC00000000345")]
            XRP,
            /// <summary>
            /// 퀀텀 구매/판매
            /// </summary>
            [StringValue("CMMC00000000457")]
            QTUM,
            /// <summary>
            /// SGC(스타그램코인)
            /// </summary>
            [StringValue("CMMC00000000686")]
            SGC,
            /// <summary>
            /// BTG(비트코인골드)
            /// </summary>
            [StringValue("CMMC00000000930")]
            BTG,
            /// <summary>
            /// DASH(대시)
            /// </summary>
            [StringValue("CMMC00000000931")]
            DASH,
            /// <summary>
            /// LTC(라이트코인)
            /// </summary>
            [StringValue("CMMC00000000932")]
            LTC,
            /// <summary>
            /// BMC(빙고뮤직코인)
            /// </summary>
            [StringValue("CMMC00000001005")]
            BMC,
            /// <summary>
            /// 전체거래현황
            /// </summary>
            [StringValue("CMMC00000000346")]
            AllExchange,
            /// <summary>
            /// 원화충전
            /// </summary>
            [StringValue("CMMC00000000347")]
            Recharge,
            /// <summary>
            /// 코인입금
            /// </summary>
            [StringValue("CMMC00000000348")]
            Deposit,
            /// <summary>
            /// 원화출금
            /// </summary>
            [StringValue("CMMC00000000349")]
            Withdraw,
            /// <summary>
            /// 코인송금
            /// </summary>
            [StringValue("CMMC00000000350")]
            Transfer,
            /// <summary>
            /// 정액쿠폰
            /// </summary>
            [StringValue("CMMC00000000351")]
            Coupon,
            /// <summary>
            /// 시세알림
            /// </summary>
            [StringValue("CMMC00000000352")]
            Additional,
            /// <summary>
            /// 자동거래
            /// </summary>
            [StringValue("CMMC00000000353")]
            AutoTrade,
            /// <summary>
            /// 공지사항
            /// </summary>
            [StringValue("CMMC00000000354")]
            Notice,
            /// <summary>
            /// FAQ
            /// </summary>
            [StringValue("CMMC00000000355")]
            FAQ,
            /// <summary>
            /// 1:1문의
            /// </summary>
            [StringValue("CMMC00000000356")]
            QnA,
            /// <summary>
            /// 수수료 및 입출금 한도
            /// </summary>
            [StringValue("CMMC00000000357")]
            Fee,
            /// <summary>
            /// Market Cap
            /// </summary>
            [StringValue("CMMC00000000358")]
            MarkerCap,
            /// <summary>
            /// 인증자료제출
            /// </summary>
            [StringValue("CMMC00000000359")]
            CertifySubmit,
            /// <summary>
            /// 회원정보관리
            /// </summary>
            [StringValue("CMMC00000000360")]
            MyPage,
            /// <summary>
            /// 인증센터
            /// </summary>
            [StringValue("CMMC00000000361")]
            CertifyCenter,
            /// <summary>
            /// 접속정보
            /// </summary>
            [StringValue("CMMC00000000362")]
            LoginHistory,
            /// <summary>
            /// 자산현황
            /// </summary>
            [StringValue("CMMC00000000363")]
            MyAssets,
            /// <summary>
            /// 내 거래내역
            /// </summary>
            [StringValue("CMMC00000000364")]
            TradeHistory,
            /// <summary>
            /// 구매/판매
            /// </summary>
            [StringValue("CMMC00000001046")]
            BuySell

        }

        public enum BoardCode
        {
            /// <summary>
            /// 공지사항
            /// </summary>
            [StringValue("BDMT_000000000001")]
            Notice,
            /// <summary>
            /// FAQ
            /// </summary>
            [StringValue("BDMT_000000000002")]
            FAQ,
            /// <summary>
            /// 1:1문의
            /// </summary>
            [StringValue("BDMT_000000000003")]
            Qna,
            /// <summary>
            /// 코인동향
            /// </summary>
            [StringValue("BDMT_000000000021")]
            Coin
        }
    }

    public class StringValue : System.Attribute
    {
        private string _value;
        public StringValue(string value) { _value = value; }
        public string Value { get { return _value; } }
    }

    public static class StringEnum
    {
        public static string GetStringValue(object value)
        {
            string output = null;
            try
            {
                Type type = value.GetType();
                FieldInfo fi = type.GetField(value.ToString());
                StringValue[] attrs = fi.GetCustomAttributes(typeof(StringValue), false) as StringValue[];
                if (attrs.Length > 0)
                {
                    output = attrs[0].Value;
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
            return output;
        }

        public static T ToEnum<T>(string str)
        {
            try
            {
                foreach (T item in Enum.GetValues(typeof(T)))
                {
                    StringValue[] attributes = (StringValue[])item.GetType().GetField(item.ToString()).GetCustomAttributes(typeof(StringValue), false);
                    if ((attributes != null) && (attributes.Length > 0) && (attributes[0].Value.Equals(str)))
                    {
                        return item;
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
            return (T)Enum.Parse(typeof(T), str, true);
        }


        public static IEnumerable<T> EnumToList<T>()
        {
            Type enumType = typeof(T);

            // Can't use generic type constraints on value types,
            // so have to do check like this
            if (enumType.BaseType != typeof(Enum))
                throw new ArgumentException("T must be of type System.Enum");

            Array enumValArray = Enum.GetValues(enumType);
            List<T> enumValList = new List<T>(enumValArray.Length);

            foreach (int val in enumValArray)
            {
                enumValList.Add((T)Enum.Parse(enumType, val.ToString()));
            }
            return enumValList;
        }
    }
}