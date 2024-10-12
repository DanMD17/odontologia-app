using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Data
{
    public class DiagnosisDat
    {
        Persistence objPer = new Persistence();

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
        public bool saveDiagnosis(int p_cita_id, string p_diag_descripcion, DateTime p_diag_fecha, string p_diag_observaciones, int p_historial_id)
        {
            bool executed = false;
            int row;
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();

            objSelectCmd.CommandText = "spInsertDiagnosis"; // Nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            // Agregar parámetros al comando
            objSelectCmd.Parameters.Add("p_cita_id", MySqlDbType.Int32).Value = p_cita_id;
            objSelectCmd.Parameters.Add("p_diag_descripcion", MySqlDbType.Text).Value = p_diag_descripcion;
            objSelectCmd.Parameters.Add("p_diag_fecha", MySqlDbType.Date).Value = p_diag_fecha;
            objSelectCmd.Parameters.Add("p_diag_observaciones", MySqlDbType.Text).Value = p_diag_observaciones;
            objSelectCmd.Parameters.Add("p_historial_id", MySqlDbType.Int32).Value = p_historial_id;

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
        public bool updateDiagnosis(int p_diag_id, int p_cita_id, string p_diag_descripcion, DateTime p_diag_fecha, string p_diag_observaciones, int p_historial_id)
        {
            bool executed = false;
            int row;
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();

            objSelectCmd.CommandText = "spUpdateDiagnosis"; // Nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            // Agregar parámetros al comando
            objSelectCmd.Parameters.Add("p_diag_id", MySqlDbType.Int32).Value = p_diag_id;
            objSelectCmd.Parameters.Add("p_cita_id", MySqlDbType.Int32).Value = p_cita_id;
            objSelectCmd.Parameters.Add("p_diag_descripcion", MySqlDbType.Text).Value = p_diag_descripcion;
            objSelectCmd.Parameters.Add("p_diag_fecha", MySqlDbType.Date).Value = p_diag_fecha;
            objSelectCmd.Parameters.Add("p_diag_observaciones", MySqlDbType.Text).Value = p_diag_observaciones;
            objSelectCmd.Parameters.Add("p_historial_id", MySqlDbType.Int32).Value = p_historial_id;

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
        public bool deleteDiagnosis(int p_diag_id)
        {
            bool executed = false;
            int row;
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();

            objSelectCmd.CommandText = "spDeleteDiagnosis"; // Nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            objSelectCmd.Parameters.Add("p_diag_id", MySqlDbType.Int32).Value = p_diag_id;

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
