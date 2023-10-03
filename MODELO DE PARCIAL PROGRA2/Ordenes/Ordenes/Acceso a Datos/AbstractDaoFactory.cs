using Ordenes.Acceso_a_Datos.interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordenes.Acceso_a_Datos
{
    public abstract class AbstractDaoFactory
    {
        public abstract IOrden CrearOrdenDao();

        public abstract IMaterial CrearMaterial();
    }
}
