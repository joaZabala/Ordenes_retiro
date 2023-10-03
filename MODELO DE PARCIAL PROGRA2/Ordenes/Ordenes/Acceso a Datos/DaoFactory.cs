using Ordenes.Acceso_a_Datos.implementacion;
using Ordenes.Acceso_a_Datos.interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordenes.Acceso_a_Datos
{
    public class DaoFactory : AbstractDaoFactory
    {
        public override IMaterial CrearMaterial()
        {
           return new MaterialDao();
        }

        public override IOrden CrearOrdenDao()
        {
             return new OrdenDao();
        }
    }
}
