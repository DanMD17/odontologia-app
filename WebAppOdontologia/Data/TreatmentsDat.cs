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
            objSelectCmd.CommandText = "spSelectTreatmentsPerformed";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        // Método para insertar un nuevo tratamiento realizado
        public bool saveTreatment(string _nombre, string _descripcion, DateTime _fecha, string _observaciones, int _fkCitaId, int _fkHistId, int _fkAuxId)
        {
            bool executed = false;
            int row;
            MySqlCommand objInsertCmd = new MySqlCommand();
            objInsertCmd.Connection = objPer.openConnection();
            objInsertCmd.CommandText = "spInserTreatmentPerformed";
            objInsertCmd.CommandType = CommandType.StoredProcedure;

            objInsertCmd.Parameters.Add("p_trata_nombre", MySqlDbType.VarChar).Value = _nombre;
            objInsertCmd.Parameters.Add("p_trata_descripcion", MySqlDbType.VarChar).Value = _descripcion;
            objInsertCmd.Parameters.Add("p_trata_fecha", MySqlDbType.Date).Value = _fecha;
            objInsertCmd.Parameters.Add("p_trata_observaciones", MySqlDbType.Text).Value = _observaciones;
            objInsertCmd.Parameters.Add("p_cita_id", MySqlDbType.Int32).Value = _fkCitaId;
            objInsertCmd.Parameters.Add("p_hist_id", MySqlDbType.Int32).Value = _fkHistId;
            objInsertCmd.Parameters.Add("p_aux_id", MySqlDbType.Int32).Value = _fkAuxId;

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
        public bool updateTreatment(int _trataId, string _nombre, string _descripcion, DateTime _fecha, string _observaciones, int _fkCitaId, int _fkHistId, int _fkAuxId)

        {
            bool executed = false;
            int row;
            MySqlCommand objUpdateCmd = new MySqlCommand();
            objUpdateCmd.Connection = objPer.openConnection();
            objUpdateCmd.CommandText = "spUpdateTreatmentPerformed";
            objUpdateCmd.CommandType = CommandType.StoredProcedure;

            objUpdateCmd.Parameters.Add("p_trata_id", MySqlDbType.Int32).Value = _trataId;
            objUpdateCmd.Parameters.Add("p_trata_nombre", MySqlDbType.VarChar).Value = _nombre;
            objUpdateCmd.Parameters.Add("p_trata_descripcion", MySqlDbType.VarChar).Value = _descripcion;
            objUpdateCmd.Parameters.Add("p_trata_fecha", MySqlDbType.Date).Value = _fecha;
            objUpdateCmd.Parameters.Add("p_trata_observaciones", MySqlDbType.Text).Value = _observaciones;

            objUpdateCmd.Parameters.Add("p_trata_cita_id", MySqlDbType.Text).Value = _fkCitaId;
            objUpdateCmd.Parameters.Add("p_trata_hist_id", MySqlDbType.Text).Value = _fkHistId;
            objUpdateCmd.Parameters.Add("p_trata_aux_id", MySqlDbType.Text).Value = _fkAuxId;

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
        public bool deleteTreatment(int _trataId)
        {
            bool executed = false;
            int row;
            MySqlCommand objDeleteCmd = new MySqlCommand();
            objDeleteCmd.Connection = objPer.openConnection();
            objDeleteCmd.CommandText = "spDeleteTreatmentPerformed";
            objDeleteCmd.CommandType = CommandType.StoredProcedure;

            objDeleteCmd.Parameters.Add("p_trata_id", MySqlDbType.Int32).Value = _trataId;

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
