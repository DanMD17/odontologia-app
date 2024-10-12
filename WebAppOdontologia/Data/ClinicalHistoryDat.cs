using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Data
{
    public class ClinicalHistoryDat
    {
       
            Persistence objPer = new Persistence();

            // Método para mostrar todos los historiales clínicos
            public DataSet showClinicalHistories()
            {
                MySqlDataAdapter objAdapter = new MySqlDataAdapter();
                DataSet objData = new DataSet();
                MySqlCommand objSelectCmd = new MySqlCommand();
                objSelectCmd.Connection = objPer.openConnection();
                objSelectCmd.CommandText = "spSelectClinicalHistory"; // Procedimiento almacenado para seleccionar historiales
                objSelectCmd.CommandType = CommandType.StoredProcedure;
                objAdapter.SelectCommand = objSelectCmd;
                objAdapter.Fill(objData);
                objPer.closeConnection();
                return objData;
            }

            // Método para guardar un nuevo historial clínico
            public bool saveClinicalHistory(int p_pac_id, DateTime p_fecha_creacion, string p_descripcion_general)
            {
                bool executed = false;
                int row;
                MySqlCommand objSelectCmd = new MySqlCommand();
                objSelectCmd.Connection = objPer.openConnection();

                objSelectCmd.CommandText = "spInsertClinicalHistory"; // Nombre del procedimiento almacenado
                objSelectCmd.CommandType = CommandType.StoredProcedure;

                objSelectCmd.Parameters.Add("p_pac_id", MySqlDbType.Int32).Value = p_pac_id;
                objSelectCmd.Parameters.Add("p_fecha_creacion", MySqlDbType.DateTime).Value = p_fecha_creacion;
                objSelectCmd.Parameters.Add("p_descripcion_general", MySqlDbType.Text).Value = p_descripcion_general;

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
            public bool updateClinicalHistory(int p_historial_id, DateTime p_fecha_creacion, string p_descripcion_general)
            {
                bool executed = false;
                int row;
                MySqlCommand objSelectCmd = new MySqlCommand();
                objSelectCmd.Connection = objPer.openConnection();

                objSelectCmd.CommandText = "spUpdateClinicalHistory"; // Nombre del procedimiento almacenado
                objSelectCmd.CommandType = CommandType.StoredProcedure;

                objSelectCmd.Parameters.Add("p_historial_id", MySqlDbType.Int32).Value = p_historial_id;
                objSelectCmd.Parameters.Add("p_fecha_creacion", MySqlDbType.DateTime).Value = p_fecha_creacion;
                objSelectCmd.Parameters.Add("p_descripcion_general", MySqlDbType.Text).Value = p_descripcion_general;

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
            public bool deleteClinicalHistory(int p_hist_id)
            {
                bool executed = false;
                int row;
                MySqlCommand objSelectCmd = new MySqlCommand();
                objSelectCmd.Connection = objPer.openConnection();

                objSelectCmd.CommandText = "spDeleteClinicalHistory"; // Nombre del procedimiento almacenado
                objSelectCmd.CommandType = CommandType.StoredProcedure;

                objSelectCmd.Parameters.Add("p_hist_id", MySqlDbType.Int32).Value = p_hist_id;

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
