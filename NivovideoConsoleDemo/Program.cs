using System;
using System.Linq;
using NicovideoUtil;

namespace NivovideoUtilTest
{
    class Program
    {
        //メールアドレス
        private const string LOGIN_MAIL = "***@***.***";
        //パスワード
        private const string LOGIN_PASS = "***";
        //動画ID
        private const string TEST_VID_ID = "sm***";

        static void Main(string[] args)
        {
            //ニコニコ動画にログイン
            Nicovideo.LoginToNicovideo(LOGIN_MAIL, LOGIN_PASS);

            //FlvInfoを取得
            var flvinfo = FlvInfo.Create(TEST_VID_ID);
            Console.WriteLine("FlvInfoのテスト");
            Console.WriteLine("動画ID:{0}", flvinfo.Id);
            Console.WriteLine("FLVのURL:{0}", flvinfo.FlvUrl);
            Console.WriteLine("違反動画の通報ページのURL:{0}", flvinfo.AbuseLink);
            Console.WriteLine("メッセージサーバのURL:{0}", flvinfo.MessageServer);
            Console.WriteLine("スレッドID:{0}", flvinfo.ThreadId);

            //コメントを取得
            Console.WriteLine("コメント取得のテスト");
            foreach (var comment in flvinfo.GetComments(100)) {
                Console.WriteLine(comment);
            }

            //ThumbInfoを取得
            var thumbinfo = ThumbInfo.Create(TEST_VID_ID);
            Console.WriteLine("ThumbInfoのテスト");
            Console.WriteLine("動画ID:{0}", thumbinfo.Id);
            Console.WriteLine("タイトル:{0}", thumbinfo.Title);
            Console.WriteLine("説明:{0}", thumbinfo.Description);
            Console.WriteLine("再生数:{0}", thumbinfo.ViewCounter);
            Console.WriteLine("コメント数:{0}", thumbinfo.CommentNum);
            Console.WriteLine("タグ:{0}", thumbinfo.Tags.Aggregate((x, y) => x + "," + y));
            Console.WriteLine("投稿日:{0}", thumbinfo.FirstRetrieve);

            //FLVを取得
            Console.WriteLine("FLVファイルのダウンロード開始");
            //WebClientを拡張したCustomWebClientを取得（WebClientのメソッドは全て使えます）
            var client = Nicovideo.GetWebClientForFlv(TEST_VID_ID);
            //FLVファイルをダウンロード
            client.DownloadFile(flvinfo.FlvUrl, TEST_VID_ID + ".flv");
            Console.WriteLine("FLVファイルダウンロード終了");

            Console.ReadLine();
        }
    }
}
