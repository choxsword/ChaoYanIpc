namespace ChaoYanIpc
{
    partial class ProgramBar
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
            this.Lab_Disp = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Lab_Disp
            // 
            this.Lab_Disp.AutoSize = true;
            this.Lab_Disp.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Lab_Disp.Location = new System.Drawing.Point(119, 52);
            this.Lab_Disp.Name = "Lab_Disp";
            this.Lab_Disp.Size = new System.Drawing.Size(184, 16);
            this.Lab_Disp.TabIndex = 0;
            this.Lab_Disp.Text = "导出Excel中，请稍后...";
            // 
            // ProgramBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 121);
            this.Controls.Add(this.Lab_Disp);
            this.Name = "ProgramBar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "提示";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Lab_Disp;
    }
}