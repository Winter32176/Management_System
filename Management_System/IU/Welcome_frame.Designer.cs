
namespace Management_System.IU
{
    partial class Welcome_frame
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
            this.button1 = new System.Windows.Forms.Button();
            this.userNameLabel = new System.Windows.Forms.Label();
            this.sessionIdLabel = new System.Windows.Forms.Label();
            this.logTimeLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(314, 236);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(176, 53);
            this.button1.TabIndex = 0;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // userNameLabel
            // 
            this.userNameLabel.AutoSize = true;
            this.userNameLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.userNameLabel.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.userNameLabel.Location = new System.Drawing.Point(118, 48);
            this.userNameLabel.Name = "userNameLabel";
            this.userNameLabel.Size = new System.Drawing.Size(98, 40);
            this.userNameLabel.TabIndex = 1;
            this.userNameLabel.Text = "Hello, ";
            this.userNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // sessionIdLabel
            // 
            this.sessionIdLabel.AutoSize = true;
            this.sessionIdLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sessionIdLabel.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.sessionIdLabel.Location = new System.Drawing.Point(118, 105);
            this.sessionIdLabel.Name = "sessionIdLabel";
            this.sessionIdLabel.Size = new System.Drawing.Size(217, 40);
            this.sessionIdLabel.TabIndex = 2;
            this.sessionIdLabel.Text = "Your session id: ";
            // 
            // logTimeLabel
            // 
            this.logTimeLabel.AutoSize = true;
            this.logTimeLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.logTimeLabel.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.logTimeLabel.Location = new System.Drawing.Point(118, 166);
            this.logTimeLabel.Name = "logTimeLabel";
            this.logTimeLabel.Size = new System.Drawing.Size(164, 40);
            this.logTimeLabel.TabIndex = 3;
            this.logTimeLabel.Text = "Login time: ";
            // 
            // Welcome_frame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 358);
            this.Controls.Add(this.logTimeLabel);
            this.Controls.Add(this.sessionIdLabel);
            this.Controls.Add(this.userNameLabel);
            this.Controls.Add(this.button1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(800, 405);
            this.MinimumSize = new System.Drawing.Size(800, 405);
            this.Name = "Welcome_frame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Welcome_frame";
            this.Load += new System.EventHandler(this.Welcome_frame_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label userNameLabel;
        private System.Windows.Forms.Label sessionIdLabel;
        private System.Windows.Forms.Label logTimeLabel;
    }
}