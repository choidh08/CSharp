﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinBlock.Model.Mq
{
    public class MqExchangeDashboardCoinModel : MqBaseModel
    {
        public virtual MqExchangeDashboardCoinDataModel data { get; set; }
        public MqExchangeDashboardCoinModel()
        {
            data = new MqExchangeDashboardCoinDataModel();
        }
    }

    public class MqExchangeDashboardCoinDataModel : IDisposable
    {
        public virtual string marketType { get; set; }
        public virtual string Time { get; set; }
        public virtual ObservableCollection<MqExchangeDashboardCoinListModel> list { get; set; }
        public MqExchangeDashboardCoinDataModel()
        {
            list = new ObservableCollection<MqExchangeDashboardCoinListModel>();
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

    public class MqExchangeDashboardCoinListModel : IDisposable
    {
        ///코인코드
        public virtual string cd { get; set; }
        /// <summary>
        /// 코인명
        /// </summary>
        public virtual string cn { get; set; }
        /// <summary>
        /// 코인시세
        /// </summary>
        public virtual decimal cp { get; set; }
        /// <summary>
        /// 변동금액
        /// </summary>
        public virtual decimal hp { get; set; }
        /// <summary>
        /// 변동률
        /// </summary>
        public virtual decimal vo { get; set; }
        /// <summary>
        /// 거래금액
        /// </summary>
        public virtual decimal tc { get; set; }
        /// <summary>
        /// 거래량
        /// </summary>
        public virtual decimal sa { get; set; }

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