using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace FilmMagicProyect.Objetos
{
    public class Empleado
    {
        DataSet perfil;

        public DataSet logear() 
        { 
            perfil = ConexionSql.Ejecutar(string.Format("SELECT * FROM usuario")); 
            return perfil; 
        }
    }
}
