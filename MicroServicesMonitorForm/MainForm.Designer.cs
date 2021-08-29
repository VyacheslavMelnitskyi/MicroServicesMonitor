namespace MicroServicesMonitor
{
    partial class MainForm
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
            this.CheckStatusTimer = new System.Windows.Forms.Timer(this.components);
            this.TopMostCheckBox = new System.Windows.Forms.CheckBox();
            this.TransparentCheckBox = new System.Windows.Forms.CheckBox();
            this.CheckServicesButton = new System.Windows.Forms.Button();
            this.TimerCheckBox = new System.Windows.Forms.CheckBox();
            this.EnableCheckButtonTimer = new System.Windows.Forms.Timer(this.components);
            this.StartAllButton = new System.Windows.Forms.Button();
            this.StopAllButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CheckStatusTimer
            // 
            this.CheckStatusTimer.Interval = 5000;
            this.CheckStatusTimer.Tick += new System.EventHandler(this.CheckStatusTimer_Tick);
            // 
            // TopMostCheckBox
            // 
            this.TopMostCheckBox.AutoSize = true;
            this.TopMostCheckBox.Checked = true;
            this.TopMostCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TopMostCheckBox.Location = new System.Drawing.Point(10, 16);
            this.TopMostCheckBox.Name = "TopMostCheckBox";
            this.TopMostCheckBox.Size = new System.Drawing.Size(72, 19);
            this.TopMostCheckBox.TabIndex = 0;
            this.TopMostCheckBox.Text = "TopMost";
            this.TopMostCheckBox.UseVisualStyleBackColor = true;
            this.TopMostCheckBox.CheckedChanged += new System.EventHandler(this.TopMostCheckBox_CheckedChanged);
            // 
            // TransparentCheckBox
            // 
            this.TransparentCheckBox.AutoSize = true;
            this.TransparentCheckBox.Checked = true;
            this.TransparentCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TransparentCheckBox.Location = new System.Drawing.Point(120, 16);
            this.TransparentCheckBox.Name = "TransparentCheckBox";
            this.TransparentCheckBox.Size = new System.Drawing.Size(87, 19);
            this.TransparentCheckBox.TabIndex = 1;
            this.TransparentCheckBox.Text = "Transparent";
            this.TransparentCheckBox.UseVisualStyleBackColor = true;
            this.TransparentCheckBox.CheckedChanged += new System.EventHandler(this.TransparentCheckBox_CheckedChanged);
            // 
            // CheckServicesButton
            // 
            this.CheckServicesButton.Location = new System.Drawing.Point(10, 50);
            this.CheckServicesButton.Name = "CheckServicesButton";
            this.CheckServicesButton.Size = new System.Drawing.Size(100, 30);
            this.CheckServicesButton.TabIndex = 2;
            this.CheckServicesButton.Text = "Check";
            this.CheckServicesButton.UseVisualStyleBackColor = true;
            this.CheckServicesButton.Click += new System.EventHandler(this.CheckServicesButton_Click);
            // 
            // TimerCheckBox
            // 
            this.TimerCheckBox.AutoSize = true;
            this.TimerCheckBox.Checked = true;
            this.TimerCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TimerCheckBox.Location = new System.Drawing.Point(120, 57);
            this.TimerCheckBox.Name = "TimerCheckBox";
            this.TimerCheckBox.Size = new System.Drawing.Size(89, 19);
            this.TimerCheckBox.TabIndex = 3;
            this.TimerCheckBox.Text = "TimerCheck";
            this.TimerCheckBox.UseVisualStyleBackColor = true;
            this.TimerCheckBox.CheckedChanged += new System.EventHandler(this.TimerCheckBox_CheckedChanged);
            // 
            // EnableCheckButtonTimer
            // 
            this.EnableCheckButtonTimer.Interval = 5000;
            this.EnableCheckButtonTimer.Tick += new System.EventHandler(this.EnableCheckButtonTimer_Tick);
            // 
            // StartAllButton
            // 
            this.StartAllButton.Location = new System.Drawing.Point(10, 89);
            this.StartAllButton.Name = "StartAllButton";
            this.StartAllButton.Size = new System.Drawing.Size(100, 30);
            this.StartAllButton.TabIndex = 4;
            this.StartAllButton.Text = "Start All";
            this.StartAllButton.UseVisualStyleBackColor = true;
            this.StartAllButton.Click += new System.EventHandler(this.StartAllButton_Click);
            // 
            // StopAllButton
            // 
            this.StopAllButton.Location = new System.Drawing.Point(120, 89);
            this.StopAllButton.Name = "StopAllButton";
            this.StopAllButton.Size = new System.Drawing.Size(100, 30);
            this.StopAllButton.TabIndex = 5;
            this.StopAllButton.Text = "Stop All";
            this.StopAllButton.UseVisualStyleBackColor = true;
            this.StopAllButton.Click += new System.EventHandler(this.StopAllButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(232, 136);
            this.Controls.Add(this.StopAllButton);
            this.Controls.Add(this.StartAllButton);
            this.Controls.Add(this.TimerCheckBox);
            this.Controls.Add(this.CheckServicesButton);
            this.Controls.Add(this.TransparentCheckBox);
            this.Controls.Add(this.TopMostCheckBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Opacity = 0.5D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "MicroServicesMonitor";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer CheckStatusTimer;
        private System.Windows.Forms.CheckBox TransparentCheckBox;
        private System.Windows.Forms.Button CheckServicesButton;
        private System.Windows.Forms.CheckBox TimerCheckBox;
        private System.Windows.Forms.CheckBox TopMostCheckBox;
        private System.Windows.Forms.Timer EnableCheckButtonTimer;
        private System.Windows.Forms.Button StartAllButton;
        private System.Windows.Forms.Button StopAllButton;
    }
}

