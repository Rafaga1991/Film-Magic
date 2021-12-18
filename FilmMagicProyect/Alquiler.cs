using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace FilmMagicProyect.Objetos
{
    public class Alquiler
    {
        public string renta(char opcion, int @alquilerId, string @alquilerNumero, int @alquilerClienteId, string @alquilerFechaAdquisicion, string @alquilerFechaRetorno, string ProductoNombre, int productoCantidad, float productoPrecio)
        {

            float descuento = 0; 
            int idProducto = buscarproductoId(ProductoNombre); 
            float impuesto = 0.18F; 
            double bruto = (productoCantidad * productoPrecio) - descuento; 
            float neto = Convert.ToInt32(bruto + (bruto * impuesto)); 
            int idDetalle = 0; 

            string mensaje2 = "";

            try
            {
                string cmd = string.Format("Insert into alquiler(alquilerNumero,alquilerClienteId,alquilerFechaAlquiler,alquilerFechaRetorno,alquilerMontoBruto, alquilerImpuesto, alquilerMontoTotalDescuento, alquilerMontoTotal) values ('{0}', {1}, '{2}', '{3}', {4}, {5}, {6}, {7})", @alquilerNumero, @alquilerClienteId, @alquilerFechaAdquisicion, @alquilerFechaRetorno, float.Parse(bruto.ToString()), impuesto, descuento ,neto);


                ConexionSql.Ejecutar(cmd);
                mensaje2 = rentaDetalle(opcion, idDetalle, @alquilerId, idProducto, productoCantidad, productoPrecio, descuento, impuesto, neto); 
                actualizarProducto(productoCantidad, idProducto); 
                return "Gracias por su Compra Son " + neto.ToString() + " RD$ Impuestos Incluidos";
            }
            catch (Exception e) { return e.ToString(); }

        }

        public string rentaDetalle(char @opc, int @alquilerDetalleFilmMagic, int @alquilerDetalleAlquilerId, int @alquilerDetalleProductoId, int @alquilerDetalleCantidad, float @alquilerDetallePrecio, float @alquilerDetalleDescuento, float @alquilerDetalleImpuesto, float @alquilerDetalleMontoNeto) 
        { 
            try 
            {
                int alquilerId = buscarUltimoId();
                string cmd2 = string.Format("Insert into alquilerDetalle(alquilerId,productoId,alquilerDetalleCantidad,alquilerDetallePrecio,alquilerDetalleDescuento, alquilerDetalleImpuesto, alquilerDetalleMontoNeto) values ({0}, {1}, {2}, {3}, {4}, {5}, {6})", alquilerId, @alquilerDetalleProductoId, @alquilerDetalleCantidad, @alquilerDetallePrecio, @alquilerDetalleDescuento, @alquilerDetalleImpuesto, @alquilerDetalleMontoNeto); 
                ConexionSql.Ejecutar(cmd2); 
                return "Gracias por su renta"; 

            } catch (Exception e) { return e.ToString(); } 
        }


        public int buscarUltId()
        {
            int actual = 0; 
            int index = 0; 
            string cmd = string.Format("SELECT max(alquilerId) as alquilerId FROM alquiler"); 
            DataSet columna = ConexionSql.Ejecutar(cmd);

            index = columna.Tables[0].Rows.Count; 
            actual = Convert.ToInt32(columna.Tables[0].Rows[index - 1]["alquilerId"]); 
            return actual + 1;

        }

        public int buscarUltimoId()
        {
            int actual = 0;
            int index = 0;
            string cmd = string.Format("SELECT max(alquilerId) as alquilerId FROM alquiler");
            DataSet columna = ConexionSql.Ejecutar(cmd);

            index = columna.Tables[0].Rows.Count;
            actual = Convert.ToInt32(columna.Tables[0].Rows[index - 1]["alquilerId"]);
            return actual;

        }

        public string buscarUltNUmero()
        {
            string actual = ""; 
            string cmd = string.Format("select alquilerNumero from alquiler"); 
            DataSet columna = ConexionSql.Ejecutar(cmd);

            actual = columna.Tables[0].Rows.Count.ToString(); 
            actual = (Convert.ToInt32(actual + 1)).ToString(); 
            return actual;

        }

        public int buscarclienteId(string codigo)
        {
            
            int actual = 0; 
            string cmd = string.Format("select * from cliente where clienteCodigo like '{0}'", codigo); 
            DataSet columna = ConexionSql.Ejecutar(cmd);

            actual = Convert.ToInt32(columna.Tables[0].Rows[0]["clienteId"]);
            return actual;
        }
        public int buscarproductoId(string producto)
        {
            int actual = 0; 
            string cmd = string.Format("select * from producto where productoTitulo like '{0}'", producto); 
            DataSet columna = ConexionSql.Ejecutar(cmd);

            actual = Convert.ToInt32(columna.Tables[0].Rows[0]["productoId"]);
            return actual;
        }

        public void actualizarProducto(int cantidad, int productId) 
        { 
            string cmd = string.Format("update producto set productoCantidad = productoCantidad - '{0}' where productoId = '{1}'", cantidad, productId); 
            ConexionSql.Ejecutar(cmd); 
        }
    }
}