using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Data
{
    public class UsersDat
    {
        Persistence objPer = new Persistence();
        //Metodo para mostrar todas los usuarios 


        public DataSet showUsers()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectUsers";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        // Método que retorna un objeto con el usuario encontrado por el correo
        public User showUsersMail(string mail)
        {
            User objUser = null;
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectUserMail";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_mail", MySqlDbType.VarString).Value = mail;
            MySqlDataReader reader = objSelectCmd.ExecuteReader();
            if (!reader.HasRows)
            {
                return objUser;
            }
            else
            {
                while (reader.Read())
                {
                    objUser = new User(reader["usu_correo"].ToString(),
                    reader["usu_contrasena"].ToString(), reader["usu_salt"].ToString(),
                    reader["usu_estado"].ToString(), reader["rol_nombre"].ToString(), Convert.ToInt32(reader["per_id"]));
                }
            }
            objPer.closeConnection();
            return objUser;
        }

        // Método para guardar un nuevo usuario
        public bool saveUser(string _mail, string _password, string _salt, string _state, DateTime _date, int _fkEmployee, int _fkRol)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spInsertUser"; // nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_usu_correo", MySqlDbType.VarString).Value = _mail;
            objSelectCmd.Parameters.Add("p_usu_contrasena", MySqlDbType.Text).Value = _password;
            objSelectCmd.Parameters.Add("p_salt", MySqlDbType.VarString).Value = _salt;
            objSelectCmd.Parameters.Add("p_usu_estado", MySqlDbType.VarString).Value = _state;
            objSelectCmd.Parameters.Add("p_usu_fecha_creacion", MySqlDbType.Date).Value = _date;
            objSelectCmd.Parameters.Add("p_usu_empleado_emp_id", MySqlDbType.Int32).Value = _fkEmployee;
            objSelectCmd.Parameters.Add("p_usu_roles_rol_id", MySqlDbType.Int32).Value = _fkRol;

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

        // Método para actualizar un Usuario
        public bool updateUser(int _id, string _mail, string _password, string _salt, string _state, int _fkEmployee, int _fkRol)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spUpdateUser"; // nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_usu_id", MySqlDbType.Int32).Value = _id;
            objSelectCmd.Parameters.Add("p_usu_correo", MySqlDbType.VarString).Value = _mail;
            objSelectCmd.Parameters.Add("p_usu_contrasena", MySqlDbType.Text).Value = _password;
            objSelectCmd.Parameters.Add("p_salt", MySqlDbType.VarString).Value = _salt;
            objSelectCmd.Parameters.Add("p_usu_estado", MySqlDbType.VarString).Value = _state;
            objSelectCmd.Parameters.Add("p_usu_empleado_emp_id", MySqlDbType.Int32).Value = _fkEmployee;
            objSelectCmd.Parameters.Add("p_usu_roles_rol_id", MySqlDbType.Int32).Value = _fkRol;

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

        // Método para eliminar un Usuario
        //public bool deleteUser(int _id)
        //{
        //    bool executed = false;
        //    int row;

        //    MySqlCommand objSelectCmd = new MySqlCommand();
        //    objSelectCmd.Connection = objPer.openConnection();
        //    objSelectCmd.CommandText = "spDeleteUser"; // nombre del procedimiento almacenado
        //    objSelectCmd.CommandType = CommandType.StoredProcedure;
        //    objSelectCmd.Parameters.Add("p_usu_id", MySqlDbType.Int32).Value = _id;

        //    try
        //    {
        //        row = objSelectCmd.ExecuteNonQuery();
        //        if (row == 1)
        //        {
        //            executed = true;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("Error: " + e.ToString());
        //    }
        //    objPer.closeConnection();
        //    return executed;
        //}

    }
}