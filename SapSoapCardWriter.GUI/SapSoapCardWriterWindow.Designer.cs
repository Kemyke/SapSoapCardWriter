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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fájlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.súgóToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.névjegyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolReaderStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tbLastWriteUser = new System.Windows.Forms.TextBox();
            this.tbLastWriteDate = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbCardStatus = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbCardType = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbTaxNo = new System.Windows.Forms.TextBox();
            this.tbTaxId = new System.Windows.Forms.TextBox();
            this.tbChamberId = new System.Windows.Forms.TextBox();
            this.tbBirthDate = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbBirthPlace = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbFullName = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.btnWriteCard = new System.Windows.Forms.Button();
            this.tlpFunctionSelector = new System.Windows.Forms.TableLayoutPanel();
            this.btnEventRegistration = new System.Windows.Forms.Button();
            this.btnCardWriter = new System.Windows.Forms.Button();
            this.tlpEventSelector = new System.Windows.Forms.TableLayoutPanel();
            this.btnSelectEvent = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tlpEventRegistration = new System.Windows.Forms.TableLayoutPanel();
            this.lbEventData = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.eventDataBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.iDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.locationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tlpFunctionSelector.SuspendLayout();
            this.tlpEventSelector.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tlpEventRegistration.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eventDataBindingSource)).BeginInit();
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
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label11, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.tbLastWriteUser, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.tbLastWriteDate, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label9, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbCardStatus, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbCardType, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label7, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.tbTaxNo, 4, 3);
            this.tableLayoutPanel1.Controls.Add(this.tbTaxId, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.tbChamberId, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbBirthDate, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbBirthPlace, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbFullName, 3, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(103, 60);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(688, 353);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // label11
            // 
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label11.Location = new System.Drawing.Point(357, 220);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(94, 55);
            this.label11.TabIndex = 20;
            this.label11.Text = "Utolsó író";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label10.Location = new System.Drawing.Point(3, 220);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(114, 55);
            this.label10.TabIndex = 19;
            this.label10.Text = "Utolsó írás";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbLastWriteUser
            // 
            this.tbLastWriteUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLastWriteUser.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbLastWriteUser.Location = new System.Drawing.Point(457, 233);
            this.tbLastWriteUser.Margin = new System.Windows.Forms.Padding(3, 13, 3, 3);
            this.tbLastWriteUser.Name = "tbLastWriteUser";
            this.tbLastWriteUser.ReadOnly = true;
            this.tbLastWriteUser.Size = new System.Drawing.Size(228, 26);
            this.tbLastWriteUser.TabIndex = 18;
            // 
            // tbLastWriteDate
            // 
            this.tbLastWriteDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLastWriteDate.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbLastWriteDate.Location = new System.Drawing.Point(123, 233);
            this.tbLastWriteDate.Margin = new System.Windows.Forms.Padding(3, 13, 3, 3);
            this.tbLastWriteDate.Name = "tbLastWriteDate";
            this.tbLastWriteDate.ReadOnly = true;
            this.tbLastWriteDate.Size = new System.Drawing.Size(228, 26);
            this.tbLastWriteDate.TabIndex = 17;
            // 
            // label9
            // 
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label9.Location = new System.Drawing.Point(357, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(94, 55);
            this.label9.TabIndex = 16;
            this.label9.Text = "Kártya státusz";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbCardStatus
            // 
            this.tbCardStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbCardStatus.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbCardStatus.Location = new System.Drawing.Point(457, 13);
            this.tbCardStatus.Margin = new System.Windows.Forms.Padding(3, 13, 3, 3);
            this.tbCardStatus.Name = "tbCardStatus";
            this.tbCardStatus.ReadOnly = true;
            this.tbCardStatus.Size = new System.Drawing.Size(228, 26);
            this.tbCardStatus.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label8.Location = new System.Drawing.Point(3, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(114, 55);
            this.label8.TabIndex = 14;
            this.label8.Text = "Kártya típus";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbCardType
            // 
            this.tbCardType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbCardType.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbCardType.Location = new System.Drawing.Point(123, 13);
            this.tbCardType.Margin = new System.Windows.Forms.Padding(3, 13, 3, 3);
            this.tbCardType.Name = "tbCardType";
            this.tbCardType.ReadOnly = true;
            this.tbCardType.Size = new System.Drawing.Size(228, 26);
            this.tbCardType.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label7.Location = new System.Drawing.Point(357, 165);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 55);
            this.label7.TabIndex = 12;
            this.label7.Text = "Adószám";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbTaxNo
            // 
            this.tbTaxNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbTaxNo.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbTaxNo.Location = new System.Drawing.Point(457, 178);
            this.tbTaxNo.Margin = new System.Windows.Forms.Padding(3, 13, 3, 3);
            this.tbTaxNo.Name = "tbTaxNo";
            this.tbTaxNo.ReadOnly = true;
            this.tbTaxNo.Size = new System.Drawing.Size(228, 26);
            this.tbTaxNo.TabIndex = 11;
            // 
            // tbTaxId
            // 
            this.tbTaxId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbTaxId.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbTaxId.Location = new System.Drawing.Point(123, 178);
            this.tbTaxId.Margin = new System.Windows.Forms.Padding(3, 13, 3, 3);
            this.tbTaxId.Name = "tbTaxId";
            this.tbTaxId.ReadOnly = true;
            this.tbTaxId.Size = new System.Drawing.Size(228, 26);
            this.tbTaxId.TabIndex = 10;
            // 
            // tbChamberId
            // 
            this.tbChamberId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbChamberId.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbChamberId.Location = new System.Drawing.Point(123, 68);
            this.tbChamberId.Margin = new System.Windows.Forms.Padding(3, 13, 3, 3);
            this.tbChamberId.Name = "tbChamberId";
            this.tbChamberId.ReadOnly = true;
            this.tbChamberId.Size = new System.Drawing.Size(228, 26);
            this.tbChamberId.TabIndex = 9;
            // 
            // tbBirthDate
            // 
            this.tbBirthDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbBirthDate.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbBirthDate.Location = new System.Drawing.Point(457, 123);
            this.tbBirthDate.Margin = new System.Windows.Forms.Padding(3, 13, 3, 3);
            this.tbBirthDate.Name = "tbBirthDate";
            this.tbBirthDate.ReadOnly = true;
            this.tbBirthDate.Size = new System.Drawing.Size(228, 26);
            this.tbBirthDate.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.Location = new System.Drawing.Point(3, 165);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(114, 55);
            this.label6.TabIndex = 7;
            this.label6.Text = "Adóazonosító";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(3, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 55);
            this.label5.TabIndex = 6;
            this.label5.Text = "Kamarai azonosító";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(357, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 55);
            this.label4.TabIndex = 5;
            this.label4.Text = "Szül.idő";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(357, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 55);
            this.label1.TabIndex = 1;
            this.label1.Text = "Név";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbBirthPlace
            // 
            this.tbBirthPlace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbBirthPlace.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbBirthPlace.Location = new System.Drawing.Point(123, 123);
            this.tbBirthPlace.Margin = new System.Windows.Forms.Padding(3, 13, 3, 3);
            this.tbBirthPlace.Name = "tbBirthPlace";
            this.tbBirthPlace.ReadOnly = true;
            this.tbBirthPlace.Size = new System.Drawing.Size(228, 26);
            this.tbBirthPlace.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(3, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 55);
            this.label2.TabIndex = 2;
            this.label2.Text = "Szül. hely";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbFullName
            // 
            this.tbFullName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbFullName.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tbFullName.Location = new System.Drawing.Point(457, 68);
            this.tbFullName.Margin = new System.Windows.Forms.Padding(3, 13, 3, 3);
            this.tbFullName.Name = "tbFullName";
            this.tbFullName.ReadOnly = true;
            this.tbFullName.Size = new System.Drawing.Size(228, 26);
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
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 57F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 71F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(894, 535);
            this.tableLayoutPanel2.TabIndex = 3;
            this.tableLayoutPanel2.Visible = false;
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
            this.btnWriteCard.Location = new System.Drawing.Point(803, 467);
            this.btnWriteCard.Name = "btnWriteCard";
            this.btnWriteCard.Size = new System.Drawing.Size(88, 65);
            this.btnWriteCard.TabIndex = 3;
            this.btnWriteCard.Text = "Kártya írás";
            this.btnWriteCard.UseVisualStyleBackColor = true;
            this.btnWriteCard.Click += new System.EventHandler(this.btnWriteCard_Click);
            // 
            // tlpFunctionSelector
            // 
            this.tlpFunctionSelector.ColumnCount = 2;
            this.tlpFunctionSelector.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpFunctionSelector.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpFunctionSelector.Controls.Add(this.btnEventRegistration, 1, 0);
            this.tlpFunctionSelector.Controls.Add(this.btnCardWriter, 0, 0);
            this.tlpFunctionSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpFunctionSelector.Location = new System.Drawing.Point(0, 0);
            this.tlpFunctionSelector.Name = "tlpFunctionSelector";
            this.tlpFunctionSelector.RowCount = 2;
            this.tlpFunctionSelector.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpFunctionSelector.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpFunctionSelector.Size = new System.Drawing.Size(894, 535);
            this.tlpFunctionSelector.TabIndex = 5;
            // 
            // btnEventRegistration
            // 
            this.btnEventRegistration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnEventRegistration.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnEventRegistration.Location = new System.Drawing.Point(467, 20);
            this.btnEventRegistration.Margin = new System.Windows.Forms.Padding(20);
            this.btnEventRegistration.Name = "btnEventRegistration";
            this.btnEventRegistration.Size = new System.Drawing.Size(407, 227);
            this.btnEventRegistration.TabIndex = 1;
            this.btnEventRegistration.Text = "Esemény regisztráció";
            this.btnEventRegistration.UseVisualStyleBackColor = true;
            this.btnEventRegistration.Click += new System.EventHandler(this.btnEventRegistration_Click);
            // 
            // btnCardWriter
            // 
            this.btnCardWriter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCardWriter.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnCardWriter.Location = new System.Drawing.Point(20, 20);
            this.btnCardWriter.Margin = new System.Windows.Forms.Padding(20);
            this.btnCardWriter.Name = "btnCardWriter";
            this.btnCardWriter.Size = new System.Drawing.Size(407, 227);
            this.btnCardWriter.TabIndex = 0;
            this.btnCardWriter.Text = "NAK kártyaírás";
            this.btnCardWriter.UseVisualStyleBackColor = true;
            this.btnCardWriter.Click += new System.EventHandler(this.btnCardWriter_Click);
            // 
            // tlpEventSelector
            // 
            this.tlpEventSelector.ColumnCount = 1;
            this.tlpEventSelector.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpEventSelector.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpEventSelector.Controls.Add(this.btnSelectEvent, 0, 1);
            this.tlpEventSelector.Controls.Add(this.dataGridView1, 0, 0);
            this.tlpEventSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpEventSelector.Location = new System.Drawing.Point(0, 24);
            this.tlpEventSelector.Name = "tlpEventSelector";
            this.tlpEventSelector.RowCount = 2;
            this.tlpEventSelector.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 89.73306F));
            this.tlpEventSelector.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.26694F));
            this.tlpEventSelector.Size = new System.Drawing.Size(894, 487);
            this.tlpEventSelector.TabIndex = 2;
            this.tlpEventSelector.Visible = false;
            // 
            // btnSelectEvent
            // 
            this.btnSelectEvent.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSelectEvent.Location = new System.Drawing.Point(816, 440);
            this.btnSelectEvent.Name = "btnSelectEvent";
            this.btnSelectEvent.Size = new System.Drawing.Size(75, 44);
            this.btnSelectEvent.TabIndex = 1;
            this.btnSelectEvent.Text = "Kiválaszt";
            this.btnSelectEvent.UseVisualStyleBackColor = true;
            this.btnSelectEvent.Click += new System.EventHandler(this.btnSelectEvent_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iDDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn,
            this.locationDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.eventDataBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(888, 431);
            this.dataGridView1.TabIndex = 0;
            // 
            // tlpEventRegistration
            // 
            this.tlpEventRegistration.ColumnCount = 1;
            this.tlpEventRegistration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpEventRegistration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpEventRegistration.Controls.Add(this.lbEventData, 0, 1);
            this.tlpEventRegistration.Controls.Add(this.label12, 0, 0);
            this.tlpEventRegistration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpEventRegistration.Location = new System.Drawing.Point(0, 0);
            this.tlpEventRegistration.Name = "tlpEventRegistration";
            this.tlpEventRegistration.RowCount = 3;
            this.tlpEventRegistration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.26694F));
            this.tlpEventRegistration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 89.73306F));
            this.tlpEventRegistration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpEventRegistration.Size = new System.Drawing.Size(894, 535);
            this.tlpEventRegistration.TabIndex = 2;
            this.tlpEventRegistration.Visible = false;
            // 
            // lbEventData
            // 
            this.lbEventData.AutoSize = true;
            this.lbEventData.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbEventData.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lbEventData.Location = new System.Drawing.Point(3, 52);
            this.lbEventData.Name = "lbEventData";
            this.lbEventData.Size = new System.Drawing.Size(888, 29);
            this.lbEventData.TabIndex = 1;
            this.lbEventData.Text = "esemény adatok";
            this.lbEventData.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label12.Location = new System.Drawing.Point(3, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(888, 29);
            this.label12.TabIndex = 0;
            this.label12.Text = "Kiválasztott esemény";
            this.label12.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // eventDataBindingSource
            // 
            this.eventDataBindingSource.DataSource = typeof(SapSoapCardWriter.BusinessLogic.SapService.EventData);
            // 
            // iDDataGridViewTextBoxColumn
            // 
            this.iDDataGridViewTextBoxColumn.DataPropertyName = "ID";
            this.iDDataGridViewTextBoxColumn.HeaderText = "ID";
            this.iDDataGridViewTextBoxColumn.Name = "iDDataGridViewTextBoxColumn";
            this.iDDataGridViewTextBoxColumn.Width = 200;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.Width = 150;
            // 
            // locationDataGridViewTextBoxColumn
            // 
            this.locationDataGridViewTextBoxColumn.DataPropertyName = "Location";
            this.locationDataGridViewTextBoxColumn.HeaderText = "Location";
            this.locationDataGridViewTextBoxColumn.Name = "locationDataGridViewTextBoxColumn";
            this.locationDataGridViewTextBoxColumn.Width = 250;
            // 
            // SapSoapCardWriterWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(193)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(894, 535);
            this.Controls.Add(this.tlpEventSelector);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tlpEventRegistration);
            this.Controls.Add(this.tlpFunctionSelector);
            this.Controls.Add(this.tableLayoutPanel2);
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
            this.tlpFunctionSelector.ResumeLayout(false);
            this.tlpEventSelector.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tlpEventRegistration.ResumeLayout(false);
            this.tlpEventRegistration.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eventDataBindingSource)).EndInit();
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
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbLastWriteUser;
        private System.Windows.Forms.TextBox tbLastWriteDate;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbCardStatus;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbCardType;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbTaxNo;
        private System.Windows.Forms.TableLayoutPanel tlpFunctionSelector;
        private System.Windows.Forms.Button btnEventRegistration;
        private System.Windows.Forms.Button btnCardWriter;
        private System.Windows.Forms.TableLayoutPanel tlpEventSelector;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnSelectEvent;
        private System.Windows.Forms.TableLayoutPanel tlpEventRegistration;
        private System.Windows.Forms.Label lbEventData;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.BindingSource eventDataBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn locationDataGridViewTextBoxColumn;
    }
}

