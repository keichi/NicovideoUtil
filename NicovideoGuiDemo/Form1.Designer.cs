namespace NicoDown
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnDl = new System.Windows.Forms.Button();
            this.progressDownload = new System.Windows.Forms.ProgressBar();
            this.txtVidId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnDl
            // 
            this.btnDl.Location = new System.Drawing.Point(197, 10);
            this.btnDl.Name = "btnDl";
            this.btnDl.Size = new System.Drawing.Size(75, 23);
            this.btnDl.TabIndex = 0;
            this.btnDl.Text = "DL!";
            this.btnDl.UseVisualStyleBackColor = true;
            this.btnDl.Click += new System.EventHandler(this.button1_Click);
            // 
            // progressDownload
            // 
            this.progressDownload.Location = new System.Drawing.Point(12, 52);
            this.progressDownload.Name = "progressDownload";
            this.progressDownload.Size = new System.Drawing.Size(260, 23);
            this.progressDownload.TabIndex = 1;
            // 
            // txtVidId
            // 
            this.txtVidId.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtVidId.Location = new System.Drawing.Point(64, 12);
            this.txtVidId.Name = "txtVidId";
            this.txtVidId.Size = new System.Drawing.Size(127, 19);
            this.txtVidId.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "動画ID";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 89);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtVidId);
            this.Controls.Add(this.progressDownload);
            this.Controls.Add(this.btnDl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDl;
        private System.Windows.Forms.ProgressBar progressDownload;
        private System.Windows.Forms.TextBox txtVidId;
        private System.Windows.Forms.Label label1;
    }
}

