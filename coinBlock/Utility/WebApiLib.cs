using System;
using System.Collections.Specialized;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Controls;
using coinBlock.Model.Trading;
using coinBlock.Model;

namespace coinBlock.Utility
{
    public class WebClientEx : WebClient
    {
        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest request = base.GetWebRequest(address);
            request.Timeout = 30 * 1000;
            return request;
        }
    }

    public class WebApiLib
    {
        /// <summary>
        /// API 서버 상세 URL 이름
        /// </summary>
        private static readonly string TargetUrl = "targetUrl";

        /// <summary>
        /// API 서버 결과 코드 파라미터 이름
        /// </summary>
        private static readonly string ResultStrCode = "resultStrCode";

        /// <summary>
        /// API 서버 결과 메세지 파라미터 이름
        /// </summary>
        private static readonly string ResultMsg = "resultMsg";

        /// <summary>
        /// ASync
        /// </summary>
        /// <returns>T</returns>
        public static async Task<Response> AsyncCall<Response, Request>(Request req) where Response : class, IDisposable, new()
        {
            Response response = new Response();
            WebClientEx wc = new WebClientEx();
            
            string targetUrl = null;
            try
            {
                //해더 셋팅
                wc.Encoding = Encoding.UTF8;
                wc.Headers.Add("Accept-Language", "utf-8");
                wc.Headers.Add("Accept", "text/html, application/xhtml+xml, */*");
                wc.Headers.Add("User-Agent", "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)");

                //파라미터 세팅
                wc.QueryString = GetParam(req);

                //상세 API URL 설정
                targetUrl = GetPropertyValue(req, TargetUrl);

                //API 요청
                byte[] bt = await wc.UploadValuesTaskAsync(ConfigLib.ApiUrl + targetUrl, new NameValueCollection());

                var dy = new JavaScriptSerializer().Deserialize<dynamic>(Encoding.UTF8.GetString(bt));
                
                //API 결과 코드 값
                string resultStrCode = dy["resultStrCode"];
                //API 결과 메시지
                string resultMsg = dy["resultMsg"];

                if (resultStrCode == "000")
                {
                    response = new JavaScriptSerializer().Deserialize<Response>(Encoding.UTF8.GetString(bt));
                }
                else
                {
                    if (resultStrCode != "000")
                    {                       
                        //SysLog.Info("API resultMsg : " + resultMsg);
                    }
                }
            }
            catch (Exception ex)
            {   
                SysLog.Error("DP : " + targetUrl);
                throw ex;
            }
            finally
            {
                wc.Dispose();
                response.Dispose();
            }
            return response;
        }

        /// <summary>
        /// ASync
        /// </summary>
        /// <returns>T</returns>
        public static async Task<Response> AsyncCallEncrypt<Response, Request>(Request req) where Response : class, IDisposable, new()
        {
            Response response = new Response();
            WebClientEx wc = new WebClientEx();
            string targetUrl = null;
            try
            {
                //해더 셋팅
                wc.Encoding = Encoding.UTF8;
                wc.Headers.Add("Accept-Language", "utf-8");
                wc.Headers.Add("Accept", "text/html, application/xhtml+xml, */*");
                wc.Headers.Add("User-Agent", "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)");

                //파라미터 직렬화 
                string sReqSerial = new JavaScriptSerializer().Serialize(req);
                //암호화
                string sEncrpt = EncodingLib.AesEncrypt(sReqSerial, RequestBaseModel.GidParam);

                NameValueCollection nvc = new NameValueCollection();
                nvc.Add("encStr", sEncrpt);
                nvc.Add("clientPe", RequestBaseModel.RsaParam);
                wc.QueryString = nvc;

                //상세 API URL 설정                
                targetUrl = "bt.arbitrage.listen.dp/proc.go";

                //API 요청
                byte[] bt = await wc.UploadValuesTaskAsync(ConfigLib.ApiUrl + targetUrl, new NameValueCollection());

                var dy = new JavaScriptSerializer().Deserialize<dynamic>(Encoding.UTF8.GetString(bt));

                //API 결과 코드 값
                string resultStrCode = dy["resultStrCode"];
                //API 결과 메시지
                string resultMsg = dy["resultMsg"];

                if (resultStrCode == "000")
                {
                    response = new JavaScriptSerializer().Deserialize<Response>(Encoding.UTF8.GetString(bt));
                }
                else
                {
                    if (resultStrCode != "000")
                    {
                        //SysLog.Info("API resultMsg : " + resultMsg);
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("DP : " + targetUrl);
                throw ex;
            }
            finally
            {
                wc.Dispose();
                response.Dispose();
            }
            return response;
        }

        /// <summary>
        /// Sync
        /// </summary>
        /// <typeparam name="Response"></typeparam>
        /// <typeparam name="Request"></typeparam>
        /// <param name="req"></param>
        /// <returns></returns>
        public static Response SyncCall<Response, Request>(Request req) where Response : IDisposable, new()
        {
            Response response = new Response();
            WebClientEx wc = new WebClientEx();
            try
            {
                //해더 셋팅
                wc.Encoding = Encoding.UTF8;
                wc.Headers.Add("Accept-Language", "utf-8");
                wc.Headers.Add("Accept", "text/html, application/xhtml+xml, */*");
                wc.Headers.Add("User-Agent", "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)");

                //파라미터 세팅
                wc.QueryString = GetParam(req);

                //상세 API URL 설정
                string targetUrl = GetPropertyValue(req, TargetUrl);
                //API 요청
                byte[] bt = wc.UploadValues(ConfigLib.ApiUrl + targetUrl, new NameValueCollection());

                //JSON 파싱
                response = new JavaScriptSerializer().Deserialize<Response>(Encoding.UTF8.GetString(bt));

                //API 결과 코드 값
                string resultStrCode = GetPropertyValue(response, ResultStrCode);

                //API 결과 메시지
                string resultMsg = GetPropertyValue(response, ResultMsg);

                if (resultStrCode != "000")
                {
                    throw new Exception("API resultMsg : " + resultMsg);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                wc.Dispose();
                response.Dispose();
            }
            return response;
        }

        /// <summary>
        /// Sync
        /// </summary>
        /// <typeparam name="Response"></typeparam>
        /// <typeparam name="Request"></typeparam>
        /// <param name="req"></param>
        /// <returns></returns>
        public static Response SyncCallEncrypt<Response, Request>(Request req) where Response : IDisposable, new()
        {
            Response response = new Response();
            WebClientEx wc = new WebClientEx();
            try
            {
                //해더 셋팅
                wc.Encoding = Encoding.UTF8;
                wc.Headers.Add("Accept-Language", "utf-8");
                wc.Headers.Add("Accept", "text/html, application/xhtml+xml, */*");
                wc.Headers.Add("User-Agent", "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)");

                //파라미터 직렬화 
                string sReqSerial = new JavaScriptSerializer().Serialize(req);
                //암호화
                string sEncrpt = EncodingLib.AesEncrypt(sReqSerial, RequestBaseModel.GidParam);

                NameValueCollection nvc = new NameValueCollection();
                nvc.Add("encStr", sEncrpt);
                nvc.Add("clientPe", RequestBaseModel.RsaParam);
                wc.QueryString = nvc;

                //상세 API URL 설정                
                string targetUrl = "bt.arbitrage.listen.dp/proc.go";

                //API 요청
                byte[] bt = wc.UploadValues(ConfigLib.ApiUrl + targetUrl, new NameValueCollection());

                //JSON 파싱
                response = new JavaScriptSerializer().Deserialize<Response>(Encoding.UTF8.GetString(bt));

                //API 결과 코드 값
                string resultStrCode = GetPropertyValue(response, ResultStrCode);

                //API 결과 메시지
                string resultMsg = GetPropertyValue(response, ResultMsg);

                if (resultStrCode != "000")
                {
                    throw new Exception("API resultMsg : " + resultMsg);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                wc.Dispose();
                response.Dispose();
            }
            return response;
        }

        /// <summary>
        /// Sync
        /// </summary>
        /// <typeparam name="Response"></typeparam>
        /// <typeparam name="Request"></typeparam>
        /// <param name="req"></param>
        /// <returns></returns>
        public static Response SyncCallWeb<Response, Request>(Request req) where Response : IDisposable, new()
        {
            Response response = new Response();
            WebClientEx wc = new WebClientEx();
            try
            {
                //해더 셋팅
                wc.Encoding = Encoding.UTF8;
                wc.Headers.Add("Accept-Language", "utf-8");
                wc.Headers.Add("Accept", "text/html, application/xhtml+xml, */*");
                wc.Headers.Add("User-Agent", "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)");

                //파라미터 세팅
                wc.QueryString = GetParam(req);

                //상세 API URL 설정
                string targetUrl = GetPropertyValue(req, TargetUrl);
                //API 요청
                //byte[] bt = wc.UploadValues("http://wtest.xlogic.co.kr/" + targetUrl, new NameValueCollection());
                byte[] bt = wc.UploadValues(ConfigLib.WebUrl + targetUrl, new NameValueCollection());

                //JSON 파싱
                response = new JavaScriptSerializer().Deserialize<Response>(Encoding.UTF8.GetString(bt));

                //API 결과 코드 값
                string resultStrCode = GetPropertyValue(response, ResultStrCode);

                //API 결과 메시지
                string resultMsg = GetPropertyValue(response, ResultMsg);

                if (resultStrCode != "000")
                {
                    throw new Exception("API resultMsg : " + resultMsg);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                wc.Dispose();
                response.Dispose();
            }
            return response;
        }

        /// <summary>
        /// Sync
        /// </summary>
        /// <typeparam name="Response"></typeparam>
        /// <typeparam name="Request"></typeparam>
        /// <param name="req"></param>
        /// <returns></returns>
        public static Response UpLoad<Response, Request>(Request req, string filename) where Response : IDisposable, new()
        {
            Response response = new Response();
            WebClientEx wc = new WebClientEx();
            try
            {
                //해더 셋팅
                wc.Encoding = Encoding.UTF8;
                wc.Headers.Add("Accept-Language", "utf-8");
                wc.Headers.Add("Accept", "text/html, application/xhtml+xml, */*");
                wc.Headers.Add("User-Agent", "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)");

                //파라미터 세팅
                wc.QueryString = GetParam(req);

                //상세 API URL 설정
                string targetUrl = GetPropertyValue(req, TargetUrl);
                //API 요청
                byte[] bt = wc.UploadFile(new Uri(ConfigLib.ApiUrl + targetUrl), filename); //(운영)

                //JSON 파싱
                response = new JavaScriptSerializer().Deserialize<Response>(Encoding.UTF8.GetString(bt));

                //API 결과 코드 값
                string resultStrCode = GetPropertyValue(response, ResultStrCode);

                //API 결과 메시지
                string resultMsg = GetPropertyValue(response, ResultMsg);

                if (resultStrCode != "000")
                {
                    throw new Exception("API resultMsg : " + resultMsg);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                wc.Dispose();
                response.Dispose();
            }
            return response;
        }

        /// <summary>
        /// Sync
        /// </summary>
        /// <typeparam name="Response"></typeparam>
        /// <typeparam name="Request"></typeparam>
        /// <param name="req"></param>
        /// <returns></returns>
        public static string MaintenanceCheck()
        {
            WebClientEx wc = new WebClientEx();
            string res = null;
            try
            {
                //해더 셋팅
                wc.Encoding = Encoding.UTF8;
                wc.Headers.Add("Accept-Language", "utf-8");
                wc.Headers.Add("Accept", "text/html, application/xhtml+xml, */*");
                wc.Headers.Add("User-Agent", "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)");

                //API 요청
                byte[] bt = wc.DownloadData(new Uri("http://app.noryweb.com/" + Assembly.GetEntryAssembly().GetName().Name + ".txt")); //(운영)
                res = Encoding.GetEncoding("euc-kr").GetString(bt);
            }
            catch (Exception ex)
            {
                return null;
                //throw ex;
            }
            finally
            {
                wc.Dispose();
            }
            return res;
        }

        /// <summary>
        /// 제네릭 클래스에서 특정 함수 실행 결과값 가져오기
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        static NameValueCollection GetParam(object src)
        {
            try
            {
                return src.GetType().GetMethod("GetParam").Invoke(src, null) as NameValueCollection;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 제네릭 클래스에서 특정 파라미터 값 가져오기
        /// </summary>
        /// <param name="src"></param>
        /// <param name="propName"></param>
        /// <returns></returns>
        static string GetPropertyValue(object src, string propName)
        {
            try
            {
                return src.GetType().GetProperty(propName).GetValue(src, null).ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}