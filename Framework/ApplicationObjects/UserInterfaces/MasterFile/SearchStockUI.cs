using NSites.ApplicationObjects.Classes;
using System;
using System.Windows.Forms;
using NSites.Global;
using System.Drawing;
using System.Linq;

namespace NSites.ApplicationObjects.UserInterfaces.MasterFile
{
    public partial class SearchStockUI : Form
    {
        #region "Variable"
        Stock loStocks = new Stock();
        public bool lFromSearch;
        public string lSearch;
        public int lCurrRow;
        #endregion
        public SearchStockUI()
        {
            InitializeComponent();
            lSearch = null;
            lCurrRow = 0;
            lFromSearch = false;
        }

        private void SearchStockUI_Load(object sender, EventArgs e)
        {
            dgvStockList.DataSource = loStocks.getAllData("ViewAll", "");
        }

        private void dgvStockList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
               if (this.dgvStockList.Columns[e.ColumnIndex].Name == "Id" ||
                   this.dgvStockList.Columns[e.ColumnIndex].Name == "Picture" ||
                   this.dgvStockList.Columns[e.ColumnIndex].Name == "Remarks")
                {
                    this.dgvStockList.Columns[e.ColumnIndex].Visible = false;
                }
            }
            catch { }
        }

        private void dgvStockList_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try { 
                string lPicture = dgvStockList.CurrentRow.Cells["Picture"].Value.ToString();
                byte[] hextobyte = GlobalFunctions.HexToBytes(lPicture);
                pbPicture.BackgroundImage = GlobalFunctions.ConvertByteArrayToImage(hextobyte);
                pbPicture.BackgroundImageLayout = ImageLayout.Stretch;
            }
            catch
            {
                pbPicture.BackgroundImage = null;
            }

            lCurrRow = dgvStockList.CurrentRow.Index;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            lSearch = txtSearch.Text;
            if (lSearch != "")
            {
                dgvStockList.DataSource = loStocks.getAllData("", lSearch);
                lblSearchStatus.Text = "Showing records for: " + lSearch;
            }
            else
            {
                SearchStockUI_Load(null, new EventArgs());
                lblSearchStatus.Text = "Showing all records";
                lSearch = "";
            }
        }

        private void dgvStockList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            lFromSearch = true;
            this.Close();
        }

        private void dgvStockList_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                lCurrRow--;
                dgvStockList_CellDoubleClick(null, new DataGridViewCellEventArgs(0, 0));
            }
        }
    }
}
