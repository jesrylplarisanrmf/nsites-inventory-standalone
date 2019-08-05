using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Collections;
using MySql.Data.MySqlClient;

using NSites.Global;
using NSites.ApplicationObjects.Classes;
using System.IO;

namespace NSites.ApplicationObjects.UserInterfaces.MasterFile
{
    public partial class StockDetailUI : Form
    {
        #region "VARIABLES"
        string[] lRecords = new string[11];
        GlobalVariables.Operation lOperation;
        string lPicture;
        Stock loStock;
        Category loCategory;
        Unit loUnit;
        Brand loBrand;
        Supplier loSupplier;
        string lId;
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public StockDetailUI()
        {
            InitializeComponent();
            lOperation = GlobalVariables.Operation.Add;
            loStock = new Stock();
            lPicture = "";
            loCategory = new Category();
            loUnit = new Unit();
            loBrand = new Brand();
            loSupplier = new Supplier();
        }

        public StockDetailUI(string[] pRecords)
        {
            InitializeComponent();
            lOperation = GlobalVariables.Operation.Edit;
            loStock = new Stock();
            lPicture = "";
            loCategory = new Category();
            loUnit = new Unit();
            loBrand = new Brand();
            loSupplier = new Supplier();
            lRecords = pRecords;
        }
        #endregion "END OF VARIABLES"

        #region "PROPERTIES"
        public Form ParentList
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"

        #region "METHODS"
        private void clear()
        {
            lId = "";
            txtDescription.Clear();
            cboCategory.SelectedIndex = -1;
            cboUnit.SelectedIndex = -1;
            txtUnitCost.Text = "0.00";
            txtUnitPrice.Text = "0.00";
            txtReorderLevel.Text = "0.00";
            txtRemarks.Clear();
            txtDescription.Focus();
        }
        #endregion "END OF METHODS"

        private void StockDetailUI_Load(object sender, EventArgs e)
        {
            cboCategory.DataSource = loCategory.getAllData("ViewAll","");
            cboCategory.ValueMember = "Id";
            cboCategory.DisplayMember = "Description";
            cboCategory.SelectedIndex = -1;

            cboUnit.DataSource = loUnit.getAllData("ViewAll", "");
            cboUnit.ValueMember = "Id";
            cboUnit.DisplayMember = "Description";
            cboUnit.SelectedIndex = -1;

            cboBrand.DataSource = loBrand.getAllData("ViewAll", "");
            cboBrand.ValueMember = "Id";
            cboBrand.DisplayMember = "Description";
            cboBrand.SelectedIndex = -1;

            cboSupplier.DataSource = loSupplier.getAllData("ViewAll", "");
            cboSupplier.ValueMember = "Id";
            cboSupplier.DisplayMember = "Name";
            cboSupplier.SelectedIndex = -1;
            
            if (lOperation == GlobalVariables.Operation.Edit)
            {
                lId = lRecords[0];
                txtDescription.Text = lRecords[1];
                cboCategory.Text = lRecords[2];
                cboUnit.Text = lRecords[3];
                cboBrand.Text = lRecords[4];
                cboSupplier.Text = lRecords[5];
                try
                {
                    byte[] hextobyte = GlobalFunctions.HexToBytes(lRecords[6]);
                    pbPicture.BackgroundImage = GlobalFunctions.ConvertByteArrayToImage(hextobyte);
                    pbPicture.BackgroundImageLayout = ImageLayout.Stretch;
                }
                catch
                {
                    pbPicture.BackgroundImage = null;
                }

                txtUnitCost.Text = lRecords[7];
                txtUnitPrice.Text = lRecords[8];
                txtReorderLevel.Text = string.Format("{0:n}", decimal.Parse(lRecords[9]));
                txtRemarks.Text = lRecords[10];
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            loStock.Id = lId;
            loStock.Description = GlobalFunctions.replaceChar(txtDescription.Text);
            try
            {
                loStock.CategoryId = cboCategory.SelectedValue.ToString();
            }
            catch
            {
                loStock.CategoryId = "";
                /*
                MessageBoxUI _mb = new MessageBoxUI("You must select a correct Category!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                _mb.showDialog();
                cboCategory.Focus();
                return;
                */
            }
            try
            {
                loStock.UnitId = cboUnit.SelectedValue.ToString();
            }
            catch
            {
                loStock.UnitId = "";
                /*
                MessageBoxUI _mb = new MessageBoxUI("You must select a correct Unit!", GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                _mb.showDialog();
                cboUnit.Focus();
                return;
                */
            }
            try
            {
                loStock.BrandId = cboBrand.SelectedValue.ToString();
            }
            catch
            {
                loStock.BrandId = "";
            }
            try
            {
                loStock.SupplierId = cboSupplier.SelectedValue.ToString();
            }
            catch
            {
                loStock.SupplierId = "";
            }
            loStock.Picture = lPicture;
            loStock.UnitCost = decimal.Parse(txtUnitCost.Text);
            loStock.UnitPrice = decimal.Parse(txtUnitPrice.Text);
            loStock.ReorderLevel = decimal.Parse(txtReorderLevel.Text);
            loStock.Remarks = GlobalFunctions.replaceChar(txtRemarks.Text);

            MySqlTransaction _Trans = GlobalVariables.Connection.BeginTransaction();
            try
            {
                string _StockId = loStock.save(lOperation, ref _Trans);
                if (_StockId != "")
                {
                    _Trans.Commit();
                    MessageBoxUI _mb = new MessageBoxUI("Stock has been saved successfully!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    lRecords[0] = _StockId;
                    lRecords[1] = txtDescription.Text;
                    lRecords[2] = cboCategory.Text;
                    lRecords[3] = cboUnit.Text;
                    lRecords[4] = cboBrand.Text;
                    lRecords[5] = cboSupplier.Text;
                    lRecords[6] = lPicture;
                    lRecords[7] = decimal.Parse(txtUnitCost.Text).ToString();
                    lRecords[8] = decimal.Parse(txtUnitPrice.Text).ToString();
                    lRecords[9] = decimal.Parse(txtReorderLevel.Text).ToString();
                    lRecords[10] = txtRemarks.Text;

                    object[] _params = { lRecords };
                    if (lOperation == GlobalVariables.Operation.Edit)
                    {
                        try
                        {
                            ParentList.GetType().GetMethod("updateData").Invoke(ParentList, _params);
                        }
                        catch { }
                        this.Close();
                    }
                    else
                    {
                        ParentList.GetType().GetMethod("addData").Invoke(ParentList, _params);
                        clear();
                        //this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                _Trans.Rollback();
                if (ex.Message.Contains("Duplicate"))
                {
                    MessageBoxUI _mb = new MessageBoxUI("Stock Id already exist!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    return;
                }
            }
        }

        private void txtReorderLevel_Leave(object sender, EventArgs e)
        {
            try
            {
                txtReorderLevel.Text = string.Format("{0:n}", decimal.Parse(txtReorderLevel.Text));
            }
            catch
            {
                txtReorderLevel.Text = "0.00";
            }
        }

        private void txtUnitCost_Leave(object sender, EventArgs e)
        {
            try
            {
                txtUnitCost.Text = string.Format("{0:n}", decimal.Parse(txtUnitCost.Text));
            }
            catch
            {
                txtUnitCost.Text = "0.00";
            }
        }

        private void txtUnitPrice_Leave(object sender, EventArgs e)
        {
            try
            {
                txtUnitPrice.Text = string.Format("{0:n}", decimal.Parse(txtUnitPrice.Text));
            }
            catch
            {
                txtUnitPrice.Text = "0.00";
            }
        }

        private void pbPicture_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFD = new OpenFileDialog();
            openFD.InitialDirectory = ".../Main/ScreenSaverImages/";
            openFD.Title = "Insert an Image";
            openFD.Filter = "JPEG Images|*.jpg|GIF Images|*.gif|PNG Images|*.png";
            if (openFD.ShowDialog() == DialogResult.Cancel)
            {
                MessageBoxUI mb = new MessageBoxUI("Operation Cancelled", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                mb.ShowDialog();
            }
            else
            {
                string lChosenFile = openFD.FileName;
                string _FileName = openFD.SafeFileName;
                byte[] m_Bitmap = null;

                FileStream fs = new FileStream(lChosenFile, FileMode.Open);
                BinaryReader br = new BinaryReader(fs);
                int length = (int)br.BaseStream.Length;
                m_Bitmap = new byte[length];
                m_Bitmap = br.ReadBytes(length);
                br.Close();
                fs.Close();

                lPicture = GlobalFunctions.ToHex(m_Bitmap);

                pbPicture.BackgroundImage = GlobalFunctions.ConvertByteArrayToImage(m_Bitmap);
                pbPicture.BackgroundImageLayout = ImageLayout.Stretch;
            }
        }
    }
}
