using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace FilmMagicProyect.Objetos
{
    public class Cliente : Interfaces.Cliente.ICliente
    {
        private string cedula, nombre, direccion, celular; 
        private int codigo; 
        public Cliente()
        {

        }
        public Cliente(string p_cedula, string p_nombre, string p_dir, string p_cel, int cod) 
        { 
            cedula = p_cedula; 
            nombre = p_nombre; 
            direccion = p_dir; 
            celular = p_cel; 
            codigo = cod; 
        }

        public bool agregar()
        {
            bool identificador; 
            try
            {             
                ConexionSql.Ejecutar(string.Format("Insert into cliente(clienteCodigo,clienteNombre,clienteCedula,clienteDireccion,clienteTelefono) values ('{0}','{1}','{2}','{3}','{4}')", codigo, nombre, cedula, direccion, celular)); 
                identificador = true;
            }
            catch (Exception e) { identificador = false; }
            return identificador;


        }

        public DataSet buscar(float p_codigo)
        {
            DataSet data = ConexionSql.Ejecutar(string.Format("SELECT * FROM cliente Where clienteCodigo like '{0}'", p_codigo));

            return data;
        }

        public bool eliminar() { throw new NotImplementedException(); }
        public DataSet LlenarDataGrid(string tabla)
        {
            DataSet dataTabla = ConexionSql.Ejecutar(string.Format("SELECT * FROM " + tabla));

            return dataTabla;
        }

        public DataSet listarClientes()
        {
            DataSet Clientes = ConexionSql.Ejecutar(string.Format("SELECT * FROM cliente"));

            return Clientes;
        }

        public DataSet consultar(string p_nombre)
        {
            DataSet data = ConexionSql.Ejecutar(string.Format("SELECT * FROM cliente Where clienteNombre like '{0}'", p_nombre + "%"));

            return data;
        }
    }
}
