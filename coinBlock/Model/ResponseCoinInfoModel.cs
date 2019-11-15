using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinBlock.Model
{
    public class ResponseCoinInfoModel : ResponseBaseModel
    {
        public ResponseCoinInfoDataModel data { get; set; }
        public ResponseCoinInfoModel()
        {
            data = new ResponseCoinInfoDataModel();
        }
    }

    public class ResponseCoinInfoDataModel : IDisposable
    {
        public virtual string marketType { get; set; }
        public virtual string marketCode { get; set; }
        public virtual string curcyCd { get; set; }
        public virtual decimal nowPrc { get; set; } = 0;
        public virtual string nowColor { get; set; }
        public virtual decimal ytdPrc { get; set; } = 0;
        public virtual string ytdPer { get; set; } = "0";
        public virtual string ytdColor { get; set; }
        public virtual decimal maxPrc { get; set; } = 0;
        public virtual string maxPer { get; set; } = "0";
        public virtual string maxColor { get; set; }
        public virtual decimal minPrc { get; set; } = 0;
        public virtual string minPer { get; set; } = "0";
        public virtual string minColor { get; set; }
        public virtual decimal totAmt { get; set; } = 0;
        public virtual decimal ytdAmt { get; set; } = 0;
        public virtual decimal totPrc { get; set; } = 0;
        public virtual decimal ytdTotPrc { get; set; } = 0;
        public virtual decimal volume { get; set; } = 0;
        public virtual string volumeArrow { get; set; }
        public virtual decimal svcTranAmt { get; set; } = 0;

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
