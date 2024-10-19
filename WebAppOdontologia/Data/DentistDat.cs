using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace Data
{
    public class DentistDat
    {
        Persistence objPer = new Persistence();

        // Método para mostrar todos los odontólogos
        public DataSet showDentists()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectOdontologo";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        // Método para insertar un nuevo odontólogo
        public bool saveDentist(string especialidad, int empId)
        {
            bool executed = false;
            int row;
            MySqlCommand objInsertCmd = new MySqlCommand();
            objInsertCmd.Connection = objPer.openConnection();
            objInsertCmd.CommandText = "spInsertOdontologo"; // Nombre del procedimiento almacenado
            objInsertCmd.CommandType = CommandType.StoredProcedure;

            objInsertCmd.Parameters.Add("p_odo_especialidad", MySqlDbType.VarChar).Value = especialidad;
            objInsertCmd.Parameters.Add("p_emp_id", MySqlDbType.Int32).Value = empId;

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

        // Método para actualizar un odontólogo
        public bool updateDentist(int odoId, string especialidad, int empId)
        {
            bool executed = false;
            int row;
            MySqlCommand objUpdateCmd = new MySqlCommand();
            objUpdateCmd.Connection = objPer.openConnection();
            objUpdateCmd.CommandText = "spUpdateOdontologo"; // Nombre del procedimiento almacenado
            objUpdateCmd.CommandType = CommandType.StoredProcedure;

            objUpdateCmd.Parameters.Add("p_odo_id", MySqlDbType.Int32).Value = odoId;
            objUpdateCmd.Parameters.Add("p_odo_especialidad", MySqlDbType.VarChar).Value = especialidad;
            objUpdateCmd.Parameters.Add("p_emp_id", MySqlDbType.Int32).Value = empId;

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

        // Método para eliminar un odontólogo
        public bool deleteDentist(int odoId)
        {
            bool executed = false;
            int row;
            MySqlCommand objDeleteCmd = new MySqlCommand();
            objDeleteCmd.Connection = objPer.openConnection();
            objDeleteCmd.CommandText = "spDeleteOdontologo"; // Nombre del procedimiento almacenado
            objDeleteCmd.CommandType = CommandType.StoredProcedure;

            objDeleteCmd.Parameters.Add("p_odo_id", MySqlDbType.Int32).Value = odoId;

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
