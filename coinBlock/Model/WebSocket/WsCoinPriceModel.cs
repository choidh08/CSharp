using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace coinBlock.Model.WebSocket
{
    public class WsCoinPriceModel : IDisposable
    {
        public WsCoinPriceDataModel data { get; set; }
        public WsCoinPriceModel()
        {
            data = new WsCoinPriceDataModel();
        }

        #region IDisposable Support
        private bool disposedValue = false; // 중복 호출을 검색하려면

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 관리되는 상태(관리되는 개체)를 삭제합니다.
                }

                // TODO: 관리되지 않는 리소스(관리되지 않는 개체)를 해제하고 아래의 종료자를 재정의합니다.
                // TODO: 큰 필드를 null로 설정합니다.

                disposedValue = true;
            }
        }

        // TODO: 위의 Dispose(bool disposing)에 관리되지 않는 리소스를 해제하는 코드가 포함되어 있는 경우에만 종료자를 재정의합니다.
        // ~BaseModel() {
        //   // 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
        //   Dispose(false);
        // }

        // 삭제 가능한 패턴을 올바르게 구현하기 위해 추가된 코드입니다.
        public void Dispose()
        {
            // 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
            Dispose(true);
            // TODO: 위의 종료자가 재정의된 경우 다음 코드 줄의 주석 처리를 제거합니다.
            //GC.SuppressFinalize(this);
        }
        #endregion
    }

    public class WsCoinPriceDataModel : IDisposable
    {
        public virtual string marketType { get; set; }
        public virtual string marketCode { get; set; }
        public virtual ObservableCollection<WsCoinPriceListModel> list { get; set; }
        public WsCoinPriceDataModel()
        {
            list = new ObservableCollection<WsCoinPriceListModel>();
        }

        #region IDisposable Support
        private bool disposedValue = false; // 중복 호출을 검색하려면

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 관리되는 상태(관리되는 개체)를 삭제합니다.
                }

                // TODO: 관리되지 않는 리소스(관리되지 않는 개체)를 해제하고 아래의 종료자를 재정의합니다.
                // TODO: 큰 필드를 null로 설정합니다.

                disposedValue = true;
            }
        }

        // TODO: 위의 Dispose(bool disposing)에 관리되지 않는 리소스를 해제하는 코드가 포함되어 있는 경우에만 종료자를 재정의합니다.
        // ~BaseModel() {
        //   // 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
        //   Dispose(false);
        // }

        // 삭제 가능한 패턴을 올바르게 구현하기 위해 추가된 코드입니다.
        public void Dispose()
        {
            // 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
            Dispose(true);
            // TODO: 위의 종료자가 재정의된 경우 다음 코드 줄의 주석 처리를 제거합니다.
            //GC.SuppressFinalize(this);
        }
        #endregion
    }

    public class WsCoinPriceListModel : IDisposable
    {
        ///코인코드
        public virtual string curcyCd { get; set; }
        /// <summary>
        /// 코인시세
        /// </summary>
        public virtual decimal? coinPrc { get; set; }
        /// <summary>
        /// 미체결 구매 최고가 
        /// </summary>
        public virtual decimal? maxPrc { get; set; }
        /// <summary>
        /// 미체결 판매 최저가
        /// </summary>
        public virtual decimal? minPrc { get; set; }
        /// <summary>
        /// 전일 종가
        /// </summary>
        public virtual decimal? ytdPrc { get; set; }

        /////////////////////아래 변수 클라이언트에서 처리
        /// <summary>
        /// 코인 이미지 아이콘
        /// </summary>
        public virtual string coinImage { get; set; }
        /// <summary>
        /// 코인 표시명
        /// </summary>
        public virtual string coinDisplayName { get; set; }

        public virtual string NowPriceLangView { get; set; }
        //코인 현재 시세 및 등락 표시
        public virtual string CoinPrcData { get; set; }

        /// <summary>
        /// 시세 등락 표시
        /// </summary>
        public virtual string UpDown { get; set; }
        /// <summary>
        /// 시세 등락 표시 색상표
        /// </summary>
        public virtual string DataColor { get; set; }

        /// <summary>
        /// 거래가능_코인수량
        /// </summary>
        public virtual decimal posCnAmt { get; set; }
        /// <summary>
        /// 거래중_코인수량
        /// </summary>
        public virtual decimal impCnAmt { get; set; }
        /// <summary>
        /// 통화수량 합계(거래가능 + 거래중)
        /// </summary>
        public virtual decimal totCoinAmt { get; set; }
        /// <summary>
        /// 원화금액
        /// </summary>
        public virtual decimal kwdPrc { get; set; }

        public virtual decimal lendgMny { get; set; }

        public virtual string exchangeCode { get; set; }

        public virtual Visibility Visible { get; set; } = Visibility.Visible;

        public virtual string floatFormat { get; set; }

        #region IDisposable Support
        private bool disposedValue = false; // 중복 호출을 검색하려면

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 관리되는 상태(관리되는 개체)를 삭제합니다.
                }

                // TODO: 관리되지 않는 리소스(관리되지 않는 개체)를 해제하고 아래의 종료자를 재정의합니다.
                // TODO: 큰 필드를 null로 설정합니다.

                disposedValue = true;
            }
        }

        // TODO: 위의 Dispose(bool disposing)에 관리되지 않는 리소스를 해제하는 코드가 포함되어 있는 경우에만 종료자를 재정의합니다.
        // ~BaseModel() {
        //   // 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
        //   Dispose(false);
        // }

        // 삭제 가능한 패턴을 올바르게 구현하기 위해 추가된 코드입니다.
        public void Dispose()
        {
            // 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
            Dispose(true);
            // TODO: 위의 종료자가 재정의된 경우 다음 코드 줄의 주석 처리를 제거합니다.
            //GC.SuppressFinalize(this);
        }
        #endregion
    }
}
