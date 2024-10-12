using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace Data
{
    public class QuotesDat
    {
        Persistence objPer = new Persistence();

        // Método para mostrar todas las citas
        public DataSet showQuotes()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectCita";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        // Método para insertar una nueva cita
        public bool saveQuote(DateTime fecha, TimeSpan hora, string estado, int paciId, int odoId)
        {
            bool executed = false;
            int row;
            MySqlCommand objInsertCmd = new MySqlCommand();
            objInsertCmd.Connection = objPer.openConnection();
            objInsertCmd.CommandText = "spInsertCita"; // Nombre del procedimiento almacenado
            objInsertCmd.CommandType = CommandType.StoredProcedure;

            objInsertCmd.Parameters.Add("p_cita_fecha", MySqlDbType.Date).Value = fecha;
            objInsertCmd.Parameters.Add("p_cita_hora", MySqlDbType.Time).Value = hora;
            objInsertCmd.Parameters.Add("p_cita_estado", MySqlDbType.VarChar).Value = estado;
            objInsertCmd.Parameters.Add("p_paci_id", MySqlDbType.Int32).Value = paciId;
            objInsertCmd.Parameters.Add("p_odo_id", MySqlDbType.Int32).Value = odoId;

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

        // Método para actualizar una cita
        public bool updateQuote(int citaId, DateTime fecha, TimeSpan hora, string estado, int paciId, int odoId)
        {
            bool executed = false;
            int row;
            MySqlCommand objUpdateCmd = new MySqlCommand();
            objUpdateCmd.Connection = objPer.openConnection();
            objUpdateCmd.CommandText = "spUpdateCita"; // Nombre del procedimiento almacenado
            objUpdateCmd.CommandType = CommandType.StoredProcedure;

            objUpdateCmd.Parameters.Add("p_cita_id", MySqlDbType.Int32).Value = citaId;
            objUpdateCmd.Parameters.Add("p_cita_fecha", MySqlDbType.Date).Value = fecha;
            objUpdateCmd.Parameters.Add("p_cita_hora", MySqlDbType.Time).Value = hora;
            objUpdateCmd.Parameters.Add("p_cita_estado", MySqlDbType.VarChar).Value = estado;
            objUpdateCmd.Parameters.Add("p_paci_id", MySqlDbType.Int32).Value = paciId;
            objUpdateCmd.Parameters.Add("p_odo_id", MySqlDbType.Int32).Value = odoId;

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

        // Método para eliminar una cita
        public bool deleteQuote(int citaId)
        {
            bool executed = false;
            int row;
            MySqlCommand objDeleteCmd = new MySqlCommand();
            objDeleteCmd.Connection = objPer.openConnection();
            objDeleteCmd.CommandText = "spDeleteCita"; // Nombre del procedimiento almacenado
            objDeleteCmd.CommandType = CommandType.StoredProcedure;

            objDeleteCmd.Parameters.Add("p_cita_id", MySqlDbType.Int32).Value = citaId;

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
