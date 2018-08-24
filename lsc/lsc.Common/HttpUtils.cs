using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace lsc.Common
{
    public class HttpUtils
    {
        HttpClient Client { get; } = new HttpClient();
        /// <summary>
        /// Http请求UA
        /// </summary>
        public string UserAgent { get; set; } = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.78 Safari/537.36";
        /// <summary>
        /// 代理类型
        /// </summary>
        public HttpProxyType HttpProxy { get; set; } = HttpProxyType.Default;

        public string Accept { get; set; }

        public string Content_Type { get; set; }
        public string Content_Encoding { get; set; }
        public string Referer { get; set; }


        public string Get(string url,string Encode="",int TimeOut=6000, string cookies = "")
        {
            int errortimes = 4;
            A:
            try
            {
                ClassLoger.Info("HttpUtils.GetAsync",url);
                System.Net.WebRequest wRequest = System.Net.WebRequest.Create(url);
                wRequest.Method = "GET";
                wRequest.UseDefaultCredentials = true;
                wRequest.Headers.Set(HttpRequestHeader.UserAgent,UserAgent);
                wRequest.Headers.Set(HttpRequestHeader.KeepAlive, "true");
                wRequest.Headers.Set(HttpRequestHeader.AcceptLanguage, "zh-CN,zh;q=0.8");
                if (!Content_Encoding.IsNull())
                {
                    wRequest.Headers.Set(HttpRequestHeader.ContentEncoding, Content_Encoding);
                }
                if (!Content_Type.IsNull())
                {
                    wRequest.Headers.Set(HttpRequestHeader.ContentType, Content_Type);
                }
                if (!Accept.IsNull())
                {
                    wRequest.Headers.Set(HttpRequestHeader.Accept, Accept);
                }
                if (!Referer.IsNull())
                {
                    wRequest.Headers.Set(HttpRequestHeader.Referer, Referer);
                }

                switch (HttpProxy)
                {
                    case HttpProxyType.Default:
                        break;
                    case HttpProxyType.IE:
                        wRequest.Proxy = WebRequest.GetSystemWebProxy();
                        break;
                    case HttpProxyType.None:
                        wRequest.Proxy = null;
                        break;
                }
                if (url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                {
                   
                }
                wRequest.Timeout = TimeOut;
                if (!string.IsNullOrEmpty(cookies))
                {
                    wRequest.Headers.Set(HttpRequestHeader.Cookie, cookies);
                }
                Encoding encoder = Encoding.Default;
                if (!Encode.IsNull())
                {
                    encoder = Encoding.GetEncoding(Encode);
                }
                System.Net.WebResponse wResp = wRequest.GetResponse();
                System.IO.Stream respStream = wResp.GetResponseStream();
                using (System.IO.StreamReader reader = new System.IO.StreamReader(respStream, encoder))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                ClassLoger.Error("HTTPHelper.Get",ex);
                if (errortimes>0)
                {
                    errortimes--;
                    goto A;
                }
            }
            return string.Empty;
        }

        public string Post(string url, byte[] data,string Encode= "UTF-8", int timeout = 5000, string cookies = "")
        {
            int errortimes = 4;
            A:
            try
            {
                
                ClassLoger.Info("HttpUtils.Post", url);
                System.Net.WebRequest wRequest = System.Net.WebRequest.Create(url);
                wRequest.Method = "POST";
                wRequest.UseDefaultCredentials = true;
                wRequest.Headers.Set(HttpRequestHeader.UserAgent, UserAgent);
                wRequest.Headers.Set(HttpRequestHeader.KeepAlive, "true");
                wRequest.Headers.Set(HttpRequestHeader.AcceptLanguage, "zh-CN,zh;q=0.8");
                if (!Content_Encoding.IsNull())
                {
                    wRequest.Headers.Set(HttpRequestHeader.ContentEncoding, Content_Encoding);
                }
                if (!Content_Type.IsNull())
                {
                    wRequest.Headers.Set(HttpRequestHeader.ContentType, Content_Type);
                }
                if (!Accept.IsNull())
                {
                    wRequest.Headers.Set(HttpRequestHeader.Accept, Accept);
                }
                if (!Referer.IsNull())
                {
                    wRequest.Headers.Set(HttpRequestHeader.Referer, Referer);
                }

                switch (HttpProxy)
                {
                    case HttpProxyType.Default:
                        break;
                    case HttpProxyType.IE:
                        wRequest.Proxy = WebRequest.GetSystemWebProxy();
                        break;
                    case HttpProxyType.None:
                        wRequest.Proxy = null;
                        break;
                }
                if (url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                {

                }
                wRequest.Timeout = timeout;
                if (!string.IsNullOrEmpty(cookies))
                {
                    wRequest.Headers.Set(HttpRequestHeader.Cookie, cookies);
                }

                Stream writeStream = wRequest.GetRequestStream();
                writeStream.Write(data,0,data.Length);
                writeStream.Close();
                System.Net.WebResponse wResp =  wRequest.GetResponse();
                Stream respStream = wResp.GetResponseStream();
                using (System.IO.StreamReader reader = new System.IO.StreamReader(respStream, Encoding.GetEncoding(Encode)))
                {
                    return reader.ReadToEnd();
                }
            } catch (Exception ex)
            {
                ClassLoger.Error("HttpUtils.PostAsync",ex);
                if (errortimes>0)
                {
                    errortimes--;
                    goto A;
                }
            }
            return string.Empty;
        }
        public string Post(string url, string param, string Encode = "UTF-8", int timeout = 5000, string cookies = "")
        {
            byte[] byteArray = Encoding.GetEncoding(Encode).GetBytes(param);
            return  Post(url,byteArray,Encode,timeout,cookies);
        }

        /// <summary>
        /// URL编码
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public string UrlEncode(string v)
        {
            return System.Web.HttpUtility.UrlEncode(v);
        }

        /// <summary>
        /// URL编码
        /// </summary>
        /// <param name="v"></param>
        /// <param name="charset"></param>
        /// <returns></returns>
        public string UrlEncode(string v, Encoding charset)
        {
            return System.Web.HttpUtility.UrlEncode(v, charset);
        }

        /// <summary>
        /// URL 转码
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public string UrlDecode(string v)
        {
            return System.Web.HttpUtility.UrlDecode(v);
        }

        /// <summary>
        /// URL 转码
        /// </summary>
        /// <param name="v"></param>
        /// <param name="charset"></param>
        /// <returns></returns>
        public string UrlDencode(string v, Encoding charset)
        {
            return System.Web.HttpUtility.UrlDecode(v, charset);
        }
    }
    /// <summary>
    /// 代理类型
    /// </summary>
    public enum HttpProxyType
    {
        /// <summary>
        /// 使用默认代理
        /// </summary>
        Default = 1,
        /// <summary>
        /// 使用IE代理
        /// </summary>
        IE = 2,
       /// <summary>
       /// 不使用代理
       /// </summary>
        None = 3
    }
}
