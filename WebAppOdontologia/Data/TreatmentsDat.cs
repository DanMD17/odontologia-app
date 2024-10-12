using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace Data
{
    public class TreatmentsDat
    {
        Persistence objPer = new Persistence();

        // Método para mostrar todos los tratamientos realizados
        public DataSet showTreatments()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectTratamientoRealizado";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        // Método para insertar un nuevo tratamiento realizado
        public bool saveTreatment(string nombre, string descripcion, DateTime fecha, string observaciones, int citaId, int histId, int auxId)
        {
            bool executed = false;
            int row;
            MySqlCommand objInsertCmd = new MySqlCommand();
            objInsertCmd.Connection = objPer.openConnection();
            objInsertCmd.CommandText = "spInsertTratamientoRealizado";
            objInsertCmd.CommandType = CommandType.StoredProcedure;

            objInsertCmd.Parameters.Add("p_trata_nombre", MySqlDbType.VarChar).Value = nombre;
            objInsertCmd.Parameters.Add("p_trata_descripcion", MySqlDbType.VarChar).Value = descripcion;
            objInsertCmd.Parameters.Add("p_trata_fecha", MySqlDbType.Date).Value = fecha;
            objInsertCmd.Parameters.Add("p_trata_observaciones", MySqlDbType.Text).Value = observaciones;
            objInsertCmd.Parameters.Add("p_cita_id", MySqlDbType.Int32).Value = citaId;
            objInsertCmd.Parameters.Add("p_hist_id", MySqlDbType.Int32).Value = histId;
            objInsertCmd.Parameters.Add("p_aux_id", MySqlDbType.Int32).Value = auxId;

            try
            {
                row = objInsertCmd.ExecuteNonQuery();
                executed = row == 1;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.ToString());
            }
            objPer.closeConnection();
            return executed;
        }

        // Método para actualizar un tratamiento realizado
        public bool updateTreatment(int trataId, string nombre, string descripcion, DateTime fecha, string observaciones)
        {
            bool executed = false;
            int row;
            MySqlCommand objUpdateCmd = new MySqlCommand();
            objUpdateCmd.Connection = objPer.openConnection();
            objUpdateCmd.CommandText = "spUpdateTratamientoRealizado";
            objUpdateCmd.CommandType = CommandType.StoredProcedure;

            objUpdateCmd.Parameters.Add("p_trata_id", MySqlDbType.Int32).Value = trataId;
            objUpdateCmd.Parameters.Add("p_trata_nombre", MySqlDbType.VarChar).Value = nombre;
            objUpdateCmd.Parameters.Add("p_trata_descripcion", MySqlDbType.VarChar).Value = descripcion;
            objUpdateCmd.Parameters.Add("p_trata_fecha", MySqlDbType.Date).Value = fecha;
            objUpdateCmd.Parameters.Add("p_trata_observaciones", MySqlDbType.Text).Value = observaciones;

            try
            {
                row = objUpdateCmd.ExecuteNonQuery();
                executed = row == 1;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.ToString());
            }
            objPer.closeConnection();
            return executed;
        }

        // Método para eliminar un tratamiento realizado
        public bool deleteTreatment(int trataId)
        {
            bool executed = false;
            int row;
            MySqlCommand objDeleteCmd = new MySqlCommand();
            objDeleteCmd.Connection = objPer.openConnection();
            objDeleteCmd.CommandText = "spDeleteTratamientoRealizado";
            objDeleteCmd.CommandType = CommandType.StoredProcedure;

            objDeleteCmd.Parameters.Add("p_trata_id", MySqlDbType.Int32).Value = trataId;

            try
            {
                row = objDeleteCmd.ExecuteNonQuery();
                executed = row == 1;
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
