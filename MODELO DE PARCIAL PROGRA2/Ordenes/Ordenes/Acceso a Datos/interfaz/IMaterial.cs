using Ordenes.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordenes.Acceso_a_Datos.interfaz
{
    public interface IMaterial
    {
        List<Material> getAll();
    }
}
