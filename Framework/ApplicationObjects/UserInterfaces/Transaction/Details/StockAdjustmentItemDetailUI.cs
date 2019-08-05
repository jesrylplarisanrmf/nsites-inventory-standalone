using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NSites.Global;
using NSites.ApplicationObjects.Classes;
namespace NSites.ApplicationObjects.UserInterfaces.Transaction.Details
{
    public partial class StockAdjustmentItemDetailUI : Form
    {
        Stock loStock;
        Location loLocation;
        LookUpValueUI loLookupValue;
        Brand loBrand;
        Supplier loSupplier;
        string[] lRecordData = new string[16];

        string lDetailId;
        string lStockId;
        string lStockDescription;
        string lUnit;
        string lBrand;
        string lSupplier;
        string lPicture;
        string lLocationId;
        decimal lQtyIN;
        decimal lQtyOUT;
        decimal lBalance;
        decimal lUnitPrice;
        decimal lTotalPrice;
        string lRemarks;
        string lOperator;
        string lQtyType;
        
        public StockAdjustmentItemDetailUI(string pQtyType)
        {
            InitializeComponent();
            loStock = new Stock();
            loLocation = new Location();
            loLookupValue = new LookUpValueUI();
            loBrand = new Brand();
            loSupplier = new Supplier();
            lDetailId = "";
            lQtyType = pQtyType;
            lOperator = "Add";
        }

        public StockAdjustmentItemDetailUI(string pDetailId, string pStockId, string pStockDescription, string pUnit, string pBrand, string pSupplier, string pPicture,
            string pLocationId, decimal pQtyIN,decimal pQtyOUT, decimal pBalance, decimal pUnitPrice, decimal pTotalPrice, string pRemarks, string pQtyType)
        {
            InitializeComponent();
            loStock = new Stock();
            loLocation = new Location();
            loLookupValue = new LookUpValueUI();
            lDetailId = pDetailId;
            lStockId = pStockId;
            lStockDescription = pStockDescription;
            lUnit = pUnit;
            lBrand = pBrand;
            lSupplier = pSupplier;
            lPicture = pPicture;
            lLocationId = pLocationId;
            lQtyIN = pQtyIN;
            lQtyOUT = pQtyOUT;
            lBalance = pBalance;
            lUnitPrice = pUnitPrice;
            lTotalPrice = pTotalPrice;
            lRemarks = pRemarks;
            lQtyType = pQtyType;
            lOperator = "Edit";
        }

        #region "PROPERTIES"
        public Form ParentList
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"

        private void clear()
        {
            cboStockDescription.SelectedIndex = -1;
            cboStockDescription.Text = "";
            txtUnit.Clear();
            txtBrand.Clear();
            txtSupplier.Clear();
            cboLocation.SelectedIndex = 0;
            txtQtyIN.Text = "0.00";
            txtQtyOUT.Text = "0.00";
            lblRunningBalance.Text = "0.00";
            txtBalance.Text = "0.00";
            txtUnitPrice.Text = "0.00";
            txtTotalPrice.Text = "0.00";
            txtRemarks.Clear();
            cboStockDescription.Focus();
            pbPicture.BackgroundImage = null;
        }

        private void computeTotalPrice()
        {
            try
            {
                decimal _qty = 0;
                if (decimal.Parse(txtQtyIN.Text) != 0)
                {
                    _qty = decimal.Parse(txtQtyIN.Text);
                }
                else
                {
                    _qty = decimal.Parse(txtQtyOUT.Text);
                }
                txtTotalPrice.Text = string.Format("{0:n}", _qty * decimal.Parse(txtUnitPrice.Text));
            }
            catch
            {
                txtTotalPrice.Text = "0.00";
            }
        }

        private void getQtyOnHand()
        {
            try
            {
                foreach (DataRow _dr in loStock.getStockQtyOnHand(cboLocation.SelectedValue.ToString(), cboStockDescription.SelectedValue.ToString()).Rows)
                {
                    lblRunningBalance.Text = string.Format("{0:n}", decimal.Parse(_dr[0].ToString()));
                }
            }
            catch
            {
                lblRunningBalance.Text = "0.00";
            }
            computeTotalQtyOnHand();
        }

        private void computeTotalQtyOnHand()
        {
            txtBalance.Text = string.Format("{0:n}", (decimal.Parse(lblRunningBalance.Text) + decimal.Parse(txtQtyIN.Text)) - decimal.Parse(txtQtyOUT.Text));
        }

        private void StockAdjustmentItemDetailUI_Load(object sender, EventArgs e)
        {
            cboStockDescription.DataSource = loStock.getAllData("ViewAll", "");
            cboStockDescription.DisplayMember = "Description";
            cboStockDescription.ValueMember = "Id";
            cboStockDescription.SelectedIndex = -1;

            cboLocation.DataSource = loLocation.getAllData("ViewAll", "");
            cboLocation.DisplayMember = "Description";
            cboLocation.ValueMember = "Id";
            cboLocation.SelectedIndex = 0;

            txtQtyIN.Enabled = false;
            txtQtyOUT.Enabled = false;

            if (lQtyType == "In")
            {
                txtQtyIN.Enabled = true;
                txtQtyIN.Text = "1.00";
                txtQtyOUT.Enabled = false;
                txtQtyOUT.Text = "0.00";
            }
            else if (lQtyType == "Out")
            {
                txtQtyIN.Enabled = false;
                txtQtyIN.Text = "0.00";
                txtQtyOUT.Enabled = true;
                txtQtyOUT.Text = "1.00";
            }
            else if (lQtyType == "In & Out")
            {
                txtQtyIN.Enabled = true;
                txtQtyOUT.Enabled = true;
            }

            if (lOperator == "Edit")
            {
                cboStockDescription.SelectedValue = lStockId;
                txtUnit.Text = lUnit;
                txtBrand.Text = lBrand;
                txtSupplier.Text = lSupplier;

                try
                {
                    byte[] hextobyte = GlobalFunctions.HexToBytes(lPicture);
                    pbPicture.BackgroundImage = GlobalFunctions.ConvertByteArrayToImage(hextobyte);
                    pbPicture.BackgroundImageLayout = ImageLayout.Stretch;
                }
                catch
                {
                    lPicture = "";
                    pbPicture.BackgroundImage = null;
                }

                cboLocation.SelectedValue = lLocationId;
                txtQtyIN.Text = string.Format("{0:n}", lQtyIN);
                txtQtyOUT.Text = string.Format("{0:n}", lQtyOUT);
                txtBalance.Text = string.Format("{0:n}", lBalance);
                txtUnitPrice.Text = string.Format("{0:n}", lUnitPrice);
                txtTotalPrice.Text = string.Format("{0:n}", lTotalPrice);
                txtRemarks.Text = lRemarks;
            }
            else
            {
                lblRunningBalance.Text = "0.00";
                txtBalance.Text = "0.00";
            }
        }

        private void cboStockDescription_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRow _dr in loStock.getStock(cboStockDescription.SelectedValue.ToString()).Rows)
                {
                    txtUnit.Text = _dr["Unit"].ToString();
                    txtBrand.Text = _dr["Brand"].ToString();
                    txtSupplier.Text = _dr["Supplier"].ToString();

                    try
                    {
                        lPicture = _dr["Picture"].ToString();
                        byte[] hextobyte = GlobalFunctions.HexToBytes(lPicture);
                        pbPicture.BackgroundImage = GlobalFunctions.ConvertByteArrayToImage(hextobyte);
                        pbPicture.BackgroundImageLayout = ImageLayout.Stretch;
                    }
                    catch
                    {
                        lPicture = "";
                        pbPicture.BackgroundImage = null;
                    }

                    txtUnitPrice.Text = string.Format("{0:n}", decimal.Parse(_dr["Unit Price"].ToString()));
                    computeTotalPrice();
                    getQtyOnHand();
                }
            }
            catch
            { }
        }

        private void txtQtyIN_Leave(object sender, EventArgs e)
        {
            try
            {
                txtQtyIN.Text = string.Format("{0:n}", decimal.Parse(txtQtyIN.Text));
            }
            catch 
            {
                txtQtyIN.Text = "0.00";
            }
        }

        private void txtQtyOUT_Leave(object sender, EventArgs e)
        {
            try
            {
                txtQtyOUT.Text = string.Format("{0:n}", decimal.Parse(txtQtyOUT.Text));
            }
            catch 
            {
                txtQtyOUT.Text = "0.00";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            /*if (decimal.Parse(txtQtyIN.Text) == 0 && decimal.Parse(txtQtyOUT.Text) == 0)
            {
                MessageBoxUI _mbStatus = new MessageBoxUI("Qty must not be Zero(0)!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                _mbStatus.ShowDialog();
                return;
            }*/

            if (decimal.Parse(txtQtyIN.Text) != 0 && decimal.Parse(txtQtyOUT.Text) != 0)
            {
                MessageBoxUI _mbStatus = new MessageBoxUI("Only 1 (one) Qty must not be Zero(0)!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                _mbStatus.ShowDialog();
                return;
            }  

            lRecordData[0] = lDetailId;
            try
            {
                lRecordData[1] = cboStockDescription.SelectedValue.ToString();
            }
            catch
            {
                MessageBoxUI _mbStatus = new MessageBoxUI("You must select a correct Stock!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                _mbStatus.ShowDialog();
                cboStockDescription.Focus();
                return;
            }
            lRecordData[2] = cboStockDescription.Text;
            lRecordData[3] = txtUnit.Text;
            lRecordData[4] = txtBrand.Text;
            lRecordData[5] = txtSupplier.Text;
            lRecordData[6] = lPicture;
            lRecordData[7] = cboLocation.SelectedValue.ToString();
            lRecordData[8] = cboLocation.Text;
            lRecordData[9] = string.Format("{0:n}", decimal.Parse(txtQtyIN.Text));
            lRecordData[10] = string.Format("{0:n}", decimal.Parse(txtQtyOUT.Text));
            lRecordData[11] = string.Format("{0:n}", decimal.Parse(txtBalance.Text));
            lRecordData[12] = string.Format("{0:n}", decimal.Parse(txtUnitPrice.Text));
            lRecordData[13] = string.Format("{0:n}", decimal.Parse(txtTotalPrice.Text));
            lRecordData[14] = GlobalFunctions.replaceChar(txtRemarks.Text);

            object[] _params = { lRecordData };
            if (lOperator == "Add")
            {
                lRecordData[15] = "Add";
                ParentList.GetType().GetMethod("addData").Invoke(ParentList, _params);
                MessageBoxUI _mbStatus = new MessageBoxUI("Successfully added!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                _mbStatus.ShowDialog();
                clear();
            }
            else if (lOperator == "Edit")
            {
                lRecordData[15] = "Edit";
                ParentList.GetType().GetMethod("updateData").Invoke(ParentList, _params);
                Close();
            }
        }

        private void txtQtyIN_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (decimal.Parse(txtQtyIN.Text) != 0)
                {
                    txtQtyOUT.Enabled = false;
                }
                else
                {
                    txtQtyOUT.Enabled = true;
                }
            }
            catch 
            {
                txtQtyOUT.Enabled = true;
            }
            computeTotalPrice();
            computeTotalQtyOnHand();
        }

        private void txtQtyOUT_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (decimal.Parse(txtQtyOUT.Text) != 0)
                {
                    txtQtyIN.Enabled = false;
                }
                else
                {
                    txtQtyIN.Enabled = true;
                }
            }
            catch
            {
                txtQtyIN.Enabled = true;
            }
            computeTotalPrice();
            computeTotalQtyOnHand();
        }

        private void cboLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            getQtyOnHand();
            computeTotalQtyOnHand();
        }

        private void btnLookUpStock_Click(object sender, EventArgs e)
        {
            loLookupValue.lObject = loStock;
            loLookupValue.lType = typeof(Stock);
            loLookupValue.lTableName = "Stock";
            loLookupValue.ShowDialog();
            if (loLookupValue.lFromSelection)
            {
                cboStockDescription.SelectedValue = loLookupValue.lCode;
            }
        }
    }
}
