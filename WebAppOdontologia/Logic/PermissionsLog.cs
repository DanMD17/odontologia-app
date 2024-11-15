using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data;
using System.Data.SqlClient;
using System.Security;

namespace Logic
{
    public class PermissionsLog
    {
        PermissionsDat objPer = new PermissionsDat();

        //Metodo para mostrar todos los Permisos
        public DataSet showPermissions()
        {
            return objPer.showPermissions();
        }

        public DataSet showPermissionDDl()
        {
            return objPer.showPermissionsDDl();
        }

        //Metodo para guardar un Permiso
        public bool savePermission(string _nombre, string _descripcion)
        {
            return objPer.savePermission(_nombre, _descripcion);

        }

        //Metodo para actualizar un Permiso
        public bool updatePermision(int _id, string _nombre, string _descripcion)
        {
            return objPer.updatePermision(_id, _nombre, _descripcion);

        }

        //Metodo para borrar un Permiso
        public bool deletePermision(int _id)
        {
            return objPer.deletePermision(_id);

        }
    }
}
