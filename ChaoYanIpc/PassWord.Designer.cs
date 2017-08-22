namespace ChaoYanIpc
{
    partial class PassWord
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
            this.label1 = new System.Windows.Forms.Label();
            this.TB_PassWord = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(144, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "请输入密码";
            // 
            // TB_PassWord
            // 
            this.TB_PassWord.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TB_PassWord.Location = new System.Drawing.Point(104, 79);
            this.TB_PassWord.Name = "TB_PassWord";
            this.TB_PassWord.Size = new System.Drawing.Size(183, 26);
            this.TB_PassWord.TabIndex = 1;
            this.TB_PassWord.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TB_PassWord_KeyUp);
            // 
            // PassWord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 160);
            this.Controls.Add(this.TB_PassWord);
            this.Controls.Add(this.label1);
            this.Name = "PassWord";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "超级权限密码";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TB_PassWord;
    }
}