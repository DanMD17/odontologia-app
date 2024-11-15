using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Data
{
    public class PatientsDat
    {

        PersistenceDat objPer = new PersistenceDat();

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
        public DataSet showPatientsDDL()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectPatientsDDL"; // Procedimiento almacenado para seleccionar solo ID y nombre
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        // Método para guardar un nuevo paciente
        public bool savePatient(string _pNombre, string _pApellido, DateTime _pFechaNacimiento, string _pDireccion, string _pCelular, string _pCorreo)
        {
            bool executed = false;
            int row;
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();

            objSelectCmd.CommandText = "spInsertPatient"; // Nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            // Agregar parámetros al comando
            objSelectCmd.Parameters.Add("p_paci_nombre", MySqlDbType.VarChar).Value = _pNombre;
            objSelectCmd.Parameters.Add("p_paci_apellido", MySqlDbType.VarChar).Value = _pApellido;
            objSelectCmd.Parameters.Add("p_paci_fecha_nacimiento", MySqlDbType.Date).Value = _pFechaNacimiento;
            objSelectCmd.Parameters.Add("p_paci_direccion", MySqlDbType.VarChar).Value = _pDireccion;
            objSelectCmd.Parameters.Add("p_paci_celular", MySqlDbType.VarChar).Value = _pCelular;
            objSelectCmd.Parameters.Add("p_paci_correo", MySqlDbType.VarChar).Value = _pCorreo;

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
        public bool updatePatient(int _pPacienteId, string _pNombre, string _pApellido, DateTime _pFechaNacimiento, string _pDireccion, string _pCelular, string _pCorreo)
        {
            bool executed = false;
            int row;
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();

            objSelectCmd.CommandText = "spUpdatePatient"; // Nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            // Agregar parámetros al comando
            objSelectCmd.Parameters.Add("p_paciente_id", MySqlDbType.Int32).Value = _pPacienteId;
            objSelectCmd.Parameters.Add("p_paci_nombre", MySqlDbType.VarChar).Value = _pNombre;
            objSelectCmd.Parameters.Add("p_paci_apellido", MySqlDbType.VarChar).Value = _pApellido;
            objSelectCmd.Parameters.Add("p_paci_fecha_nacimiento", MySqlDbType.Date).Value = _pFechaNacimiento;
            objSelectCmd.Parameters.Add("p_paci_direccion", MySqlDbType.VarChar).Value = _pDireccion;
            objSelectCmd.Parameters.Add("p_paci_celular", MySqlDbType.VarChar).Value = _pCelular;
            objSelectCmd.Parameters.Add("p_paci_correo", MySqlDbType.VarChar).Value = _pCorreo;

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
        public bool deletePatient(int _pPacienteId)
        {
            bool executed = false;
            int row;
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();

            objSelectCmd.CommandText = "spDeletePatient"; // Nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            objSelectCmd.Parameters.Add("p_paciente_id", MySqlDbType.Int32).Value = _pPacienteId;

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