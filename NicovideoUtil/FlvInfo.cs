using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Xml.Linq;

namespace NicovideoUtil
{
    /// <summary>
    /// 動画のFlvInfoを表すクラス.
    /// </summary>
    public class FlvInfo
    {
        /// <summary>
        /// 動画ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// スレッドID（コメントの取得で使用）
        /// </summary>
        public string ThreadId { get; set;}
        /// <summary>
        /// FLVファイルのURL
        /// </summary>
        public string FlvUrl { get; set;}
        /// <summary>
        /// 違反報告フォームのURL
        /// </summary>
        public string AbuseLink { get; set;}
        /// <summary>
        /// メッセージサーバのURL
        /// </summary>
        public string MessageServer { get;set; }

        private FlvInfo()
        {
            
        }

        /// <summary>
        /// 指定された動画IDのFlvInfoを取得する.
        /// </summary>
        /// <param name="id">動画ID</param>
        /// <returns></returns>
        public static FlvInfo Create(string id)
        {
            var req = (HttpWebRequest)HttpWebRequest.Create(String.Format("http://ext.nicovideo.jp/api/getflv/{0}?as3=1", id));
            req.CookieContainer = Nicovideo.LoginCookie;

            StreamReader sr;
            try {
                var resp = req.GetResponse();
                sr = new StreamReader(resp.GetResponseStream());
            }
            catch(Exception ex) {
                throw new Exception("Error while retrieving FlvInfo.", ex);
            }

            var dict = sr
                .ReadToEnd()
                .Split('&')
                .Select(t => t.Split('='))
                .ToDictionary(splitted => splitted[0], splitted => HttpUtility.UrlDecode(splitted[1]));

            if ((dict.ContainsKey("closed") && dict["closed"] == "1") || !dict.ContainsKey("url")) {
                throw new Exception("Currently logged out.");
            }

            var info = new FlvInfo();
            try {
                info.Id = id;
                info.FlvUrl = dict["url"];
                info.AbuseLink = dict["link"];
                info.ThreadId = dict["thread_id"];
                info.MessageServer = dict["ms"];
            }
            catch (Exception ex) {
                throw new Exception("Retrieved FlvInfo is corrupted.", ex);
            }

            return info;

        }

        /// <summary>
        /// このFlvInfoの動画のコメントを指定数だけ取得する.
        /// </summary>
        /// <param name="num">取得するコメントの数</param>
        /// <returns>コメント文字列の配列</returns>
        public string[] GetComments(int num)
        {
            var req = (HttpWebRequest)HttpWebRequest.Create(MessageServer);
            req.Method = "POST";

            var s = string.Format(@"<thread res_from=""-{0}"" version=""20061206"" thread=""{1}"" />", num, ThreadId);
            var bs = Encoding.UTF8.GetBytes(s);
            req.ContentLength = bs.Length;
            req.CookieContainer = Nicovideo.LoginCookie;
            req.GetRequestStream().Write(bs, 0, bs.Length);

            XDocument doc;
            try {
                var resp = (HttpWebResponse) req.GetResponse();
                doc = XDocument.Load(resp.GetResponseStream());
            }
            catch (Exception ex) {
                throw new Exception("Error while retrieving comment xml.", ex);
            }

            try {
                return doc.Descendants("chat").Select(x => x.Value).ToArray();
            }
            catch(Exception ex) {
                throw new Exception("Retrieved Comment Xml is corrupted or no comments.", ex);
            }
        }
    }
}
