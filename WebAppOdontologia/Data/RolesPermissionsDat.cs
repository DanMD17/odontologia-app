using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Data
{
    public class Roles_PermissionDat
    {
        PersistenceDat objPer = new PersistenceDat();

        //Metodo para mostrar Todos los roles y permisos
        public DataSet showRolesPermissions()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectRolesPermissions"; // Nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;


            // Asignar el comando al adaptador
            objAdapter.SelectCommand = objSelectCmd;

            try
            {
                // Rellenar el DataSet con los resultados de la consulta
                objAdapter.Fill(objData);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.ToString());
            }
            finally
            {
                // Asegurarse de cerrar la conexión
                objPer.closeConnection();
            }

            return objData;
        }
        

        //Metodo para guardar rol y permiso
        public bool saveRolePermission(int _rol_id, int _permiso_id)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spInsertRolePermission"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_rol_id", MySqlDbType.Int32).Value = _rol_id;
            objSelectCmd.Parameters.Add("p_permiso_id", MySqlDbType.Int32).Value = _permiso_id;


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

        //Metodo para actualizar rol y permiso
        public bool updateRolePermission(int _o_rol_id, int _o_permiso_id, int _n_rol_id,
            int _n_permiso_id)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spUpdateRolePermission"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("old_rol_id", MySqlDbType.Int32).Value = _o_rol_id;
            objSelectCmd.Parameters.Add("old_permiso_id", MySqlDbType.Int32).Value = _o_permiso_id;
            objSelectCmd.Parameters.Add("new_rol_id", MySqlDbType.Int32).Value = _n_rol_id;
            objSelectCmd.Parameters.Add("new_permiso_id", MySqlDbType.Int32).Value = _n_permiso_id;

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

        //Metodo para borrar un Rol-Permiso
        public bool deleteRolePermision(int _rol_id, int _permiso_id)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spDeleteRolePermission"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_rol_id", MySqlDbType.Int32).Value = _rol_id;
            objSelectCmd.Parameters.Add("p_permiso_id", MySqlDbType.Int32).Value = _permiso_id;

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
