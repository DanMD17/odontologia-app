using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Data
{
    public class MaterialsDat
    {
        PersistenceDat objPer = new PersistenceDat();

        //Metodo para mostrar todos los Usuarios
        public DataSet showMaterials()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectMaterial";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        // Método para guardar un nuevo Material
        public bool saveMaterial(string _nombre, string _descripcion, int _cantidad, int _fktrataId)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spInsertMaterial";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_mate_nombre", MySqlDbType.VarString).Value = _nombre;
            objSelectCmd.Parameters.Add("p_mate_descripcion", MySqlDbType.Text).Value = _descripcion;
            objSelectCmd.Parameters.Add("p_mate_cantidad", MySqlDbType.Int32).Value = _cantidad;
            objSelectCmd.Parameters.Add("p_trata_id", MySqlDbType.Int32).Value = _fktrataId;

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

        // Método para actualizar un Material
        public bool updateMaterial(int _materialId, string _nombre, string _descripcion, int _cantidad, int _fkTrataId)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spUpdateMaterials";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_mate_id", MySqlDbType.Int32).Value = _materialId;
            objSelectCmd.Parameters.Add("p_mate_nombre", MySqlDbType.VarString).Value = _nombre;
            objSelectCmd.Parameters.Add("p_mate_descripcion", MySqlDbType.Text).Value = _descripcion;
            objSelectCmd.Parameters.Add("p_mate_cantidad", MySqlDbType.Int32).Value = _cantidad;
            objSelectCmd.Parameters.Add("p_trata_id", MySqlDbType.Int32).Value = _fkTrataId;

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

        // Método para eliminar un Material
        public bool deleteMaterial(int _materialId)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spDeleteMaterial";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_mate_id", MySqlDbType.Int32).Value = _materialId;

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