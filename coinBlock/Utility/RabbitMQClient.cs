using coinBlock.Model;
using coinBlock.Model.Dashboard;
using coinBlock.Model.WebSocket;
using DevExpress.Mvvm;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.IO;
using System.IO.Compression;
using coinBlock.Model.Mq;
using coinBlock.ViewModels;

namespace coinBlock.Utility
{
    public class RabbitMQClient
    {
        ConnectionFactory factory;
        IModel channel;
        IConnection connection;

        string hostName = ConfigLib.RabbitMQ_HostName;
        int hostPort = ConfigLib.RabbitMQ_HostPort;
        string userName = ConfigLib.RabbitMQ_UserName;
        string passWord = ConfigLib.RabbitMQ_PassWord;
        string virtualHost = ConfigLib.RabbitMQ_VirtualHost;
        string exchangeName = ConfigLib.RabbitMQ_ExchangeName;

        private static RabbitMQClient _instance;

        //public WsCoinPriceDataModel WsCoinPriceData;
        //public Dictionary<string, ResponseCoinInfoDataModel> CoinInfoData;
        //public Dictionary<string, ResponseExchangeDashboardFillDataModel> FillData;
        //public Dictionary<string, ResponseExchangeDashboardOrderDataModel> OrderData;
        //public Dictionary<string, ResponseExchangeDashboardCoinDataModel> CoinPriceData;

        public Dictionary<KeyValuePair<string, string>, WsCoinPriceDataModel> WsCoinPriceData2;
        public Dictionary<KeyValuePair<string, string>, ResponseExchangeDashboardCoinDataModel> CoinPriceData2;
        public Dictionary<KeyValuePair<string, string>, ResponseCoinInfoDataModel> CoinInfoData2;
        public Dictionary<KeyValuePair<string, string>, ResponseExchangeDashboardFillDataModel> FillData2;
        public Dictionary<KeyValuePair<string, string>, ResponseExchangeDashboardOrderDataModel> OrderData2;

        JavaScriptSerializer Jss = new JavaScriptSerializer();

        public static RabbitMQClient Instance()
        {
            if (_instance == null)
            {
                _instance = new RabbitMQClient();
            }
            return _instance;
        }

        protected RabbitMQClient()
        {
            try
            {
                //WsCoinPriceData = new WsCoinPriceDataModel();
                //CoinInfoData = new Dictionary<string, ResponseCoinInfoDataModel>();
                //FillData = new Dictionary<string, ResponseExchangeDashboardFillDataModel>();
                //OrderData = new Dictionary<string, ResponseExchangeDashboardOrderDataModel>();
                //CoinPriceData = new Dictionary<string, ResponseExchangeDashboardCoinDataModel>();

                WsCoinPriceData2 = new Dictionary<KeyValuePair<string, string>, WsCoinPriceDataModel>();
                CoinPriceData2 = new Dictionary<KeyValuePair<string, string>, ResponseExchangeDashboardCoinDataModel>();
                CoinInfoData2 = new Dictionary<KeyValuePair<string, string>, ResponseCoinInfoDataModel>();
                FillData2 = new Dictionary<KeyValuePair<string, string>, ResponseExchangeDashboardFillDataModel>();
                OrderData2 = new Dictionary<KeyValuePair<string, string>, ResponseExchangeDashboardOrderDataModel>();

                factory = new ConnectionFactory() { HostName = hostName, Port = hostPort, UserName = userName, Password = passWord, VirtualHost = virtualHost };
                connection = factory.CreateConnection();
                channel = connection.CreateModel();

                channel.ExchangeDeclare(exchange: exchangeName, type: "fanout");

                var queueName = channel.QueueDeclare().QueueName;
                channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: "");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += Consumer_Received;
                channel.BasicConsume(queueName, true, consumer);
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public void ClientClose()
        {
            try
            {
                if (factory != null)
                {
                    channel.Close();
                    channel.Dispose();
                    connection.Close();
                    connection.Dispose();
                    factory = null;
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        private void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            try
            {
                byte[] debodyMsg = Convert.FromBase64String(Encoding.UTF8.GetString(e.Body));

                //프리픽스 값 2바이트 제거
                List<byte> temp = debodyMsg.ToList();
                temp.RemoveRange(0, 2);
                temp.ToArray();

                //압축해제
                using (MemoryStream decompress = new MemoryStream())
                {
                    using (MemoryStream ms = new MemoryStream(temp.ToArray()))
                    {
                        using (DeflateStream ds = new DeflateStream(ms, CompressionMode.Decompress))
                        {
                            ds.CopyTo(decompress);
                            ds.Close();
                        }
                    }

                    decompress.Position = 0;
                    string body = Encoding.UTF8.GetString(decompress.ToArray());
                    Process(body);
                }

                temp = null;

                //var body = Encoding.UTF8.GetString(e.Body);
                //SysLog.Info("Rece=======[{0}]", body);
                //Process(body);
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        private void Process(string message)
        {
            try
            {
                var response = new JavaScriptSerializer().Deserialize<dynamic>(message);

                string marketType = response["mt"];
                string marketCode = response["mc"];
                string curcyCd = response["cd"];
                string dt = response["dt"];

                if (MainViewModel.CoinData == null) return;

                switch (dt)
                {
                    //코인별 시세
                    case "cr":
                        using (var tempWsCoinPriceData = new WsCoinPriceDataModel())
                        {
                            tempWsCoinPriceData.marketType = marketType;
                            tempWsCoinPriceData.marketCode = marketCode;

                            var tempWsCoinPriceData1 = Jss.Deserialize<MqCoinPriceModel>(message).data;

                            foreach (var item in tempWsCoinPriceData1.list)
                            {
                                string floatFormat = "n8";
                                if (marketCode == CommonLib.StandardCurcyCd)
                                {
                                    ResponseCoinListModel cl = MainViewModel.CoinData.list.Where(w => w.CoinCode == item.cd).FirstOrDefault();
                                    floatFormat = "n" + (cl == null ? "0" : cl.CashDecimal.ToString());
                                }

                                tempWsCoinPriceData.list.Add(new WsCoinPriceListModel() { curcyCd = item.cd, coinPrc = item.cp, maxPrc = item.bp, minPrc = item.sp, ytdPrc = item.yp, floatFormat = floatFormat });
                            }

                            if (WsCoinPriceData2.ToList().Where(w => w.Key.Key == marketCode && w.Key.Value == marketCode).Count() == 0)
                            {
                                WsCoinPriceData2.Add(new KeyValuePair<string, string>(marketCode, marketCode), tempWsCoinPriceData);
                            }
                            else
                            {
                                WsCoinPriceData2[new KeyValuePair<string, string>(marketCode, marketCode)] = tempWsCoinPriceData;
                            }

                            if (WsCoinPriceData2 != null)
                            {
                                Messenger.Default.Send(tempWsCoinPriceData);
                            }
                        };
                        break;
                    //거래소 실시간 코인시세
                    case "rp":
                        using (var tempCoinPriceData = new ResponseExchangeDashboardCoinDataModel())
                        {
                            tempCoinPriceData.Time = curcyCd;
                            tempCoinPriceData.marketType = marketType;
                            tempCoinPriceData.marketCode = marketCode;

                            var tempCoinPriceData1 = Jss.Deserialize<MqExchangeDashboardCoinModel>(message).data;

                            foreach (var item in tempCoinPriceData1.list)
                            {
                                string floatFormat = "n8";
                                if (marketCode == CommonLib.StandardCurcyCd)
                                {
                                    ResponseCoinListModel cl = MainViewModel.CoinData.list.Where(w => w.CoinCode == item.cd).FirstOrDefault();
                                    floatFormat = "n" + (cl == null ? "0" : cl.CashDecimal.ToString());
                                }

                                tempCoinPriceData.list.Add(new ResponseExchangeDashboardCoinListModel() { curcyCd = item.cd, coinNm = item.cn, coinPrc = item.cp, chgPrc = item.hp, volume = item.vo, svcTranPrc = item.tc, svcTranAmt = item.sa, floatFormat = floatFormat });
                            }

                            if (CoinPriceData2.ToList().Where(w => w.Key.Key == marketCode && w.Key.Value == tempCoinPriceData.Time).Count() == 0)
                            {
                                CoinPriceData2.Add(new KeyValuePair<string, string>(marketCode, tempCoinPriceData.Time), tempCoinPriceData);
                            }
                            else
                            {
                                CoinPriceData2[new KeyValuePair<string, string>(marketCode, tempCoinPriceData.Time)] = tempCoinPriceData;
                            }

                            if (tempCoinPriceData != null)
                            {
                                Messenger.Default.Send(tempCoinPriceData);
                            }
                        };
                        break;
                    //전체 주문내역
                    case "ob":
                        using (var tempOrderData = new ResponseExchangeDashboardOrderDataModel())
                        {
                            tempOrderData.curcyCd = curcyCd;
                            tempOrderData.marketType = marketType;
                            tempOrderData.marketCode = marketCode;

                            var tempOrderData1 = Jss.Deserialize<MqExchangeDashboardOrderModel>(message).data;

                            string floatFormat = "n8";
                            if (marketCode == CommonLib.StandardCurcyCd)
                            {
                                ResponseCoinListModel cl = MainViewModel.CoinData.list.Where(w => w.CoinCode == curcyCd).FirstOrDefault();
                                floatFormat = "n" + (cl == null ? "0" : cl.CashDecimal.ToString());
                            }

                            foreach (var item in tempOrderData1.list)
                            {
                                tempOrderData.list.Add(new ResponseExchangeDashboardOrderListModel() { buyCnAmt = item.ba, buyTranPrc = item.bp, sellCnAmt = item.sa, sellTranPrc = item.sp, floatFormat = floatFormat });
                            }

                            if (OrderData2.ToList().Where(w => w.Key.Key == marketCode && w.Key.Value == tempOrderData.curcyCd).Count() == 0)
                            {
                                OrderData2.Add(new KeyValuePair<string, string>(marketCode, tempOrderData.curcyCd), tempOrderData);
                            }
                            else
                            {
                                OrderData2[new KeyValuePair<string, string>(marketCode, tempOrderData.curcyCd)] = tempOrderData;
                            }

                            if (tempOrderData != null)
                            {
                                Messenger.Default.Send(tempOrderData);
                            }
                        };
                        break;
                    //전체 체결 내역
                    case "oc":
                        using (var tempFillData = new ResponseExchangeDashboardFillDataModel())
                        {
                            tempFillData.curcyCd = curcyCd;
                            tempFillData.marketType = marketType;
                            tempFillData.marketCode = marketCode;

                            var tempFillData1 = Jss.Deserialize<MqExchangeDashboardFillModel>(message).data;

                            string floatFormat = "n8";
                            if (marketCode == CommonLib.StandardCurcyCd)
                            {
                                ResponseCoinListModel cl = MainViewModel.CoinData.list.Where(w => w.CoinCode == curcyCd).FirstOrDefault();
                                floatFormat = "n" + (cl == null ? "0" : cl.CashDecimal.ToString());
                            }

                            foreach (var item in tempFillData1.list)
                            {
                                tempFillData.list.Add(new ResponseExchangeDashboardFillListModel() { tradeTime = item.tt, coinPrc = item.cp, cnAmt = item.ca, tradePrc = item.tp, curcyCd = item.cd, orderCd = item.oc == "B" ? "CMMC00000000197" : "CMMC00000000198", floatFormat = floatFormat });
                            }

                            if (FillData2.ToList().Where(w => w.Key.Key == marketCode && w.Key.Value == tempFillData.curcyCd).Count() == 0)
                            {
                                FillData2.Add(new KeyValuePair<string, string>(marketCode, tempFillData.curcyCd), tempFillData);
                            }
                            else
                            {
                                FillData2[new KeyValuePair<string, string>(marketCode, tempFillData.curcyCd)] = tempFillData;
                            }

                            if (tempFillData != null)
                            {
                                Messenger.Default.Send(tempFillData);
                            }
                        };
                        break;
                    //코인 체결 정보
                    case "ci":
                        using (var tempCoinInfoData = new ResponseCoinInfoDataModel())
                        {
                            var tempCoinInfoData1 = Jss.Deserialize<MqCoinInfoModel>(message).data;

                            ResponseCoinListModel cl = MainViewModel.CoinData.list.Where(w => w.CoinCode == curcyCd).FirstOrDefault();
                            string floatFormat = "n" + (cl == null ? "0" : cl.CashDecimal.ToString());

                            tempCoinInfoData.curcyCd = curcyCd;
                            tempCoinInfoData.marketType = marketType;
                            tempCoinInfoData.marketCode = marketCode;
                            tempCoinInfoData.nowPrc = tempCoinInfoData1.cp;
                            tempCoinInfoData.ytdPrc = tempCoinInfoData1.yp;
                            tempCoinInfoData.ytdPer = tempCoinInfoData1.yr;
                            tempCoinInfoData.maxPrc = tempCoinInfoData1.mp;
                            tempCoinInfoData.maxPer = tempCoinInfoData1.mr;
                            tempCoinInfoData.minPrc = tempCoinInfoData1.np;
                            tempCoinInfoData.minPer = tempCoinInfoData1.nr;
                            tempCoinInfoData.totAmt = tempCoinInfoData1.ta;
                            tempCoinInfoData.ytdAmt = tempCoinInfoData1.ya;
                            tempCoinInfoData.totPrc = tempCoinInfoData1.tc;
                            tempCoinInfoData.ytdTotPrc = tempCoinInfoData1.yc;
                            tempCoinInfoData.volume = tempCoinInfoData1.vo;
                            tempCoinInfoData.svcTranAmt = tempCoinInfoData1.sa;
                            tempCoinInfoData.floatFormat = floatFormat;

                            if (CoinInfoData2.ToList().Where(w => w.Key.Key == marketCode && w.Key.Value == tempCoinInfoData.curcyCd).Count() == 0)
                            {
                                CoinInfoData2.Add(new KeyValuePair<string, string>(marketCode, tempCoinInfoData.curcyCd), tempCoinInfoData);
                            }
                            else
                            {
                                CoinInfoData2[new KeyValuePair<string, string>(marketCode, tempCoinInfoData.curcyCd)] = tempCoinInfoData;
                            }

                            if (tempCoinInfoData != null)
                            {
                                Messenger.Default.Send(tempCoinInfoData);
                            }
                        };
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
    }
}