using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinBlock.Model.Dashboard
{
    public class ResponseExchangeDashboardChartModel : ResponseBaseModel
    {
        public ResponseExchangeDashboardChartDataModel data { get; set; }

        public ResponseExchangeDashboardChartModel()
        {
            data = new ResponseExchangeDashboardChartDataModel();
        }
    }

    public class ResponseExchangeDashboardChartDataModel
    {
        public ObservableCollection<ResponseExchangeDashboardChartListModel> list { get; set; }
        public ResponseExchangeDashboardChartDataModel()
        {
            list = new ObservableCollection<ResponseExchangeDashboardChartListModel>();
        }
    }

    public class ResponseExchangeDashboardChartListModel
    {
        /// <summary>
        /// 시간
        /// </summary>
        public virtual string tradeTime { get; set; }
        /// <summary>
        /// 시작가
        /// </summary>
        public virtual string openValue { get; set; }
        /// <summary>
        /// 종료가
        /// </summary>
        public virtual string closeValue { get; set; }
        /// <summary>
        /// 최고가
        /// </summary>
        public virtual string hightValue { get; set; }
        /// <summary>
        /// 최저가
        /// </summary>
        public virtual string lowValue { get; set; }
        /// <summary>
        /// 이동평균 15
        /// </summary>
        public virtual string ma15Value { get; set; }
        /// <summary>
        /// 이동평균 50
        /// </summary>
        public virtual string ma50Value { get; set; }
        /// <summary>
        /// 거래량
        /// </summary>
        public virtual string tradeValue { get; set; }


        public virtual string color { get; set; }
    }
}
