namespace Outfits
{
    partial class ImportOutfits
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
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
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportOutfits));
            this.lstCustomOutfits = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importOutfitsFromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnChooseOutfit = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstCustomOutfits
            // 
            this.lstCustomOutfits.FormattingEnabled = true;
            this.lstCustomOutfits.Location = new System.Drawing.Point(12, 27);
            this.lstCustomOutfits.Name = "lstCustomOutfits";
            this.lstCustomOutfits.Size = new System.Drawing.Size(228, 251);
            this.lstCustomOutfits.TabIndex = 0;
            this.lstCustomOutfits.SelectedIndexChanged += new System.EventHandler(this.lstCustomOutfits_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 19);
            this.label1.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(256, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importOutfitsFromFileToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // importOutfitsFromFileToolStripMenuItem
            // 
            this.importOutfitsFromFileToolStripMenuItem.Name = "importOutfitsFromFileToolStripMenuItem";
            this.importOutfitsFromFileToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.importOutfitsFromFileToolStripMenuItem.Text = "Import Outfits from file...";
            this.importOutfitsFromFileToolStripMenuItem.Click += new System.EventHandler(this.importOutfitsFromFileToolStripMenuItem_Click);
            // 
            // btnChooseOutfit
            // 
            this.btnChooseOutfit.Enabled = false;
            this.btnChooseOutfit.Location = new System.Drawing.Point(81, 288);
            this.btnChooseOutfit.Name = "btnChooseOutfit";
            this.btnChooseOutfit.Size = new System.Drawing.Size(75, 23);
            this.btnChooseOutfit.TabIndex = 3;
            this.btnChooseOutfit.Text = "Import";
            this.btnChooseOutfit.UseVisualStyleBackColor = true;
            this.btnChooseOutfit.Click += new System.EventHandler(this.btnChooseOutfit_Click);
            // 
            // ImportOutfits
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(256, 323);
            this.Controls.Add(this.btnChooseOutfit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstCustomOutfits);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImportOutfits";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Custom Outfits";
            this.Load += new System.EventHandler(this.ImportOutfits_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstCustomOutfits;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importOutfitsFromFileToolStripMenuItem;
        private System.Windows.Forms.Button btnChooseOutfit;
    }
}