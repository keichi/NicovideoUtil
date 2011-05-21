using System;
using System.Net;

namespace NicovideoUtil
{
    /// <summary>
    /// クッキーを用いる、WebClientの継承クラス
    /// </summary>
    public class CustomWebClient : WebClient
    {
        /// <summary>
        /// このWebClientでのリクエストで用いるクッキー
        /// </summary>
        public CookieContainer CookieContainer { get; set; }

        /// <summary>
        /// CookieContainerを初期化するためコンストラクタをオーバーライド
        /// </summary>
        public CustomWebClient()
        {
            CookieContainer = new CookieContainer();
        }

        /// <summary>
        /// クッキーを利用したリクエストをさせるため、オーバーライド
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = base.GetWebRequest(address);
            if (request is HttpWebRequest) {
                (request as HttpWebRequest).CookieContainer = CookieContainer;
            }
            return request;
        }
    }
}