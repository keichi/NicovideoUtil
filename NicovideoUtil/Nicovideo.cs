using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace NicovideoUtil
{
    /// <summary>
    /// ニコニコ動画へのログインなどの機能
    /// </summary>
    public static class Nicovideo
    {
        /// <summary>
        /// ログイン時のセッションID
        /// </summary>
        public static CookieContainer LoginCookie = new CookieContainer();

        /// <summary>
        /// 指定されたメールアドレスとパスワードでニコニコ動画にログインする.<para/>
        /// アプリケーションの起動時に１回だけ呼ぶ.
        /// </summary>
        /// <param name="mail">メールアドレス</param>
        /// <param name="passwd">パスワード</param>
        public static void LoginToNicovideo(string mail, string passwd)
        {
            var req = (HttpWebRequest)HttpWebRequest.Create("https://secure.nicovideo.jp/secure/login?site=niconico");
            req.Method = "POST";

            var args = new Dictionary<string, string>
                           {
                               {"mail", mail},
                               {"password", passwd}
                           };

            var s = args.Select(x => x.Key + "=" + x.Value).Aggregate((x, y) => x + "&" + y);
            var bs = Encoding.UTF8.GetBytes(s);
            req.ContentLength = bs.Length;
            req.ContentType = "application/x-www-form-urlencoded";
            req.CookieContainer = LoginCookie;
            req.GetRequestStream().Write(bs, 0, bs.Length);

            string result;
            try {
                var resp = (HttpWebResponse) req.GetResponse();
                result = new StreamReader(resp.GetResponseStream()).ReadToEnd();
            }
            catch (Exception ex) {
                throw new Exception("Error during login proccess.", ex);
            }
            if (result.Contains("wrongPass")) {
                throw new Exception("Wrong ID or Password. Login Failed.");
            }
        }

        /// <summary>
        /// 指定された動画の動画視聴ページのクッキーを取得する.
        /// </summary>
        /// <param name="id">動画ID</param>
        /// <returns>動画視聴ページのクッキー</returns>
        private static CookieCollection GetWatchCookie(string id)
        {
            var req = (HttpWebRequest)HttpWebRequest.Create(String.Format("http://www.nicovideo.jp/watch/{0}", id));
            req.CookieContainer = LoginCookie;

            HttpWebResponse resp;
            try {
                resp = (HttpWebResponse) req.GetResponse();
            }
            catch (Exception ex){
                throw new Exception("Error while retrieving watch cookie.", ex);
            }

            return resp.Cookies;
        }

        /// <summary>
        /// 指定された動画を視聴できるクッキーをセットされたWebClientを取得する.
        /// </summary>
        /// <param name="id">動画ID</param>
        /// <returns>クッキーを使用するCustomWebClientk</returns>
        public static CustomWebClient GetWebClientForFlv(string id)
        {
            var client = new CustomWebClient();
            client.CookieContainer.Add(GetWatchCookie(id));

            return client;
        }
    }
}
