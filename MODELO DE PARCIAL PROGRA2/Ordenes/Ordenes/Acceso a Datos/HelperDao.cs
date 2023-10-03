using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordenes.Acceso_a_Datos
{
    public class HelperDao
    {
        private static HelperDao instancia;
        SqlConnection conexion;

        public HelperDao()
        {
            conexion = new SqlConnection(@"Data Source=DESKTOP-NMS8A8J\SQLEXPRESS;Initial Catalog=db_ordenes;Integrated Security=True");
        }

        public static HelperDao ObtenerInstancia()
        {
            if(instancia == null)
            {
                instancia = new HelperDao();
            }
            return instancia;
        }

        public SqlConnection ObtenerConexion()
        {
            return this.conexion;
        }

        public DataTable consultaSP(string nombreSP)
        {
            DataTable tabla= new DataTable();
            conexion.Open();
            SqlCommand comando = new SqlCommand( nombreSP , conexion);
            comando.CommandType = CommandType.StoredProcedure;
            tabla.Load(comando.ExecuteReader());
            conexion.Close();
            return tabla;   

        }

    }
}
