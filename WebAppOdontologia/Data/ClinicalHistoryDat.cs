using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Data
{
    public class ClinicalHistoryDat
    {

        PersistenceDat objPer = new PersistenceDat();

        // Método para mostrar todos los historiales clínicos
        public DataSet showClinicalHistories()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectClinicalHistories"; // Procedimiento almacenado para seleccionar historiales
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }
        //Metodo para mostrar unicamente el id y la descripcion
        public DataSet showClinicalHistoriesDDL()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectClinicalHistoriesDDL";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        // Método para guardar un nuevo historial clínico
        public bool saveClinicalHistory(int _fkPacId, DateTime _pFechaCreacion, string _pDescripcionGeneral)
        {
            bool executed = false;
            int row;
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();

            objSelectCmd.CommandText = "spInsertClinicalHistory"; // Nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            objSelectCmd.Parameters.Add("p_hist_pac_id", MySqlDbType.Int32).Value = _fkPacId;
            objSelectCmd.Parameters.Add("p_hist_fecha_creacion", MySqlDbType.DateTime).Value = _pFechaCreacion;
            objSelectCmd.Parameters.Add("p_hist_descripcion_general", MySqlDbType.Text).Value = _pDescripcionGeneral;

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

        // Método para actualizar un historial clínico
        public bool updateClinicalHistory(int _pHistorialId, DateTime _pFechaCreacion, string _pDescripcionGeneral, int _fkPacId)
        {
            bool executed = false;
            int row;
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();

            objSelectCmd.CommandText = "spUpdateClinicalHistory"; // Nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            objSelectCmd.Parameters.Add("p_hist_historial_id", MySqlDbType.Int32).Value = _pHistorialId;
            objSelectCmd.Parameters.Add("p_hist_fecha_creacion", MySqlDbType.DateTime).Value = _pFechaCreacion;
            objSelectCmd.Parameters.Add("p_hist_descripcion_general", MySqlDbType.Text).Value = _pDescripcionGeneral;
            objSelectCmd.Parameters.Add("p_hist_pac_id", MySqlDbType.Int32).Value = _fkPacId;

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

        // Método para borrar un historial clínico
        public bool deleteClinicalHistory(int _pHistId)
        {
            bool executed = false;
            int row;
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();

            objSelectCmd.CommandText = "spDeleteClinicalHistory"; // Nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            objSelectCmd.Parameters.Add("p_hist_id", MySqlDbType.Int32).Value = _pHistId;

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
