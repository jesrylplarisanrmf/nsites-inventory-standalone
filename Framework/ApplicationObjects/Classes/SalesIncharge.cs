using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Reflection;
using MySql.Data.MySqlClient;

using NSites.Global;
using NSites.ApplicationObjects.DataAccessObjects;

namespace NSites.ApplicationObjects.Classes
{
    class SalesIncharge
    {
        #region "VARIABLES"
        SalesInchargeDAO lSalesInchargeDAO;
        #endregion

        #region "CONSTRUCTORS"
        public SalesIncharge()
        {
            lSalesInchargeDAO = new SalesInchargeDAO();
        }
        #endregion

        #region "PROPERTIES"
        public string Id
        {
            get;
            set;
        }
        public string Description
        {
            get;
            set;
        }
        public string Remarks
        {
            get;
            set;
        }
        #endregion

        #region "METHODS"
        public DataTable getSampleTable()
        {
            return lSalesInchargeDAO.getSampleTable();
        }
        public DataTable getAllData(string pDisplayType, string pSearchString)
        {
            return lSalesInchargeDAO.getSalesIncharges(pDisplayType, pSearchString);
        }

        public string save(GlobalVariables.Operation pOperation, ref MySqlTransaction pTrans)
        {
            string _Id = "";
            switch (pOperation)
            {
                case GlobalVariables.Operation.Add:
                    _Id = lSalesInchargeDAO.insertSalesIncharge(this, ref pTrans);
                    break;
                case GlobalVariables.Operation.Edit:
                    _Id = lSalesInchargeDAO.updateSalesIncharge(this, ref pTrans);
                    break;
            }
            return _Id;
        }

        public bool remove(string pId,ref MySqlTransaction pTrans)
        {
            return lSalesInchargeDAO.removeSalesIncharge(pId, ref pTrans);
        }
        #endregion
    }
}
