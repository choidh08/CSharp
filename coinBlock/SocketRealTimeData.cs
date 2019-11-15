using coinBlock.Model.Dashboard;
using coinBlock.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace coinBlock
{
    public class SocketRealTimeData
    {
        Dictionary<EnumLib.RealTimeCoinCode, ResponseExchangeDashboardCoinDataModel> CoinData;

        private static SocketRealTimeData _instance;
        public static SocketRealTimeData Instance()
        {
            if (_instance == null)
            {
                _instance = new SocketRealTimeData();
            }
            return _instance;
        }

        private SocketRealTimeData()
        {
            CoinData = new Dictionary<EnumLib.RealTimeCoinCode, ResponseExchangeDashboardCoinDataModel>();
        }

        public void SetCoinData(EnumLib.RealTimeCoinCode ctype, ResponseExchangeDashboardCoinDataModel item)
        {
            try
            {
                int i = CoinData.Where(w => w.Key == ctype).Count();
                if (i == 0)
                {
                    CoinData.Add(ctype, item);
                }
                else
                {
                    CoinData[ctype] = item;
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public ResponseExchangeDashboardCoinDataModel GetCoinData(EnumLib.RealTimeCoinCode ctype)
        {
            ResponseExchangeDashboardCoinDataModel item = new ResponseExchangeDashboardCoinDataModel();
            try
            {
                int i = CoinData.Where(w => w.Key == ctype).Count();
                if (i != 0)
                {
                    item = CoinData[ctype];
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
            return item;
        }
    }
}