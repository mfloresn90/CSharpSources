using System;
using System.Data;
using System.Windows.Forms;
using AdminDCZOEM.Properties;
using MySql.Data.MySqlClient;

namespace AdminDCZOEM.Config
{
    class Conn
    {

        static MySqlConnection _conex = new MySqlConnection();
        static MySqlDataAdapter _adaptador = new MySqlDataAdapter();
        static MySqlCommand _command = new MySqlCommand();
        static DataSet _daSet = null;
        static DataTable _daTab;
        static bool _seEjecuto;

        //metodo que carga datos a un DataTable
        public static DataTable TraerTabla(string consulta)
        {
            try
            {
                _conex = new MySqlConnection(Resources.ConexionBD);
                _adaptador = new MySqlDataAdapter(consulta, Resources.ConexionBD);
                _daTab = new DataTable();
                _adaptador.Fill(_daTab);
            }
            catch (MySqlException mExcep)
            {
                MessageBox.Show(mExcep.Message);
            }
            finally
            {
                Desconectar();
            }
            return _daTab;
        }

        //metodo que llama a un procedimiento o una consulta
        public static bool EjecutarConsulta(string consulta)
        {
            _seEjecuto = false;

            try
            {
                _conex = new MySqlConnection(Resources.ConexionBD);
                Conectar();
                _command = new MySqlCommand(consulta, _conex);
                _command.ExecuteNonQuery();
                _seEjecuto = true;
            }
            catch (MySqlException)
            {
                _seEjecuto = false;
            }
            finally
            {
                Desconectar();
            }

            return _seEjecuto;
        }

        //metodo que abre la conexion a la base de datos
        public static void Conectar()
        {
            try
            {
                _conex.Open();
            }
            catch (MySqlException oe)
            {
                MessageBox.Show(oe.Message);
            }
        }

        //metodo que cierra la conexion a base de datos
        public static void Desconectar()
        {
            try
            {
                _conex.Close();
            }
            catch (MySqlException oe)
            {
                MessageBox.Show(oe.Message);
            }
        }

    }
}
