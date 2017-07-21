using System;
using System.Data;
using System.Windows.Forms;
using AdminDCZOEM.Properties;
using MySql.Data.MySqlClient;

namespace AdminDCZOEM.Config
{
    class CrudOp
    {

        #region Global Variables
        private static String _query;
        private static Boolean _flag;
        #endregion

        #region Create Data
        public static Boolean Create(String table, String fields, String values)
        {
            _flag = false;
            try
            {
                _query = "INSERT INTO " + table + " (" + fields + ") VALUES (" + values + ")";
                if (Conn.EjecutarConsulta(_query))
                    _flag = true;
                else
                    _flag = false;
            }
            catch (MySqlException)
            {
                MessageBox.Show(Resources.MsgRegistroFail, Resources.infoWindow, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return _flag;
        }
        #endregion

        #region Read All/Specific To DataGrid
        public static DataTable ReadData(String table, String fields = null, String condition = null)
        {
            DataTable dt = new DataTable();
            try
            {
                if (fields != null)
                {
                    if (condition != null)
                        _query = "SELECT " + fields + " FROM " + table + " WHERE " + condition;
                    else
                        _query = "SELECT " + fields + " FROM " + table;
                }
                else
                {
                    if (condition != null)
                        _query = "SELECT * FROM " + table + " WHERE " + condition;
                    else
                        _query = "SELECT * FROM " + table;
                }
                dt = Conn.TraerTabla(_query);
            }
            catch (MySqlException)
            {
                MessageBox.Show(Resources.MsgBuscarFail, Resources.infoWindow, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _query = null;
            }
            return dt;
        }
        #endregion

        #region Update Info
        public static Boolean Update(String table, String values, String idField, String idValue)
        {
            _flag = false;
            try
            {
                _query = "UPDATE " + table + " SET " + values + " WHERE " + idField + " = " + idValue;
                if (Conn.EjecutarConsulta(_query))
                    _flag = true;
                else
                    _flag = false;
            }
            catch (MySqlException)
            {
                MessageBox.Show(Resources.MsgActualizacionFail, Resources.infoWindow, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return _flag;
        }
        #endregion

        #region Delete Info
        public static Boolean Delete(String table, String idField, String idValue)
        {
            _flag = false;
            try
            {
                _query = "DELETE FROM " + table + " WHERE " + idField + " = " + idValue;
                if (Conn.EjecutarConsulta(_query))
                    _flag = true;
                else
                    _flag = false;
            }
            catch (MySqlException)
            {
                MessageBox.Show(Resources.MsgRelEliFail, Resources.infoWindow, MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
            }
            return _flag;
        }
        #endregion

    }
}
