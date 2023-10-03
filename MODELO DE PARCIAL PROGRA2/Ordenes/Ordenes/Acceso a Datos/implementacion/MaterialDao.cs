using Ordenes.Acceso_a_Datos.interfaz;
using Ordenes.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordenes.Acceso_a_Datos.implementacion
{
    public class MaterialDao : IMaterial
    {
        public List<Material> getAll()
        {
            List<Material> lst = new List<Material>();
             DataTable dt = HelperDao.ObtenerInstancia().consultaSP("SP_CONSULTAR_MATERIALES");

            foreach (DataRow row in dt.Rows)
            {
                
                int nro = int.Parse(row["codigo"].ToString());
                string nombre = row["nombre"].ToString();
                double stock = double.Parse(row["stock"].ToString());

                Material material = new Material(nro , nombre , stock);
                lst.Add(material);
            }

            return lst;
        }
    }
}
