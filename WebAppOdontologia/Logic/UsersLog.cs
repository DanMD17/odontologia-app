using Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Runtime.Remoting;
using System.Web;
using Model;

namespace Logic
{
    public class UsersLog
    {
        UsersDat objUser = new UsersDat();

        // Método para mostrar todos los usuarios
        public DataSet showUsers()
        {
            return objUser.showUsers();
        }

        //Metodo para mostrar el Usuarios pasandole el correo
        public User showUsersMail(string _mail)
        {
            return objUse.showUsersMail(_mail);
        }

        // Método para guardar un nuevo usuario
        public bool saveUser(string _mail, string _password, string _salt, string _state, DateTime _date, int _fkEmployee, int _fkRol)
        {
            return objUser.saveUser(_mail, _password, _salt, _state, _date, _fkEmployee, _fkRol);
        }

        // Método para actualizar un usuario existente
        public bool updateUser(int _id, string _mail, string _password, string _salt, string _state, int _fkEmployee, int _fkRol)
        {
            return objUser.updateUser(_id, _mail, _password, _salt, _state, _fkEmployee, _fkRol);
        }

        // Método para borrar un usuario
        public bool deleteUser(int _id)
        {
            return objUser.deleteUser(_id);
        }
    }
}