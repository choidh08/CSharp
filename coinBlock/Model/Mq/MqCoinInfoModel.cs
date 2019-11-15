using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinBlock.Model.Mq
{
    public class MqCoinInfoModel : MqBaseModel
    {
        public MqCoinInfoDataModel data { get; set; }
        public MqCoinInfoModel()
        {
            data = new MqCoinInfoDataModel();
        }
    }

    public class MqCoinInfoDataModel : IDisposable
    {
        /// <summary>
        /// 코인코드
        /// </summary>
        public virtual string cd { get; set; }
        /// <summary>
        /// 현재시세
        /// </summary>
        public virtual decimal cp { get; set; } = 0;
        /// <summary>
        /// 전일종가
        /// </summary>
        public virtual decimal yp { get; set; } = 0;
        /// <summary>
        /// 현재시세 와 전일종가 차이 퍼센트
        /// </summary>
        public virtual string yr { get; set; } = "0";
        /// <summary>
        /// 당일 최고가
        /// </summary>
        public virtual decimal mp { get; set; } = 0;
        /// <summary>
        /// 현재 시세와 당일 최고가 차이 퍼센트
        /// </summary>
        public virtual string mr { get; set; } = "0";
        /// <summary>
        /// 당일 최저가
        /// </summary>
        public virtual decimal np { get; set; } = 0;
        /// <summary>
        /// 현재 시세와 당일 최저가 차이 퍼센트
        /// </summary>
        public virtual string nr { get; set; } = "0";
        /// <summary>
        /// 당일 거래 수량
        /// </summary>
        public virtual decimal ta { get; set; } = 0;
        /// <summary>
        /// 전일 거래 수량
        /// </summary>
        public virtual decimal ya { get; set; } = 0;
        /// <summary>
        /// 당일 거래 금액
        /// </summary>
        public virtual decimal tc { get; set; } = 0;
        /// <summary>
        /// 전일 거래 금액
        /// </summary>
        public virtual decimal yc { get; set; } = 0;
        /// <summary>
        /// 변동률(당일 과 전일 거래량 차이)
        /// </summary>
        public virtual decimal vo { get; set; } = 0;
        /// <summary>
        /// 당일 거래량
        /// </summary>
        public virtual decimal sa { get; set; }
        ///// <summary>
        ///// 코인명
        ///// </summary>
        public virtual string cn { get; set; }
        ///// <summary>
        ///// 변동금액
        ///// </summary>
        public virtual double hp { get; set; }
        /// <summary>
        /// 미체결 구매 최고가 
        /// </summary>
        public virtual decimal? bp { get; set; }
        /// <summary>
        /// 미체결 판매 최저가
        /// </summary>
        public virtual decimal? sp { get; set; }

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