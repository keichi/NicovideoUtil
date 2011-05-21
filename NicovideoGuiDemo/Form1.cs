using System;
using System.ComponentModel;
using System.Net;
using System.Windows.Forms;
using NicovideoUtil;

namespace NicoDown
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Nicovideo.LoginToNicovideo("***@***.***", "***");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtVidId.Text == "") return;

            var client = Nicovideo.GetWebClientForFlv(txtVidId.Text);

            client.DownloadProgressChanged += client_DownloadProgressChanged;
            client.DownloadFileCompleted += client_DownloadFileCompleted;

            var info = FlvInfo.Create(txtVidId.Text);
            client.DownloadFileAsync(new Uri(info.FlvUrl), txtVidId.Text + ".flv");
        }

        private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            MessageBox.Show("Download Finished");
            (sender as CustomWebClient).Dispose();
        }

        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressDownload.Value = e.ProgressPercentage;
        }
    }
}
