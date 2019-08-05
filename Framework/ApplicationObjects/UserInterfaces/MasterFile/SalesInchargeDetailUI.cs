using MySql.Data.MySqlClient;
using NSites.ApplicationObjects.Classes;
using NSites.Global;
using System;
using System.Windows.Forms;

namespace NSites.ApplicationObjects.UserInterfaces.MasterFile
{
    public partial class SalesInchargeDetailUI : Form
    {
        #region "VARIABLES"
        string[] lRecords = new string[3];
        GlobalVariables.Operation lOperation;
        SalesIncharge lSalesIncharge;
        string lId;
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public SalesInchargeDetailUI()
        {
            InitializeComponent();
            lOperation = GlobalVariables.Operation.Add;
            lSalesIncharge = new SalesIncharge();
        }

        public SalesInchargeDetailUI(string[] pRecords)
        {
            InitializeComponent();
            lOperation = GlobalVariables.Operation.Edit;
            lSalesIncharge = new SalesIncharge();
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

        private void SalesInchargeDetailUI_Load(object sender, EventArgs e)
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
            lSalesIncharge.Id = lId;
            lSalesIncharge.Description = GlobalFunctions.replaceChar(txtDescription.Text);
            lSalesIncharge.Remarks = txtRemarks.Text;

            MySqlTransaction _Trans = GlobalVariables.Connection.BeginTransaction();
            try
            {
                string _SalesInchargeId = lSalesIncharge.save(lOperation, ref _Trans);
                if (_SalesInchargeId != "")
                {
                    _Trans.Commit();
                    MessageBoxUI _mb = new MessageBoxUI("Sales Incharge has been saved successfully!", GlobalVariables.Icons.Save, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    lRecords[0] = _SalesInchargeId;
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
                    MessageBoxUI _mb = new MessageBoxUI("Sales Incharge Id already exist!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                    _mb.showDialog();
                    return;
                }
            }
        }
    }
}
