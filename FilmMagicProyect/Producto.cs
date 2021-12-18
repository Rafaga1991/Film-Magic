using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace FilmMagicProyect.Objetos
{
    public class Producto
    {
        private DataSet categorias; 
        public DataSet listarCategoria()
        {
            categorias = ConexionSql.Ejecutar(string.Format("SELECT * FROM Categoria where categoriaTipo like 'P' or categoriaTipo like 'A'"));

            return categorias;
        }
        public DataSet listarCategoria(string tipo)
        {
            categorias = ConexionSql.Ejecutar(string.Format("SELECT * FROM Categoria where categoriaTipo like '{0}' or categoriaTipo like 'A'", tipo));

            return categorias;
        }

        public DataSet listarProductosRentar(string categoria)
        {
            DataSet id = ConexionSql.Ejecutar(string.Format("SELECT * FROM Categoria where categoriaNombre like '{0}'", categoria)); 
            string idCat = id.Tables[0].Rows[0]["categoriaCodigo"].ToString(); 
            categorias = ConexionSql.Ejecutar(string.Format("SELECT * FROM producto where categoriaCodigo like '{0}'", idCat));

            return categorias;
        }

        public DataSet listarDetalles(string articulo)
        {
            categorias = ConexionSql.Ejecutar(string.Format("SELECT * FROM producto where productoTitulo like '{0}'", articulo));

            return categorias;
        }

        public DataSet listarProductos()
        {
            categorias = ConexionSql.Ejecutar(string.Format("SELECT * FROM producto"));

            return categorias;
        }
    }
}
