namespace NotaryNoProject
{
    partial class MegBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelMsg = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.textBoxError = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelMsg
            // 
            this.labelMsg.AutoSize = true;
            this.labelMsg.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelMsg.Location = new System.Drawing.Point(92, 162);
            this.labelMsg.Name = "labelMsg";
            this.labelMsg.Size = new System.Drawing.Size(154, 16);
            this.labelMsg.TabIndex = 0;
            this.labelMsg.Text = "正在导入数据中...";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(188, 357);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(82, 30);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "关 闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // textBoxError
            // 
            this.textBoxError.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxError.Location = new System.Drawing.Point(17, 41);
            this.textBoxError.Multiline = true;
            this.textBoxError.Name = "textBoxError";
            this.textBoxError.ReadOnly = true;
            this.textBoxError.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBoxError.Size = new System.Drawing.Size(435, 289);
            this.textBoxError.TabIndex = 2;
            this.textBoxError.Visible = false;
            // 
            // MegBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(476, 414);
            this.Controls.Add(this.textBoxError);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.labelMsg);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MegBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "客户信息导入";
            this.Load += new System.EventHandler(this.MegBox_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelMsg;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox textBoxError;
    }
}