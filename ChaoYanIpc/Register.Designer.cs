namespace ChaoYanIpc
{
    partial class Register
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
            this.label2 = new System.Windows.Forms.Label();
            this.TB_MachineCode = new System.Windows.Forms.TextBox();
            this.TB_RegisterCode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Btn_ConfirmReg = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(38, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "机器码";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(38, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "注册码";
            // 
            // TB_MachineCode
            // 
            this.TB_MachineCode.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TB_MachineCode.Location = new System.Drawing.Point(100, 69);
            this.TB_MachineCode.Name = "TB_MachineCode";
            this.TB_MachineCode.ReadOnly = true;
            this.TB_MachineCode.Size = new System.Drawing.Size(220, 26);
            this.TB_MachineCode.TabIndex = 1;
            // 
            // TB_RegisterCode
            // 
            this.TB_RegisterCode.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TB_RegisterCode.Location = new System.Drawing.Point(100, 125);
            this.TB_RegisterCode.Name = "TB_RegisterCode";
            this.TB_RegisterCode.Size = new System.Drawing.Size(220, 26);
            this.TB_RegisterCode.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(38, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(312, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "请填写注册码（可联系软件开发人员获取）";
            // 
            // Btn_ConfirmReg
            // 
            this.Btn_ConfirmReg.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Btn_ConfirmReg.Location = new System.Drawing.Point(148, 171);
            this.Btn_ConfirmReg.Name = "Btn_ConfirmReg";
            this.Btn_ConfirmReg.Size = new System.Drawing.Size(85, 35);
            this.Btn_ConfirmReg.TabIndex = 2;
            this.Btn_ConfirmReg.Text = "永久注册";
            this.Btn_ConfirmReg.UseVisualStyleBackColor = true;
            this.Btn_ConfirmReg.Click += new System.EventHandler(this.Btn_ConfirmReg_Click);
            // 
            // Register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 218);
            this.Controls.Add(this.Btn_ConfirmReg);
            this.Controls.Add(this.TB_RegisterCode);
            this.Controls.Add(this.TB_MachineCode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Name = "Register";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "注册软件";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TB_MachineCode;
        private System.Windows.Forms.TextBox TB_RegisterCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Btn_ConfirmReg;
    }
}