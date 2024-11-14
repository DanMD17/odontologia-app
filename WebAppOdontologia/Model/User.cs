using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model
{
    public class User
    {
        private string _correo;
        private string _contrasena;
        private string _salt;
        private string _state;

        private Roles _rol;
        private List<Permissions> _permisos;

        public string Correo { get => _correo; set => _correo = value; }
        public string Contrasena { get => _contrasena; set => _contrasena = value; }
        public string Salt { get => _salt; set => _salt = value; }
        public string State { get => _state; set => _state = value; }
        public Roles Rol { get => _rol; set => _rol = value; }
        public List<Permissions> Permisos { get => _permisos; set => _permisos = value; }

        public User(string correo, string contrasena, string salt, string state, Roles rol, List<Permissions> permisos)
        {
            _correo = correo;
            _contrasena = contrasena;
            _salt = salt;
            _state = state;
            _rol = rol;
            _permisos = permisos;
        }

        public User()
        {
            _permisos = new List<Permissions>(); // Inicializa lista vacía
        }

    }
}