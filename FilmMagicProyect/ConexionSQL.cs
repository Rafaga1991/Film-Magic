using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace FilmMagicProyect
{ 
public class ConexionSql
    {
        protected static SqlConnection conexion; 
        public static void conectar()
        {
            conexion = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=;Integrated Security=True");
            conexion.Open();

        }

        public static DataSet Ejecutar(string sentencia)
        {
            conectar(); 
            DataSet DS = new DataSet(); 
            SqlDataAdapter DA = new SqlDataAdapter(sentencia, conexion); 
            DA.Fill(DS); 
            conexion.Close(); 
            return DS;

        }
    }
}

