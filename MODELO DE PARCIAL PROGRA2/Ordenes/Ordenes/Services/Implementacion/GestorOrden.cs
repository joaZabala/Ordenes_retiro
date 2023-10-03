using Ordenes.Acceso_a_Datos;
using Ordenes.Acceso_a_Datos.implementacion;
using Ordenes.Acceso_a_Datos.interfaz;
using Ordenes.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordenes.Services
{
    public class GestorOrden 
    {
        private IOrden OrdenDao;
        public GestorOrden(AbstractDaoFactory factory)
        {
            this.OrdenDao=factory.CrearOrdenDao();
        }
        public bool crearOrden(OrdenRetiro orden)
        {
            return OrdenDao.crear(orden);
        }
    }
}
