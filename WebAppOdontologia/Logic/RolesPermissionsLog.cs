using Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Logic
{
    public class PermisoRolLog
    {
        PermisoRolDat objPerRol = new PermisoRolDat();

        //Metodo para mostrar todos los Permisos Roles
        public DataSet showPermissionRol()
        {
            return objPerRol.showPermissionRol();
        }

        //Metodo para guardar un nuevo Permiso Rol
        public bool savePermissionRol(int _fkRol, int _fkPermiso, DateTime _date)
        {
            return objPerRol.savePermissionRol(_fkRol, _fkPermiso, _date);
        }
        //Metodo para actualizar un nuevo Permiso Rol
        public bool updatePermissionRol(int _id, int _fkRol, int _fkPermiso, DateTime _date)
        {
            return objPerRol.updatePermissionRol(_id, _fkRol, _fkPermiso, _date);
        }
        //Metodo para borrar un Permiso Rol
        public bool deletePermissionRol(int _idPermissionRol)
        {
            return objPerRol.deletePermissionRol(_idPermissionRol);
        }
    }
}