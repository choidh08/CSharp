using coinBlock.Model;
using coinBlock.Model.DepositWithdraw;
using coinBlock.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Windows.Media.Imaging;

namespace coinBlock.Utility
{
    public static class CommonLib
    {
        public static string ExchangeNm { get; set; } = "CoinBlock";
        public static string StandardCurcyNm { get; set; }
        public static string StandardCurcyCd { get; set; }

        public static string GetTime
        {
            get
            {
                return DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt", new System.Globalization.CultureInfo("en-US"));
            }
        }

        public static string Client_IP
        {
            get
            {
                try
                {                    
                    string ClientIP = string.Empty;

                    //내부아이피
                    IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
                    for (int i = 0; i < host.AddressList.Length; i++)
                    {
                        if (host.AddressList[i].AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            ClientIP = host.AddressList[i].ToString();
                        }
                    }

                    //외부아이피1
                    //string url = "http://checkip.dyndns.org/";
                    //string html = GetResponse(url);
                    //string headPattern = @"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}";
                    //Regex regex = new Regex(headPattern);
                    //Match m = regex.Match(html);

                    //외부아이피2
                    //WebClient wc = new WebClient();
                    //ClientIP = wc.DownloadString("http://icanhazip.com/").Replace("\n","");

                    return ClientIP;
                }
                catch (Exception ex)
                {
                    SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
                    return string.Empty;
                }
            }
        }

        public static string CardNumChange(string sCardNum)
        {
            if (sCardNum.Length == 16)
            {
                sCardNum = sCardNum.Substring(0, 6) + "******" + sCardNum.Substring(12, 4);
                string sTemp = string.Empty;
                for (int i = 0; i < sCardNum.Length; i = i + 4)
                {
                    sTemp += "-" + sCardNum.Substring(i, 4);
                }
                sCardNum = sTemp.Substring(1);
                return sCardNum;
            }
            else
            {
                return sCardNum;
            }
        }

        public static string UserFuncCheck()
        {
            try
            {
                using (RequestUserFuncCheckModel req = new RequestUserFuncCheckModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;

                    using (ResponseUserFuncCheckModel res = WebApiLib.SyncCall<ResponseUserFuncCheckModel, RequestUserFuncCheckModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            return res.data.status;
                        }
                        else
                        {
                            return string.Empty;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
                return string.Empty;
            }
        }

        public static List<EnumLib.ExchangeCurrencyCode> Market = new List<EnumLib.ExchangeCurrencyCode>
        {
            EnumLib.ExchangeCurrencyCode.KRW,
            EnumLib.ExchangeCurrencyCode.ETH,
            EnumLib.ExchangeCurrencyCode.USDT,
            EnumLib.ExchangeCurrencyCode.BTC
        };

        public static List<string> sgcTradeOkList = new List<string>
        {
            "testplan01@naver.com",
            "testplan02@naver.com",
            "testplan03@naver.com",
            "found123@nate.com"
        };

        public static string GetFloatPoint(string cash, string FloatNum)
        {
            try
            {
                string rVal = string.Empty;

                decimal value = decimal.Parse(cash.ToString());
                decimal floatCount = decimal.Parse(Math.Pow(10.0, int.Parse(FloatNum.Replace("n", ""))).ToString());
                string strFormat = FloatNum.ToString();

                rVal = (Math.Truncate(value * floatCount) / floatCount).ToString(strFormat);

                return rVal;
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
                return string.Empty;
            }
        }


        public static void GetOtpYn(ref string otpYn)
        {
            try
            {
                if (MainViewModel.LoginDataModel == null) return;

                using (RequestUserOtpCheckModel req = new RequestUserOtpCheckModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;

                    using (ResponseUserOtpCheckModel res = WebApiLib.SyncCall<ResponseUserOtpCheckModel, RequestUserOtpCheckModel>(req))
                    {
                        if (res.resultStrCode == "000")
                        {
                            otpYn = res.data.otpYn;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        public static BitmapImage GetNoticePop(string noticeUrl)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.Headers["User-Agent"] = "Mozilla/4.0 (Compatible; Windows Nt 5.1; MSIE 6.0)";
                    //client.Headers["Accept-Encoding"] = "gzip";
                    byte[] rByte = client.DownloadData(noticeUrl);
                    System.Drawing.Image img = System.Drawing.Image.FromStream(new MemoryStream(rByte), true);

                    MemoryStream mStream = new MemoryStream();
                    img.Save(mStream, ImageFormat.Png);
                    mStream.Seek(0, System.IO.SeekOrigin.Begin);

                    BitmapImage bImg = new BitmapImage();
                    bImg.BeginInit();
                    bImg.StreamSource = mStream;
                    bImg.EndInit();

                    return bImg;
                };
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
                throw ex;
            }
        }

        //static string GetResponse(string url)
        //{
        //    HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
        //    HttpWebResponse response = request.GetResponse() as HttpWebResponse;
        //    StreamReader sr = new StreamReader(response.GetResponseStream());

        //    return sr.ReadToEnd();
        //}
    }
}
