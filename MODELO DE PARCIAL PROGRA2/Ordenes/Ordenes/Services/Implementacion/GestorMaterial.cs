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
    public class GestorMaterial 
    {
        IMaterial DaoMaterial;

        public GestorMaterial(IMaterial dao)
        {
            DaoMaterial = dao;
        }
        public List<Material> TrearMateriales()
        {
            return DaoMaterial.getAll();
        }
    }
}
