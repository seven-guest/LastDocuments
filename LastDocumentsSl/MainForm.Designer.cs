namespace OpenResetDocs
{
    partial class MainForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.dGrid = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tbAdd = new System.Windows.Forms.ToolStripButton();
            this.tbDel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tSearch = new System.Windows.Forms.ToolStripButton();
            this.tTextSearch = new System.Windows.Forms.ToolStripTextBox();
            this.cbOpen = new System.Windows.Forms.CheckBox();
            this.popMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.popOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolOpenWith = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.bFix = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lExt = new System.Windows.Forms.Label();
            this.lPath = new System.Windows.Forms.Label();
            this.bAbout = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dGrid)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.popMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dGrid
            // 
            this.dGrid.AllowDrop = true;
            this.dGrid.AllowUserToAddRows = false;
            this.dGrid.AllowUserToDeleteRows = false;
            this.dGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dGrid.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dGrid.DefaultCellStyle = dataGridViewCellStyle1;
            this.dGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dGrid.GridColor = System.Drawing.SystemColors.ButtonFace;
            this.dGrid.Location = new System.Drawing.Point(0, 42);
            this.dGrid.MultiSelect = false;
            this.dGrid.Name = "dGrid";
            this.dGrid.ReadOnly = true;
            this.dGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dGrid.Size = new System.Drawing.Size(717, 246);
            this.dGrid.TabIndex = 0;
            this.dGrid.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dGrid_CellMouseClick);
            this.dGrid.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dGrid_CellMouseDoubleClick);
            this.dGrid.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dGrid_RowEnter);
            this.dGrid.DragDrop += new System.Windows.Forms.DragEventHandler(this.dGrid_DragDrop);
            this.dGrid.DragOver += new System.Windows.Forms.DragEventHandler(this.dGrid_DragOver);
            this.dGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dGrid_KeyDown);
            this.dGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dGrid_MouseDown);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbAdd,
            this.tbDel,
            this.toolStripSeparator1,
            this.tSearch,
            this.tTextSearch});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(719, 39);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tbAdd
            // 
            this.tbAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbAdd.Image = ((System.Drawing.Image)(resources.GetObject("tbAdd.Image")));
            this.tbAdd.ImageTransparentColor = System.Drawing.Color.Lime;
            this.tbAdd.Name = "tbAdd";
            this.tbAdd.Size = new System.Drawing.Size(36, 36);
            this.tbAdd.Text = "toolStripButton1";
            this.tbAdd.Click += new System.EventHandler(this.tbAdd_Click);
            // 
            // tbDel
            // 
            this.tbDel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbDel.Image = ((System.Drawing.Image)(resources.GetObject("tbDel.Image")));
            this.tbDel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbDel.Name = "tbDel";
            this.tbDel.Size = new System.Drawing.Size(36, 36);
            this.tbDel.Text = "toolStripButton2";
            this.tbDel.Click += new System.EventHandler(this.tbDel_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // tSearch
            // 
            this.tSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tSearch.Image = ((System.Drawing.Image)(resources.GetObject("tSearch.Image")));
            this.tSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tSearch.Name = "tSearch";
            this.tSearch.Size = new System.Drawing.Size(36, 36);
            this.tSearch.Text = "toolStripButton1";
            this.tSearch.Click += new System.EventHandler(this.tSearch_Click);
            // 
            // tTextSearch
            // 
            this.tTextSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tTextSearch.Margin = new System.Windows.Forms.Padding(5, 0, 1, 0);
            this.tTextSearch.MaxLength = 25;
            this.tTextSearch.Name = "tTextSearch";
            this.tTextSearch.Size = new System.Drawing.Size(200, 39);
            this.tTextSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tTextSearch_KeyDown);
            // 
            // cbOpen
            // 
            this.cbOpen.AutoSize = true;
            this.cbOpen.Location = new System.Drawing.Point(374, 12);
            this.cbOpen.Name = "cbOpen";
            this.cbOpen.Size = new System.Drawing.Size(166, 17);
            this.cbOpen.TabIndex = 2;
            this.cbOpen.Text = "Открывать при добавлении";
            this.cbOpen.UseVisualStyleBackColor = true;
            // 
            // popMenu
            // 
            this.popMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.popOpen,
            this.toolOpenWith,
            this.toolStripMenuItem3});
            this.popMenu.Name = "contextMenuStrip1";
            this.popMenu.Size = new System.Drawing.Size(191, 70);
            // 
            // popOpen
            // 
            this.popOpen.Name = "popOpen";
            this.popOpen.Size = new System.Drawing.Size(190, 22);
            this.popOpen.Text = "Открыть";
            this.popOpen.Click += new System.EventHandler(this.popOpen_Click);
            // 
            // toolOpenWith
            // 
            this.toolOpenWith.Name = "toolOpenWith";
            this.toolOpenWith.Size = new System.Drawing.Size(190, 22);
            this.toolOpenWith.Text = "Открыть с помощью";
            this.toolOpenWith.Click += new System.EventHandler(this.toolOpenWith_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(190, 22);
            this.toolStripMenuItem3.Text = "Удалить";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.tbDel_Click);
            // 
            // bFix
            // 
            this.bFix.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bFix.BackColor = System.Drawing.SystemColors.Control;
            this.bFix.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.bFix.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bFix.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bFix.ForeColor = System.Drawing.Color.SteelBlue;
            this.bFix.Location = new System.Drawing.Point(566, 8);
            this.bFix.Name = "bFix";
            this.bFix.Size = new System.Drawing.Size(111, 23);
            this.bFix.TabIndex = 5;
            this.bFix.Text = "Сохранить размер";
            this.bFix.UseVisualStyleBackColor = false;
            this.bFix.Click += new System.EventHandler(this.bFix_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.lExt);
            this.panel1.Controls.Add(this.lPath);
            this.panel1.Location = new System.Drawing.Point(0, 290);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(717, 49);
            this.panel1.TabIndex = 6;
            // 
            // lExt
            // 
            this.lExt.AutoSize = true;
            this.lExt.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lExt.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lExt.Location = new System.Drawing.Point(9, 25);
            this.lExt.Name = "lExt";
            this.lExt.Size = new System.Drawing.Size(0, 18);
            this.lExt.TabIndex = 0;
            // 
            // lPath
            // 
            this.lPath.AutoSize = true;
            this.lPath.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lPath.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lPath.Location = new System.Drawing.Point(9, 6);
            this.lPath.Name = "lPath";
            this.lPath.Size = new System.Drawing.Size(0, 18);
            this.lPath.TabIndex = 0;
            // 
            // bAbout
            // 
            this.bAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bAbout.FlatAppearance.BorderSize = 0;
            this.bAbout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bAbout.Image = ((System.Drawing.Image)(resources.GetObject("bAbout.Image")));
            this.bAbout.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.bAbout.Location = new System.Drawing.Point(691, 9);
            this.bAbout.Name = "bAbout";
            this.bAbout.Size = new System.Drawing.Size(22, 22);
            this.bAbout.TabIndex = 7;
            this.bAbout.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.bAbout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bAbout.UseVisualStyleBackColor = true;
            this.bAbout.Click += new System.EventHandler(this.bAbout_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 339);
            this.Controls.Add(this.bAbout);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.bFix);
            this.Controls.Add(this.cbOpen);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.dGrid);
            this.Name = "MainForm";
            this.Text = "Мои документы";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dGrid)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.popMenu.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dGrid;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tbAdd;
        private System.Windows.Forms.ToolStripButton tbDel;
        private System.Windows.Forms.CheckBox cbOpen;
        private System.Windows.Forms.ContextMenuStrip popMenu;
        private System.Windows.Forms.ToolStripMenuItem popOpen;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolOpenWith;
        private System.Windows.Forms.Button bFix;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lExt;
        private System.Windows.Forms.Label lPath;
        private System.Windows.Forms.Button bAbout;
        private System.Windows.Forms.ToolStripButton tSearch;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripTextBox tTextSearch;
    }
}

