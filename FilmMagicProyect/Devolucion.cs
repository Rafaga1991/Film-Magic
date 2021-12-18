using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FilmMagicProyect.Objetos
{
    public class Devolucion
    {

        public string devolucionRenta(int @devolucionAlquilerAlquilerId, string @devolucionAlquilerFechaRetorno, int @devolucionAlquilerProductoId, int @devolucionAlquilerCantidad)
        {
            int @devolucionAlquilerId = buscarUltId();

            try
            {
                string cmd = string.Format("Insert into devolucionAlquiler(alquilerId, devolucionAlquilerFechaRetorno, devolucionAlquilerProductoId, devolucionAlquilerCantidad) values ({0}, '{1}', {2}, {3})", @devolucionAlquilerAlquilerId, @devolucionAlquilerFechaRetorno, @devolucionAlquilerProductoId, @devolucionAlquilerCantidad); 
                ConexionSql.Ejecutar(cmd); 
                actualizarProducto(@devolucionAlquilerCantidad, @devolucionAlquilerProductoId); 
                return "Gracias por Preferirnos";

            }
            catch (Exception error) { return error.ToString(); }

        }

        public int buscarclienteId(string codigo)
        {
            int actual = 0; 
            string cmd = string.Format("select * from cliente where clienteCodigo like '{0}'", codigo); 
            DataSet columna = ConexionSql.Ejecutar(cmd);

            actual = Convert.ToInt32(columna.Tables[0].Rows[0]["clienteId"]);
            return actual;
        }

        public int buscarArquilerId(int clienteID)
        { 
            int actual = 0; 

            string cmd = string.Format("select alquilerId from alquiler where alquilerClienteId like '{0}'", clienteID); 
            DataSet columna = ConexionSql.Ejecutar(cmd);

            actual = Convert.ToInt32(columna.Tables[0].Rows[0]["alquilerId"]); 
            return actual;

        }

        public int buscarUltId()
        {
            int actual = 0; 
            int index = 0; 
            string cmd = string.Format("select devolucionAlquilerId from devolucionAlquiler"); 
            DataSet columna = ConexionSql.Ejecutar(cmd);

            index = columna.Tables[0].Rows.Count; 
            if (index == 0) 
            { return 1; } 
            else 
            { 
                actual = Convert.ToInt32(columna.Tables[0].Rows[index - 1]["devolucionAlquilerId"]); 
                return actual + 1; 
            }
        }

        public int buscarProductoId(string nombre)
        {
            int actual = 0; 
            string cmd = string.Format("select * from producto where productoTitulo like '{0}'", nombre); 
            DataSet columna = ConexionSql.Ejecutar(cmd);

            actual = Convert.ToInt32(columna.Tables[0].Rows[0]["productoId"]);
            return actual;
        }

        public void actualizarProducto(int cantidad, int productId)
        {
            string cmd = string.Format("update producto set productoCantidad = productoCantidad + '{0}' where productoId = '{1}'", cantidad, productId); 
            ConexionSql.Ejecutar(cmd);
        }

        public int verificarAtraso(DateTime fechaR, int cliente)
        {
            int id = buscarclienteId(cliente.ToString());

            string cmd = string.Format("select alquilerFechaRetorno from alquiler where alquilerClienteId like '{0}'", id); 
            DataSet dato = ConexionSql.Ejecutar(cmd); 
            DateTime fechaAcordada = Convert.ToDateTime(dato.Tables[0].Rows[0]["alquilerFechaRetorno"]);

            fechaAcordada = Convert.ToDateTime(fechaAcordada.ToString("yyyy-MM-dd")); 
            fechaR = Convert.ToDateTime(fechaR.ToString("yyyy-MM-dd")); 
            int resultado = DateTime.Compare(fechaR, fechaAcordada); 
            return resultado;
        }

        public DataSet LlenarDataGrid(int clienteID)
        {
            DataSet dataTabla = ConexionSql.Ejecutar(string.Format("SELECT alquilerId, alquilerNumero, alquilerFechaAlquiler, alquilerFechaRetorno FROM alquiler where alquilerClienteId like " + clienteID));

            return dataTabla;
        }
    }
}


