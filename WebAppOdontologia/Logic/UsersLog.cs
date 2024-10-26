using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

        // Método para mostrar solo el ID y el nombre de usuario 
        public DataSet showUsersDDL()
        {
            return objUser.showUsersDDL();
        }

        // Método para guardar un nuevo usuario
        public bool saveUser(string _mail, string _password, string _state, DateTime _date, int _fkEmployee, int _fkRol)
        {
            return objUser.saveUser(_mail, _password, _state, _date, _fkEmployee, _fkRol);
        }

        // Método para actualizar un usuario existente
        public bool updateUser(int _id, string _mail, string _password, string _state, int _fkEmployee, int _fkRol)
        {
            return objUser.updateUser(_id, _mail, _password, _state, _fkEmployee, _fkRol);
        }

        // Método para borrar un usuario
        public bool deleteUser(int _id)
        {
            return objUser.deleteUser(_id);
        }
    }
}