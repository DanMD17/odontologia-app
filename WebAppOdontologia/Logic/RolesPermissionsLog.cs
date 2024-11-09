using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Data;
using System.Data.SqlClient;
using System.Runtime.Remoting;

namespace Logic
{
    public class Roles_PermissionLog
    {
        Roles_PermissionDat objRolPer = new Roles_PermissionDat();
        public DataSet showRolesPermissions()
        {
            return objRolPer.showRolesPermissions();
        }

        
      
        public bool saveRolePermission(int _rol_id, int _permiso_id)
        {
            return objRolPer.saveRolePermission(_rol_id, _permiso_id);
        }

        //Metodo para actualizar rol y permiso
        public bool updateRolePermission(int _o_rol_id, int _o_permiso_id, int _n_rol_id,
            int _n_permiso_id)
        {
            return objRolPer.updateRolePermission(_o_rol_id, _o_permiso_id, _n_rol_id,
             _n_permiso_id);
        }

        //Metodo para borrar un Rol-Permiso
        public bool deleteRolePermision(int _rol_id, int _permiso_id)
        {
            return objRolPer.deleteRolePermision(_rol_id, _permiso_id);
        }
    }
}
