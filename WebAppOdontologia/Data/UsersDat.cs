using Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Data
{
    public class UsersDat
    {
        PersistenceDat objPer = new PersistenceDat();
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
        // public User showUsersMail(string mail)
        // {
        //     User objUser = null;
        //     MySqlCommand objSelectCmd = new MySqlCommand();
        //     objSelectCmd.Connection = objPer.openConnection();
        //     objSelectCmd.CommandText = "spSelectUserMail";
        //     objSelectCmd.CommandType = CommandType.StoredProcedure;
        //     objSelectCmd.Parameters.Add("p_mail", MySqlDbType.VarString).Value = mail;
        //     MySqlDataReader reader = objSelectCmd.ExecuteReader();
        //     if (!reader.HasRows)
        //     {
        //         return objUser;
        //     }
        //    else
        //    {
        //        while (reader.Read())
        //       {
        //            objUser = new User(reader["usu_correo"].ToString(),
        //            reader["usu_contrasena"].ToString(), reader["usu_salt"].ToString(),
        //            reader["usu_estado"].ToString(), reader["rol_nombre"].ToString(), Convert.ToInt32(reader["per_id"]));
        //        }
        //   }
        //   objPer.closeConnection();
        //   return objUser;
        // }

        // Metodo modificado que retorna un objeto con el usuario encontrado por el correo
        public User showUsersMail(string mail)
        {
            User objUser = null;
            List<Permissions> permisos = new List<Permissions>();

            using (MySqlCommand objSelectCmd = new MySqlCommand())
            {
                objSelectCmd.Connection = objPer.openConnection();
                objSelectCmd.CommandText = "spSelectUserMail";
                objSelectCmd.CommandType = CommandType.StoredProcedure;
                objSelectCmd.Parameters.Add("p_mail", MySqlDbType.VarChar).Value = mail;

                using (MySqlDataReader reader = objSelectCmd.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        return objUser;
                    }

                    while (reader.Read())
                    {
                        // Inicializar User si es nulo (se hace una vez)
                        if (objUser == null)
                        {
                            Roles userRol = new Roles(
                                id: Convert.ToInt32(reader["rol_id"]),
                                nombre: reader["rol_nombre"].ToString(),
                                descripcion: reader["rol_descripcion"].ToString()
                            );

                            objUser = new User(
                                correo: reader["usu_correo"].ToString(),
                                contrasena: reader["usu_contrasena"].ToString(),
                                salt: reader["usu_salt"].ToString(),
                                state: reader["usu_estado"].ToString(),
                                rol: userRol,
                                permisos: permisos
                            );
                        }

                        // Agregar permisos a la lista
                        Permissions permiso = new Permissions(
                            id: Convert.ToInt32(reader["per_id"]),
                            nombre: reader["per_nombre"].ToString(),
                            descripcion: reader["per_descripcion"].ToString()
                        );
                        permisos.Add(permiso);
                    }
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

            using (MySqlCommand objSelectCmd = new MySqlCommand())
            {
                objSelectCmd.Connection = objPer.openConnection();
                objSelectCmd.CommandText = "spInsertUser";
                objSelectCmd.CommandType = CommandType.StoredProcedure;
                objSelectCmd.Parameters.Add("p_mail", MySqlDbType.VarChar).Value = _mail;
                objSelectCmd.Parameters.Add("p_password", MySqlDbType.Text).Value = _password;
                objSelectCmd.Parameters.Add("p_salt", MySqlDbType.VarChar).Value = _salt;
                objSelectCmd.Parameters.Add("p_state", MySqlDbType.VarChar).Value = _state;
                objSelectCmd.Parameters.Add("p_date", MySqlDbType.Date).Value = _date;
                objSelectCmd.Parameters.Add("p_fkrol", MySqlDbType.Int32).Value = _fkRol;
                objSelectCmd.Parameters.Add("p_fkemployee", MySqlDbType.Int32).Value = _fkEmployee;

                try
                {
                    row = objSelectCmd.ExecuteNonQuery();
                    executed = (row == 1);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
            }
            objPer.closeConnection();
            return executed;
        }

        // Método para actualizar un Usuario
        public bool updateUser(int _id, string _mail, string _password, string _salt, string _state, DateTime _date, int _fkEmployee, int _fkRol)
        {
            bool executed = false;
            int row;

            using (MySqlCommand objSelectCmd = new MySqlCommand())
            {
                objSelectCmd.Connection = objPer.openConnection();
                objSelectCmd.CommandText = "spUpdateUser";
                objSelectCmd.CommandType = CommandType.StoredProcedure;
                objSelectCmd.Parameters.Add("p_id", MySqlDbType.Int32).Value = _id;
                objSelectCmd.Parameters.Add("p_correo", MySqlDbType.VarChar).Value = _mail;
                objSelectCmd.Parameters.Add("p_contrasena", MySqlDbType.Text).Value = _password;
                objSelectCmd.Parameters.Add("p_salt", MySqlDbType.VarChar).Value = _salt;
                objSelectCmd.Parameters.Add("p_estado", MySqlDbType.VarChar).Value = _state;
                objSelectCmd.Parameters.Add("p_fecha_creacion", MySqlDbType.Date).Value = _date;
                objSelectCmd.Parameters.Add("p_fkrol", MySqlDbType.Int32).Value = _fkRol;
                objSelectCmd.Parameters.Add("p_fkempleado", MySqlDbType.Int32).Value = _fkEmployee;

                try
                {
                    row = objSelectCmd.ExecuteNonQuery();
                    executed = (row == 1);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
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

        public int showCountUsers()
        {
            int totalUsers;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectCountUsers";
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            // Agregar el parámetro de salida
            objSelectCmd.Parameters.Add(new MySqlParameter("@total_usuarios", MySqlDbType.Int32));
            objSelectCmd.Parameters["@total_usuarios"].Direction = ParameterDirection.Output;

            // Ejecutar el comando
            objSelectCmd.ExecuteNonQuery();

            // Obtener el valor del parámetro de salida
            totalUsers = Convert.ToInt32(objSelectCmd.Parameters["@total_usuarios"].Value);

            return totalUsers;
        }

        public DataSet showUsersPerRol()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectUsersPerRol";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }
    }
}