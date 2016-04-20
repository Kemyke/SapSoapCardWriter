namespace SapSoapCardWriter.GUI
{
    partial class SapSoapCardWriterWindow
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fájlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.súgóToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.névjegyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolReaderStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.tbBirthPlace = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbFullName = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.btnWriteCard = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbBirthDate = new System.Windows.Forms.TextBox();
            this.tbChamberId = new System.Windows.Forms.TextBox();
            this.tbTaxId = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fájlToolStripMenuItem,
            this.súgóToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(894, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fájlToolStripMenuItem
            // 
            this.fájlToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeMenuItem});
            this.fájlToolStripMenuItem.Name = "fájlToolStripMenuItem";
            this.fájlToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fájlToolStripMenuItem.Text = "Fájl";
            // 
            // closeMenuItem
            // 
            this.closeMenuItem.Name = "closeMenuItem";
            this.closeMenuItem.Size = new System.Drawing.Size(111, 22);
            this.closeMenuItem.Text = "Kilépés";
            this.closeMenuItem.Click += new System.EventHandler(this.closeMenuItem_Click);
            // 
            // súgóToolStripMenuItem
            // 
            this.súgóToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.névjegyToolStripMenuItem});
            this.súgóToolStripMenuItem.Name = "súgóToolStripMenuItem";
            this.súgóToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.súgóToolStripMenuItem.Text = "Súgó";
            // 
            // névjegyToolStripMenuItem
            // 
            this.névjegyToolStripMenuItem.Name = "névjegyToolStripMenuItem";
            this.névjegyToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.névjegyToolStripMenuItem.Text = "Névjegy";
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(193)))), ((int)(((byte)(35)))));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolReaderStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 511);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(894, 24);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolReaderStatus
            // 
            this.toolReaderStatus.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.toolReaderStatus.Name = "toolReaderStatus";
            this.toolReaderStatus.Size = new System.Drawing.Size(95, 19);
            this.toolReaderStatus.Text = "Nincs kártya";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 61F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 87F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tbTaxId, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbChamberId, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbBirthDate, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label5, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbBirthPlace, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbFullName, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(103, 60);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(688, 353);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 45);
            this.label1.TabIndex = 1;
            this.label1.Text = "Név";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbBirthPlace
            // 
            this.tbBirthPlace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbBirthPlace.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbBirthPlace.Location = new System.Drawing.Point(421, 13);
            this.tbBirthPlace.Margin = new System.Windows.Forms.Padding(3, 13, 3, 3);
            this.tbBirthPlace.Name = "tbBirthPlace";
            this.tbBirthPlace.ReadOnly = true;
            this.tbBirthPlace.Size = new System.Drawing.Size(264, 26);
            this.tbBirthPlace.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(334, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 45);
            this.label2.TabIndex = 2;
            this.label2.Text = "Szül. hely";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbFullName
            // 
            this.tbFullName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbFullName.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbFullName.Location = new System.Drawing.Point(64, 13);
            this.tbFullName.Margin = new System.Windows.Forms.Padding(3, 13, 3, 3);
            this.tbFullName.Name = "tbFullName";
            this.tbFullName.ReadOnly = true;
            this.tbFullName.Size = new System.Drawing.Size(264, 26);
            this.tbFullName.TabIndex = 3;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.Controls.Add(this.label3, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnWriteCard, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 1, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 57F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 71F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(894, 487);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Tahoma", 16F);
            this.label3.Location = new System.Drawing.Point(103, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(688, 57);
            this.label3.TabIndex = 4;
            this.label3.Text = "Kártya adatok";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnWriteCard
            // 
            this.btnWriteCard.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnWriteCard.Enabled = false;
            this.btnWriteCard.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnWriteCard.Location = new System.Drawing.Point(803, 419);
            this.btnWriteCard.Name = "btnWriteCard";
            this.btnWriteCard.Size = new System.Drawing.Size(88, 65);
            this.btnWriteCard.TabIndex = 3;
            this.btnWriteCard.Text = "Kártya írás";
            this.btnWriteCard.UseVisualStyleBackColor = true;
            this.btnWriteCard.Click += new System.EventHandler(this.btnWriteCard_Click);
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(3, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 45);
            this.label4.TabIndex = 5;
            this.label4.Text = "Szül.idő";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(334, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 45);
            this.label5.TabIndex = 6;
            this.label5.Text = "Kamarai azonosító";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.Location = new System.Drawing.Point(3, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 45);
            this.label6.TabIndex = 7;
            this.label6.Text = "Adóazonosító";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbBirthDate
            // 
            this.tbBirthDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbBirthDate.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbBirthDate.Location = new System.Drawing.Point(64, 58);
            this.tbBirthDate.Margin = new System.Windows.Forms.Padding(3, 13, 3, 3);
            this.tbBirthDate.Name = "tbBirthDate";
            this.tbBirthDate.ReadOnly = true;
            this.tbBirthDate.Size = new System.Drawing.Size(264, 26);
            this.tbBirthDate.TabIndex = 8;
            // 
            // tbChamberId
            // 
            this.tbChamberId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbChamberId.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbChamberId.Location = new System.Drawing.Point(421, 58);
            this.tbChamberId.Margin = new System.Windows.Forms.Padding(3, 13, 3, 3);
            this.tbChamberId.Name = "tbChamberId";
            this.tbChamberId.ReadOnly = true;
            this.tbChamberId.Size = new System.Drawing.Size(264, 26);
            this.tbChamberId.TabIndex = 9;
            // 
            // tbTaxId
            // 
            this.tbTaxId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbTaxId.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbTaxId.Location = new System.Drawing.Point(64, 103);
            this.tbTaxId.Margin = new System.Windows.Forms.Padding(3, 13, 3, 3);
            this.tbTaxId.Name = "tbTaxId";
            this.tbTaxId.ReadOnly = true;
            this.tbTaxId.Size = new System.Drawing.Size(264, 26);
            this.tbTaxId.TabIndex = 10;
            // 
            // SapSoapCardWriterWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(193)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(894, 535);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "SapSoapCardWriterWindow";
            this.Text = "NAK kártyaíró";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fájlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem súgóToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem névjegyToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStripStatusLabel toolReaderStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbFullName;
        private System.Windows.Forms.TextBox tbBirthPlace;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnWriteCard;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbBirthDate;
        private System.Windows.Forms.TextBox tbTaxId;
        private System.Windows.Forms.TextBox tbChamberId;
    }
}

