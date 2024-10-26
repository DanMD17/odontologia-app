using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Data;

namespace Logic
{
    public class EmployeesLog
    {
        
        EmployeesDat objEmp = new EmployeesDat();

        //Metodo para mostrar unicamente el id y la descripcion de los Provedores, en el DropDownList
        public DataSet showEmployeesDDL()
        {
            return objEmp.showEmployeesDDL();
        }

        //Metodo para mostrar los Provedores
        public DataSet showEmployees()
        {
            return objEmp.showEmployees();
        }

        //Metodo para guardar un nuevo Proveedor
        public bool saveEmployee(int _identificacion, string _nombre, string _apellidos, string _celular, string _direccion, string _correo)
        {
            return objEmp.saveEmployee(_identificacion, _nombre, _apellidos, _celular, _direccion, _correo);
        }

        //Metodo para actualizar un Proveedor
        public bool updateEmployee(int _id, int _identificacion, string _nombre, string _apellidos, string _celular, string _direccion, string _correo)
        {
            return objEmp.updateEmployee(_id, _identificacion, _nombre, _apellidos, _celular, _direccion, _correo);
        }
    }
}