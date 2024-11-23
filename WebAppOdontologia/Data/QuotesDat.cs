using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Data
{
    public class QuotesDat
    {
        PersistenceDat objPer = new PersistenceDat();

        // Método para mostrar todas las citas
        public DataSet showQuotes()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectQuotes";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }
        //Metodo para mostrar unicamente el id y la descripcion
        public DataSet showQuotesDDL()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectQuotesDDL";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        // Método para insertar una nueva cita
        public bool saveQuote(DateTime _fecha, TimeSpan _hora, string _estado, int _fkPaciId, int _fkOdoId)
        {
            bool executed = false;
            int row;
            MySqlCommand objInsertCmd = new MySqlCommand();
            objInsertCmd.Connection = objPer.openConnection();
            objInsertCmd.CommandText = "spInsertQuote"; // Nombre del procedimiento almacenado
            objInsertCmd.CommandType = CommandType.StoredProcedure;

            objInsertCmd.Parameters.Add("p_cita_fecha", MySqlDbType.Date).Value = _fecha;
            objInsertCmd.Parameters.Add("p_cita_hora", MySqlDbType.Time).Value = _hora;
            objInsertCmd.Parameters.Add("p_cita_estado", MySqlDbType.VarChar).Value = _estado;
            objInsertCmd.Parameters.Add("p_paci_id", MySqlDbType.Int32).Value = _fkPaciId;
            objInsertCmd.Parameters.Add("p_odo_id", MySqlDbType.Int32).Value = _fkOdoId;

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
        public bool updateQuote(int _citaId, DateTime _fecha, TimeSpan _hora, string _estado, int _fkPaciId, int _fkOdoId)
        {
            bool executed = false;
            int row;
            MySqlCommand objUpdateCmd = new MySqlCommand();
            objUpdateCmd.Connection = objPer.openConnection();
            objUpdateCmd.CommandText = "spUpdateQuote"; // Nombre del procedimiento almacenado
            objUpdateCmd.CommandType = CommandType.StoredProcedure;

            objUpdateCmd.Parameters.Add("p_cita_id", MySqlDbType.Int32).Value = _citaId;
            objUpdateCmd.Parameters.Add("p_cita_fecha", MySqlDbType.Date).Value = _fecha;
            objUpdateCmd.Parameters.Add("p_cita_hora", MySqlDbType.Time).Value = _hora;
            objUpdateCmd.Parameters.Add("p_cita_estado", MySqlDbType.VarChar).Value = _estado;
            objUpdateCmd.Parameters.Add("p_paci_id", MySqlDbType.Int32).Value = _fkPaciId;
            objUpdateCmd.Parameters.Add("p_odo_id", MySqlDbType.Int32).Value = _fkOdoId;

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
        public bool deleteQuote(int _citaId)
        {
            bool executed = false;
            int row;
            MySqlCommand objDeleteCmd = new MySqlCommand();
            objDeleteCmd.Connection = objPer.openConnection();
            objDeleteCmd.CommandText = "spDeleteQuote"; // Nombre del procedimiento almacenado
            objDeleteCmd.CommandType = CommandType.StoredProcedure;

            objDeleteCmd.Parameters.Add("p_cita_id", MySqlDbType.Int32).Value = _citaId;

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
