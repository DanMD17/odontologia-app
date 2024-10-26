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

        //Metodo para mostrar unicamente el id y la descripcion de los empleados en DDL
        public DataSet showEmployeesDDL()
        {
            return objEmp.showEmployeesDDL();
        }

        //Metodo para mostrar los empleados
        public DataSet showEmployees()
        {
            return objEmp.showEmployees();
        }

        //Metodo para guardar un nuevo empleado
        public bool saveEmployee(int _identificacion, string _nombre, string _apellidos, string _celular, string _direccion, string _correo)
        {
            return objEmp.saveEmployee(_identificacion, _nombre, _apellidos, _celular, _direccion, _correo);
        }

        //Metodo para actualizar un empleado
        public bool updateEmployee(int _id, int _identificacion, string _nombre, string _apellidos, string _celular, string _direccion, string _correo)
        {
            return objEmp.updateEmployee(_id, _identificacion, _nombre, _apellidos, _celular, _direccion, _correo);
        }

        //Metodo para borrar un empleado
        public bool deleteEmployee(int _idEmployee)
        {
            return objCat.deleteEmployee(_idEmployee);
        }
    }
}