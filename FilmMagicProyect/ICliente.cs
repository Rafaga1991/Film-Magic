using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace FilmMagicProyect.Interfaces.Cliente
{
    public interface ICliente
    {
        DataSet buscar(float codigo); 
        bool agregar(); 
        bool eliminar();

    }
}
