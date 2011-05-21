using System;
using System.Linq;
using System.Xml.Linq;

namespace NicovideoUtil
{
    /// <summary>
    /// 動画のThumbInfoを表すクラス
    /// </summary>
    public class ThumbInfo
    {
        /// <summary>
        /// 動画ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// タイトル
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 説明
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 再生数
        /// </summary>
        public int ViewCounter { get; set; }
        /// <summary>
        /// コメント数
        /// </summary>
        public int CommentNum { get; set; }
        /// <summary>
        /// サムネイル画象のURL
        /// </summary>
        public string ThumbnailUrl { get; set; }
        /// <summary>
        /// タグ
        /// </summary>
        public string[] Tags { get; set; }
        /// <summary>
        /// 投稿日
        /// </summary>
        public DateTime FirstRetrieve { get; set; }

        private ThumbInfo()
        {
            
        }

        /// <summary>
        /// 指定された動画IDのThumbInfoを取得する.
        /// </summary>
        /// <param name="id">動画ID</param>
        /// <returns></returns>
        public static ThumbInfo Create(string id)
        {
            XDocument xml;
            try {
                xml = XDocument.Load(String.Format("http://ext.nicovideo.jp/api/getthumbinfo/{0}", id));
            }
            catch(Exception ex) {
                throw new Exception("Error while retrieving ThumbInfo", ex);
            }

            if (xml.Element("nicovideo_thumb_response").Attribute("status").Value == "fail") {
                throw new Exception("This video is deleted.");
            }

            var info = new ThumbInfo();

            try {
                info.Title = xml.Descendants("title").First().Value;
                info.Description = xml.Descendants("description").First().Value;
                info.ThumbnailUrl = xml.Descendants("thumbnail_url").First().Value;
                info.Id = id;
                info.ViewCounter = int.Parse(xml.Descendants("view_counter").First().Value);
                info.CommentNum = int.Parse(xml.Descendants("comment_num").First().Value);
                info.Tags = xml
                    .Descendants("tags")
                    .Where(x => x.Attribute("domain").Value == "jp")
                    .Elements()
                    .Select(x => x.Value)
                    .ToArray();
                info.FirstRetrieve = DateTime.Parse(xml.Descendants("first_retrieve").First().Value);
            }
            catch(Exception ex) {
                throw new Exception("Error while parsing the retrieved ThumbInfo", ex);
            }

            return info;
        }
    }
}
