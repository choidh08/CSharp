using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace coinBlock.Model.Dashboard
{
    public class ResponseExchangeDashboardCoinModel : ResponseBaseModel
    {
        public virtual ResponseExchangeDashboardCoinDataModel data { get; set; }
        public ResponseExchangeDashboardCoinModel()
        {
            data = new ResponseExchangeDashboardCoinDataModel();
        }
    }

    public class ResponseExchangeDashboardCoinDataModel : IDisposable
    {
        public virtual string marketType { get; set; }
        public virtual string marketCode { get; set; }
        public virtual string Time { get; set; }
        public virtual ObservableCollection<ResponseExchangeDashboardCoinListModel> list { get; set; }
        public ResponseExchangeDashboardCoinDataModel()
        {
            list = new ObservableCollection<ResponseExchangeDashboardCoinListModel>();
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

    public class ResponseExchangeDashboardCoinListModel : IDisposable
    {
        ///코인코드
        public virtual string curcyCd { get; set; }
        /// <summary>
        /// 코인명
        /// </summary>
        public virtual string coinNm { get; set; }
        /// <summary>
        /// 코인명
        /// </summary>
        public virtual string coinNmView { get; set; }
        /// <summary>
        /// 코인시세
        /// </summary>
        public virtual decimal coinPrc { get; set; }
        /// <summary>
        /// 변동금액
        /// </summary>
        public virtual decimal chgPrc { get; set; }
        /// <summary>
        /// 변동률
        /// </summary>
        public virtual decimal volume { get; set; }
        /// <summary>
        /// 거래금액
        /// </summary>
        public virtual decimal svcTranPrc { get; set; }
        /// <summary>
        /// 거래량
        /// </summary>
        public virtual decimal svcTranAmt { get; set; }
        public virtual decimal chgPrc2 { get; set; }
        public virtual string Image { get; set; }
        public virtual string coinImage { get; set; }
        public virtual FontWeight bold_Gb { get; set; }
        public virtual string ieoImage { get; set; }
        public virtual Visibility ieoVisible { get; set; } = Visibility.Collapsed;        

        public virtual string floatFormat { get; set;}

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