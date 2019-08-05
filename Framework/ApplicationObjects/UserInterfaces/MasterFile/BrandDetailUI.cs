using MySql.Data.MySqlClient;
using NSites.ApplicationObjects.Classes;
using NSites.Global;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NSites.ApplicationObjects.UserInterfaces.MasterFile
{
    public partial class BrandDetailUI : Form
    {
        #region "VARIABLES"
        string[] lRecords = new string[3];
        GlobalVariables.Operation lOperation;
        Brand lBrand;
        string lId;
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public BrandDetailUI()
        {
            InitializeComponent();
            lOperation = GlobalVariables.Operation.Add;
            lBrand = new Brand();
        }

        public BrandDetailUI(string[] pRecords)
        {
            InitializeComponent();
            lOperation = GlobalVariables.Operation.Edit;
            lBrand = new Brand();
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
            txtRemarks.Clear();
            txtDescription.Focus();
        }
        #endregion "END OF METHODS"

        private void BrandDetailUI_Load(object sender, EventArgs e)
        {
            if (lOperation == GlobalVariables.Operation.Edit)
            {
                lId = lRecords[0];
                txtDescription.Text = lRecords[1];
                txtRemarks.Text = lRecords[2];
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            lBrand.Id = lId;
            lBrand.Description = GlobalFunctions.replaceChar(txtDescription.Text);
            lBrand.Remarks = txtRemarks.Text;

            MySqlTransaction _Trans = GlobalVariables.Connection.BeginTransaction();
            try
            {
                string _UnitId = lBrand.save(lOperation, ref _Trans);
                if (_UnitId != "")
                {
                    _Trans.Commit();
                    MessageBoxUI _mb = new MessageBoxUI("Brand has been saved successfully!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    lRecords[0] = _UnitId;
                    lRecords[1] = txtDescription.Text;
                    lRecords[2] = txtRemarks.Text;

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
                    MessageBoxUI _mb = new MessageBoxUI("Brand Id already exist!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    return;
                }
            }
        }
    }
}
