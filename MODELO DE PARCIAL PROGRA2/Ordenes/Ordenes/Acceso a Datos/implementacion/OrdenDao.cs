using Ordenes.Acceso_a_Datos.interfaz;
using Ordenes.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordenes.Acceso_a_Datos.implementacion
{
    public class OrdenDao : IOrden
    {
        public bool crear(OrdenRetiro orden)
        {
            bool aux = true;
            SqlConnection conexion = HelperDao.ObtenerInstancia().ObtenerConexion();

            SqlTransaction t = null;

            try { 
            conexion.Open();
            t = conexion.BeginTransaction();

            SqlCommand comando = new SqlCommand("SP_INSERTAR_ORDEN", conexion, t);
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@responsable", orden.Responsable);

            SqlParameter parametro = new SqlParameter();
            parametro.ParameterName = "nro";
            parametro.SqlDbType = System.Data.SqlDbType.Int;
            parametro.Direction = System.Data.ParameterDirection.Output;
            comando.Parameters.Add(parametro);

            comando.ExecuteNonQuery();

            int nro = Convert.ToInt32( parametro.Value);
            int detalle = 1;

            foreach (DetalleOrden d in orden.Detalles)
            {
                SqlCommand cmd = new SqlCommand("SP_INSERTAR_DETALLES", conexion, t);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("nro_orden", nro);
                cmd.Parameters.AddWithValue("detalle", detalle);
                cmd.Parameters.AddWithValue("codigo", d.Material.Codigo);
                cmd.Parameters.AddWithValue("cantidad", d.Cantidad);
                detalle++;  
                cmd.ExecuteNonQuery();

            }
            
            t.Commit();
            }
            catch 
            {
                if (t != null)
                    t.Rollback();
                aux = false;

            }finally 
            {
                if(conexion.State == System.Data.ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            return aux;
        }

       
    }
}
