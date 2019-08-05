using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using System.Reflection;

using NSites.Global;

namespace NSites.ApplicationObjects.DataAccessObjects
{
    class StockDAO
    {
        #region "VARIABLES"
        string lId;
        string lDescription;
        string lCategoryId;
        string lUnitId;
        string lBrandId;
        string lSupplierId;
        string lPicture;
        decimal lUnitCost;
        decimal lUnitPrice;
        decimal lReorderLevel;
        string lRemarks;
        #endregion "END OF VARIABLES"

        #region "CONSTRUCTORS"
        public StockDAO()
        {
            lId = "";
            lDescription = "";
            lCategoryId = "";
            lUnitId = "";
            lBrandId = "";
            lSupplierId = "";
            lPicture = "";
            lUnitCost = 0;
            lUnitPrice = 0;
            lReorderLevel = 0;
            lRemarks = "";
        }
        #endregion "END OF CONSTRUCTORS"

        #region "METHODS"
        public void loadAttributes(object pObject)
        {
            try
            {
                lId = pObject.GetType().GetProperty("Id").GetValue(pObject, null).ToString();
            }
            catch
            { }
            lDescription = pObject.GetType().GetProperty("Description").GetValue(pObject, null).ToString();
            lCategoryId = pObject.GetType().GetProperty("CategoryId").GetValue(pObject, null).ToString();
            lUnitId = pObject.GetType().GetProperty("UnitId").GetValue(pObject, null).ToString();
            lBrandId = pObject.GetType().GetProperty("BrandId").GetValue(pObject, null).ToString();
            lSupplierId = pObject.GetType().GetProperty("SupplierId").GetValue(pObject, null).ToString();
            lPicture = pObject.GetType().GetProperty("Picture").GetValue(pObject, null).ToString();
            lUnitCost = decimal.Parse(pObject.GetType().GetProperty("UnitCost").GetValue(pObject, null).ToString());
            lUnitPrice = decimal.Parse(pObject.GetType().GetProperty("UnitPrice").GetValue(pObject, null).ToString());
            lReorderLevel = decimal.Parse(pObject.GetType().GetProperty("ReorderLevel").GetValue(pObject, null).ToString());
            lRemarks = pObject.GetType().GetProperty("Remarks").GetValue(pObject, null).ToString();
        }

        public DataTable getStocks(string pDisplayType, string pSearchString)
        {
            DataTable _dt = new DataTable();
            try
            {
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetStocks('" + pDisplayType + "','" + pSearchString + "')", GlobalVariables.Connection);
                _da.Fill(_dt);

                return _dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable getStock(string pId)
        {
            DataTable _dt = new DataTable();
            try
            {
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetStock(" + pId + ")", GlobalVariables.Connection);
                _da.Fill(_dt);

                return _dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable getStockReservation(string pId)
        {
            DataTable _dt = new DataTable();
            try
            {
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetStockReservation(" + pId + ")", GlobalVariables.Connection);
                _da.Fill(_dt);

                return _dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable getStockReorderLevel()
        {
            DataTable _dt = new DataTable();
            try
            {
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetStockReorderLevel()", GlobalVariables.Connection);
                _da.Fill(_dt);

                return _dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable getStockReinventory()
        {
            DataTable _dt = new DataTable();
            try
            {
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetStockReinventory()", GlobalVariables.Connection);
                _da.Fill(_dt);

                return _dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable getStockCard(DateTime pFromDate, DateTime pToDate,string pStockId)
        {
            DataTable _dt = new DataTable();
            try
            {
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetStockCard('" + string.Format("{0:yyyy-MM-dd}", pFromDate) + "','" + string.Format("{0:yyyy-MM-dd}", pToDate) + "'," + pStockId + ")", GlobalVariables.Connection);
                _da.Fill(_dt);

                return _dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable getStockCardBegBal(DateTime pFromDate, string pStockId)
        {
            DataTable _dt = new DataTable();
            try
            {
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetStockCardBegBal('" + string.Format("{0:yyyy-MM-dd}", pFromDate) + "'," + pStockId + ")", GlobalVariables.Connection);
                _da.Fill(_dt);

                return _dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable getStockQtyOnHand(string pLocationId, string pStockId)
        {
            DataTable _dt = new DataTable();
            try
            {
                MySqlDataAdapter _da = new MySqlDataAdapter("call spGetStockQtyOnHand('" + pLocationId + "'," + pStockId + ")", GlobalVariables.Connection);
                _da.Fill(_dt);

                return _dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string insertStock(object pObject, ref MySqlTransaction pTrans)
        {
            string _Id = "";
            try
            {
                loadAttributes(pObject);
                MySqlCommand _cmd = new MySqlCommand("call spInsertStock('" + lDescription + "','" +
                                                                               lCategoryId + "','" +
                                                                               lUnitId + "','" +
                                                                               lBrandId + "','" +
                                                                               lSupplierId + "','" +
                                                                               lPicture + "','" +
                                                                               lUnitCost + "','" +
                                                                               lUnitPrice + "','" +
                                                                               lReorderLevel + "','" +
                                                                               lRemarks + "','" +
                                                                               GlobalVariables.Username + "','" +
                                                                               GlobalVariables.Hostname + "')", GlobalVariables.Connection);
                try
                {
                    _cmd.Transaction = pTrans;
                    //_cmd.CommandText = "SET GLOBAL max_allowed_packet=32*1024*1024;";
                    _Id = _cmd.ExecuteNonQuery().ToString();

                    return _Id;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string updateStock(object pObject, ref MySqlTransaction pTrans)
        {
            string _Id = "";
            try
            {
                loadAttributes(pObject);
                MySqlCommand _cmd = new MySqlCommand("call spUpdateStock('" + lId + "','" +
                                                                            lDescription + "','" +
                                                                            lCategoryId + "','" +
                                                                            lUnitId + "','" +
                                                                            lBrandId + "','" +
                                                                            lSupplierId + "','" +
                                                                            lPicture + "','" +
                                                                            lUnitCost + "','" +
                                                                            lUnitPrice + "','" +
                                                                            lReorderLevel + "','" +
                                                                            lRemarks + "','" +
                                                                            GlobalVariables.Username + "','" +
                                                                            GlobalVariables.Hostname + "')", GlobalVariables.Connection);
                try
                {
                    _cmd.Transaction = pTrans;
                    _Id = _cmd.ExecuteScalar().ToString();

                    return _Id;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool finalizeStockReinventory(string pDetailId, string pType, ref MySqlTransaction pTrans)
        {
            bool _success = false;
            try
            {
                MySqlCommand _cmd = new MySqlCommand("call spFinalizeStockReinventory('" + pDetailId + "','" +
                                                                            pType + "','" +
                                                                            GlobalVariables.Username + "','" +
                                                                            GlobalVariables.Hostname + "')", GlobalVariables.Connection);
                try
                {
                    _cmd.Transaction = pTrans;
                    int _RowsAffected = _cmd.ExecuteNonQuery();
                    if (_RowsAffected > 0)
                    {
                        _success = true;
                    }
                    else
                    {
                        _success = false;
                    }
                    return _success;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool removeStock(string pId, ref MySqlTransaction pTrans)
        {
            bool _success = false;
            try
            {
                MySqlCommand _cmd = new MySqlCommand("call spRemoveStock('" + pId + "','" +
                                                                            GlobalVariables.Username + "','" +
                                                                            GlobalVariables.Hostname + "')", GlobalVariables.Connection);
                try
                {
                    _cmd.Transaction = pTrans;
                    int _RowsAffected = _cmd.ExecuteNonQuery();
                    if (_RowsAffected > 0)
                    {
                        _success = true;
                    }
                    else
                    {
                        _success = false;
                    }
                    return _success;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion "END OF METHODS"
    }
}
