using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Data
{
    public class DiagnosisDat
    {
        PersistenceDat objPer = new PersistenceDat();

        // Método para mostrar todos los diagnósticos
        public DataSet showDiagnosis()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectDiagnosis"; // Procedimiento almacenado para seleccionar diagnósticos
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        // Método para guardar un nuevo diagnóstico
        public bool saveDiagnosis(int _fkCitaId, string _pDiagDescripcion, DateTime _pDiagFecha, string _pDiagObservaciones, int _fkHistorialId)
        {
            bool executed = false;
            int row;
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();

            objSelectCmd.CommandText = "spInsertDiagnosis"; // Nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            // Agregar parámetros al comando
            objSelectCmd.Parameters.Add("p_cita_id", MySqlDbType.Int32).Value = _fkCitaId;
            objSelectCmd.Parameters.Add("p_diag_descripcion", MySqlDbType.Text).Value = _pDiagDescripcion;
            objSelectCmd.Parameters.Add("p_diag_fecha", MySqlDbType.Date).Value = _pDiagFecha;
            objSelectCmd.Parameters.Add("p_diag_observaciones", MySqlDbType.Text).Value = _pDiagObservaciones;
            objSelectCmd.Parameters.Add("p_historial_id", MySqlDbType.Int32).Value = _fkHistorialId;

            try
            {
                row = objSelectCmd.ExecuteNonQuery();
                if (row == 1)
                {
                    executed = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.ToString());
            }
            objPer.closeConnection();
            return executed;
        }

        // Método para actualizar un diagnóstico
        public bool updateDiagnosis(int _pDiagId, int _fkCitaId, string _pDiagDescripcion, DateTime _pDiagFecha, string _pDiagObservaciones, int _fkHistorialId)
        {
            bool executed = false;
            int row;
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();

            objSelectCmd.CommandText = "spUpdateDiagnosis"; // Nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            // Agregar parámetros al comando
            objSelectCmd.Parameters.Add("p_diag_id", MySqlDbType.Int32).Value = _pDiagId;
            objSelectCmd.Parameters.Add("p_cita_id", MySqlDbType.Int32).Value = _fkCitaId;
            objSelectCmd.Parameters.Add("p_diag_descripcion", MySqlDbType.Text).Value = _pDiagDescripcion;
            objSelectCmd.Parameters.Add("p_diag_fecha", MySqlDbType.Date).Value = _pDiagFecha;
            objSelectCmd.Parameters.Add("p_diag_observaciones", MySqlDbType.Text).Value = _pDiagObservaciones;
            objSelectCmd.Parameters.Add("p_historial_id", MySqlDbType.Int32).Value = _fkHistorialId;

            try
            {
                row = objSelectCmd.ExecuteNonQuery();
                if (row == 1)
                {
                    executed = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.ToString());
            }
            objPer.closeConnection();
            return executed;
        }

        // Método para borrar un diagnóstico
        public bool deleteDiagnosis(int _pDiagId)
        {
            bool executed = false;
            int row;
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();

            objSelectCmd.CommandText = "spDeleteDiagnosis"; // Nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            objSelectCmd.Parameters.Add("p_diag_id", MySqlDbType.Int32).Value = _pDiagId;

            try
            {
                row = objSelectCmd.ExecuteNonQuery();
                if (row == 1)
                {
                    executed = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.ToString());
            }
            objPer.closeConnection();
            return executed;
        }
    }
}
