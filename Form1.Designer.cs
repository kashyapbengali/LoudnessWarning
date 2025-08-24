
namespace LoudnessWarning
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblCurrentLevel = new System.Windows.Forms.Label();
            this.lblWarningLimit = new System.Windows.Forms.Label();
            this.txtWarningLimit = new System.Windows.Forms.TextBox();
            this.btnSetLimit = new System.Windows.Forms.Button();
            this.btnStartStop = new System.Windows.Forms.Button();
            this.pnlSoundBar = new System.Windows.Forms.Panel();
            this.lblDbUnit = new System.Windows.Forms.Label();
            this.lblLimitUnit = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lblCurrentLevel
            // 
            this.lblCurrentLevel.AutoSize = true;
            this.lblCurrentLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblCurrentLevel.Location = new System.Drawing.Point(12, 32);
            this.lblCurrentLevel.Name = "lblCurrentLevel";
            this.lblCurrentLevel.Size = new System.Drawing.Size(178, 20);
            this.lblCurrentLevel.TabIndex = 0;
            this.lblCurrentLevel.Text = "Current Level: 0.0 dB";
            // 
            // lblWarningLimit
            // 
            this.lblWarningLimit.AutoSize = true;
            this.lblWarningLimit.Location = new System.Drawing.Point(12, 9);
            this.lblWarningLimit.Name = "lblWarningLimit";
            this.lblWarningLimit.Size = new System.Drawing.Size(110, 15);
            this.lblWarningLimit.TabIndex = 1;
            this.lblWarningLimit.Text = "Warning Limit (dB):";
            // 
            // txtWarningLimit
            // 
            this.txtWarningLimit.Location = new System.Drawing.Point(128, 6);
            this.txtWarningLimit.Name = "txtWarningLimit";
            this.txtWarningLimit.Size = new System.Drawing.Size(80, 23);
            this.txtWarningLimit.TabIndex = 2;
            this.txtWarningLimit.Text = "80";
            // 
            // btnSetLimit
            // 
            this.btnSetLimit.Location = new System.Drawing.Point(241, 6);
            this.btnSetLimit.Name = "btnSetLimit";
            this.btnSetLimit.Size = new System.Drawing.Size(75, 23);
            this.btnSetLimit.TabIndex = 3;
            this.btnSetLimit.Text = "Set Limit";
            this.btnSetLimit.UseVisualStyleBackColor = true;
            this.btnSetLimit.Click += new System.EventHandler(this.btnSetLimit_Click);
            // 
            // btnStartStop
            // 
            this.btnStartStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnStartStop.Location = new System.Drawing.Point(12, 133);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(626, 48);
            this.btnStartStop.TabIndex = 4;
            this.btnStartStop.Text = "Start Monitoring";
            this.btnStartStop.UseVisualStyleBackColor = true;
            this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
            // 
            // pnlSoundBar
            // 
            this.pnlSoundBar.BackColor = System.Drawing.Color.LightGray;
            this.pnlSoundBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSoundBar.Location = new System.Drawing.Point(12, 55);
            this.pnlSoundBar.Name = "pnlSoundBar";
            this.pnlSoundBar.Size = new System.Drawing.Size(544, 55);
            this.pnlSoundBar.TabIndex = 5;
            this.pnlSoundBar.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlSoundBar_Paint);
            // 
            // lblDbUnit
            // 
            this.lblDbUnit.AutoSize = true;
            this.lblDbUnit.Location = new System.Drawing.Point(214, 9);
            this.lblDbUnit.Name = "lblDbUnit";
            this.lblDbUnit.Size = new System.Drawing.Size(21, 15);
            this.lblDbUnit.TabIndex = 6;
            this.lblDbUnit.Text = "dB";
            // 
            // lblLimitUnit
            // 
            this.lblLimitUnit.AutoSize = true;
            this.lblLimitUnit.Location = new System.Drawing.Point(562, 75);
            this.lblLimitUnit.Name = "lblLimitUnit";
            this.lblLimitUnit.Size = new System.Drawing.Size(76, 15);
            this.lblLimitUnit.TabIndex = 7;
            this.lblLimitUnit.Text = "0 dB - 100 dB";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblStatus.ForeColor = System.Drawing.Color.Green;
            this.lblStatus.Location = new System.Drawing.Point(12, 113);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(140, 17);
            this.lblStatus.TabIndex = 8;
            this.lblStatus.Text = "Status: Monitoring";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 20;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 193);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblLimitUnit);
            this.Controls.Add(this.lblDbUnit);
            this.Controls.Add(this.pnlSoundBar);
            this.Controls.Add(this.btnStartStop);
            this.Controls.Add(this.btnSetLimit);
            this.Controls.Add(this.txtWarningLimit);
            this.Controls.Add(this.lblWarningLimit);
            this.Controls.Add(this.lblCurrentLevel);
            this.Name = "Form1";
            this.Text = "Loudness Warning Monitor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCurrentLevel;
        private System.Windows.Forms.Label lblWarningLimit;
        private System.Windows.Forms.TextBox txtWarningLimit;
        private System.Windows.Forms.Button btnSetLimit;
        private System.Windows.Forms.Button btnStartStop;
        private System.Windows.Forms.Panel pnlSoundBar;
        private System.Windows.Forms.Label lblDbUnit;
        private System.Windows.Forms.Label lblLimitUnit;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Timer timer1;
    }
}

