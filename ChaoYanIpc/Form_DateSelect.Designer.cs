namespace ChaoYanIpc
{
    partial class Form_DateSelect
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
            this.components = new System.ComponentModel.Container();
            this.DTP_Start = new System.Windows.Forms.DateTimePicker();
            this.DTP_End = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.PanForDate = new System.Windows.Forms.Panel();
            this.TP_End = new System.Windows.Forms.DateTimePicker();
            this.TP_Start = new System.Windows.Forms.DateTimePicker();
            this.Btn_DateConfirm = new System.Windows.Forms.Button();
            this.PanForWait = new System.Windows.Forms.Panel();
            this.Label_Wait = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Timer_Wait = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.PanForDate.SuspendLayout();
            this.PanForWait.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DTP_Start
            // 
            this.DTP_Start.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DTP_Start.Location = new System.Drawing.Point(45, 18);
            this.DTP_Start.Name = "DTP_Start";
            this.DTP_Start.Size = new System.Drawing.Size(99, 21);
            this.DTP_Start.TabIndex = 0;
            this.DTP_Start.ValueChanged += new System.EventHandler(this.DTP_Start_ValueChanged);
            // 
            // DTP_End
            // 
            this.DTP_End.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DTP_End.Location = new System.Drawing.Point(45, 54);
            this.DTP_End.Name = "DTP_End";
            this.DTP_End.Size = new System.Drawing.Size(99, 21);
            this.DTP_End.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(59, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "请选择需要查询的日期/时间范围";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(15, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "从";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(15, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "至";
            // 
            // PanForDate
            // 
            this.PanForDate.Controls.Add(this.label2);
            this.PanForDate.Controls.Add(this.label3);
            this.PanForDate.Controls.Add(this.TP_End);
            this.PanForDate.Controls.Add(this.TP_Start);
            this.PanForDate.Controls.Add(this.DTP_Start);
            this.PanForDate.Controls.Add(this.DTP_End);
            this.PanForDate.Location = new System.Drawing.Point(25, 62);
            this.PanForDate.Name = "PanForDate";
            this.PanForDate.Size = new System.Drawing.Size(274, 85);
            this.PanForDate.TabIndex = 3;
            // 
            // TP_End
            // 
            this.TP_End.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.TP_End.Location = new System.Drawing.Point(169, 54);
            this.TP_End.Name = "TP_End";
            this.TP_End.ShowUpDown = true;
            this.TP_End.Size = new System.Drawing.Size(99, 21);
            this.TP_End.TabIndex = 0;
            this.TP_End.Value = new System.DateTime(2017, 7, 6, 0, 0, 0, 0);
            // 
            // TP_Start
            // 
            this.TP_Start.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.TP_Start.Location = new System.Drawing.Point(169, 18);
            this.TP_Start.Name = "TP_Start";
            this.TP_Start.ShowUpDown = true;
            this.TP_Start.Size = new System.Drawing.Size(99, 21);
            this.TP_Start.TabIndex = 0;
            this.TP_Start.Value = new System.DateTime(2017, 7, 12, 0, 0, 0, 0);
            this.TP_Start.ValueChanged += new System.EventHandler(this.TP_Start_ValueChanged);
            // 
            // Btn_DateConfirm
            // 
            this.Btn_DateConfirm.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Btn_DateConfirm.Location = new System.Drawing.Point(110, 158);
            this.Btn_DateConfirm.Name = "Btn_DateConfirm";
            this.Btn_DateConfirm.Size = new System.Drawing.Size(105, 38);
            this.Btn_DateConfirm.TabIndex = 4;
            this.Btn_DateConfirm.Text = "确认";
            this.Btn_DateConfirm.UseVisualStyleBackColor = true;
            this.Btn_DateConfirm.Click += new System.EventHandler(this.Btn_DateConfirm_Click);
            // 
            // PanForWait
            // 
            this.PanForWait.Controls.Add(this.Label_Wait);
            this.PanForWait.Controls.Add(this.label5);
            this.PanForWait.Location = new System.Drawing.Point(-3, 0);
            this.PanForWait.Name = "PanForWait";
            this.PanForWait.Size = new System.Drawing.Size(349, 199);
            this.PanForWait.TabIndex = 5;
            // 
            // Label_Wait
            // 
            this.Label_Wait.AutoSize = true;
            this.Label_Wait.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_Wait.Location = new System.Drawing.Point(224, 85);
            this.Label_Wait.Name = "Label_Wait";
            this.Label_Wait.Size = new System.Drawing.Size(32, 16);
            this.Label_Wait.TabIndex = 1;
            this.Label_Wait.Text = "...";
            this.Label_Wait.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(52, 85);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(176, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "更新数据中，请稍后...";
            // 
            // Timer_Wait
            // 
            this.Timer_Wait.Tick += new System.EventHandler(this.Timer_Wait_Tick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.Btn_DateConfirm);
            this.panel1.Controls.Add(this.PanForDate);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(343, 208);
            this.panel1.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(213, 43);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 16);
            this.label6.TabIndex = 1;
            this.label6.Text = "时间";
            this.label6.Click += new System.EventHandler(this.label1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(80, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 16);
            this.label4.TabIndex = 1;
            this.label4.Text = "日期";
            this.label4.Click += new System.EventHandler(this.label1_Click);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(565, 98);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(200, 21);
            this.dateTimePicker2.TabIndex = 6;
            // 
            // Form_DateSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 207);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.PanForWait);
            this.Name = "Form_DateSelect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "查询日期";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_DateSelect_FormClosing);
            this.PanForDate.ResumeLayout(false);
            this.PanForDate.PerformLayout();
            this.PanForWait.ResumeLayout(false);
            this.PanForWait.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker DTP_Start;
        private System.Windows.Forms.DateTimePicker DTP_End;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel PanForDate;
        private System.Windows.Forms.Button Btn_DateConfirm;
        private System.Windows.Forms.Panel PanForWait;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label Label_Wait;
        private System.Windows.Forms.Timer Timer_Wait;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DateTimePicker TP_Start;
        private System.Windows.Forms.DateTimePicker TP_End;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
    }
}