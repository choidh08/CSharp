using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinBlock.Utility
{
    public static class ConfigLib
    {
        /// <summary>
        /// 웹소켓
        /// </summary>
        public static string WebSocketUrl = "ws://rtime1.coincapital.asia/bt/realtime/data"; //개발

        /// <summary>
        /// API 주소
        /// </summary>        
        public static readonly string ApiUrl = "http://oapi.coinblock.kr/"; //운영(CoinBlock)        
        //public static readonly string ApiUrl = "http://iexblue.xlogic.co.kr/"; //스테이징(CoinBlock)        

        /// <summary>
        /// Web 주소
        /// </summary>
        public static readonly string WebUrl = "http://www.coinblock.kr/";//(운영)
        //public static readonly string WebUrl = "http://wexblue.xlogic.co.kr/";//(스테이징)

        public static readonly string RabbitMQ_HostName = "121.78.60.177"; //(운영)
        //public static readonly string RabbitMQ_HostName = "121.78.60.154"; //(스테이징)

        public static readonly int RabbitMQ_HostPort = 10071;
        public static readonly string RabbitMQ_UserName = "CoinBlock";
        public static readonly string RabbitMQ_PassWord = "CoinBlock";
        public static readonly string RabbitMQ_VirtualHost = "CoinBlock";
        public static readonly string RabbitMQ_ExchangeName = "MarketPrice";

        /// <summary>
        /// 공용 갱신 타임(초)
        /// </summary>
        public static readonly int DefaultTime = 3;
    }
}
