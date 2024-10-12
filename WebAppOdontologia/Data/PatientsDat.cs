using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Data
{
    public class PatientsDat
    {

        Persistence objPer = new Persistence();

        // Método para mostrar todos los pacientes
        public DataSet showPatients()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectPatient"; // Procedimiento almacenado para seleccionar pacientes
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        // Método para guardar un nuevo paciente
        public bool savePatient(string p_nombre, string p_apellido, DateTime p_fecha_nacimiento, string p_direccion, string p_celular, string p_correo)
        {
            bool executed = false;
            int row;
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();

            objSelectCmd.CommandText = "spInsertPatient"; // Nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            // Agregar parámetros al comando
            objSelectCmd.Parameters.Add("p_nombre", MySqlDbType.VarChar).Value = p_nombre;
            objSelectCmd.Parameters.Add("p_apellido", MySqlDbType.VarChar).Value = p_apellido;
            objSelectCmd.Parameters.Add("p_fecha_nacimiento", MySqlDbType.Date).Value = p_fecha_nacimiento;
            objSelectCmd.Parameters.Add("p_direccion", MySqlDbType.VarChar).Value = p_direccion;
            objSelectCmd.Parameters.Add("p_celular", MySqlDbType.VarChar).Value = p_celular;
            objSelectCmd.Parameters.Add("p_correo", MySqlDbType.VarChar).Value = p_correo;

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

        // Método para actualizar un paciente
        public bool updatePatient(int p_paciente_id, string p_nombre, string p_apellido, DateTime p_fecha_nacimiento, string p_direccion, string p_celular, string p_correo)
        {
            bool executed = false;
            int row;
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();

            objSelectCmd.CommandText = "spUpdatePatient"; // Nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            // Agregar parámetros al comando
            objSelectCmd.Parameters.Add("p_paciente_id", MySqlDbType.Int32).Value = p_paciente_id;
            objSelectCmd.Parameters.Add("p_nombre", MySqlDbType.VarChar).Value = p_nombre;
            objSelectCmd.Parameters.Add("p_apellido", MySqlDbType.VarChar).Value = p_apellido;
            objSelectCmd.Parameters.Add("p_fecha_nacimiento", MySqlDbType.Date).Value = p_fecha_nacimiento;
            objSelectCmd.Parameters.Add("p_direccion", MySqlDbType.VarChar).Value = p_direccion;
            objSelectCmd.Parameters.Add("p_celular", MySqlDbType.VarChar).Value = p_celular;
            objSelectCmd.Parameters.Add("p_correo", MySqlDbType.VarChar).Value = p_correo;

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

        // Método para borrar un paciente
        public bool deletePatient(int p_paciente_id)
        {
            bool executed = false;
            int row;
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();

            objSelectCmd.CommandText = "spDeletePatient"; // Nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            objSelectCmd.Parameters.Add("p_paciente_id", MySqlDbType.Int32).Value = p_paciente_id;

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