using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmMagicProyect.Objetos
{
    public class Articulo : Interfaces.Articulo.IArticulo
    {
        public DataSet listarInventario()
        {
            DataSet articulos = ConexionSql.Ejecutar(string.Format("SELECT * FROM producto"));

            return articulos;
        }

        public DataSet buscarEnInventario(string categoriaId)
        {
            DataSet datos = ConexionSql.Ejecutar(string.Format("SELECT * FROM producto where categoriaCodigo like {0}", categoriaId));

            return datos;
        }

        public DataSet buscarArticulo(string articulo)
        {
            DataSet datos = ConexionSql.Ejecutar(string.Format("SELECT * FROM producto where productoTitulo like '{0}'", articulo));

            return datos;
        }
    }
}
