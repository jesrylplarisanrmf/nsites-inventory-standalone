namespace NSites.ApplicationObjects.UserInterfaces.MasterFile
{
    partial class SearchStockUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchStockUI));
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvStockList = new System.Windows.Forms.DataGridView();
            this.pbPicture = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblSearchStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPicture)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 13.2F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(10, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 30);
            this.label2.TabIndex = 0;
            this.label2.Text = "Stocks";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 13.2F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(10, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 30);
            this.label3.TabIndex = 1;
            this.label3.Text = "Preview";
            // 
            // dgvStockList
            // 
            this.dgvStockList.AllowUserToAddRows = false;
            this.dgvStockList.AllowUserToDeleteRows = false;
            this.dgvStockList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvStockList.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dgvStockList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvStockList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStockList.Location = new System.Drawing.Point(-1, -2);
            this.dgvStockList.MultiSelect = false;
            this.dgvStockList.Name = "dgvStockList";
            this.dgvStockList.ReadOnly = true;
            this.dgvStockList.RowHeadersVisible = false;
            this.dgvStockList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.dgvStockList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStockList.Size = new System.Drawing.Size(426, 364);
            this.dgvStockList.TabIndex = 0;
            this.dgvStockList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStockList_CellDoubleClick);
            this.dgvStockList.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStockList_CellEnter);
            this.dgvStockList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvStockList_CellFormatting);
            this.dgvStockList.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgvStockList_KeyPress);
            // 
            // pbPicture
            // 
            this.pbPicture.Location = new System.Drawing.Point(41, 98);
            this.pbPicture.Name = "pbPicture";
            this.pbPicture.Size = new System.Drawing.Size(250, 250);
            this.pbPicture.TabIndex = 2;
            this.pbPicture.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.pbPicture);
            this.panel1.Location = new System.Drawing.Point(425, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(331, 416);
            this.panel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(-1, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(331, 53);
            this.panel2.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.txtSearch);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Location = new System.Drawing.Point(-1, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(427, 53);
            this.panel3.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(144, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 23);
            this.label4.TabIndex = 2;
            this.label4.Text = "Search:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(211, 12);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(201, 27);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.dgvStockList);
            this.panel4.Location = new System.Drawing.Point(0, 53);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(425, 360);
            this.panel4.TabIndex = 5;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.lblSearchStatus);
            this.panel5.Location = new System.Drawing.Point(-1, 414);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(758, 30);
            this.panel5.TabIndex = 6;
            // 
            // lblSearchStatus
            // 
            this.lblSearchStatus.AutoSize = true;
            this.lblSearchStatus.Location = new System.Drawing.Point(6, 5);
            this.lblSearchStatus.Name = "lblSearchStatus";
            this.lblSearchStatus.Size = new System.Drawing.Size(139, 20);
            this.lblSearchStatus.TabIndex = 0;
            this.lblSearchStatus.Text = "Showing all records";
            // 
            // SearchStockUI
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(755, 443);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SearchStockUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Search Stock";
            this.Load += new System.EventHandler(this.SearchStockUI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPicture)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblStockView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvStockList;
        private System.Windows.Forms.PictureBox pbPicture;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lblSearchStatus;
    }
}