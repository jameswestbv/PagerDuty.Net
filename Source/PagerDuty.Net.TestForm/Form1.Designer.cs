namespace PagerDuty.Net.TestForm
{
    partial class Form1
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
            this.txtServiceKey = new System.Windows.Forms.TextBox();
            this.txtIncidentKey = new System.Windows.Forms.TextBox();
            this.btnTrigger = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Service Key:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Incident Key:";
            // 
            // txtServiceKey
            // 
            this.txtServiceKey.Location = new System.Drawing.Point(85, 24);
            this.txtServiceKey.Name = "txtServiceKey";
            this.txtServiceKey.Size = new System.Drawing.Size(187, 20);
            this.txtServiceKey.TabIndex = 2;
            // 
            // txtIncidentKey
            // 
            this.txtIncidentKey.Location = new System.Drawing.Point(85, 51);
            this.txtIncidentKey.Name = "txtIncidentKey";
            this.txtIncidentKey.Size = new System.Drawing.Size(187, 20);
            this.txtIncidentKey.TabIndex = 3;
            // 
            // btnTrigger
            // 
            this.btnTrigger.Location = new System.Drawing.Point(85, 97);
            this.btnTrigger.Name = "btnTrigger";
            this.btnTrigger.Size = new System.Drawing.Size(133, 33);
            this.btnTrigger.TabIndex = 4;
            this.btnTrigger.Text = "Trigger";
            this.btnTrigger.UseVisualStyleBackColor = true;
            this.btnTrigger.Click += new System.EventHandler(this.btnTrigger_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.btnTrigger);
            this.Controls.Add(this.txtIncidentKey);
            this.Controls.Add(this.txtServiceKey);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtServiceKey;
        private System.Windows.Forms.TextBox txtIncidentKey;
        private System.Windows.Forms.Button btnTrigger;
    }
}

