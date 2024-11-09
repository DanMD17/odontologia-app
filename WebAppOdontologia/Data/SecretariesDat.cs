﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Data
{
    public class SecretariesDat
    {
        PersistenceDat objPer = new PersistenceDat();

        // Método para mostrar todas las secretarias
        public DataSet showSecretaries()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectSecretaries";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        // Método para insertar una nueva secretaria
        public bool saveSecretary(int _fkEmpId, string _funcion, string _aniosExperiencia)
        {
            bool executed = false;
            int row;
            MySqlCommand objInsertCmd = new MySqlCommand();
            objInsertCmd.Connection = objPer.openConnection();
            objInsertCmd.CommandText = "spInsertSecretary"; // Nombre del procedimiento almacenado
            objInsertCmd.CommandType = CommandType.StoredProcedure;

            objInsertCmd.Parameters.Add("p_emp_id", MySqlDbType.Int32).Value = _fkEmpId;
            objInsertCmd.Parameters.Add("p_sec_funcion", MySqlDbType.VarChar).Value = _funcion;
            objInsertCmd.Parameters.Add("p_sec_anios_experiencia", MySqlDbType.VarChar).Value = _aniosExperiencia;

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


        // Método para actualizar una secretaria
        public bool updateSecretaria(int _secId, int _fkEmpId, string _funcion, string _aniosExperiencia)
        {
            bool executed = false;
            int row;
            MySqlCommand objUpdateCmd = new MySqlCommand();
            objUpdateCmd.Connection = objPer.openConnection();
            objUpdateCmd.CommandText = "spUpdateSecretary"; // Nombre del procedimiento almacenado
            objUpdateCmd.CommandType = CommandType.StoredProcedure;

            objUpdateCmd.Parameters.Add("p_sec_id", MySqlDbType.Int32).Value = _secId;
            objUpdateCmd.Parameters.Add("p_emp_id", MySqlDbType.Int32).Value = _fkEmpId;
            objUpdateCmd.Parameters.Add("p_sec_funcion", MySqlDbType.VarChar).Value = _funcion;
            objUpdateCmd.Parameters.Add("p_sec_anios_experiencia", MySqlDbType.VarChar).Value = _aniosExperiencia;

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

        // Método para eliminar una secretaria
        public bool deleteSecretaria(int _secId)
        {
            bool executed = false;
            int row;
            MySqlCommand objDeleteCmd = new MySqlCommand();
            objDeleteCmd.Connection = objPer.openConnection();
            objDeleteCmd.CommandText = "spDeleteSecretary"; // Nombre del procedimiento almacenado
            objDeleteCmd.CommandType = CommandType.StoredProcedure;

            objDeleteCmd.Parameters.Add("p_sec_id", MySqlDbType.Int32).Value = _secId;

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