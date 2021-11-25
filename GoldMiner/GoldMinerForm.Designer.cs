
namespace GoldMiner {
    partial class GoldMinerForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
        if (disposing && (components != null)) {
        components.Dispose();
        }
        base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.lsbGPU = new System.Windows.Forms.ListBox();
            this.lbDriver = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lsbGPU
            // 
            this.lsbGPU.FormattingEnabled = true;
            this.lsbGPU.Location = new System.Drawing.Point(118, 79);
            this.lsbGPU.Name = "lsbGPU";
            this.lsbGPU.Size = new System.Drawing.Size(531, 147);
            this.lsbGPU.TabIndex = 0;
            this.lsbGPU.SelectedIndexChanged += new System.EventHandler(this.lsbGPU_SelectedIndexChanged);
            // 
            // lbDriver
            // 
            this.lbDriver.AutoSize = true;
            this.lbDriver.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDriver.Location = new System.Drawing.Point(96, 13);
            this.lbDriver.Name = "lbDriver";
            this.lbDriver.Size = new System.Drawing.Size(56, 18);
            this.lbDriver.TabIndex = 1;
            this.lbDriver.Text = "label1";
            // 
            // GoldMinerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lbDriver);
            this.Controls.Add(this.lsbGPU);
            this.Name = "GoldMinerForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.GoldMinerForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lsbGPU;
        private System.Windows.Forms.Label lbDriver;
    }
}

