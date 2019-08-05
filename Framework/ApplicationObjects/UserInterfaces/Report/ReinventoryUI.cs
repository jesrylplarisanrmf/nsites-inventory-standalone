using MySql.Data.MySqlClient;
using NSites.ApplicationObjects.Classes;
using NSites.ApplicationObjects.UserInterfaces.Report.ReportRpt;
using NSites.ApplicationObjects.UserInterfaces.Transaction;
using NSites.Global;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NSites.ApplicationObjects.UserInterfaces.Report
{
    public partial class ReinventoryUI : Form
    {
        #region "VARIABLES"
        Stock loStock;
        DataTable ldtReports;
        ReinventoryRpt loReinventoryRpt;
        ReportViewerUI loReportViewer;
        #endregion "END OF VARIABLES"

        public ReinventoryUI()
        {
            InitializeComponent();
            loStock = new Stock();
            ldtReports = new DataTable();
            loReinventoryRpt = new ReinventoryRpt();
            loReportViewer = new ReportViewerUI();
        }

        #region "PROPERTIES"
        public Form ParentList
        {
            get;
            set;
        }
        #endregion "END OF PROPERTIES"

        private void ReinventoryUI_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            ParentList.GetType().GetMethod("closeTabPage").Invoke(ParentList, null);
        }

        private void viewAllRecordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlobalFunctions.refreshAll(ref dgvReport, ldtReports);
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            if (!GlobalFunctions.checkRights("tsmReinventoryReport", "Preview"))
            {
                return;
            }
            try
            {
                loReinventoryRpt.SetDataSource(GlobalVariables.DTCompanyLogo);
                loReinventoryRpt.Database.Tables[1].SetDataSource(ldtReports);
                loReinventoryRpt.SetParameterValue("CompanyName", GlobalVariables.CompanyName);
                loReinventoryRpt.SetParameterValue("CompanyAddress", GlobalVariables.CompanyAddress);
                loReinventoryRpt.SetParameterValue("CompanyContactNumber", GlobalVariables.ContactNumber);
                loReinventoryRpt.SetParameterValue("Username", GlobalVariables.Userfullname);
                loReinventoryRpt.SetParameterValue("Title", "Reinventory");
                loReinventoryRpt.SetParameterValue("SubTitle", "Reinventory");
                loReportViewer.crystalReportViewer.ReportSource = loReinventoryRpt;
                loReportViewer.ShowDialog();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void dgvReport_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point pt = dgvReport.PointToScreen(e.Location);
                cmsFunctions.Show(pt);
            }
        }

        private void dgvReport_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (this.dgvReport.Columns[e.ColumnIndex].Name == "Stock Id" || this.dgvReport.Columns[e.ColumnIndex].Name == "Unit")
                {
                    if (e.Value != null)
                    {
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                else if (this.dgvReport.Columns[e.ColumnIndex].Name == "Qty on Hand" || this.dgvReport.Columns[e.ColumnIndex].Name == "Variance" ||
                    this.dgvReport.Columns[e.ColumnIndex].Name == "Reorder Level")
                {
                    if (e.Value != null)
                    {
                        e.Value = String.Format("{0:n}", decimal.Parse(e.Value.ToString()));
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                }else if (this.dgvReport.Columns[e.ColumnIndex].Name == "Date Finalized" || this.dgvReport.Columns[e.ColumnIndex].Name == "Type" ||
                          this.dgvReport.Columns[e.ColumnIndex].Name == "Variance")
                {
                    this.dgvReport.Columns[e.ColumnIndex].Visible = false;
                }
            }
            catch { }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (!GlobalFunctions.checkRights("tsmReorderLevelReport", "Refresh"))
            {
                return;
            }
            try
            {
                dgvReport.Rows.Clear();
            }
            catch
            {
                dgvReport.DataSource = null;
            }

            try
            {
                ldtReports = loStock.getStockReinventory();
                GlobalFunctions.refreshGrid(ref dgvReport, ldtReports);
            }
            catch
            {

            }
        }

        private void btnFinalize_Click(object sender, EventArgs e)
        {
            //loStock.finalizeStockReinventory();
            if (!GlobalFunctions.checkRights("tsmStockReinventory", "Finalize"))
            {
                return;
            }
            try
            {
                if (dgvReport.Rows.Count > 0)
                {
                    DialogResult _dr = new DialogResult();
                    MessageBoxUI _mb = new MessageBoxUI("Are sure you want to continue finalizing this record?", GlobalVariables.Icons.QuestionMark, GlobalVariables.Buttons.YesNo);
                    _mb.ShowDialog();
                    _dr = _mb.Operation;
                    if (_dr == DialogResult.Yes)
                    {
                        MySqlTransaction Trans = GlobalVariables.Connection.BeginTransaction();
                        try
                        {
                            if (loStock.finalizeStockReinventory(dgvReport.CurrentRow.Cells[1].Value.ToString(), dgvReport.CurrentRow.Cells[0].Value.ToString(), ref Trans))
                            {
                                /* Probably Needed */
                                /*foreach (DataRow _dr1 in loInventoryDetail.getAllData(dgvList.CurrentRow.Cells[0].Value.ToString()).Rows)
                                {
                                    loInventoryDetail.updateQtyOnHand(_dr1[0].ToString(), ref Trans);
                                }*/

                                Trans.Commit();
                                MessageBoxUI _mb1 = new MessageBoxUI("Record has been successfully finalized!", GlobalVariables.Icons.Information, GlobalVariables.Buttons.OK);
                                _mb1.ShowDialog();

                                btnRefresh_Click(null, new EventArgs());
                            }
                        }
                        catch (Exception ex)
                        {
                            Trans.Rollback();
                            MessageBoxUI mb = new MessageBoxUI(ex, GlobalVariables.Icons.Error, GlobalVariables.Buttons.OK);
                            mb.ShowDialog();
                            return;
                        }
                    }
                }
            }
            catch { }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            new StockAdjustmentUI().triggerCreateClickFromOutside();
        }
    }
}
