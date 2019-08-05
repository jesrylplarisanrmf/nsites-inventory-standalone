using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NSites.ApplicationObjects.UserInterfaces.Transaction
{
    public partial class CancelReasonUI : Form
    {
        public bool lFromSave;
        public string lReason;

        public CancelReasonUI()
        {
            InitializeComponent();
            lFromSave = false;
            lReason = "";
        }

        private void CancelReasonUI_Load(object sender, EventArgs e)
        {
            lFromSave = false;
            lReason = "";
            txtReason.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            lFromSave = true;
            lReason = txtReason.Text;
            this.Close();
        }
    }
}
