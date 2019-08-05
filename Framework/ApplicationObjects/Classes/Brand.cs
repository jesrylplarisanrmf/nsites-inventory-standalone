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
    class Brand
    {
        #region "VARIABLES"
        BrandDAO lBrandDAO;
        #endregion

        #region "CONSTRUCTORS"
        public Brand()
        {
            lBrandDAO = new BrandDAO();
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
            return lBrandDAO.getSampleTable();
        }
        public DataTable getAllData(string pDisplayType, string pSearchString)
        {
            return lBrandDAO.getBrands(pDisplayType, pSearchString);
        }

        public string save(GlobalVariables.Operation pOperation, ref MySqlTransaction pTrans)
        {
            string _Id = "";
            switch (pOperation)
            {
                case GlobalVariables.Operation.Add:
                    _Id = lBrandDAO.insertBrand(this, ref pTrans);
                    break;
                case GlobalVariables.Operation.Edit:
                    _Id = lBrandDAO.updateBrand(this, ref pTrans);
                    break;
            }
            return _Id;
        }

        public bool remove(string pId,ref MySqlTransaction pTrans)
        {
            return lBrandDAO.removeBrand(pId, ref pTrans);
        }
        #endregion
    }
}
