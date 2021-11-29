
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
            this.components = new System.ComponentModel.Container();
            this.lsbGPU = new System.Windows.Forms.ListBox();
            this.lbDriver = new System.Windows.Forms.Label();
            this.tbOverclock_Core = new System.Windows.Forms.TextBox();
            this.tbOverclock_Mem = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSelected = new System.Windows.Forms.Button();
            this.btnAll = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btReset = new System.Windows.Forms.Button();
            this.gbGPU = new System.Windows.Forms.GroupBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.gbGPUList = new System.Windows.Forms.GroupBox();
            this.gbGPU.SuspendLayout();
            this.SuspendLayout();
            // 
            // lsbGPU
            // 
            this.lsbGPU.FormattingEnabled = true;
            this.lsbGPU.Location = new System.Drawing.Point(12, 34);
            this.lsbGPU.Name = "lsbGPU";
            this.lsbGPU.Size = new System.Drawing.Size(873, 69);
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
            // tbOverclock_Core
            // 
            this.tbOverclock_Core.Location = new System.Drawing.Point(477, 470);
            this.tbOverclock_Core.Name = "tbOverclock_Core";
            this.tbOverclock_Core.Size = new System.Drawing.Size(100, 20);
            this.tbOverclock_Core.TabIndex = 2;
            // 
            // tbOverclock_Mem
            // 
            this.tbOverclock_Mem.Location = new System.Drawing.Point(477, 491);
            this.tbOverclock_Mem.Name = "tbOverclock_Mem";
            this.tbOverclock_Mem.Size = new System.Drawing.Size(100, 20);
            this.tbOverclock_Mem.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(433, 471);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Core:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(492, 449);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Overclock:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(433, 493);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Mem:";
            // 
            // btnSelected
            // 
            this.btnSelected.Location = new System.Drawing.Point(583, 471);
            this.btnSelected.Name = "btnSelected";
            this.btnSelected.Size = new System.Drawing.Size(58, 35);
            this.btnSelected.TabIndex = 7;
            this.btnSelected.Text = "Selected";
            this.btnSelected.UseVisualStyleBackColor = true;
            this.btnSelected.Click += new System.EventHandler(this.btnSelected_Click);
            // 
            // btnAll
            // 
            this.btnAll.Location = new System.Drawing.Point(647, 471);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(58, 35);
            this.btnAll.TabIndex = 8;
            this.btnAll.Text = "All";
            this.btnAll.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(609, 449);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Apply to:";
            // 
            // btReset
            // 
            this.btReset.Location = new System.Drawing.Point(726, 470);
            this.btReset.Name = "btReset";
            this.btReset.Size = new System.Drawing.Size(58, 35);
            this.btReset.TabIndex = 10;
            this.btReset.Text = "Reset OC";
            this.btReset.UseVisualStyleBackColor = true;
            this.btReset.Click += new System.EventHandler(this.btReset_Click);
            // 
            // gbGPU
            // 
            this.gbGPU.Controls.Add(this.gbGPUList);
            this.gbGPU.Location = new System.Drawing.Point(28, 153);
            this.gbGPU.Name = "gbGPU";
            this.gbGPU.Size = new System.Drawing.Size(1118, 273);
            this.gbGPU.TabIndex = 11;
            this.gbGPU.TabStop = false;
            this.gbGPU.Text = "gbGPU";
            this.gbGPU.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // gbGPUList
            // 
            this.gbGPUList.Location = new System.Drawing.Point(29, 33);
            this.gbGPUList.Name = "gbGPUList";
            this.gbGPUList.Size = new System.Drawing.Size(1187, 334);
            this.gbGPUList.TabIndex = 13;
            this.gbGPUList.TabStop = false;
            this.gbGPUList.Text = "gbGPUList";
            // 
            // GoldMinerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1244, 608);
            this.Controls.Add(this.gbGPU);
            this.Controls.Add(this.btReset);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnAll);
            this.Controls.Add(this.btnSelected);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbOverclock_Mem);
            this.Controls.Add(this.tbOverclock_Core);
            this.Controls.Add(this.lbDriver);
            this.Controls.Add(this.lsbGPU);
            this.Name = "GoldMinerForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GoldMinerForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GoldMinerForm_FormClosed);
            this.Load += new System.EventHandler(this.GoldMinerForm_Load);
            this.gbGPU.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lsbGPU;
        private System.Windows.Forms.Label lbDriver;
        private System.Windows.Forms.TextBox tbOverclock_Core;
        private System.Windows.Forms.TextBox tbOverclock_Mem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSelected;
        private System.Windows.Forms.Button btnAll;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btReset;
        private System.Windows.Forms.GroupBox gbGPU;
        private System.Windows.Forms.GroupBox gbGPUList;
        private System.Windows.Forms.ImageList imageList1;
    }
}

