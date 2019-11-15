using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinBlock.Model.Dashboard
{
    public class ResponseExchangeDashboardOrderModel : ResponseBaseModel
    {
        public ResponseExchangeDashboardOrderDataModel data { get; set; }

        public ResponseExchangeDashboardOrderModel()
        {
            data = new ResponseExchangeDashboardOrderDataModel();
        }
    }

    public class ResponseExchangeDashboardOrderDataModel : IDisposable
    {
        public virtual string marketType { get; set; }
        public virtual string marketCode { get; set; }
        public virtual string curcyCd { get; set; }
        public virtual ObservableCollection<ResponseExchangeDashboardOrderListModel> list { get; set; }
        public ResponseExchangeDashboardOrderDataModel()
        {
            list = new ObservableCollection<ResponseExchangeDashboardOrderListModel>();
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

    public class ResponseExchangeDashboardOrderListModel : IDisposable
    {
        /// <summary>
        /// 구매주문량
        /// </summary>
        public virtual string buyCnAmt { get; set; }
        /// <summary>
        /// 구매금액
        /// </summary>
        public virtual string buyTranPrc { get; set; } = "0";
        /// <summary>
        /// 전일 시세와 구매 차이 퍼센트(클라이언트에서 계산)
        /// </summary>
        public virtual string buyTranPrcPer { get; set; }
        /// <summary>
        /// 판매주문량
        /// </summary>
        public virtual string sellCnAmt { get; set; }
        /// <summary>
        /// 판매금액
        /// </summary>
        public virtual string sellTranPrc { get; set; } = "0";
        /// <summary>
        /// 전일 시세와 판매 차이 퍼센트(클라이언트에서 계산)
        /// </summary>
        public virtual string sellTranPrcPer { get; set; }
        /// <summary>
        /// 전일 시세랑 비교하여 색상 설정 금액인지 확인 필드(클라이언트에서 계산)
        /// </summary>
        public virtual string fillPrc { get; set; }

        /// <summary>
        /// 현재 리스트 중 구매 수량이 가장 큰 값
        /// </summary>
        public virtual string maxCnAmt { get; set; }

        public virtual string floatFormat { get; set; } = "n0";

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