using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Data
{
    public class PermisoRolDat
    {
        PersistenceDat objPer = new PersistenceDat();

        //Metodo para mostrar todos los Permisos Roles
        public DataSet showPermissionRol()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectPermisoRol";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }


        //Metodo para guardar un nuevo Permiso Rol
        public bool savePermissionRol(int _fkRol, int _fkPermiso, DateTime _date)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spInsertPermisoRol"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_rol_id", MySqlDbType.Int32).Value = _fkRol;
            objSelectCmd.Parameters.Add("p_permiso_id", MySqlDbType.Int32).Value = _fkPermiso;
            objSelectCmd.Parameters.Add("p_date", MySqlDbType.DateTime).Value = _date;

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
                Console.WriteLine("Error " + e.ToString());
            }
            objPer.closeConnection();
            return executed;
        }

        //Metodo para actualizar un nuevo Permiso Rol
        public bool updatePermissionRol(int _id, int _fkRol, int _fkPermiso, DateTime _date)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spUpdatePermisoRol"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_rol_permiso", MySqlDbType.Int32).Value = _id;
            objSelectCmd.Parameters.Add("p_fkrol", MySqlDbType.Int32).Value = _fkRol;
            objSelectCmd.Parameters.Add("p_fkpermiso", MySqlDbType.Int32).Value = _fkPermiso;
            objSelectCmd.Parameters.Add("p_date", MySqlDbType.DateTime).Value = _date;

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
                Console.WriteLine("Error " + e.ToString());
            }
            objPer.closeConnection();
            return executed;
        }
        //Metodo para borrar un Permiso Rol
        public bool deletePermissionRol(int _idPermissionRol)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spDeletePermisoRol"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_id", MySqlDbType.Int32).Value = _idPermissionRol;

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
                Console.WriteLine("Error " + e.ToString());
            }
            objPer.closeConnection();
            return executed;
        }
    }
}