
namespace Grendezvous.Forms
{
    partial class FingerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FingerForm));
            this.lblInfo = new System.Windows.Forms.Label();
            this.labelDevice = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labError = new System.Windows.Forms.Label();
            this.btnEmpr = new Bunifu.Framework.UI.BunifuThinButton2();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.Location = new System.Drawing.Point(125, 284);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(188, 16);
            this.lblInfo.TabIndex = 22;
            this.lblInfo.Text = "Placez votre doigt sur le lecteur !!!";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelDevice
            // 
            this.labelDevice.AutoSize = true;
            this.labelDevice.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDevice.Location = new System.Drawing.Point(6, 4);
            this.labelDevice.Name = "labelDevice";
            this.labelDevice.Size = new System.Drawing.Size(85, 13);
            this.labelDevice.TabIndex = 21;
            this.labelDevice.Text = "Device\'s name :";
            this.labelDevice.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.pictureBox1);
            this.groupBox7.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox7.Location = new System.Drawing.Point(22, 23);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(429, 258);
            this.groupBox7.TabIndex = 20;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Empreinte digitale";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(4, 19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(421, 233);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(232, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Device\'s name :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // labError
            // 
            this.labError.AutoSize = true;
            this.labError.Font = new System.Drawing.Font("Century Gothic", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labError.ForeColor = System.Drawing.Color.Red;
            this.labError.Location = new System.Drawing.Point(23, 304);
            this.labError.Name = "labError";
            this.labError.Size = new System.Drawing.Size(0, 13);
            this.labError.TabIndex = 24;
            this.labError.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // btnEmpr
            // 
            this.btnEmpr.ActiveBorderThickness = 1;
            this.btnEmpr.ActiveCornerRadius = 20;
            this.btnEmpr.ActiveFillColor = System.Drawing.Color.DodgerBlue;
            this.btnEmpr.ActiveForecolor = System.Drawing.Color.White;
            this.btnEmpr.ActiveLineColor = System.Drawing.Color.DodgerBlue;
            this.btnEmpr.BackColor = System.Drawing.SystemColors.Control;
            this.btnEmpr.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEmpr.BackgroundImage")));
            this.btnEmpr.ButtonText = "Valider";
            this.btnEmpr.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEmpr.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEmpr.ForeColor = System.Drawing.Color.Black;
            this.btnEmpr.IdleBorderThickness = 1;
            this.btnEmpr.IdleCornerRadius = 20;
            this.btnEmpr.IdleFillColor = System.Drawing.Color.White;
            this.btnEmpr.IdleForecolor = System.Drawing.Color.DimGray;
            this.btnEmpr.IdleLineColor = System.Drawing.Color.DimGray;
            this.btnEmpr.Location = new System.Drawing.Point(6, 1);
            this.btnEmpr.Margin = new System.Windows.Forms.Padding(4);
            this.btnEmpr.Name = "btnEmpr";
            this.btnEmpr.Size = new System.Drawing.Size(94, 40);
            this.btnEmpr.TabIndex = 25;
            this.btnEmpr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnEmpr.Click += new System.EventHandler(this.btnEmpr_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnEmpr);
            this.panel1.Location = new System.Drawing.Point(354, 284);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(123, 45);
            this.panel1.TabIndex = 26;
            // 
            // FingerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(472, 325);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labError);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.labelDevice);
            this.Controls.Add(this.groupBox7);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FingerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FingerForm";
            this.Load += new System.EventHandler(this.FingerForm_Load);
            this.groupBox7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label labelDevice;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labError;
        private Bunifu.Framework.UI.BunifuThinButton2 btnEmpr;
        private System.Windows.Forms.Panel panel1;
    }
}