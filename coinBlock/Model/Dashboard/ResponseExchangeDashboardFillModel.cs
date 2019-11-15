using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinBlock.Model.Dashboard
{
    public class ResponseExchangeDashboardFillModel : ResponseBaseModel
    {
        public ResponseExchangeDashboardFillDataModel data { get; set; }
        public ResponseExchangeDashboardFillModel()
        {
            data = new ResponseExchangeDashboardFillDataModel();
        }
    }

    public class ResponseExchangeDashboardFillDataModel : IDisposable
    {
        public virtual string marketType { get; set; }
        public virtual string marketCode { get; set; }
        public virtual string curcyCd { get; set; }
        public ObservableCollection<ResponseExchangeDashboardFillListModel> list { get; set; }
        public ResponseExchangeDashboardFillDataModel()
        {
            list = new ObservableCollection<ResponseExchangeDashboardFillListModel>();
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

    public class ResponseExchangeDashboardFillListModel : IDisposable
    {
        /// <summary>
        /// 체결시각
        /// </summary>
        public virtual string tradeTime { get; set; }
        /// <summary>
        /// 1코인가격
        /// </summary>
        public virtual string coinPrc { get; set; }
        /// <summary>
        /// 체결수량
        /// </summary>
        public virtual string cnAmt { get; set; }
        /// <summary>
        /// 체결가
        /// </summary>
        public virtual string tradePrc { get; set; }
        /// <summary>
        /// 통화통화 
        /// </summary>
        public virtual string curcyCd { get; set; }
        /// <summary>
        /// 주문구분(구매,판매)
        /// </summary>
        public virtual string orderCd { get; set; }

        /// <summary>
        /// 전 체결가랑 비교하여 색상 설정(크거나 같으면 빨강, 작으면 파랑)
        /// </summary>
        public virtual string fillPrc { get; set; }

        public virtual string floatDivision { get; set; }

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